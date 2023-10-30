using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;	

namespace SomeShop.Models
{
	public class CartItem
	{
		public int SessionId { get; set; }
		public ShoppingSession? ShoppingSession { get; set; }
		public int ProductId { get; set; }
		public Product? Product { get; set; }
		public int Quantity { get; set; } = 1;

		public override string ToString()
		{
			return $"{{ {SessionId}, {ProductId} }}";
		}
	}
}