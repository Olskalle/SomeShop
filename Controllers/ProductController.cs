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
		public IActionResult GetAllProducts()
		{
			var result = _service.GetProducts();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetProduct(int id)
		{
			var result = _service.GetProductById(id);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddProduct(Product? item)
		{
			if (item is null) return BadRequest();

			_service.CreateProduct(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdateProduct(int id, Product? item)
		{

			if (item is null || id != item.Id) return BadRequest();

			if (_service.GetProductById(id) != null)
			{
				_service.UpdateProduct(item);
			}
			else
			{
				_service.CreateProduct(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var itemToDelete = _service.GetProductById(id);
			if (itemToDelete != null)
			{
				_service.DeleteProduct(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
