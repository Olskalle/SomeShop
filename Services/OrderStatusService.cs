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

        public OrderStatusService(IGenericRepository<OrderStatus> repository)
        {
			_repository = repository;
        }

		public async Task CreateOrderStatusAsync(OrderStatus item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.CreateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task DeleteOrderStatusAsync(OrderStatus item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.RemoveAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task DeleteOrderStatusByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.DeleteAsync(
					x => x.Id == id,
					cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<OrderStatus?> GetStatusByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync(Expression<Func<OrderStatus, bool>> predicate, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
			try
			{
				return await _repository.GetAsync(predicate, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task UpdateOrderStatusAsync(OrderStatus item, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				await _repository.UpdateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
	}
}
