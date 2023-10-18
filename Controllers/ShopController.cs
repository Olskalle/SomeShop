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

		[HttpGet("shops")]
		public IActionResult GetAllShops()
		{
			return Ok(_service.GetShops());
		}
		[HttpPost("shops/add")]
		public IActionResult CreateCategory(Shop shop)
		{
			_service.CreateShop(shop);
			return Ok();
		}
	}
}
