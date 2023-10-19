﻿using Microsoft.EntityFrameworkCore;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

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

		public IEnumerable<OrderItem> GetOrderItems(Func<OrderItem, bool> predicate) => _repository.Get(predicate);

		public IEnumerable<OrderItem> GetOrderItemsByProductId(int id) => _repository.Get(x => x.ProductId == id);

		public void UpdateOrderItem(OrderItem item) => _repository.Update(item);
	}
}