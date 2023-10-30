using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public virtual Client Client { get; set; } = null!;
		public List<OrderItem> OrderItems { get; set; } = null!;
		public List<Product> Products { get; set; } = null!;
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime? ReceiveDate { get; set; } = null;
		public int ShopId { get; set; }
		public Shop Shop { get; set; } = null!;
		public int EmployeeId { get; set; }
		public virtual Employee? Employee { get; set; }
		public virtual Payment? Payment { get; set; }
		public int StatusId { get; set; }
		public OrderStatus Status { get; set; } = null!;

		public override string ToString()
		{
			return $"{{ ClientId: {ClientId}, ShopId: {ShopId}, CreationDate: {CreationDate} }}";
		}
	}
}
