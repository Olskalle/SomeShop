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

		[HttpGet("categories")]
		public IActionResult GetAllCategories()
		{
			return Ok(_service.GetAllCategories());
		}
		[HttpPost("categories/add")]
		public IActionResult CreateCategory(Category category)
		{
			_service.Create(category);
			return Ok();
		}
	}
}
