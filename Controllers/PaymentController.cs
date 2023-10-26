using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _service;

		public PaymentController(IPaymentService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllPayments(CancellationToken cancellationToken)
		{
			var result = await _service.GetPaymentsAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPayment(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetPaymentByOrderIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddPayment(Payment? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreatePaymentAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdatePayment(int id, Payment? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.OrderId) return BadRequest();

			var toUpdate = await _service.GetPaymentByOrderIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdatePaymentAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeletePayment(int id, CancellationToken cancellationToken)
		{
			await _service.DeletePaymentByOrderIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
