using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Models;

    public class RawMaterial
    {
        public int Id { get; set; }
		public int SupplierId { get; set; }
		required public string ProductName { get; set; }
		public int InStorage { get; set; }
		public List<RawMaterialForProduction>? RawMaterialForProduction { get; set; } = new List<RawMaterialForProduction>();
}
