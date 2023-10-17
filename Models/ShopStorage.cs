using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class ShopStorage
	{
		[Required] public int ShopId { get; set; }
		[Required] public Shop Shop { get; set; } = null!;
		[Required] public int ProductId { get; set; }
		[Required] public Product Product { get; set; } = null!;
		public int Quantity { get; set; } = 0;
	}
}
