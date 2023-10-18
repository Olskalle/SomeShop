using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IShopService
    {
	    // Manage Shops
        void CreateShop(Shop item);
        IEnumerable<Shop> GetShops();
        IEnumerable<Shop> GetShops(Func<Shop, bool> predicate);
        Shop? GetShopById(int id);
        void UpdateShop(Shop item);
        void DeleteShop(Shop item);
    }
}
