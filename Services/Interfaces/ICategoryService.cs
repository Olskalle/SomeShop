using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface ICategoryService
    {
        // Manage Categories
        void CreateCategory(Category item);
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategories(Func<Category, bool> predicate);
        Category? GetCategoryById(int id);
        void UpdateCategory(Category item);
        void DeleteCategory(Category item);
    }
}
