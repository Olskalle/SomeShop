using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class Payment
	{
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;
		public int ProviderId { get; set; }
		public PaymentProvider Provider { get; set; } = null!;
		public int StatusId { get; set; }
		public PaymentStatus Status { get; set; } = null!;
	}
}
