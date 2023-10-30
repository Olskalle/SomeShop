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
			_logger?.LogInformation("CREATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

				await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeleteShopAsync(Shop item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task DeleteShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
		}

		public async Task<Shop?> GetShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Shop>> GetShopsAsync(CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<Shop>> GetShopsAsync(Expression<Func<Shop, bool>> predicate, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET WUITH CONDITION");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdateShopAsync(Shop item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("UPDATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}
