using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ProductsInventory.Repository.Abstractions;
using ProductsInventory.Repository.Model;
using ProductsInventory.Repository.Models;
using System.Xml;

namespace ProductsInventory.Repository
{
	public class Repository(ProductsInventoryDbContext dbContext) : IRepository
	{
		#region EndProduct
		public async Task CreateEndProductAsync(EndProduct model, CancellationToken ct = default)
		{
			await dbContext.EndProducts.AddAsync(model, ct);
		}
		public async Task DeleteEndProduct(int endProductId, CancellationToken ct = default)
		{
			EndProduct? endProduct = await GetEndProductAsync(endProductId, ct);
			if(endProduct == null)
				throw new ExceptionHandlerRepository($"Nessun end product trovato con ID {endProductId}.", 404);
			dbContext.Remove(endProduct);
		}
		public async Task<EndProduct?> GetEndProductAsync(int endProductId, CancellationToken ct = default)
		{
			return await dbContext.EndProducts
				.Where(x => x.Id == endProductId)
				.Include(x => x.RawMaterialForProduction)
				.ThenInclude(x => x.RawMaterial)
				.FirstOrDefaultAsync(ct);
		}
		public async Task UpdateEndProductAsync(EndProduct model, CancellationToken ct = default)
		{
			EndProduct? endProduct = await GetEndProductAsync(model.Id, ct);
			if(endProduct == null)
				throw new ExceptionHandlerRepository($"End product con ID {model.Id} non trovato.", 404);
			dbContext.EndProducts.Update(model);

		}

		#endregion

		#region RawMaterial
		public async Task InsertRawMaterial(RawMaterial model, CancellationToken ct = default)
		{
			await dbContext.AddAsync(model, ct);
		}
		public async Task DeleteRawMaterial(int rawMaterialId, CancellationToken ct = default)
		{
			var RawMaterial = await GetRawMaterialAsync(rawMaterialId, ct);
			if(RawMaterial == null)
				throw new ExceptionHandlerRepository($"Nessun rawMaterial trovato con ID {rawMaterialId}.",404);	
			
			dbContext.Remove(RawMaterial);
		}
		public async Task<RawMaterial?> GetRawMaterialAsync(int rawMaterialId, CancellationToken ct =default)
		{
			return await dbContext.RawMaterials.Where(x => x.Id == rawMaterialId).FirstOrDefaultAsync(ct);
		}
		public async Task UpdateRawMaterialAsync(RawMaterial model, CancellationToken ct = default)
		{
			RawMaterial? rawMaterial = await GetRawMaterialAsync(model.Id, ct);
			if (rawMaterial == null)
			{
				throw new ExceptionHandlerRepository($"Raw material con ID {model.Id} non trovato.", 404);
			}

			dbContext.RawMaterials.Update(model);
		}
		#endregion

		#region Shipment
		public async Task CreateShipmentAsync(Shipment model, CancellationToken ct)
		{
			await dbContext.Shipments.AddAsync(model, ct);

		}
		public async  Task CreateShippingItemsAsync(List<ShipmentItems> modelList, CancellationToken ct = default)
		{
			await dbContext.ShippingItems.AddRangeAsync(modelList, ct);
		}

		public async Task<Shipment?> GetShipmentAsync(int shipmentId, CancellationToken ct = default)
		{
			return await dbContext.Shipments
				.Include(x => x.Items)
				.ThenInclude(x => x.EndProduct)
				.FirstOrDefaultAsync(x => x.Id == shipmentId, ct);
		}
		#endregion

		#region RawMaterialForProduction
		public async  Task<List<RawMaterialForProduction>> GetRawMaterialForProductionFromEndProductId(int endProductId, CancellationToken ct= default)
		{
			return await dbContext.RawMaterialForProductions
				.Include(x => x.RawMaterial)
				.Where(x => x.EndProductId == endProductId)
				.ToListAsync(ct);
		}
		public async Task CreateRawMaterialForProductionAsync(List<RawMaterialForProduction> ListOfRawMaterialForProduction, CancellationToken ct = default)
		{
			foreach(var rawMaterialForProduction in ListOfRawMaterialForProduction)
			{
				var rawMaterial = await GetRawMaterialAsync(rawMaterialForProduction.RawMaterialId, ct);
				if(rawMaterial == null)
				{
					throw new ExceptionHandlerRepository($"Raw material con ID {rawMaterialForProduction.RawMaterialId} non trovato.", 404);
				}
			}
			await dbContext.RawMaterialForProductions.AddRangeAsync(ListOfRawMaterialForProduction, ct);
		}
		public async Task DeleteAllRawMaterialForProductionByEndProductIdAsync(int endProductId, CancellationToken ct = default)
		{
			await GetRawMaterialForProductionFromEndProductId(endProductId, ct).ContinueWith(task =>
			{
				var rawMaterialsForProduction = task.Result;
				if (rawMaterialsForProduction == null || rawMaterialsForProduction.Count == 0)
					throw new ExceptionHandlerRepository($"Nessun raw material for production trovato per l'end product con ID {endProductId}.", 404);
				dbContext.RawMaterialForProductions.RemoveRange(rawMaterialsForProduction);
			}, ct);
		}
		#endregion

		#region TransactionalOutbox
		public async Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken ct = default)
		{
			return await dbContext.TransactionalOutboxes
				.ToListAsync(ct);
		}
		public async Task DeleteTransactionalOutboxAsync(long id, CancellationToken cancellationToken = default)
		{
			dbContext.TransactionalOutboxes.Remove(await GetTransactionalOutboxByKeyAsync(id, cancellationToken)
				?? throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));
		}

		public async Task<TransactionalOutbox?> GetTransactionalOutboxByKeyAsync(long id, CancellationToken cancellationToken = default)
		{
			return await dbContext.TransactionalOutboxes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public async Task InsertTransactionalOutboxAsync(TransactionalOutbox transactionalOutbox, CancellationToken cancellationToken = default)
		{
			await dbContext.TransactionalOutboxes.AddAsync(transactionalOutbox, cancellationToken);
		}
		#endregion

		public async Task CreateProductionProcessAsync(ProductionProcess model, CancellationToken ct = default)
		{
			EndProduct? endProduct = await GetEndProductAsync(model.EndProductId, ct);
			if(endProduct == null)
			{
				throw new ExceptionHandlerRepository($"Endproduct con ID {model.EndProductId} non trovato.", 404);
			}
			var ListOfRawMaterialForProduction = endProduct.RawMaterialForProduction;

			
			foreach (var rawMaterialForProduction in ListOfRawMaterialForProduction)
			{
				var rawMaterial = rawMaterialForProduction.RawMaterial;
				if (rawMaterial == null)
				{
					throw new ExceptionHandlerRepository($"Raw material con ID {rawMaterialForProduction.RawMaterialId} non trovato.", 404);
				}
				if (rawMaterial.InStorage < rawMaterialForProduction.QuantityNeeded * model.Quantity)
				{
					throw new ExceptionHandlerRepository($"Quantità insufficiente di raw material con ID {rawMaterialForProduction.RawMaterialId} per la produzione dell'end product con ID {model.EndProductId}.", 400);
				}
				rawMaterial.InStorage -= (rawMaterialForProduction.QuantityNeeded) * model.Quantity;
				await UpdateRawMaterialAsync(rawMaterial, ct);

			}
			endProduct.InStorage += model.Quantity;
			await UpdateEndProductAsync(endProduct, ct);

			await dbContext.ProductionProcesses.AddAsync(model, ct);
		}
		public async Task CreateTransaction(Func<Task> action)
		{
			if (dbContext.Database.CurrentTransaction != null)
			{
				await action();
			}
			else
			{
				using var transaction = await dbContext.Database.BeginTransactionAsync();
				try
				{
					await action();
					await transaction.CommitAsync();
				}
				catch
				{
					await transaction.RollbackAsync();
					throw;
				}
			}
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await dbContext.SaveChangesAsync(cancellationToken);
		}

		public Task InsertRawMaterialAsync(RawMaterial model, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}
	}
}
