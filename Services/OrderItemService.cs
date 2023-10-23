using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
    public class OrderItemService : IOrderItemService
	{
        //private readonly OrderItemContext _context;
        private readonly IGenericRepository<OrderItem> _repository;
		public OrderItemService(IGenericRepository<OrderItem> repository)
		{
			_repository = repository;
		}

		public void CreateOrderItem(OrderItem item) => _repository.Create(item);

		public void DeleteOrderItem(OrderItem item) => _repository.Remove(item);

		public IEnumerable<OrderItem> GetItemsByOrderId(int id) => _repository.Get(x => x.OrderId == id);

		public IEnumerable<OrderItem> GetOrderItems() => _repository.Get();

		public IEnumerable<OrderItem> GetOrderItems(Expression<Func<OrderItem, bool>> predicate) => _repository.Get(predicate);

		public IEnumerable<OrderItem> GetItemsByProductId(int id) => _repository.Get(x => x.ProductId == id);

		public void UpdateOrderItem(OrderItem item) => _repository.Update(item);

		public OrderItem? GetItemByKey(int orderId, int productId)
		{
			var result = _repository.Get(x => x.OrderId == orderId && x.ProductId == productId);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}
	}
}
