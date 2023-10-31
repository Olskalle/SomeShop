using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ManufacturerController : ControllerBase
	{
		private readonly IManufacturerService _service;

		public ManufacturerController(IManufacturerService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllManufacturers(CancellationToken cancellationToken)
		{
			var result = await _service.GetManufacturersAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetManufacturer(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetManufacturerByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddManufacturer(Manufacturer? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateManufacturerAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateManufacturer(int id, Manufacturer? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetManufacturerByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateManufacturerAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteManufacturer(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteManufacturerByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
