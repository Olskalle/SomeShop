using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
    public class ShopService : IShopService
	{
        //private readonly ShopContext _context;
        private readonly IGenericRepository<Shop> _repository;
        public ShopService(IGenericRepository<Shop> repository)
        {
			_repository = repository;
        }

		public void CreateShop(Shop item) => _repository.Create(item);

		public void DeleteShop(Shop item) => _repository.Remove(item);

		public Shop? GetShopById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) return null;

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<Shop> GetShops() => _repository.Get();

		public IEnumerable<Shop> GetShops(Func<Shop, bool> predicate) => _repository.Get(predicate);

		public void UpdateShop(Shop item) => _repository.Update(item);
	}
}
