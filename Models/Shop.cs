using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Shop
	{
		public int Id { get; set; }
		public string Address { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public List<Product> Products { get; set; } = null!;
		public List<ShopStorage> ShopStorages { get; set; } = null!;
		public List<Order> Orders { get; set; } = null!;
		public List<Employee> Employees { get; set; } = null!;
	}
}
