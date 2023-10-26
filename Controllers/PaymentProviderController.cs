using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentProviderController : ControllerBase
	{
		private readonly IPaymentProviderService _service;

		public PaymentProviderController(IPaymentProviderService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllPaymentProviders(CancellationToken cancellationToken)
		{
			var result = await _service.GetPaymentProvidersAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPaymentProvider(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetProviderByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddPaymentProvider(PaymentProvider? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreatePaymentProviderAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdatePaymentProvider(int id, PaymentProvider? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetProviderByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdatePaymentProviderAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeletePaymentProvider(int id, CancellationToken cancellationToken)
		{
			await _service.DeletePaymentProviderByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
