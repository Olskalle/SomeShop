using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IProductService
    {
        // Manage Products
        void CreateProduct(Product item);
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProducts(Func<Product, bool> predicate);
        Product? GetProductById(int id);
        void UpdateProduct(Product item);
        void DeleteProduct(Product item);
    }
}
