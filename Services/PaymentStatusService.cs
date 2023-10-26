using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class PaymentStatusService : IPaymentStatusService
	{
		private readonly IGenericRepository<PaymentStatus> _repository;

		public PaymentStatusService(IGenericRepository<PaymentStatus> repository)
		{
			_repository = repository;
		}

		public async Task CreatePaymentStatusAsync(PaymentStatus item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeletePaymentStatusAsync(PaymentStatus item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task DeletePaymentStatusByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
		}

		public async Task<PaymentStatus?> GetPaymentStatusByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<PaymentStatus>> GetPaymentStatusesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<PaymentStatus>> GetPaymentStatusesAsync(Expression<Func<PaymentStatus, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdatePaymentStatusAsync(PaymentStatus item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}
