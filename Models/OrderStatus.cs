using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class OrderStatus
	{
		[Key,
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required] public string Name { get; set; } = null!;
	}
}
