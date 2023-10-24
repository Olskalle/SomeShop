using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private readonly IShopService _service;

		public ShopController(IShopService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllShops(CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetShopsAsync(cancellationToken);

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
		public async Task<IActionResult> GetShop(int id, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetShopByIdAsync(id, cancellationToken);

				if (result is null) return NotFound();

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddShop(Shop? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			try
			{
				await _service.CreateShopAsync(item, cancellationToken);
				return Ok();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateShop(int id, Shop? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			try
			{
				var toUpdate = await _service.GetShopByIdAsync(id, cancellationToken);
				if (toUpdate != null)
				{
					await _service.UpdateShopAsync(item, cancellationToken);
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
		public async Task<IActionResult> DeleteShop(int id, CancellationToken cancellationToken)
		{
			try
			{
				await _service.DeleteShopByIdAsync(id, cancellationToken);
				return NoContent();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}
	}
}
