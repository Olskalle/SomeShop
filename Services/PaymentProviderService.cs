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

		public async Task CreatePaymentProvider(PaymentProvider item) => _repository.Create(item);

		public async Task DeletePaymentProvider(PaymentProvider item) => _repository.Remove(item);

		public PaymentProvider? GetProviderById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<PaymentProvider>> GetPaymentProviders() => _repository.Get();

		public async Task<IEnumerable<PaymentProvider>> GetPaymentProviders(Expression<Func<PaymentProvider, bool>> predicate) => _repository.Get(predicate);

		public async Task UpdatePaymentProvider(PaymentProvider item) => _repository.Update(item);
	}
}
