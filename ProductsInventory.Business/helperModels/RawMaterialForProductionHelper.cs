using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Business.helperModels;

public class CreateRawMaterialForProductionHelperDto
{
	public int RawMaterialId { get; set; }
	public int EndProductId { get; set; }
	public int QuantityNeeded { get; set; }
}
