using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface ICategoryService
    {
        // Manage Categories
        Task CreateCategoryAsync(Category item, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<Category>>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<Category>>> GetCategoriesAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);
        Task<Category?>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateCategoryAsync(Category item, CancellationToken cancellationToken);
        Task DeleteCategoryAsync(Category item, CancellationToken cancellationToken);
    }
}
