using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;	

namespace SomeShop.Models
{
	public class CartItem
	{
		[Required]
		public int SessionId { get; set; }
		[Required]
		public virtual ShoppingSession ShoppingSession { get; set; } = null!;
		[Required]
		public int ProductId { get; set; }
		[Required]
		public virtual Product Product { get; set; } = null!;
	}
}