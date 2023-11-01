using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _service;

		public OrderController(IOrderService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
		{
			var result = await _service.GetOrdersAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrder(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetOrderByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddOrder(Order? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateOrderAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateOrder(int id, Order? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetOrderByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateOrderAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteOrderByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
