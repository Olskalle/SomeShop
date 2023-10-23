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

		public void CreatePayment(Payment item) => _repository.Create(item);

		public void DeletePayment(Payment item) => _repository.Remove(item);

		public Payment? GetPaymentByOrderId(int id)
		{
			var result = _repository.Get(x => x.OrderId == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<Payment> GetPayments() => _repository.Get();
		public IEnumerable<Payment> GetPayments(Expression<Func<Payment, bool>> predicate) => _repository.Get(predicate);

		public void UpdatePayment(Payment item) => _repository.Update(item);
	}
}
