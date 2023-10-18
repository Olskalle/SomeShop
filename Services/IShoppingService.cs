using SomeShop.Models;

namespace SomeShop.Services
{
	public interface IShoppingService
	{
		// Manage ShoppingSession
		void CreateClient(Client client);
		IEnumerable<Client> GetClients();
		IEnumerable<Client> GetClients(Func<Client, bool> predicate);
		Client? GetClientById(int id);
		void UpdateClient(Client client);
		void DeleteClient(Client client);
		// Manage Session
		void CreateShoppingSession(ShoppingSession session);
		IEnumerable<ShoppingSession> GetShoppingSessions();
		IEnumerable<ShoppingSession> GetShoppingSessions(Func<ShoppingSession, bool> predicate);
		ShoppingSession? GetShoppingSessionById(int id);
		void UpdateShoppingSession(ShoppingSession session);
		void DeleteShoppingSession(ShoppingSession session);
		// Manage CartItem
		void CreateCartItem(CartItem item);
		IEnumerable<CartItem> GetCartItems();
		IEnumerable<CartItem> GetCartItems(Func<CartItem, bool> predicate);
		CartItem? GetCartItemById(int id);
		void UpdateCartItem(CartItem item);
		void DeleteCartItem(CartItem item);
	}
}
