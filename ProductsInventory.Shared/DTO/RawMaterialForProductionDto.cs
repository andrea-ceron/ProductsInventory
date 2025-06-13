using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Shared.DTO
{
    public class CreateRawMaterialForProductionDto
    {
		public int RawMaterialId { get; set; }
		public int QuantityNeeded { get; set; }
	}

	public class ReadRawMaterialForProductionDto
	{
		public int Id { get; set; }
		required public ReadAndUpdateRawMaterialDto RawMaterial { get; set; }
		public int QuantityNeeded { get; set; }
	}

	public class UpdateRawMaterialForProductionDto
	{
		public int Id { get; set; }
		public int RawMaterialId { get; set; }
		public int QuantityNeeded { get; set; }
	}
}
