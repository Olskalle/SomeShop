using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private readonly IShopService _service;

		public ShopController(IShopService service)
		{
			_service = service;
		}

		[HttpGet]
		public IActionResult GetAllShops()
		{
			var result = _service.GetShops();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetShop(int id)
		{
			var result = _service.GetShopById(id);

			if (result is null) return NoContent();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddShop(Shop shop)
		{
			_service.CreateShop(shop);
			return Ok();
		}
	}
}
