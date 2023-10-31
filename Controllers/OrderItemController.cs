using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderItemController : ControllerBase
	{
		private readonly IOrderItemService _service;

		public OrderItemController(IOrderItemService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllOrderItems(CancellationToken cancellationToken)
		{

			var result = await _service.GetOrderItemsAsync(cancellationToken);

			if (result is null || !result.Any())
			{
				return NoContent();
			}

			return Ok(result);
		}

		[HttpGet("order/{id}")]
		public async Task<IActionResult> GetItemsByOrder(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetItemsByOrderIdAsync(id, cancellationToken);

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

		[HttpGet("{orderId}&{productId}")]
		public async Task<IActionResult> GetItemByKey(int orderId, int productId, CancellationToken cancellationToken)
		{
			var result = await _service.GetItemByKeyAsync(orderId, productId, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddOrderItem(OrderItem? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateOrderItemAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{orderId}&{productId}")]
		public async Task<IActionResult> UpdateOrderItem(int orderId, int productId, OrderItem? item, CancellationToken cancellationToken)
		{
			if (item is null || orderId != item.OrderId || productId != item.ProductId)
			{
				return BadRequest();
			}

			var toUpdate = await _service.GetItemByKeyAsync(orderId, productId, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateOrderItemAsync(item, cancellationToken);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete("delete/{orderId}&{productId}")]
		public async Task<IActionResult> DeleteOrderItem(int orderId, int productId, CancellationToken cancellationToken)
		{
			await _service.DeleteOrderItemByKeyAsync(orderId, productId, cancellationToken);

			return NoContent();
		}
	}
}
