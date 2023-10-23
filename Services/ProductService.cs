using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IGenericRepository<Product> _repository;

		public ProductService(IGenericRepository<Product> repository)
		{
			_repository = repository;
		}

		public void CreateProduct(Product item) => _repository.Create(item);

		public void DeleteProduct(Product item) => _repository.Remove(item);

		public Product? GetProductById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<Product> GetProducts() => _repository.Get();

		public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> predicate) => _repository.Get(predicate);

		public void UpdateProduct(Product item) => _repository.Update(item);
	}
}
