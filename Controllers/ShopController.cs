using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

		[HttpGet("all")]
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

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddShop(Shop shop)
		{
			_service.CreateShop(shop);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdateShop(int id, Shop shop)
		{

			if (id != shop.Id) return BadRequest();

			if (_service.GetShopById(id) != null)
			{
				_service.UpdateShop(shop);
			}
			else
			{
				_service.CreateShop(shop);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteShop(int id)
		{
			var shopToDelete = _service.GetShopById(id);
			if (shopToDelete != null)
			{
				_service.DeleteShop(shopToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
