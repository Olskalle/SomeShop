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

		public async Task CreatePaymentStatus(PaymentStatus item) => _repository.Create(item);

		public async Task DeletePaymentStatus(PaymentStatus item) => _repository.Remove(item);

		public PaymentStatus? GetPaymentStatusById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<PaymentStatus>> GetPaymentStatuses() => _repository.Get();

		public async Task<IEnumerable<PaymentStatus>> GetPaymentStatuses(Expression<Func<PaymentStatus, bool>> predicate) => _repository.Get(predicate);

		public async Task UpdatePaymentStatus(PaymentStatus item) => _repository.Update(item);
	}
}
