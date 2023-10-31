using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Manufacturer
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public virtual List<Product> Products { get; set; } = new();

		public override string ToString()
		{
			return $"{{ {Id}, {Name ?? "null"} }}";
		}
	}
}
