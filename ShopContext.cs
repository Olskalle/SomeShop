using Microsoft.EntityFrameworkCore;
using System.Data.Common;

using SomeShop.Models;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop
{
	public class ShopContext : DbContext, IShopContext
	{
		private readonly IConfiguration _configuration;
		public ShopContext(IConfiguration configuration)
		{
			_configuration = configuration;
			Database.EnsureCreated();
		}
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Shop> Shops { get; set; }
		public DbSet<ShopStorage> ShopStorages { get; set; }

		public DbSet<Client> Clients { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<ShoppingSession> ShoppingSessions { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Manufacturer> Manufacturers { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<OrderStatus> OrderStatuses { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentProvider> PaymentProviders { get; set; }
		public DbSet<PaymentStatus> PaymentStatuses { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("InMemory");
			//optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=SomeShop;Username=postgres;Password=12345qwerty");
			//base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<CartItem>().HasKey(c => new {c.SessionId, c.ProductId});
			modelBuilder.Entity<OrderItem>().HasKey(o => new {o.OrderId, o.ProductId});
			modelBuilder.Entity<ShopStorage>().HasKey(s => new {s.ShopId, s.ProductId});

			modelBuilder.Entity<Employee>().ToTable(t => t.HasCheckConstraint("PhoneNumber", "PhoneNumber LIKE '8%'"));
			modelBuilder.Entity<Client>().ToTable(t => t.HasCheckConstraint("PhoneNumber", "PhoneNumber LIKE '8%'"));
			modelBuilder.Entity<Shop>().ToTable(t => t.HasCheckConstraint("PhoneNumber", "PhoneNumber LIKE '8%'"));

			modelBuilder.Entity<ShopStorage>().ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 0"));
			modelBuilder.Entity<CartItem>().ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 1"));
			modelBuilder.Entity<OrderItem>().ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 1"));

			SeedDatabase(modelBuilder);
		}

		private void SeedDatabase(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasData(new Category[] {
					new Category() { Id = 1, Name = "For Men" , Products = new() },
					new Category() { Id = 2, Name = "For Women" , Products = new() },
					new Category() { Id = 3, Name = "Sports" , Products = new() },
					new Category() { Id = 4, Name = "Hiking", Products = new() }
				});

			modelBuilder.Entity<Manufacturer>().HasData(
				new Manufacturer() { Id = 1, Name = "Adidas"},
				new Manufacturer() { Id = 2, Name = "China-Super"},
				new Manufacturer() { Id = 3, Name = "Nike"}
				);
		}
	}
}
