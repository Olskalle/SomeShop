using Microsoft.EntityFrameworkCore;
using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
    public class ShopService : IShopService
	{
        //private readonly ShopContext _context;
        private readonly IGenericRepository<Shop> _repository;
		private readonly ILogger<ShopService>? _logger;
        public ShopService(IGenericRepository<Shop> repository, ILogger<ShopService>? logger)
        {
			_repository = repository;
			_logger = logger;
        }

		public async Task CreateShopAsync(Shop item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteShopAsync(Shop item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task DeleteShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
			_logger?.LogInformation("DELETE BY ID: {0}", id);
		}

		public async Task<Shop?> GetShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY ID: {0}", id);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Shop>> GetShopsAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellationToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<Shop>> GetShopsAsync(Expression<Func<Shop, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WUITH CONDITION");
			return result;
		}

		public async Task UpdateShopAsync(Shop item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UPDATE: {0}", item);
		}
	}
}
