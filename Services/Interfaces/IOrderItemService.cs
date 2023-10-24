using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task CreateOrderItemAsync(OrderItem item, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<OrderItem>>> GetOrderItemsAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<OrderItem>>> GetOrderItemsAsync(Expression<Func<OrderItem, bool>> predicate, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<OrderItem>>> GetItemsByOrderIdAsync(int id, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<OrderItem>>> GetItemsByProductIdAsync(int id, CancellationToken cancellationToken);
        Task<OrderItem?>> GetItemByKeyAsync(int orderId, int productId, CancellationToken cancellationToken);
        Task UpdateOrderItemAsync(OrderItem item, CancellationToken cancellationToken);
        Task DeleteOrderItemAsync(OrderItem item, CancellationToken cancellationToken);
    }
}
