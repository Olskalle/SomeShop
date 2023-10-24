using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopStorageController : ControllerBase
	{
		private readonly IShopStorageService _service;

		public ShopStorageController(IShopStorageService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllShopStorages(CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetStoragesAsync(cancellationToken);

				if (result is null || !result.Any())
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
		[HttpGet("shop/{id}")]
		public async Task<IActionResult> GetStoragesBySession(int id, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetStorageByShopIdAsync(id, cancellationToken);

				if (result is null || !result.Any()) return NoContent();

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpGet("product/{id}")]
		public async Task<IActionResult> GetStoragesByProduct(int id, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetStorageByProductIdAsync(id, cancellationToken);

				if (result is null || !result.Any()) return NoContent();

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpGet("{shopId}&{productId}")]
		public async Task<IActionResult> GetStorageByKey(int shopId, int productId, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _service.GetStorageByKeyAsync(shopId, productId, cancellationToken);

				if (result is null) return NotFound();

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddShopStorage(ShopStorage? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			try
			{
				await _service.CreateStorageAsync(item, cancellationToken);
				return Ok();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpPut("update/{shopId}&{productId}")]
		public async Task<IActionResult> UpdateShopStorage(int shopId, int productId, ShopStorage? item, CancellationToken cancellationToken)
		{
			if (item is null || shopId != item.ShopId || productId != item.ProductId)
			{
				return BadRequest();
			}

			try
			{
				var toUpdate = await _service.GetStorageByKeyAsync(shopId, productId, cancellationToken);

				if (toUpdate != null)
				{
					await _service.UpdateStorageAsync(item, cancellationToken);
					return Ok();
				}

				return NotFound();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}

		[HttpDelete("delete/{shopId}&{productId}")]
		public async Task<IActionResult> DeleteShopStorage(int shopId, int productId, CancellationToken cancellationToken)
		{
			try
			{
				await _service.DeleteStorageByKeyAsync(shopId, productId, cancellationToken);

				return NoContent();
			}
			catch (OperationCanceledException)
			{
				return BadRequest();
			}
		}
	}
}
