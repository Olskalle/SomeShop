using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShopService
    {
	    // Manage Shops
        Task CreateShopAsync(Shop item, CancellationToken cancellationToken);
        Task<IEnumerable<Shop>> GetShopsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Shop>> GetShopsAsync(Expression<Func<Shop, bool>> predicate, CancellationToken cancellationToken);
        Task<Shop?> GetShopByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateShopAsync(Shop item, CancellationToken cancellationToken);
        Task DeleteShopAsync(Shop item, CancellationToken cancellationToken);
        Task DeleteShopByIdAsync(int id, CancellationToken cancellationToken);
    }
}
