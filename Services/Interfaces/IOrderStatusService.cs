using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IOrderStatusService
    {
        // Manage OrderStatuses
        void CreateOrderStatus(OrderStatus item);
        IEnumerable<OrderStatus> GetOrderStatuses();
        IEnumerable<OrderStatus> GetOrderStatuses(Func<OrderStatus, bool> predicate);
        OrderStatus? GetStatusById(int id);
        void UpdateOrderStatus(OrderStatus item);
        void DeleteOrderStatus(OrderStatus item);
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
