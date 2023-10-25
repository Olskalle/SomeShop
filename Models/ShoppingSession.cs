using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class ShoppingSession
	{
		public int Id { get; set; }
		public List<CartItem> CartItems { get; set; } = null!;
		public List<Product> Products { get; set; } = null!;
		public int ClientId { get; set; }
		public Client Client { get; set; } = null!;
	}
}
