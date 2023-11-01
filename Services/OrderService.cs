using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IGenericRepository<Order> _repository;
		private readonly ILogger<OrderService>? _logger;

		public OrderService(IGenericRepository<Order> repository, ILogger<OrderService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}


		public async Task CreateOrderAsync(Order item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteOrderAsync(Order item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task DeleteOrderByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
			_logger?.LogInformation("DELETE BY ID: {0}", id);
		}

		public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY ID: {0}", id);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellationToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task UpdateOrderAsync(Order item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UPDATE: {0}", item);
		}
	}
}
