
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Business.Abstractions;
using ProductsInventory.Shared.DTO;

namespace CustomerManager.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ShipmentController(IBusiness business, ILogger<ShipmentController> logger) : Controller
{
		private readonly IBusiness _business = business;
		private readonly ILogger<ShipmentController> _logger = logger;



	[HttpPost(Name = "ShipProduct")]
	public async Task<ActionResult> CreateShipment(CreateShipmentDto shipmentDto)
    {
		await _business.CreateShipment(shipmentDto);
		return Ok();
    }

	[HttpGet(Name = "ReadShipment")]
	public async Task<ActionResult<ReadShipmentDto>> GetShipment(int shipmentId)
    {
		var res = await _business.GetShipment(shipmentId);
		return Ok(res);
    }





}
