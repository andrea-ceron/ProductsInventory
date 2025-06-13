using ProductsInventory.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Models;

public class Shipment
{
	public int Id { get; set; }
	public DateTime ShipmentDate { get; set; }
	required public string DestinationAddress { get; set; }
	public List<ShipmentItems> Items { get; set; } = new List<ShipmentItems>();
}
