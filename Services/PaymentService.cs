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
		private readonly ILogger<PaymentService>? _logger;

		public PaymentService(IGenericRepository<Payment> repository, ILogger<PaymentService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreatePaymentAsync(Payment item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("CRAETE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeletePaymentAsync(Payment item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task DeletePaymentByOrderIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.OrderId == id,
				cancellationToken);
		}

		public async Task<Payment?> GetPaymentByOrderIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY ID", id);
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.OrderId == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Payment>> GetPaymentsAsync(CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<Payment>> GetPaymentsAsync(Expression<Func<Payment, bool>> predicate, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET WITH CONDITION");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdatePaymentAsync(Payment item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("UPDATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}
