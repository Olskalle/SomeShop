﻿using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class CartItemService : ICartItemService
	{
		private readonly IGenericRepository<CartItem> _repository;
		private readonly ILogger<CartItemService>? _logger;
		
		public CartItemService(IGenericRepository<CartItem> repository, ILogger<CartItemService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateCartItemAsync(CartItem item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteCartItemAsync(CartItem item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task DeleteCartItemByKeyAsync(int sessionId, int productId, CancellationToken cancellationToken)
		{	
			cancellationToken.ThrowIfCancellationRequested();

				await _repository.DeleteAsync(
					x => x.SessionId == sessionId && x.ProductId == productId, 
					cancellationToken);
			_logger?.LogInformation("DELETE BY KEY: {{{0}, {1}}}", sessionId, productId);
		}

		public async Task<IEnumerable<CartItem>> GetCartItemsAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellationToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<CartItem>> GetCartItemsAsync(Expression<Func<CartItem, bool>> predicate, 
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task<CartItem?> GetItemByKeyAsync(int sessionId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository
				.GetAsync(x => x.SessionId == sessionId && x.ProductId == productId, 
					cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY KEY: {{ {0}, {1} }}", sessionId, productId);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<CartItem>> GetItemsByProductIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.ProductId == id, cancellationToken);
			_logger?.LogInformation("GET BY PRODUCT: {0}", id);
			return result;
		}

		public async Task<IEnumerable<CartItem>> GetItemsBySessionIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.SessionId == id, cancellationToken);
			_logger?.LogInformation("GET BY SESSION {0}", id);
			return result;
		}

		public async Task UpdateCartItemAsync(CartItem item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UPDATE {0}", item);
		}
	}
}
