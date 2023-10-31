using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Client
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public List<Order> Orders { get; set; } = new();
		public List<ShoppingSession> Sessions { get; set; } = new();

		public override string ToString()
		{
			return $"{{ Id: {Id}, Name: {Name ?? "null"} }}";
		}
	}
}
