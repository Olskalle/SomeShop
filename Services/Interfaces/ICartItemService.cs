using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface ICartItemService
    {
        // Manage CartItem
        void CreateCartItem(CartItem item);
        IEnumerable<CartItem> GetCartItems();
        IEnumerable<CartItem> GetCartItems(Func<CartItem, bool> predicate);
        IEnumerable<CartItem> GetItemsBySessionId(int sessionId);
        IEnumerable<CartItem> GetItemsByproductId(int productId);
        void UpdateCartItem(CartItem item);
        void DeleteCartItem(CartItem item);
    }
}
