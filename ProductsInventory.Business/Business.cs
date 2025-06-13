using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductsInventory.Business.Abstractions;
using ProductsInventory.Business.helperModels;
using ProductsInventory.Repository.Abstractions;
using ProductsInventory.Repository.Models;
using ProductsInventory.Shared.DTO;

namespace ProductsInventory.Business
{
	public class Business(IRepository repository, ILogger<Business> logger, IMapper mapper) : IBusiness

	{
		#region EndProduct
		public async  Task CreateEndProductAsync(CreateEndProductDto endProductdto, CancellationToken ct = default)
		{
			List<CreateRawMaterialForProductionHelperDto> rawMaterialsNeeded = new();
			var endProduct = mapper.Map<EndProduct>(endProductdto);
			endProduct.RawMaterialForProduction = new List<RawMaterialForProduction>();

			await repository.CreateTransaction(async() =>
			{
				await repository.CreateEndProductAsync(endProduct, ct);
				await repository.SaveChangesAsync(ct);

				foreach (var rawmaterial in endProductdto.RawMaterialForProduction)
				{
					CreateRawMaterialForProductionHelperDto helper = new()
					{
						EndProductId = endProduct.Id,
						RawMaterialId = rawmaterial.RawMaterialId,
						QuantityNeeded = rawmaterial.QuantityNeeded
					};
					rawMaterialsNeeded.Add(helper);
				}
				var listOfRawMaterialForProduction = mapper.Map<List<RawMaterialForProduction>>(rawMaterialsNeeded);
				await repository.CreateRawMaterialForProductionAsync(listOfRawMaterialForProduction, ct);
				await repository.SaveChangesAsync(ct);

			});

		}
		public async Task DeleteEndProductAsync(int endProductId, CancellationToken ct = default)
		{
			await repository.CreateTransaction(async () =>
			{
				await repository.DeleteAllRawMaterialForProductionByEndProductIdAsync(endProductId, ct);
				await repository.SaveChangesAsync(ct);
				await repository.DeleteEndProduct(endProductId, ct);
				await repository.SaveChangesAsync(ct);
			});
		}
		public async  Task<ReadEndProductDto> GetEndProductAsync(int endProductId, CancellationToken ct = default)
		{
			var result = await repository.GetEndProductAsync(endProductId, ct);
			if(result == null)
				throw new ExceptionHandlerBuisiness($"Nessun end product trovato con ID {endProductId}.", 404);
			return mapper.Map<ReadEndProductDto>(result);
		}
		public async  Task UpdateEndProductAsync(UpdateEndProductDto endProductdto, CancellationToken ct = default)
		{
			var model = mapper.Map<EndProduct>(endProductdto);
			model.RawMaterialForProduction = new List<RawMaterialForProduction>();
			await repository.UpdateEndProductAsync(model, ct);
			await repository.SaveChangesAsync(ct);
			List<CreateRawMaterialForProductionHelperDto> newRawMaterials = new();

			foreach (var rawmaterial in endProductdto.RawMaterialForProduction)
			{
				CreateRawMaterialForProductionHelperDto helper = new()
				{
					EndProductId = endProductdto.Id,
					RawMaterialId = rawmaterial.RawMaterialId,
					QuantityNeeded = rawmaterial.QuantityNeeded
				};
				newRawMaterials.Add(helper);
			}
			var listOfRawMaterialForProduction = mapper.Map<List<RawMaterialForProduction>>(newRawMaterials);

			await repository.CreateRawMaterialForProductionAsync(listOfRawMaterialForProduction, ct);
			await repository.SaveChangesAsync(ct);
		}
		#endregion

		#region RawMaterial
		public async Task CreateRawMaterialAsync(CreateRawMaterialDto rawMaterialDto, CancellationToken ct = default)
		{
			RawMaterial model = mapper.Map<RawMaterial>(rawMaterialDto);
			await repository.CreateRawMaterialAsync(model, ct);
			await repository.SaveChangesAsync(ct);
		}
		public async  Task DeleteRawMaterialAsync(int rawMaterialId, CancellationToken ct = default)
		{
			await repository.DeleteRawMaterial(rawMaterialId, ct);
			await repository.SaveChangesAsync(ct);

		}
		public async Task<ReadAndUpdateRawMaterialDto> GetRawMaterialAsync(int rawMaterialId, CancellationToken ct = default)
		{
			var rawMaterial = await repository.GetRawMaterialAsync(rawMaterialId, ct);	
			if(rawMaterial == null)
				throw new ExceptionHandlerBuisiness($"Nessun rawMaterial trovato con ID {rawMaterialId}.", 404);
			return mapper.Map<ReadAndUpdateRawMaterialDto>(rawMaterial);

		}
		#endregion

		#region Shipment
		public async Task<Dictionary<int,int>?> CheckEndProductsQuantity(List<CreateShippingItemsDto> customerRequest, CancellationToken ct = default)
		{
			Dictionary<int, int> endProductsToBuild = new();
			foreach (var req in customerRequest)
			{
				var endProduct = await  GetEndProductAsync(req.EndProductId, ct);
				if (endProduct == null)
				{
					throw new ExceptionHandlerBuisiness($"Nessun end product trovato con ID {req.EndProductId}.", 404);
				}
				if (endProduct.InStorage < req.Quantity)
				{
					endProductsToBuild.Add(req.EndProductId, req.Quantity - endProduct.InStorage);
					logger.LogDebug("la quantita di elementi ridhiesti e inferiore alla quantita in storage, quindi si procede alla creazione del processo di produzione per l'end product con ID {EndProductId} e quantita {Quantity}", req.EndProductId, req.Quantity - endProduct.InStorage);
				}

			}
			return endProductsToBuild;

		}
		public async Task CreateShipment(CreateShipmentDto shipmentDto, CancellationToken ct = default)
		{
			var productsToBuild = await CheckEndProductsQuantity(shipmentDto.Items, ct);
			 await repository.CreateTransaction(async () =>
			{
				if (productsToBuild != null && productsToBuild.Count > 0)
				{
					foreach (var item in shipmentDto.Items)
					{
						foreach (var productToBuild in productsToBuild)
						{
							if (item.EndProductId == productToBuild.Key)
							{
								CreateProductionProcessDto? productionProcessDto = new ()
								{
									EndProductId = item.EndProductId,
									Quantity = productToBuild.Value
								};
								ProductionProcess productionProcess = mapper.Map<ProductionProcess>(productionProcessDto);

								await repository.CreateProductionProcessAsync(productionProcess);
							}
						}
					}
				}


				Shipment shipment = mapper.Map<Shipment>(shipmentDto);

				await repository.CreateShipmentAsync(shipment, ct); 

				foreach (var item in shipment.Items)
				{
					item.ShipmentId = shipment.Id;

					item.EndProduct = await repository.GetEndProductAsync(item.EndProductId, ct);
					item.EndProduct.InStorage -= item.Quantity;
					await repository.UpdateEndProductAsync(item.EndProduct, ct);
				}

				await repository.CreateShippingItemsAsync(shipment.Items, ct);
				await repository.SaveChangesAsync(ct);

			});
		}

		public async Task<ReadShipmentDto> GetShipment(int ShipmentId, CancellationToken ct = default)
		{
			
			var shipment = await repository.GetShipmentAsync(ShipmentId, ct);
			if (shipment == null)
				throw new ExceptionHandlerBuisiness($"Nessun shipment trovato con ID {ShipmentId}.", 404);
			return mapper.Map<ReadShipmentDto>(shipment);
		
		}


		#endregion











	}
}
