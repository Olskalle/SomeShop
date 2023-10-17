using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class Order
	{
		[Required, 
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public Client Client { get; set; } = null!;
		[Required]
		public List<OrderItem> OrderItems { get; set; } = null!;
		[Required]
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime ReceiveDate { get; set; }
		[Required]
		public virtual Shop Shop { get; set; } = null!;
		public virtual Employee? Employee { get; set; }
		public virtual Payment? Payment { get; set; }
		[Required]
		public virtual OrderStatus Status { get; set; } = null!;
	}
}
