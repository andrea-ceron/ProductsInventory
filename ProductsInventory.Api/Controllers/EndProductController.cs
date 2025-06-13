using CustomerManager.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsInventory.Business.Abstractions;
using ProductsInventory.Business;
using ProductsInventory.Shared.DTO;

namespace ProductsInventory.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EndProductController(IBusiness business, ILogger<EndProductController> logger) : Controller
    {
	private readonly IBusiness _business = business;
	private readonly ILogger<EndProductController> _logger = logger;



	[HttpPost(Name = "CreateEndProduct")]
	public async Task<ActionResult> CreateEndProduct(CreateEndProductDto endProductDto)
	{

		await _business.CreateEndProductAsync(endProductDto);
		return Ok();


	}



	[HttpDelete(Name = "DeleteEndProduct")]
	public async Task<ActionResult> DeleteEndProduct(int endProductId)
	{
		await _business.DeleteEndProductAsync(endProductId);
		return Ok();
	}


	[HttpGet(Name = "GetEndProduct")]
	public async Task<ActionResult> GetEndProduct( int endProductId)
	{
		var endProduct = await _business.GetEndProductAsync(endProductId);
		return Ok(endProduct);
	}

}
