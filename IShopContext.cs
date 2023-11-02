using Microsoft.EntityFrameworkCore;
using SomeShop.Models;

namespace SomeShop
{
	public interface IShopContext
	{
		DbSet<CartItem> CartItems { get; set; }
		DbSet<Category> Categories { get; set; }
		DbSet<Client> Clients { get; set; }
		DbSet<Employee> Employees { get; set; }
		DbSet<Manufacturer> Manufacturers { get; set; }
		DbSet<OrderItem> OrderItems { get; set; }
		DbSet<Order> Orders { get; set; }
		DbSet<OrderStatus> OrderStatuses { get; set; }
		DbSet<PaymentProvider> PaymentProviders { get; set; }
		DbSet<Payment> Payments { get; set; }
		DbSet<PaymentStatus> PaymentStatuses { get; set; }
		DbSet<Product> Products { get; set; }
		DbSet<ShoppingSession> ShoppingSessions { get; set; }
		DbSet<Shop> Shops { get; set; }
		DbSet<ShopStorage> ShopStorages { get; set; }
		DbSet<T> Set<T>() where T : class;
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}