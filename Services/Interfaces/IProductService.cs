using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IProductService
    {
        // Manage Products
        Task CreateProductAsync(Product item, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetProductsAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product item, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product item, CancellationToken cancellationToken);
        Task DeleteProductByIdAsync(int id, CancellationToken cancellationToken);
    }
}
