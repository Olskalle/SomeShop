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
		public IActionResult GetAllPaymentProviders()
		{
			var result = _service.GetPaymentProviders();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetPaymentProvider(int id)
		{
			var result = _service.GetProviderById(id);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddPaymentProvider(PaymentProvider item)
		{
			_service.CreatePaymentProvider(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdatePaymentProvider(int id, PaymentProvider item)
		{

			if (id != item.Id) return BadRequest();

			if (_service.GetProviderById(id) != null)
			{
				_service.UpdatePaymentProvider(item);
			}
			else
			{
				_service.CreatePaymentProvider(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeletePaymentProvider(int id)
		{
			var itemToDelete = _service.GetProviderById(id);
			if (itemToDelete != null)
			{
				_service.DeletePaymentProvider(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
