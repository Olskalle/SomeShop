using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Product
	{
		[Required,
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public List<Category> Categories { get; set; } = null!;
		[Required]
		public Manufacturer Manufacturer { get; set; } = null!;
		public decimal Rating { get; set; } = 0;
	}
}
