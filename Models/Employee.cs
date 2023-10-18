using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Employee
	{
		[Key,
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required] public string Name { get; set; } = null!;
		[MaxLength(32)]
		public string? PhoneNumber { get; set; }
		public List<Order> Orders { get; set; } = new();
	}
}
