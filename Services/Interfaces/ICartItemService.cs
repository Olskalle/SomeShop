using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface ICartItemService
    {
        // Manage CartItem
        Task CreateCartItemAsync(CartItem item, CancellationToken cancellationToken);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(Expression<Func<CartItem, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<CartItem>> GetItemsBySessionIdAsync(int sessionId, CancellationToken cancellationToken);
        Task<IEnumerable<CartItem>> GetItemsByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<CartItem?> GetItemByKeyAsync(int sessionId, int productId, CancellationToken cancellationToken);
        Task UpdateCartItemAsync(CartItem item, CancellationToken cancellationToken);
        Task DeleteCartItemAsync(CartItem item, CancellationToken cancellationToken);
        Task DeleteCartItemByKeyAsync(int sessionId, int productId, CancellationToken cancellationToken);
    }
}
