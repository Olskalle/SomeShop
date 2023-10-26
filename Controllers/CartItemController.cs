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
		public async Task<IActionResult> GetAllCartItems(CancellationToken cancellationToken)
		{
			var result = await _service.GetCartItemsAsync(cancellationToken);

			if (result is null || !result.Any())
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("session/{id}")]
		public async Task<IActionResult> GetItemsBySession(int id, CancellationToken cancellationToken)
		{

			var result = await _service.GetItemsBySessionIdAsync(id, cancellationToken);

			if (result is null || !result.Any()) return NoContent();

			return Ok(result);
		}

		[HttpGet("product/{id}")]
		public async Task<IActionResult> GetItemsByProduct(int id, CancellationToken cancellationToken)
		{

			var result = await _service.GetItemsByProductIdAsync(id, cancellationToken);

			if (result is null || !result.Any()) return NoContent();

			return Ok(result);
		}

		[HttpGet("{sessionId}&{productId}")]
		public async Task<IActionResult> GetItemByKey(int sessionId, int productId, CancellationToken cancellationToken)
		{

			var result = await _service.GetItemByKeyAsync(sessionId, productId, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddCartItem(CartItem? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();


			await _service.CreateCartItemAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{sessionId}&{productId}")]
		public async Task<IActionResult> UpdateCartItem(int sessionId, int productId, CartItem? item, CancellationToken cancellationToken)
		{
			if (item is null || sessionId != item.SessionId || productId != item.ProductId)
			{
				return BadRequest();
			}

			var toUpdate = await _service.GetItemByKeyAsync(sessionId, productId, cancellationToken);

			if (toUpdate != null)
			{
				await _service.UpdateCartItemAsync(item, cancellationToken);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete("delete/{sessionId}&{productId}")]
		public async Task<IActionResult> DeleteCartItem(int sessionId, int productId, CancellationToken cancellationToken)
		{

			await _service.DeleteCartItemByKeyAsync(sessionId, productId, cancellationToken);

			return NoContent();
		}
	}
}
