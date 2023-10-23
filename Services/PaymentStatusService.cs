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

		public void CreatePaymentStatus(PaymentStatus item) => _repository.Create(item);

		public void DeletePaymentStatus(PaymentStatus item) => _repository.Remove(item);

		public PaymentStatus? GetPaymentStatusById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<PaymentStatus> GetPaymentStatuses() => _repository.Get();

		public IEnumerable<PaymentStatus> GetPaymentStatuses(Expression<Func<PaymentStatus, bool>> predicate) => _repository.Get(predicate);

		public void UpdatePaymentStatus(PaymentStatus item) => _repository.Update(item);
	}
}
