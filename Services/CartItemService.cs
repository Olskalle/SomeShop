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

		public async Task CreateCartItemAsync(CartItem item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeleteCartItemAsync(CartItem item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeleteCartItemByKeyAsync(int sessionId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				await _repository.DeleteAsync(
					x => x.SessionId == sessionId && x.ProductId == productId, 
					cancellationToken);
		}

		public async Task<IEnumerable<CartItem>> GetCartItemsAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<CartItem>> GetCartItemsAsync(Expression<Func<CartItem, bool>> predicate, 
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task<CartItem?> GetItemByKeyAsync(int sessionId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				var result = await _repository
					.GetAsync(x => x.SessionId == sessionId && x.ProductId == productId, 
						cancellationToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
		}

		public async Task<IEnumerable<CartItem>> GetItemsByProductIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				return await _repository.GetAsync(x => x.ProductId == id, cancellationToken);
		}

		public async Task<IEnumerable<CartItem>> GetItemsBySessionIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				return await _repository.GetAsync(x => x.SessionId == id, cancellationToken);
		}

		public async Task UpdateCartItemAsync(CartItem item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

				await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}
