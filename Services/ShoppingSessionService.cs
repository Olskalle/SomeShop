using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
	public class ShoppingSessionService : IShoppingSessionService
	{
		private readonly IGenericRepository<ShoppingSession> _repository;

		public ShoppingSessionService(IGenericRepository<ShoppingSession> repository)
		{
			_repository = repository;
		}

		public void CreateShoppingSession(ShoppingSession item) => _repository.Create(item);

		public void DeleteShoppingSession(ShoppingSession item) => _repository.Remove(item);

		public ShoppingSession? GetSessionById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) return null;

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<ShoppingSession> GetShoppingSessions() => _repository.Get();

		public IEnumerable<ShoppingSession> GetShoppingSessions(Func<ShoppingSession, bool> predicate) => _repository.Get(predicate);

		public void UpdateShoppingSession(ShoppingSession item) => _repository.Update(item);
	}
}
