using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public List<Order> Orders { get; set; } = new();
		public int ShopId { get; set; }
		public Shop Shop { get; set; } = null!;
	}
}
