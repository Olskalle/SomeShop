using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;	

namespace SomeShop.Models
{
	public class CartItem
	{
		public int SessionId { get; set; }
		public virtual ShoppingSession? ShoppingSession { get; set; }
		public int ProductId { get; set; }
		public virtual Product? Product { get; set; }
	}
}