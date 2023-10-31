using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;
using System.Threading;

namespace SomeShop.Services
{
	public class OrderItemService : IOrderItemService
	{
		//private readonly OrderItemContext _context;
		private readonly IGenericRepository<OrderItem> _repository;
		private readonly ILogger<OrderItemService> _logger;

		public OrderItemService(IGenericRepository<OrderItem> repository, ILogger<OrderItemService> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateOrderItemAsync(OrderItem item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("CREATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeleteOrderItemAsync(OrderItem item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task<IEnumerable<OrderItem>> GetItemsByOrderIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY ORDER: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(x => x.OrderId == id, cancellationToken);
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(Expression<Func<OrderItem, bool>> predicate, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET WITH CONDITION");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task<IEnumerable<OrderItem>> GetItemsByProductIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY PRODUCT: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(x => x.ProductId == id, cancellationToken);
		}

		public async Task UpdateOrderItemAsync(OrderItem item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("UPDATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
		}

		public async Task<OrderItem?> GetItemByKeyAsync(int orderId, int productId, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY KEY: {{ OrderId: {0}, ProductId: {1} }}", orderId, productId);
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x =>
				x.OrderId == orderId && x.ProductId == productId,
				cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task DeleteOrderItemByKeyAsync(int orderId, int productId, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE BY KEY: {{ OrderId: {0}, ProductId: {1} }}", orderId, productId);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.OrderId == orderId && x.ProductId == productId,
				cancellationToken);

		}
	}
}
