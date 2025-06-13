using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Shared.DTO
{
    public class CreateProductionProcessDto
    {
		public int EndProductId { get; set; }
		public int Quantity { get; set; }

	}

	public class ReadProductionProcessDto
	{
		public int Id { get; set; }
		required public ReadEndProductDto EndProduct { get; set; }
		public int Quantity { get; set; }

	}
}
