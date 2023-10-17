using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class ShoppingSession
	{
		[Key,
		DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required] public List<CartItem> Products { get; set; } = null!;
	}
}
