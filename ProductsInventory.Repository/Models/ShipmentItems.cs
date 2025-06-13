using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Models
{
    public class ShipmentItems
    {
        public int Id { get; set; }
        public Shipment? Shipment { get; set; }
        public int ShipmentId { get; set; }
        public EndProduct? EndProduct { get; set; }
		public int EndProductId { get; set; }
        public int Quantity { get; set; }
	}
}
