using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Models
{
    public class EndProduct
    {
        public int Id { get; set; }
		required public string ProductName { get; set; }
		public int InStorage { get; set; }
		public decimal Price { get; set; }
		public List<ProductionProcess> ProductionProcess { get; set; } = new List<ProductionProcess>();
		public List<RawMaterialForProduction> RawMaterialForProduction { get; set; } = new List<RawMaterialForProduction>();
	}
}
