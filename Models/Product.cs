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
		public List<Category> Categories { get; set; } = null!;
		public int ManufacturerId { get; set; }
		public Manufacturer Manufacturer { get; set; } = null!;
		public decimal Rating { get; set; } = 0;
		public List<Order> Orders { get; set; } = null!;
		public List<OrderItem> OrderItems { get; set; } = null!;
		public List<CartItem> CartItems { get; set; } = null!;
		public List<ShoppingSession> ShoppingSessions { get; set; } = null!;
		public List<Shop> Shops { get; set; } = null!;
		public List<ShopStorage> ShopStorages { get; set; } = null!;
	}
}
