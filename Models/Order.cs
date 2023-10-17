using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class Order
	{
		[Key, 
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required] 
		public int ClientId { get; set; }
		[Required]
		public virtual Client Client { get; set; } = null!;
		[Required]
		public List<OrderItem> OrderItems { get; set; } = null!;
		[Required]
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime ReceiveDate { get; set; }
		[Required]
		public int Shopid { get; set; }
		[Required]
		public Shop Shop { get; set; } = null!;
		public int EmployeeId { get; set; }
		public virtual Employee? Employee { get; set; }
		public virtual Payment? Payment { get; set; }
		[Required]
		public OrderStatus Status { get; set; } = null!;
	}
}
