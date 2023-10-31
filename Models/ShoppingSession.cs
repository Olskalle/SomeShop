using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class ShoppingSession
	{
		public int Id { get; set; }
		public List<CartItem> CartItems { get; set; } = new();
		public List<Product> Products { get; set; } = new();
		public int ClientId { get; set; }
		public Client Client { get; set; } = null!;

		public override string ToString()
		{
			return $"{{ Id: {Id}, ClientId: {ClientId} }}";
		}
	}
}
