using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderStatusController : ControllerBase
	{
		private readonly IOrderStatusService _service;

		public OrderStatusController(IOrderStatusService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public IActionResult GetAllOrderStatuss()
		{
			var result = _service.GetOrderStatuses();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetOrderStatus(int id)
		{
			var result = _service.GetStatusById(id);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddOrderStatus(OrderStatus item)
		{
			_service.CreateOrderStatus(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdateOrderStatus(int id, OrderStatus item)
		{

			if (id != item.Id) return BadRequest();

			if (_service.GetStatusById(id) != null)
			{
				_service.UpdateOrderStatus(item);
			}
			else
			{
				_service.CreateOrderStatus(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteOrderStatus(int id)
		{
			var itemToDelete = _service.GetStatusById(id);
			if (itemToDelete != null)
			{
				_service.DeleteOrderStatus(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
