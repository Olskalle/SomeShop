using Microsoft.EntityFrameworkCore;
using System.Data.Common;

using SomeShop.Models;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop
{
	public class ShopContext : DbContext, IShopContext
	{
		private readonly IConfiguration? _configuration;

        public ShopContext() { } // For migrations
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
			//optionsBuilder.UseInMemoryDatabase("InMemory");
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SomeShop;Username=postgres;Password=admin");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			ConfigureEntities(modelBuilder);
			ConfigureRelations(modelBuilder);

			SeedDatabase(modelBuilder);
		}

		private void ConfigureEntities(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CartItem>(c =>
			{
				c.HasKey(x => new { x.SessionId, x.ProductId });
				c.Property(x => x.SessionId).IsRequired();
				c.Property(x => x.ProductId).IsRequired();
			});
			modelBuilder.Entity<CartItem>()
				.ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 1"));

			modelBuilder.Entity<Category>(c =>
			{
				c.HasKey(x => x.Id);
				c.Property(x => x.Id).ValueGeneratedOnAdd();
				c.Property(x => x.Name).IsRequired();
			});

			modelBuilder.Entity<Client>(c =>
			{
				c.HasKey(x => x.Id);
				c.Property(x => x.Id).ValueGeneratedOnAdd();
				c.Property(x => x.Name).IsRequired();
				c.Property(x => x.PhoneNumber).HasMaxLength(15);
			});
			modelBuilder.Entity<Client>()
				.ToTable(t => t.HasCheckConstraint("PhoneNumber", "PhoneNumber LIKE '8%'"));

			modelBuilder.Entity<Employee>(e =>
			{
				e.HasKey(x => x.Id);
				e.Property(x => x.Id).ValueGeneratedOnAdd();
				e.Property(x => x.Name).IsRequired();
				e.Property(x => x.ShopId).IsRequired();
				e.Property(x => x.PhoneNumber).HasMaxLength(15);
			});
			modelBuilder.Entity<Employee>()
				.ToTable(t => t.HasCheckConstraint("PhoneNumber", "PhoneNumber LIKE '8%'"));

			modelBuilder.Entity<Manufacturer>(m =>
			{
				m.HasKey(x => x.Id);
				m.Property(x => x.Id).ValueGeneratedOnAdd();
				m.Property(x => x.Name).IsRequired();
			});

			modelBuilder.Entity<Order>(o =>
			{
				o.HasKey(x => x.Id);
				o.Property(x => x.Id).ValueGeneratedOnAdd();
				
				o.Property(x => x.ClientId).IsRequired();
				o.Property(x => x.CreationDate).IsRequired();
				o.Property(x => x.ShopId).IsRequired();
			});

			modelBuilder.Entity<OrderItem>(i =>
			{
				i.HasKey(x => new { x.OrderId, x.ProductId });
				i.Property(x => x.OrderId).IsRequired();
				i.Property(x => x.ProductId).IsRequired();
			});
			modelBuilder.Entity<OrderItem>()
				.ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 1"));

			modelBuilder.Entity<OrderStatus>(s =>
			{
				s.HasKey(x => x.Id);
				s.Property(x => x.Id).ValueGeneratedOnAdd();
				s.Property(x => x.Name).IsRequired();
			});

			modelBuilder.Entity<Payment>(p =>
			{
				p.HasKey(x => x.OrderId);
				p.Property(x => x.OrderId).IsRequired();
				p.Property(x => x.ProviderId).IsRequired();
				p.Property(x => x.StatusId).IsRequired();
			});

			modelBuilder.Entity<PaymentProvider>(p =>
			{
				p.HasKey(x => x.Id);
				p.Property(x => x.Id).ValueGeneratedOnAdd();
				p.Property(x => x.Name).IsRequired();
			});

			modelBuilder.Entity<PaymentStatus>(s =>
			{
				s.HasKey(x => x.Id);
				s.Property(x => x.Id).ValueGeneratedOnAdd();
				s.Property(x => x.Name).IsRequired();
			});

			modelBuilder.Entity<Product>(p =>
			{
				p.HasKey(x => x.Id);
				p.Property(x => x.Id).ValueGeneratedOnAdd();
				p.Property(x => x.Name).IsRequired();
				p.Property(x => x.ManufacturerId).IsRequired();
			});

			modelBuilder.Entity<Shop>(s =>
			{
				s.HasKey(x => x.Id);
				s.Property(x => x.Id).ValueGeneratedOnAdd();
				s.Property(x => x.Address).IsRequired();
			});
			modelBuilder.Entity<Shop>()
				.ToTable(t => t.HasCheckConstraint("PhoneNumber", "PhoneNumber LIKE '8%'"));

			modelBuilder.Entity<ShoppingSession>(s =>
			{
				s.HasKey(x => x.Id);
				s.Property(x => x.Id).ValueGeneratedOnAdd();
				s.Property(x => x.ClientId).IsRequired();
			});

			modelBuilder.Entity<ShopStorage>(s =>
			{
				s.HasKey(x => new { x.ShopId, x.ProductId });
				s.Property(x => x.ShopId).IsRequired();
				s.Property(x => x.ProductId).IsRequired();
			});
			modelBuilder.Entity<ShopStorage>()
				.ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 0"));

			modelBuilder.Entity<ProductCategory>(pc =>
			{
				pc.HasKey(x => new { x.ProductId, x.CategoryId });
				pc.Property(x => x.ProductId).IsRequired();
				pc.Property(x => x.CategoryId).IsRequired();
			});
		}

		private void ConfigureRelations(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(p =>
			{
				p.HasOne(p => p.Manufacturer)
					.WithMany(m => m.Products)
					.HasForeignKey(p => p.ManufacturerId)
					.IsRequired();

				p.HasMany(x => x.Categories)
					.WithMany(x => x.Products)
					.UsingEntity<ProductCategory>(
						j => j.HasOne(pc => pc.Category)
							.WithMany(c => c.ProductCategories)
							.HasForeignKey(pc => pc.CategoryId),
						j => j.HasOne(pc => pc.Product)
							.WithMany(p => p.ProductCategories)
							.HasForeignKey(pc => pc.ProductId)
					);

				p.HasMany(p => p.Orders)
					.WithMany(o => o.Products)
					.UsingEntity<OrderItem>(
						j => j.HasOne(i => i.Order)
							.WithMany(o => o.OrderItems)
							.HasForeignKey(i => i.OrderId),
						j => j.HasOne(i => i.Product)
							.WithMany(p => p.OrderItems)
							.HasForeignKey(i => i.ProductId)
					);

				p.HasMany(p => p.ShoppingSessions)
					.WithMany(s => s.Products)
					.UsingEntity<CartItem>(
						j => j.HasOne(i => i.ShoppingSession)
							.WithMany(s => s.CartItems)
							.HasForeignKey(i => i.SessionId),
						j => j.HasOne(i => i.Product)
							.WithMany(p => p.CartItems)
							.HasForeignKey(i => i.ProductId)
					);

				p.HasMany(p => p.Shops)
					.WithMany(s => s.Products)
					.UsingEntity<ShopStorage>(
						j => j.HasOne(j => j.Shop)
							.WithMany(s => s.ShopStorages)
							.HasForeignKey(j => j.ShopId),
						j => j.HasOne(j => j.Product)
							.WithMany(p => p.ShopStorages)
							.HasForeignKey(j => j.ProductId)
					);
			});

			modelBuilder.Entity<Order>(o =>
			{
				o.HasOne(x => x.Client)
					.WithMany(c => c.Orders)
					.HasForeignKey(x => x.ClientId);

				o.HasOne(x => x.Shop)
					.WithMany(s => s.Orders)
					.HasForeignKey(x => x.ShopId);

				o.HasOne(x => x.Employee)
					.WithMany(e => e.Orders)
					.HasForeignKey(x => x.EmployeeId);

				o.HasOne(x => x.Status)
					.WithMany()
					.HasForeignKey(x => x.StatusId);

				o.HasOne(x => x.Payment)
					.WithOne(p => p.Order);
			});

			modelBuilder.Entity<Payment>(p =>
			{
				p.HasOne(x => x.Order)
					.WithOne(o => o.Payment)
					.HasForeignKey<Payment>(x => x.OrderId);

				p.HasOne(x => x.Status)
					.WithMany()
					.HasForeignKey(x => x.StatusId);

				p.HasOne(x => x.Provider)
					.WithMany()
					.HasForeignKey(x => x.ProviderId);
			});

			modelBuilder.Entity<Employee>(e =>
			{
				e.HasOne(x => x.Shop)
					.WithMany(s => s.Employees)
					.HasForeignKey(x => x.ShopId);
			});

			modelBuilder.Entity<ShoppingSession>(s =>
			{
				s.HasOne(x => x.Client)
					.WithMany(c => c.Sessions)
					.HasForeignKey(x => x.ClientId);
			});
		}

		private void SeedDatabase(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasData(
					new Category() { Id = 1, Name = "For Men" },
					new Category() { Id = 2, Name = "For Women" },
					new Category() { Id = 3, Name = "Sports" },
					new Category() { Id = 4, Name = "Hiking" },
					new Category() { Id = 5, Name = "Yoga" }
				);

			modelBuilder.Entity<Manufacturer>()
				.HasData(
					new Manufacturer() { Id = 1, Name = "Adidas" },
					new Manufacturer() { Id = 2, Name = "China-Super" },
					new Manufacturer() { Id = 3, Name = "Nike"}
				);

			modelBuilder.Entity<Client>()
				.HasData(
					new Client() { Id = 1, Name = "Ivan" },
					new Client() { Id = 2, Name = "Anatoliy" },
					new Client() { Id = 3, Name = "Bartek" }
				);

			modelBuilder.Entity<Employee>()
				.HasData(
					new Employee() { Id = 1, Name = "Gus", PhoneNumber = "89001001090", ShopId = 1},
					new Employee() { Id = 2, Name = "Walter", PhoneNumber = "89053332222", ShopId = 3},
					new Employee() { Id = 3, Name = "Valentina", PhoneNumber = "89999009090", ShopId = 2},
					new Employee() { Id = 4, Name = "Gennadiy", PhoneNumber = "81234567890", ShopId = 3}
				);

			modelBuilder.Entity<Shop>()
				.HasData(
					new Shop() { Id = 1, Address = "Perm, Sibirskaya st.", PhoneNumber = "89998889955"},
					new Shop() { Id = 2, Address = "Moscow, GUM", PhoneNumber = "89665577889"},
					new Shop() { Id = 3, Address = "USA, Kansas, Hays, Oak st.", PhoneNumber = "89997775566"}
				);

			modelBuilder.Entity<Product>()
				.HasData(
					new Product()
					{
						Id = 1,
						Name = "Red boots",
						ManufacturerId = 1,
						Rating = 100
					},
					new Product()
					{
						Id = 2,
						Name = "Super sticky stick",
						ManufacturerId = 2,
						Rating = 33
					}
				);

			modelBuilder.Entity < ProductCategory>()
				.HasData(
					new {ProductId = 1, CategoryId = 1},
					new {ProductId = 1, CategoryId = 3},
					new {ProductId = 2, CategoryId = 2},
					new {ProductId = 2, CategoryId = 5}
				);

			modelBuilder.Entity<OrderStatus>()
				.HasData(
					new OrderStatus() { Id = 1, Name = "Created"},
					new OrderStatus() { Id = 2, Name = "In process"},
					new OrderStatus() { Id = 3, Name = "Delivering"},
					new OrderStatus() { Id = 4, Name = "Ready"},
					new OrderStatus() { Id = 5, Name = "Canceled"}
				);

			modelBuilder.Entity<PaymentStatus>()
				.HasData(
					new PaymentStatus() { Id = 1, Name = "Confirmed"},
					new PaymentStatus() { Id = 2, Name = "Rejected"}
				);

			modelBuilder.Entity<PaymentProvider>()
				.HasData(
					new PaymentProvider() { Id = 1, Name = "Sber"},
					new PaymentProvider() { Id = 2, Name = "Tinkoff"},
					new PaymentProvider() { Id = 3, Name = "UralFD"},
					new PaymentProvider() { Id = 4, Name = "QIWI"},
					new PaymentProvider() { Id = 5, Name = "PayPal"}
				);

			modelBuilder.Entity<ShoppingSession>()
				.HasData(
					new ShoppingSession() 
					{ 
						Id = 1, 
						ClientId = 3
					},
					new ShoppingSession()
					{
						Id = 2,
						ClientId = 2
					}
				);

			modelBuilder.Entity<CartItem>()
				.HasData(
					new CartItem() { ProductId = 1, SessionId = 2, Quantity = 1},
					new CartItem() { ProductId = 2, SessionId = 2, Quantity = 1},
					new CartItem() { ProductId = 1, SessionId = 1, Quantity = 10}
				);

			modelBuilder.Entity<Order>()
				.HasData(
					new Order()
					{
						Id = 1,
						ClientId = 1,
						ShopId = 1,
						EmployeeId = 1,
						StatusId = 1
					},
					new Order()
					{
						Id = 2,
						ClientId = 3,
						ShopId = 3,
						EmployeeId = 4,
						StatusId = 4
					}
				);

			modelBuilder.Entity<OrderItem>()
				.HasData(
					new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 2},
					new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 4},
					new OrderItem() { OrderId = 2, ProductId = 1, Quantity = 10},
					new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 1}
				);

			modelBuilder.Entity<Payment>()
				.HasData(
					new Payment() { OrderId = 2, ProviderId = 1, StatusId = 1 }
				);

			modelBuilder.Entity<ShopStorage>()
				.HasData(
					new ShopStorage() { ShopId = 1, ProductId = 1, Quantity = 100},
					new ShopStorage() { ShopId = 2, ProductId = 1, Quantity = 234},
					new ShopStorage() { ShopId = 3, ProductId = 1, Quantity = 2},
					new ShopStorage() { ShopId = 1, ProductId = 2, Quantity = 2222},
					new ShopStorage() { ShopId = 2, ProductId = 2, Quantity = 1010},
					new ShopStorage() { ShopId = 3, ProductId = 2, Quantity = 7889}
				);
		}
	}
}
