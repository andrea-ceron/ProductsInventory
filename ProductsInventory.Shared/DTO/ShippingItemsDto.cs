using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Shared.DTO
{
    public class CreateShippingItemsDto
    {
        public int EndProductId { get; set; }
        public int Quantity { get; set; }
	}

	public class ReadShippingItemsDto
	{
		public int Id { get; set; }
		required public ReadEndProductDto EndProduct { get; set; }
		public int Quantity { get; set; }
	}

	public class UpdateShippingItemsDto
	{
		public int Id { get; set; }
		public int ShipmentId { get; set; }
		public int EndProductId { get; set; }
		public int Quantity { get; set; }
	}
}
