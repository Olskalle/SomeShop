using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class CartItemService : ICartItemService
	{
		private readonly IGenericRepository<CartItem> _repository;

		public CartItemService(IGenericRepository<CartItem> repository) => _repository = repository;

		public void CreateCartItem(CartItem item) => _repository.Create(item);

		public void DeleteCartItem(CartItem item) => _repository.Remove(item);

		public IEnumerable<CartItem> GetCartItems() => _repository.Get();

		public IEnumerable<CartItem> GetCartItems(Expression<Func<CartItem, bool>> predicate) => _repository.Get(predicate);

		public CartItem? GetItemByKey(int sessionId, int productId)
		{
			var result =_repository
				.Get(x => x.SessionId == sessionId 
					&& x.ProductId == productId);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<CartItem> GetItemsByProductId(int id) => _repository.Get(x => x.ProductId == id);

		public IEnumerable<CartItem> GetItemsBySessionId(int id) => _repository.Get(x => x.SessionId == id);

		public void UpdateCartItem(CartItem item) => _repository.Update(item);
	}
}
