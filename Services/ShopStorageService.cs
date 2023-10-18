using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
    public class ShopStorageService : IShopStorageService
    {
        private readonly IGenericRepository<ShopStorage> _repository;

        public ShopStorageService(IGenericRepository<ShopStorage> repository)
        {
            _repository = repository;
        }

        public void CreateShopStorage(ShopStorage item)
        {
            _repository.Create(item);
        }

        public void DeleteShopStorage(ShopStorage item)
        {
            _repository.Remove(item);
        }

        public IEnumerable<ShopStorage> GetShopStorages()
        {
            return _repository.Get();
        }

        public IEnumerable<ShopStorage> GetShopStorages(Func<ShopStorage, bool> predicate)
        {
            return _repository.Get(predicate);
        }

        public IEnumerable<ShopStorage> GetStorageByProductId(int id)
        {
            return _repository.Get(x => x.ProductId == id);
        }

        public IEnumerable<ShopStorage> GetStorageByShopId(int id)
        {
            return _repository.Get(x => x.ShopId == id);
        }

        public void UpdateShopStorage(ShopStorage item)
        {
            _repository.Update(item);
        }
    }
}
