using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _service;

		public ProductController(IProductService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
		{
			var result = await _service.GetProductsAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetProductByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddProduct(Product? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateProductAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateProduct(int id, Product? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetProductByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateProductAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteProductByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
