

namespace ProductsInventory.Business;

public class ExceptionHandlerBuisiness : Exception
    {
	public int StatusCode { get; }
	public Object? InvolvedElement { get; }

	public ExceptionHandlerBuisiness(string message, int statusCode = 400)
		: base(message)
	{
		StatusCode = statusCode;
	}

	public ExceptionHandlerBuisiness(string message, Object elem, int statusCode = 400)
		:base(message)
	{
		StatusCode = statusCode;
		InvolvedElement = elem;

	}
}
