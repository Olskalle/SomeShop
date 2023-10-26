using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ManufacturerService : IManufacturerService
	{
		//private readonly ManufacturerContext _context;
		private readonly IGenericRepository<Manufacturer> _repository;
		public ManufacturerService(IGenericRepository<Manufacturer> repository)
		{
			_repository = repository;
		}

		public async Task CreateManufacturerAsync(Manufacturer item, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();
			await _repository.CreateAsync(item, cancellatioonToken);
		}

		public async Task DeleteManufacturerAsync(Manufacturer item, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();
			await _repository.RemoveAsync(item, cancellatioonToken);
		}

		public async Task DeleteManufacturerByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
		}

		public async Task<Manufacturer> GetManufacturerByIdAsync(int id, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();
			try
			{
				var result = await _repository.GetAsync(x => x.Id == id, cancellatioonToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellatioonToken);
		}

		public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(Expression<Func<Manufacturer, bool>> predicate, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellatioonToken);
		}

		public async Task UpdateManufacturerAsync(Manufacturer item, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();
			await _repository.UpdateAsync(item, cancellatioonToken);
		}
	}
}
