using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Business.Abstractions;

public interface IEndProductObservable
{
	IObservable<int> AddEndProduct { get; }

}
