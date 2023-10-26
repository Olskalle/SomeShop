using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingSessionController : ControllerBase
	{
		private readonly IShoppingSessionService _service;

		public ShoppingSessionController(IShoppingSessionService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllShoppingSessions(CancellationToken cancellationToken)
		{
			var result = await _service.GetSessionsAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetShoppingSession(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetSessionByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddShoppingSession(ShoppingSession? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateSessionAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateShoppingSession(int id, ShoppingSession? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetSessionByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateSessionAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteShoppingSession(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteSessionByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
