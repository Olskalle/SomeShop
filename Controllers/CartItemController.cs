using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartItemController : ControllerBase
	{
		private readonly ICartItemService _service;

		public CartItemController(ICartItemService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public IActionResult GetAllCartItems()
		{
			var result = _service.GetCartItems();

			if (result is null || !result.Any())
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("session/{id}")]
		public IActionResult GetItemsBySession(int id)
		{
			var result = _service.GetItemsBySessionId(id);

			if (result is null || !result.Any()) return NoContent();

			return Ok(result);
		}

		[HttpGet("product/{id}")]
		public IActionResult GetItemsByProduct(int id)
		{
			var result = _service.GetItemsByProductId(id);

			if (result is null || !result.Any()) return NoContent();

			return Ok(result);
		}

		[HttpGet("{sessionId}&{productId}")]
		public IActionResult GetItemByKey(int sessionId, int productId)
		{
			var result = _service.GetItemByKey(sessionId, productId);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddCartItem(CartItem? item)
		{
			if (item is null) return BadRequest();

			_service.CreateCartItem(item);
			return Ok();
		}

		[HttpPut("update/{sessionId}&{productId}")]
		public IActionResult UpdateCartItem(int sessionId, int productId, CartItem? item)
		{

			if (item is null || sessionId != item.SessionId || productId != item.ProductId)
			{
				return BadRequest();
			}

			if (_service.GetItemByKey(sessionId, productId) != null)
			{
				_service.UpdateCartItem(item);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete("delete/{sessionId}&{productId}")]
		public IActionResult DeleteCartItem(int sessionId, int productId)
		{
			var cartItemToDelete = _service.GetItemByKey(sessionId, productId);
			if (cartItemToDelete != null)
			{
				_service.DeleteCartItem(cartItemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
