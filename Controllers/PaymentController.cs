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
		public IActionResult GetAllPayments()
		{
			var result = _service.GetPayments();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("order/{orderId}")]
		public IActionResult GetPayment(int orderId)
		{
			var result = _service.GetPaymentByOrderId(orderId);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddPayment(Payment? item)
		{
			if (item is null) return BadRequest();

			_service.CreatePayment(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdatePayment(int id, Payment? item)
		{

			if (item is null || id != item.OrderId) return BadRequest();

			if (_service.GetPaymentByOrderId(id) != null)
			{
				_service.UpdatePayment(item);
			}
			else
			{
				_service.CreatePayment(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeletePayment(int id)
		{
			var itemToDelete = _service.GetPaymentByOrderId(id);
			if (itemToDelete != null)
			{
				_service.DeletePayment(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
