using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

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

		public override string ToString()
		{
			return $"{{ OrderId: {OrderId}, Status: {StatusId} }}";
		}
	}
}
