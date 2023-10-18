using Microsoft.EntityFrameworkCore;
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

		public void CreateShop(Shop item)
		{
			_repository.Create(item);
		}

		public void DeleteShop(Shop item)
		{
			_repository.Remove(item);
		}

		public Shop? GetShopById(int id)
		{
			var result = _repository.Get(x => x.Id == id);
			if (result.Count() > 1)
			{
				throw new InvalidOperationException("More than one item was found.");
			}
			return result.FirstOrDefault();
		}

		public IEnumerable<Shop> GetShops()
		{
			var result = _repository.Get();
			if (result is null) return new List<Shop>();

			return result;
		}

		public IEnumerable<Shop> GetShops(Func<Shop, bool> predicate)
		{
			var result = _repository.Get(predicate);
			if (result is null) return new List<Shop>();

			return result;
		}

		public void UpdateShop(Shop item)
		{
			_repository.Update(item);
		}
	}
}
