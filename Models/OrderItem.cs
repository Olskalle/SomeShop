﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SomeShop.Models
{
	public class OrderItem
	{
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;
		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;
		public int Quantity { get; set; } = 1;
	}
}