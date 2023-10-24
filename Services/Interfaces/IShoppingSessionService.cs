using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShoppingSessionService
    {
        Task CreateSessionAsync(ShoppingSession session, CancellationToken cancellationToken);
        Task<IEnumerable<ShoppingSession>> GetSessionsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ShoppingSession>> GetSessionsAsync(Expression<Func<ShoppingSession, bool>> predicate, CancellationToken cancellationToken);
        Task<ShoppingSession?> GetSessionByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateSessionAsync(ShoppingSession session, CancellationToken cancellationToken);
        Task DeleteSessionAsync(ShoppingSession session, CancellationToken cancellationToken);
        Task DeleteSessionByIdAsync(int id, CancellationToken cancellationToken);
    }
}
