using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class PaymentProviderService : IPaymentProviderService
	{
		private readonly IGenericRepository<PaymentProvider> _repository;

		public PaymentProviderService(IGenericRepository<PaymentProvider> repository)
		{
			_repository = repository;
		}

		public async Task CreatePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeletePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task<PaymentProvider?> GetProviderByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<PaymentProvider>> GetPaymentProvidersAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();


			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<PaymentProvider>> GetPaymentProvidersAsync(Expression<Func<PaymentProvider, bool>> predicate,
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdatePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);

		}

		public async Task DeletePaymentProviderByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
					x => x.Id == id,
					cancellationToken);
		}
	}
}
