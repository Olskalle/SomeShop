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

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("InMemory");
			//optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=SomeShop;Username=postgres;Password=12345qwerty");
			//base.OnConfiguring(optionsBuilder);
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
				//c.Property(x => x.ShoppingSession).IsRequired();
				c.Property(x => x.ProductId).IsRequired();
				//c.Property(x => x.Product).IsRequired();
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
				//e.Property(x => x.Shop).IsRequired();
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

				//o.Property(x => x.Client).IsRequired();
				o.Property(x => x.ClientId).IsRequired();
				//o.Property(x => x.OrderItems).IsRequired();
				o.Property(x => x.CreationDate).IsRequired();
				//o.Property(x => x.Shop).IsRequired();
				o.Property(x => x.ShopId).IsRequired();
				//o.Property(x => x.Status).IsRequired();
			});

			modelBuilder.Entity<OrderItem>(i =>
			{
				i.HasKey(x => new { x.OrderId, x.ProductId });
				//i.Property(x => x.Order).IsRequired();
				i.Property(x => x.OrderId).IsRequired();
				//i.Property(x => x.Product).IsRequired();
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
				//p.Property(x => x.Order).IsRequired();
				p.Property(x => x.ProviderId).IsRequired();
				//p.Property(x => x.Provider).IsRequired();
				p.Property(x => x.StatusId).IsRequired();
				//p.Property(x => x.Status).IsRequired();
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
				//p.Property(x => x.Categories).IsRequired();
				p.Property(x => x.ManufacturerId).IsRequired();
				//p.Property(x => x.Manufacturer).IsRequired();
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
				//s.Property(x => x.Products).IsRequired();
				s.Property(x => x.ClientId).IsRequired();
				//s.Property(x => x.Client).IsRequired();
			});

			modelBuilder.Entity<ShopStorage>(s =>
			{
				s.HasKey(x => new { x.ShopId, x.ProductId });
				s.Property(x => x.ShopId).IsRequired();
				//s.Property(x => x.Shop).IsRequired();
				s.Property(x => x.ProductId).IsRequired();
				//s.Property(x => x.Product).IsRequired();
			});
			modelBuilder.Entity<ShopStorage>()
				.ToTable(t => t.HasCheckConstraint("Quantity", "Quantity >= 0"));
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
					.UsingEntity(j => j.ToTable("ProductCategory"));

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
				.HasData(new Category[] {
					new Category() { Id = 1, Name = "For Men" , Products = new() },
					new Category() { Id = 2, Name = "For Women" , Products = new() },
					new Category() { Id = 3, Name = "Sports" , Products = new() },
					new Category() { Id = 4, Name = "Hiking", Products = new() }
				});

			modelBuilder.Entity<Manufacturer>()
				.HasData(
					new Manufacturer() { Id = 1, Name = "Adidas"},
					new Manufacturer() { Id = 2, Name = "China-Super"},
					new Manufacturer() { Id = 3, Name = "Nike"}
				);
		}
	}
}
