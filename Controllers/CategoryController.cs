using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
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
		public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
		{
			var result = await _service.GetCategoriesAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategory(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetCategoryByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddCategory(Category? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateCategoryAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateCategory(int id, Category? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetCategoryByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateCategoryAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteCategoryByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
