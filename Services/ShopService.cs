using Microsoft.EntityFrameworkCore;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
    public class ShopService : IShopService
	{
		private readonly ShopContext _context;

        public ShopService(ShopContext context)
        {
            _context = context;
        }
		
	}
}
