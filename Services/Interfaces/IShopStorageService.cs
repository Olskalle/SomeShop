using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShopStorageService
    {
        // Manage ShopStorage
        Task CreateStorageAsync(ShopStorage item, CancellationToken cancellationToken);
        Task<IEnumerable<ShopStorage>> GetStoragesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ShopStorage>> GetStoragesAsync(Expression<Func<ShopStorage, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<ShopStorage>> GetStorageByShopIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ShopStorage>> GetStorageByProductIdAsync(int id, CancellationToken cancellationToken);
        Task<ShopStorage?> GetStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken);
        Task UpdateStorageAsync(ShopStorage item, CancellationToken cancellationToken);
        Task DeleteStorageAsync(ShopStorage item, CancellationToken cancellationToken);
        Task DeleteStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken);
    }
}
