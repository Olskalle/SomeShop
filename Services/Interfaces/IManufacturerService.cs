using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IManufacturerService
    {
        // Manage Manufacturers
        void CreateManufacturer(Manufacturer item);
        IEnumerable<Manufacturer> GetManufacturers();
        IEnumerable<Manufacturer> GetManufacturers(Func<Manufacturer, bool> predicate);
        Manufacturer? GetManufacturerById(int id);
        void UpdateManufacturer(Manufacturer item);
        void DeleteManufacturer(Manufacturer item);
    }
}
