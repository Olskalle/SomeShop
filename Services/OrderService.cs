using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IGenericRepository<Order> _repository;

        public OrderService(IGenericRepository<Order> repository)
        {
			_repository = repository;
        }

		public void CreateOrder(Order item) => _repository.Create(item);

		public void DeleteOrder(Order item) => _repository.Remove(item);

		public Order? GetOrderById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<Order> GetOrders() => _repository.Get();

		public IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> predicate) => _repository.Get(predicate);

		public void UpdateOrder(Order item) => _repository.Update(item);
	}
}
