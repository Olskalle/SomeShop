using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShoppingSessionService
    {
        Task CreateShoppingSessionAsync(ShoppingSession session, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<ShoppingSession>>> GetShoppingSessionsAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<ShoppingSession>>> GetShoppingSessionsAsync(Expression<Func<ShoppingSession, bool>> predicate, CancellationToken cancellationToken);
        ShoppingSession? GetSessionByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateShoppingSessionAsync(ShoppingSession session, CancellationToken cancellationToken);
        Task DeleteShoppingSessionAsync(ShoppingSession session, CancellationToken cancellationToken);
    }
}
