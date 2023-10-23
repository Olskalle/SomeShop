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

		public CategoryService(IGenericRepository<Category> repository) => _repository = repository;

		public void CreateCategory(Category item) => _repository.Create(item);

		public void DeleteCategory(Category item) => _repository.Remove(item);

		public IEnumerable<Category> GetCategories() => _repository.Get();

		public IEnumerable<Category> GetCategories(Expression<Func<Category, bool>> predicate) => _repository.Get(predicate);

		public Category? GetCategoryById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public void UpdateCategory(Category item) => _repository.Update(item);
	}
}
