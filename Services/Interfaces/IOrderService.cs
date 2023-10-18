using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderService
    {
        // Manage Orders
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(Func<Order, bool> predicate);
        Order? GetOrderById(int id);
        void UpdateOrder(Order item);
        void DeleteOrder(Order item);
        // Manage OrderItems
    }
}
/*
void Create%EntityType%(%EntityType% item);
IEnumerable<%EntityType%> Get%EntityType%s();
IEnumerable<%EntityType%> Get%EntityType%s(Func<%EntityType%, bool> predicate);
%EntityType%? Get%EntityType%ById(int id);
void Update%EntityType%(%EntityType% item);
void Delete%EntityType%(%EntityType% item);
 */
