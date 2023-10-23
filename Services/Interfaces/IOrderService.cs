using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> predicate);
        Order? GetOrderById(int id);
        void UpdateOrder(Order item);
        void DeleteOrder(Order item);
    }
}
