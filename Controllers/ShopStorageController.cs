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
		public IActionResult GetAllShopStorages()
		{
			var result = _service.GetShopStorages();

			if (result is null || !result.Any())
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("shop/{id}")]
		public IActionResult GetItemsBySession(int id)
		{
			var result = _service.GetStorageByShopId(id);

			if (result is null || !result.Any()) return NoContent();

			return Ok(result);
		}

		[HttpGet("product/{id}")]
		public IActionResult GetItemsByProduct(int id)
		{
			var result = _service.GetStorageByProductId(id);

			if (result is null || !result.Any()) return NoContent();

			return Ok(result);
		}

		[HttpGet("{orderId}&{productId}")]
		public IActionResult GetItemByKey(int orderId, int productId)
		{
			var result = _service.GetStorageByKey(orderId, productId);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddShopStorage(ShopStorage ShopStorage)
		{
			_service.CreateShopStorage(ShopStorage);
			return Ok();
		}

		[HttpPut("update/{orderId}&{productId}")]
		public IActionResult UpdateShopStorage(int orderId, int productId, ShopStorage item)
		{

			if (orderId != item.ShopId || productId != item.ProductId)
			{
				return BadRequest();
			}

			if (_service.GetStorageByKey(orderId, productId) != null)
			{
				_service.UpdateShopStorage(item);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteShopStorage(int orderId, int productId)
		{
			var ShopStorageToDelete = _service.GetStorageByKey(orderId, productId);
			if (ShopStorageToDelete != null)
			{
				_service.DeleteShopStorage(ShopStorageToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
