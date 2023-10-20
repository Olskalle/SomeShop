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
		public IActionResult GetAllPaymentStatuss()
		{
			var result = _service.GetPaymentStatuses();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetPaymentStatus(int id)
		{
			var result = _service.GetPaymentStatusById(id);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddPaymentStatus(PaymentStatus item)
		{
			_service.CreatePaymentStatus(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdatePaymentStatus(int id, PaymentStatus item)
		{

			if (id != item.Id) return BadRequest();

			if (_service.GetPaymentStatusById(id) != null)
			{
				_service.UpdatePaymentStatus(item);
			}
			else
			{
				_service.CreatePaymentStatus(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeletePaymentStatus(int id)
		{
			var itemToDelete = _service.GetPaymentStatusById(id);
			if (itemToDelete != null)
			{
				_service.DeletePaymentStatus(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
