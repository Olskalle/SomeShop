using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeCategory.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _service;

		public CategoryController(ICategoryService service)
		{
			_service = service;
		}

		[HttpGet("all")]
		public IActionResult GetAllCategorys()
		{
			var result = _service.GetCategories();

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetCategory(int id)
		{
			var result = _service.GetCategoryById(id);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public IActionResult AddCategory(Category item)
		{
			_service.CreateCategory(item);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public IActionResult UpdateCategory(int id, Category item)
		{

			if (id != item.Id) return BadRequest();

			if (_service.GetCategoryById(id) != null)
			{
				_service.UpdateCategory(item);
			}
			else
			{
				_service.CreateCategory(item);
			}

			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteCategory(int id)
		{
			var itemToDelete = _service.GetCategoryById(id);
			if (itemToDelete != null)
			{
				_service.DeleteCategory(itemToDelete);
				return Ok();
			}

			return NotFound();
		}
	}
}
