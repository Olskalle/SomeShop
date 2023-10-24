using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IGenericRepository<Payment> _repository;

		public PaymentService(IGenericRepository<Payment> repository)
		{
			_repository = repository;
		}

		public async Task CreatePaymentAsync(Payment item, CancellationToken cancellationToken)
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

		public async Task DeletePaymentAsync(Payment item, CancellationToken cancellationToken)
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

		public async Task DeletePaymentByOrderIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.DeleteAsync(
					x => x.OrderId == id,
					cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<Payment?> GetPaymentByOrderIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				var result = await _repository.GetAsync(x => x.OrderId == id, cancellationToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<Payment>> GetPaymentsAsync(CancellationToken cancellationToken)
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

		public async Task<IEnumerable<Payment>> GetPaymentsAsync(Expression<Func<Payment, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(predicate, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task UpdatePaymentAsync(Payment item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

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
