using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
    public class ShopStorageService : IShopStorageService
    {
        private readonly IGenericRepository<ShopStorage> _repository;

        public ShopStorageService(IGenericRepository<ShopStorage> repository)
        {
            _repository = repository;
        }

		public void CreateShopStorage(ShopStorage item) => _repository.Create(item);

		public void DeleteShopStorage(ShopStorage item) => _repository.Remove(item);

		public IEnumerable<ShopStorage> GetShopStorages() => _repository.Get();

		public IEnumerable<ShopStorage> GetShopStorages(Expression<Func<ShopStorage, bool>> predicate) => _repository.Get(predicate);

		public ShopStorage? GetStorageByKey(int shopId, int productId)
		{
			var result = _repository.Get(x => x.ShopId == shopId && x.ProductId == productId);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<ShopStorage> GetStorageByProductId(int id) => _repository.Get(x => x.ProductId == id);

        public IEnumerable<ShopStorage> GetStorageByShopId(int id) => _repository.Get(x => x.ShopId == id);

        public void UpdateShopStorage(ShopStorage item) => _repository.Update(item);
    }
}
