using ProductsInventory.Repository.Models;
using ProductsInventory.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Business.Abstractions
{
    public interface IBusiness
    {
		//public Task CreateRawMaterialAsync(CreateRawMaterialDto rawMaterialDto, CancellationToken ct = default);
		public Task<ReadAndUpdateRawMaterialDto> GetRawMaterialAsync(int rawMaterialId, CancellationToken ct = default);
		//public Task DeleteRawMaterialAsync(int rawMaterialId, CancellationToken ct = default);

		public Task CreateEndProductAsync(CreateEndProductDto endProduct, CancellationToken ct = default);
		public Task UpdateEndProductAsync(UpdateEndProductDto endProduct, CancellationToken ct = default);
		public Task DeleteEndProductAsync(int endProductId, CancellationToken ct = default);
		public Task<ReadEndProductDto> GetEndProductAsync(int endProductId, CancellationToken ct = default);

		public Task CreateShipment(CreateShipmentDto shipment, CancellationToken ct = default);
		public Task<ReadShipmentDto> GetShipment(int ShipmentId, CancellationToken ct = default);
		public Task<Dictionary<int, int>?> CheckEndProductsQuantity(List<CreateShippingItemsDto> customerRequest, CancellationToken ct = default);
	}
}
