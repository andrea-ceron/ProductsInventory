using Microsoft.EntityFrameworkCore;
using ProductsInventory.Repository.Model;
using ProductsInventory.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Abstractions
{
	public interface IRepository
    {
		#region EndProduct
		public Task CreateEndProductAsync(EndProduct model, CancellationToken ct = default);
		public Task<EndProduct> GetEndProductAsync(int endProductId, CancellationToken ct = default);
		public Task DeleteEndProduct(int endProductId, CancellationToken ct = default);
		public Task UpdateEndProductAsync(EndProduct model, CancellationToken ct = default);
		#endregion


		#region RawMaterial
		public Task InsertRawMaterialAsync(RawMaterial model, CancellationToken ct = default);
		public Task DeleteRawMaterial(int rawMaterialId, CancellationToken ct = default);
		public Task<RawMaterial> GetRawMaterialAsync(int rawMaterialId, CancellationToken ct = default);
		public Task UpdateRawMaterialAsync(RawMaterial model, CancellationToken ct = default);

		#endregion

		#region RawMaterialForProduction
		public Task<List<RawMaterialForProduction>> GetRawMaterialForProductionFromEndProductId(int endProductId, CancellationToken ct = default);
		public Task CreateRawMaterialForProductionAsync(List<RawMaterialForProduction> ListOfRawMaterialForProduction, CancellationToken ct = default);
		public Task DeleteAllRawMaterialForProductionByEndProductIdAsync(int endProductId, CancellationToken ct = default);
		#endregion

		#region Shipment
		public Task CreateShipmentAsync(Shipment model, CancellationToken ct = default);
		public Task CreateShippingItemsAsync(List<ShipmentItems> modelList, CancellationToken ct = default);
		public Task<Shipment?> GetShipmentAsync(int shipmentId, CancellationToken ct = default);
		#endregion

		#region TransactionalOutbox
		public Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken ct = default);
		public Task DeleteTransactionalOutboxAsync(long id, CancellationToken cancellationToken = default);
		public Task<TransactionalOutbox?> GetTransactionalOutboxByKeyAsync(long id, CancellationToken cancellationToken = default);
		public Task InsertTransactionalOutboxAsync(TransactionalOutbox transactionalOutbox, CancellationToken cancellationToken = default);


		#endregion

		public Task CreateProductionProcessAsync(ProductionProcess model, CancellationToken ct = default);

		public Task CreateTransaction(Func<Task> action);
		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


	}
}
