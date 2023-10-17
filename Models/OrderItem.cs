using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class OrderItem
	{
		[Required]
		Order Order { get; set; } = null!;
		[Required]
		Product Product { get; set; } = null!;
		int Quantity { get; set; } = 1;
	}
}