using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IShoppingSessionService
    {
        // Manage Session
        void CreateShoppingSession(ShoppingSession session);
        IEnumerable<ShoppingSession> GetShoppingSessions();
        IEnumerable<ShoppingSession> GetShoppingSessions(Func<ShoppingSession, bool> predicate);
        ShoppingSession? GetSessionById(int id);
        void UpdateShoppingSession(ShoppingSession session);
        void DeleteShoppingSession(ShoppingSession session);
    }
}
