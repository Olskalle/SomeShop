using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Models
{
	public class ShopStorage
	{
		public int ShopId { get; set; }
		public Shop Shop { get; set; } = null!;
		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;
		public int Quantity { get; set; } = 0;
	}
}
