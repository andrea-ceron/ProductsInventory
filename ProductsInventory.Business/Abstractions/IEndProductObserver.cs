using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Business.Abstractions;

public interface IEndProductObserver

{
		IObserver<int> AddEndProduct { get; }
}
