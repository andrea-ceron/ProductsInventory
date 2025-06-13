using ProductsInventory.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository.Models;

    public class ProductionProcess
    {
	public int Id { get; set; }
	public EndProduct? EndProduct { get; set; }
	public int EndProductId { get; set; }
	public int Quantity { get; set; }

}
