using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Shared.DTO
{
    public class CreateEndProductDto

    {
		required public string ProductName { get; set; }
		public int InStorage { get; set; }
		public decimal Price { get; set; }
		public List<CreateRawMaterialForProductionDto> RawMaterialForProduction { get; set; } = new List<CreateRawMaterialForProductionDto>();

	}

	public class UpdateEndProductDto

	{
		public int Id { get; set; }
		required public string ProductName { get; set; }
		public int InStorage { get; set; }
		public decimal Price { get; set; }
		public List<UpdateRawMaterialForProductionDto> RawMaterialForProduction { get; set; } = new List<UpdateRawMaterialForProductionDto>();
		public List<CreateRawMaterialForProductionDto> CreateRawMaterialForProduction { get; set; } = new List<CreateRawMaterialForProductionDto>();

	}

	public class ReadEndProductDto

	{
		public int Id { get; set; }
		required public string ProductName { get; set; }
		public int InStorage { get; set; }
		public decimal Price { get; set; }
		public List<ReadRawMaterialForProductionDto> RawMaterialForProduction { get; set; } = new List<ReadRawMaterialForProductionDto>();

	}

	public class EndProductDtoForKafka

	{
		public int Id { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int InStorage { get; set; }
		public decimal Price { get; set; }
		public int VAT { get; set; } = 22; 

	}
}
