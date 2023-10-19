using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeManufacturer.Services
{
    public class ManufacturerService : IManufacturerService
    {
        //private readonly ManufacturerContext _context;
        private readonly IGenericRepository<Manufacturer> _repository;
        public ManufacturerService(IGenericRepository<Manufacturer> repository)
        {
            _repository = repository;
        }

		public void CreateManufacturer(Manufacturer item) => _repository.Create(item);

		public void DeleteManufacturer(Manufacturer item) => _repository.Remove(item);

		public Manufacturer? GetManufacturerById(int id)
        {
            var result = _repository.Get(x => x.Id == id);

			if (result is null) return null;

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
        }

		public IEnumerable<Manufacturer> GetManufacturers() => _repository.Get();

		public IEnumerable<Manufacturer> GetManufacturers(Func<Manufacturer, bool> predicate) => _repository.Get(predicate);

		public void UpdateManufacturer(Manufacturer item) => _repository.Update(item);
	}
}
