using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class OrderStatusService : IOrderStatusService
	{
		private readonly IGenericRepository<OrderStatus> _repository;

        public OrderStatusService(IGenericRepository<OrderStatus> repository)
        {
			_repository = repository;
        }

        public void CreateOrderStatus(OrderStatus item) => _repository.Create(item);

		public void DeleteOrderStatus(OrderStatus item) => _repository.Remove(item);

		public OrderStatus? GetStatusById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<OrderStatus> GetOrderStatuses() => _repository.Get();

		public IEnumerable<OrderStatus> GetOrderStatuses(Expression<Func<OrderStatus, bool>> predicate) => _repository.Get(predicate);

		public void UpdateOrderStatus(OrderStatus item) => _repository.Update(item);
	}
}
