using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class OrderStatusService : IOrderStatusService
	{
		private readonly IGenericRepository<OrderStatus> _repository;
		private readonly ILogger<OrderStatusService>? _logger;

		public OrderStatusService(IGenericRepository<OrderStatus> repository, ILogger<OrderStatusService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateOrderStatusAsync(OrderStatus item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("CREATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeleteOrderStatusAsync(OrderStatus item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task DeleteOrderStatusByIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
		}

		public async Task<OrderStatus?> GetStatusByIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync(CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync(Expression<Func<OrderStatus, bool>> predicate, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET WITH CONDITION");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdateOrderStatusAsync(OrderStatus item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("UPDATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}
