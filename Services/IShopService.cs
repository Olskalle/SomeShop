using Microsoft.EntityFrameworkCore;
using SomeShop.Models;

namespace SomeShop.Services
{
	public interface IShopService
	{
		// for testing purposes
		IEnumerable<Category> GetAllCategories();
		void Create(Category item);

		// Manage Product

		// Manage ShopStorage

		// Manage Employee

		// Manage Orders
	}
}
