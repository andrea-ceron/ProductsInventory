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



	[HttpPost(Name = "CreateRawMaterial")]
	public async Task<ActionResult> CreateRawMaterial(CreateRawMaterialDto rawMaterialDto)
	{

		await _business.CreateRawMaterialAsync(rawMaterialDto);
		return Ok();


	}

	[HttpDelete(Name = "DeleteRawMaterialQuantity")]
	public async Task<ActionResult> DeleteRawMaterial(int rawMaterialId)
	{
		await _business.DeleteRawMaterialAsync(rawMaterialId);
		return Ok();
	}


	[HttpGet(Name = "GetRawMaterialQuantity")]
	public async Task<ActionResult> GetRawMaterial(int rawMaterialId)
	{
		var rawMaterial = await _business.GetRawMaterialAsync(rawMaterialId);
		return Ok(rawMaterial);
	}


}
