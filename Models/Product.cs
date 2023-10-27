using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public virtual List<Category> Categories { get; set; } = new();
		public virtual List<ProductCategory> ProductCategories { get; set; } = new();
		public int ManufacturerId { get; set; }
		public virtual Manufacturer? Manufacturer { get; set; }
		public decimal Rating { get; set; } = 0;
		public virtual List<Order> Orders { get; set; } = new();
		public virtual List<OrderItem> OrderItems { get; set; } = new();
		public virtual List<CartItem> CartItems { get; set; } = new();
		public virtual List<ShoppingSession> ShoppingSessions { get; set; } = new();
		public virtual List<Shop> Shops { get; set; } = new();
		public virtual List<ShopStorage> ShopStorages { get; set; } = new();
	}
}
