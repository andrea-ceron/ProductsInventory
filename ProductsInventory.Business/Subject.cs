using ProductsInventory.Business.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Business;

public class Subject : ISubject
{
	private Subject<int> _endProductSubject = new Subject<int>();
	public IObservable<int> AddEndProduct => _endProductSubject;

	IObserver<int> IEndProductObserver.AddEndProduct => _endProductSubject;
}
