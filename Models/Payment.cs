using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Payment
	{
		[Required,
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public Order Order { get; set; } = null!;
		[Required]
		public Client Client { get; set; } = null!;
		public List<OrderItem> OrderItems { get; set; } = null!;
	}
}
