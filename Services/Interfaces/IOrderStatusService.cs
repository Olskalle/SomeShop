using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderStatusService
    {
        // Manage OrderStatuses
        Task CreateOrderStatusAsync(OrderStatus item, CancellationToken cancellationCancel);
        Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync(CancellationToken cancellationCancel);
        Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync(Expression<Func<OrderStatus, bool>> predicate, CancellationToken cancellationCancel);
        Task<OrderStatus?> GetStatusByIdAsync(int id, CancellationToken cancellationCancel);
        Task UpdateOrderStatusAsync(OrderStatus item, CancellationToken cancellationCancel);
        Task DeleteOrderStatusAsync(OrderStatus item, CancellationToken cancellationCancel);
        Task DeleteOrderStatusByIdAsync(int id, CancellationToken cancellationToken);

	}
}
