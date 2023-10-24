using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShopStorageService
    {
        // Manage ShopStorage
        Task CreateShopStorageAsync(ShopStorage item, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<ShopStorage>>> GetShopStoragesAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<ShopStorage>>> GetShopStoragesAsync(Expression<Func<ShopStorage, bool>> predicate, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<ShopStorage>>> GetStorageByShopIdAsync(int id, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<ShopStorage>>> GetStorageByProductIdAsync(int id, CancellationToken cancellationToken);
        ShopStorage? GetStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken);
        Task UpdateShopStorageAsync(ShopStorage item, CancellationToken cancellationToken);
        Task DeleteShopStorageAsync(ShopStorage item, CancellationToken cancellationToken);
    }
}
