using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderItemService
    {
        void CreateOrderItem(OrderItem item);
        IEnumerable<OrderItem> GetOrderItems();
        IEnumerable<OrderItem> GetOrderItems(Func<OrderItem, bool> predicate);
        IEnumerable<OrderItem> GetItemsByOrderId(int id);
        IEnumerable<OrderItem> GetItemsByProductId(int id);
        OrderItem? GetItemByKey(int orderId, int productId);
        void UpdateOrderItem(OrderItem item);
        void DeleteOrderItem(OrderItem item);
    }
}
