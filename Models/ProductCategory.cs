namespace SomeShop.Models
{
	public class ProductCategory
	{
		public int ProductId { get; set; }
		public Product? Product { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }

		public override string ToString()
		{
			return $"{{ ProductId: {ProductId}, CategoryId: {CategoryId} }}";
		}
	}
}
