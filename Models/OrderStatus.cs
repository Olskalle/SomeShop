using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class OrderStatus
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public override string ToString()
		{
			return $"{{ Id: {Id}, Name: {Name ?? "null"} }}";
		}
	}
}
