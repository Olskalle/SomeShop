using Microsoft.EntityFrameworkCore;
using SomeShop.Models;

namespace SomeShop.Services
{
	public class ShopService : IShopService
	{
		private readonly ShopContext _context;

        public ShopService(ShopContext context)
        {
            _context = context;
        }

		public IEnumerable<Category> GetAllCategories()
		{
			return _context.Categories.AsNoTracking().ToList();
		}

		public void Create(Category item)
		{
			try
			{
				_context.Categories.Add(item);
				_context.SaveChanges();
			}
			catch { }
		}
		
	}
}
