using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class OrderItem
	{
		[Required] public int OrderId { get; set; }
		[Required] public Order Order { get; set; } = null!;
		[Required] public int ProductId { get; set; }
		[Required] public Product Product { get; set; } = null!;
		public int Quantity { get; set; } = 1;
	}
}