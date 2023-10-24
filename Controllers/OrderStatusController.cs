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
		public async Task<IActionResult> GetAllOrderStatuss(CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetOrderStatusesAsync(cancellationToken);

				if (result is null || result.Count() <= 0)
				{
					return NoContent();
				}

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderStatus(int id, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetStatusByIdAsync(id, cancellationToken);

				if (result is null) return NotFound();

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddOrderStatus(OrderStatus? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			try
			{
				await _service.CreateOrderStatusAsync(item, cancellationToken);
				return Ok();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			try
			{
				var toUpdate = await _service.GetStatusByIdAsync(id, cancellationToken);
				if (toUpdate != null)
				{
					await _service.UpdateOrderStatusAsync(item, cancellationToken);
					return Ok();
				}
				return NotFound();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteOrderStatus(int id, CancellationToken cancellationToken)
		{
			try
			{
				await _service.DeleteOrderStatusByIdAsync(id, cancellationToken);
				return NoContent();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}
	}
}
