using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
			_repository = repository;
        }

        public void CreateCategory(Category item)
		{
			_repository.Create(item);
		}

		public void DeleteCategory(Category item)
		{
			_repository.Remove(item);
		}

		public IEnumerable<Category> GetCategories()
		{
			return _repository.Get();
		}

		public IEnumerable<Category> GetCategories(Func<Category, bool> predicate)
		{
			return _repository.Get(predicate);
		}

		public Category? GetCategoryById(int id)
		{
			var result = _repository.Get(x => x.Id == id);
			
			if (result.Count() > 1)
			{
				throw new InvalidOperationException($"Only one item expected, returned: {result.Count()}");
			}

			return result.FirstOrDefault();
		}

		public void UpdateCategory(Category item)
		{
			_repository.Update(item);
		}
	}
}
