using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IGenericRepository<Category> _repository;
		private readonly ILogger<CategoryService>? _logger;

		public CategoryService(IGenericRepository<Category> repository, ILogger<CategoryService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateCategoryAsync(Category item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteCategoryAsync(Category item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task DeleteCategoryByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
			_logger?.LogInformation("DELETE BY ID: {0}", id);
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellationToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync(Expression<Func<Category, bool>> predicate,
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY ID: {0}", id);
			return result.FirstOrDefault();
		}

		public async Task UpdateCategoryAsync(Category item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UPDATE: {0}", item);
		}
	}
}
