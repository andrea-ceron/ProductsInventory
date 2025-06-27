using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Business.Abstractions;
using ProductsInventory.Shared.DTO;

namespace CustomerManager.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RawMaterialController(IBusiness business, ILogger<RawMaterialController> logger) : Controller
{
	private readonly IBusiness _business = business;
	private readonly ILogger<RawMaterialController> _logger = logger;

	[HttpGet(Name = "GetRawMaterialQuantity")]
	public async Task<ActionResult> GetRawMaterial(int rawMaterialId)
	{
		var rawMaterial = await _business.GetRawMaterialAsync(rawMaterialId);
		return Ok(rawMaterial);
	}


}
