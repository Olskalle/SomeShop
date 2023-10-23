using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IShoppingSessionService
    {
        // Manage Session
        void CreateShoppingSession(ShoppingSession session);
        IEnumerable<ShoppingSession> GetShoppingSessions();
        IEnumerable<ShoppingSession> GetShoppingSessions(Expression<Func<ShoppingSession, bool>> predicate);
        ShoppingSession? GetSessionById(int id);
        void UpdateShoppingSession(ShoppingSession session);
        void DeleteShoppingSession(ShoppingSession session);
    }
}
