using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShopStorageService
    {
        // Manage ShopStorage
        void CreateShopStorage(ShopStorage item);
        IEnumerable<ShopStorage> GetShopStorages();
        IEnumerable<ShopStorage> GetShopStorages(Expression<Func<ShopStorage, bool>> predicate);
        IEnumerable<ShopStorage> GetStorageByShopId(int id);
        IEnumerable<ShopStorage> GetStorageByProductId(int id);
        ShopStorage? GetStorageByKey(int shopId, int productId);
        void UpdateShopStorage(ShopStorage item);
        void DeleteShopStorage(ShopStorage item);
    }
}
