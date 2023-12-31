﻿using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ManufacturerService : IManufacturerService
	{
		private readonly IGenericRepository<Manufacturer> _repository;
		private readonly ILogger<ManufacturerService> _logger;

		public ManufacturerService(IGenericRepository<Manufacturer> repository, ILogger<ManufacturerService> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateManufacturerAsync(Manufacturer item, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellatioonToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteManufacturerAsync(Manufacturer item, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellatioonToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task DeleteManufacturerByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
			_logger?.LogInformation("DELETE BY ID: {0}", id);
		}

		public async Task<Manufacturer?> GetManufacturerByIdAsync(int id, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellatioonToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY ID: {0}", id);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellatioonToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(Expression<Func<Manufacturer, bool>> predicate, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellatioonToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task UpdateManufacturerAsync(Manufacturer item, CancellationToken cancellatioonToken)
		{
			cancellatioonToken.ThrowIfCancellationRequested();
			await _repository.UpdateAsync(item, cancellatioonToken);
			_logger?.LogInformation("UPDATE: {0}", item);
		}
	}
}
