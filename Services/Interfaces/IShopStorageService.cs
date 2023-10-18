using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IShopStorageService
    {
        // Manage ShopStorage
        void CreateShopStorage(ShopStorage item);
        IEnumerable<ShopStorage> GetShopStorages();
        IEnumerable<ShopStorage> GetShopStorages(Func<ShopStorage, bool> predicate);
        IEnumerable<ShopStorage> GetStorageByShopId(int id);
        IEnumerable<ShopStorage> GetStorageByProductId(int id);
        void UpdateShopStorage(ShopStorage item);
        void DeleteShopStorage(ShopStorage item);
    }
}
