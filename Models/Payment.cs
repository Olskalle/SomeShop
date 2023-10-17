using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Payment
	{
		//[Key,
		//DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int Id { get; set; }
		[Key, Required] public int OrderId { get; set; }
		[Required] public Order Order { get; set; } = null!;
		[Required] public PaymentProvider Provider { get; set; } = null!;
		[Required] public PaymentStatus Status { get; set; } = null!;
	}
}
