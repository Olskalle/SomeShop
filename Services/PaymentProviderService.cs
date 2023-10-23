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

		public void CreatePaymentProvider(PaymentProvider item) => _repository.Create(item);

		public void DeletePaymentProvider(PaymentProvider item) => _repository.Remove(item);

		public PaymentProvider? GetProviderById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<PaymentProvider> GetPaymentProviders() => _repository.Get();

		public IEnumerable<PaymentProvider> GetPaymentProviders(Expression<Func<PaymentProvider, bool>> predicate) => _repository.Get(predicate);

		public void UpdatePaymentProvider(PaymentProvider item) => _repository.Update(item);
	}
}
