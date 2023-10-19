using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderItemService
    {
        void CreateOrderItem(OrderItem item);
        IEnumerable<OrderItem> GetOrderItems();
        IEnumerable<OrderItem> GetOrderItems(Func<OrderItem, bool> predicate);
        IEnumerable<OrderItem> GetItemsByOrderId(int id);
        IEnumerable<OrderItem> GetOrderItemsByProductId(int id);
        void UpdateOrderItem(OrderItem item);
        void DeleteOrderItem(OrderItem item);
    }
}
