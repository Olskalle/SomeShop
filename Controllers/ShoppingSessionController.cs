using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingSessionController : ControllerBase
	{
		private readonly IShoppingSessionService _service;

		public ShoppingSessionController(IShoppingSessionService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public IActionResult GetAllShoppingSessions()
		{
			var result = _service.GetShoppingSessions();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetSession(int id)
		{
			var result = _service.GetSessionById(id);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddSession(ShoppingSession? item)
		{
			if (item is null) return BadRequest();

			_service.CreateShoppingSession(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdateShoppingSession(int id, ShoppingSession? item)
		{

			if (item is null || id != item.Id) return BadRequest();

			if (_service.GetSessionById(id) != null)
			{
				_service.UpdateShoppingSession(item);
			}
			else
			{
				_service.CreateShoppingSession(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteShoppingSession(int id)
		{
			var itemToDelete = _service.GetSessionById(id);
			if (itemToDelete != null)
			{
				_service.DeleteShoppingSession(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
