using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

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

			if (result is null) return null;

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<OrderStatus> GetOrderStatuses() => _repository.Get();

		public IEnumerable<OrderStatus> GetOrderStatuses(Func<OrderStatus, bool> predicate) => _repository.Get(predicate);

		public void UpdateOrderStatus(OrderStatus item) => _repository.Update(item);
	}
}
