using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
	public class CartItemService : ICartItemService
	{
		private readonly IGenericRepository<CartItem> _repository;

        public CartItemService(IGenericRepository<CartItem> repository)
        {
			_repository = repository;
        }

		public void CreateCartItem(CartItem item)
		{
			_repository.Create(item);
		}

		public void DeleteCartItem(CartItem item)
		{
			_repository.Remove(item);
		}

		public IEnumerable<CartItem> GetCartItems()
		{
			return _repository.Get();
		}

		public IEnumerable<CartItem> GetCartItems(Func<CartItem, bool> predicate)
		{
			return _repository.Get(predicate);
		}

		public IEnumerable<CartItem> GetItemsByProductId(int id)
		{
			return _repository.Get(x => x.ProductId == id);
		}

		public IEnumerable<CartItem> GetItemsBySessionId(int id)
		{
			return _repository.Get(x => x.SessionId == id);
		}

		public void UpdateCartItem(CartItem item)
		{
			_repository.Update(item);
		}
	}
}
