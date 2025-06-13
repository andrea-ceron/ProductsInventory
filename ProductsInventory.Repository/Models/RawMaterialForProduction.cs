using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Models
{
    public class RawMaterialForProduction
    {
        public int Id { get; set; }
        public int RawMaterialId { get; set; }
        public RawMaterial? RawMaterial { get; set; }
        public int QuantityNeeded { get; set; }
        public int EndProductId { get; set; }   
        public EndProduct? EndProduct { get; set; }
    }
}
