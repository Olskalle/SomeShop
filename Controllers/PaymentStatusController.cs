using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentStatusController : ControllerBase
	{
		private readonly IPaymentStatusService _service;

		public PaymentStatusController(IPaymentStatusService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllPaymentStatuses(CancellationToken cancellationToken)
		{
			var result = await _service.GetPaymentStatusesAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPaymentStatus(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetPaymentStatusByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddPaymentStatus(PaymentStatus? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreatePaymentStatusAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatus? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetPaymentStatusByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdatePaymentStatusAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeletePaymentStatus(int id, CancellationToken cancellationToken)
		{
			await _service.DeletePaymentStatusByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
