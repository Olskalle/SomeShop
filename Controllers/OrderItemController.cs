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
		public IActionResult GetAllOrderItems()
		{
			var result = _service.GetOrderItems();

			if (result is null || !result.Any())
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("order/{id}")]
		public IActionResult GetItemsBySession(int id)
		{
			var result = _service.GetItemsByOrderId(id);

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

		[HttpGet("{orderId}&{productId}")]
		public IActionResult GetItemByKey(int orderId, int productId)
		{
			var result = _service.GetItemByKey(orderId, productId);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddOrderItem(OrderItem OrderItem)
		{
			_service.CreateOrderItem(OrderItem);
			return Ok();
		}

		[HttpPut("update/{orderId}&{productId}")]
		public IActionResult UpdateOrderItem(int orderId, int productId, OrderItem item)
		{

			if (orderId != item.OrderId || productId != item.ProductId)
			{
				return BadRequest();
			}

			if (_service.GetItemByKey(orderId, productId) != null)
			{
				_service.UpdateOrderItem(item);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteOrderItem(int orderId, int productId)
		{
			var OrderItemToDelete = _service.GetItemByKey(orderId, productId);
			if (OrderItemToDelete != null)
			{
				_service.DeleteOrderItem(OrderItemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
