using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken);
        Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateOrderAsync(Order item, CancellationToken cancellationToken);
        Task DeleteOrderAsync(Order item, CancellationToken cancellationToken);
        Task DeleteOrderByIdAsync(int id, CancellationToken cancellationToken);
    }
}
