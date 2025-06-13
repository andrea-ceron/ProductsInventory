using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Shared.DTO
{
    public class CreateShipmentDto
    {
		public DateTime ShipmentDate { get; set; }
		required public string DestinationAddress { get; set; }
		public List<CreateShippingItemsDto> Items { get; set; } = new List<CreateShippingItemsDto>();
	}

	public class ReadShipmentDto
	{
		public int Id { get; set; }
		public DateTime ShipmentDate { get; set; }
		required public string DestinationAddress { get; set; }
		public List<ReadShippingItemsDto> Items { get; set; } = new List<ReadShippingItemsDto>();
	}

	public class UpdateShipmentDto
	{
		public int Id { get; set; }
		public DateTime ShipmentDate { get; set; }
		required public string DestinationAddress { get; set; }
		public List<UpdateShippingItemsDto> Items { get; set; } = new List<UpdateShippingItemsDto>();
	}
}
