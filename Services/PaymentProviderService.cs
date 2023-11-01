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
		private readonly ILogger<PaymentProviderService>? _logger;

		public PaymentProviderService(IGenericRepository<PaymentProvider> repository, ILogger<PaymentProviderService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreatePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeletePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task<PaymentProvider?> GetProviderByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY ID: {0}", id);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<PaymentProvider>> GetPaymentProvidersAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellationToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<PaymentProvider>> GetPaymentProvidersAsync(Expression<Func<PaymentProvider, bool>> predicate,
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task UpdatePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UDPATE: {0}", item);
		}

		public async Task DeletePaymentProviderByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
					x => x.Id == id,
					cancellationToken);
			_logger?.LogInformation("DELETE BY ID: {0}", id);
		}
	}
}
