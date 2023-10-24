using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ShoppingSessionService : IShoppingSessionService
	{
		private readonly IGenericRepository<ShoppingSession> _repository;

		public ShoppingSessionService(IGenericRepository<ShoppingSession> repository)
		{
			_repository = repository;
		}

		public async Task CreateShoppingSession(ShoppingSession item) => _repository.Create(item);

		public async Task DeleteShoppingSession(ShoppingSession item) => _repository.Remove(item);

		public ShoppingSession? GetSessionById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<ShoppingSession>> GetShoppingSessions() => _repository.Get();

		public async Task<IEnumerable<ShoppingSession>> GetShoppingSessions(Expression<Func<ShoppingSession, bool>> predicate) => _repository.Get(predicate);

		public async Task UpdateShoppingSession(ShoppingSession item) => _repository.Update(item);
	}
}
