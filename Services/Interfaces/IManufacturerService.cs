using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IManufacturerService
    {
        // Manage Manufacturers
        Task CreateManufacturerAsync(Manufacturer item, CancellationToken cancellationToken);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync(Expression<Func<Manufacturer, bool>> predicate, CancellationToken cancellationToken);
        Task<Manufacturer> GetManufacturerByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateManufacturerAsync(Manufacturer item, CancellationToken cancellationToken);
        Task DeleteManufacturerAsync(Manufacturer item, CancellationToken cancellationToken);
        Task DeleteManufacturerByIdAsync(int id, CancellationToken cancellationToken);
    }
}
