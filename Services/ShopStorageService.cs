using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ShopStorageService : IShopStorageService
	{
		private readonly IGenericRepository<ShopStorage> _repository;
		private readonly ILogger<ShopStorageService>? _logger;
		public ShopStorageService(IGenericRepository<ShopStorage> repository, ILogger<ShopStorageService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateStorageAsync(ShopStorage item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteStorageAsync(ShopStorage item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task<IEnumerable<ShopStorage>> GetStoragesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(cancellationToken);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<ShopStorage>> GetStoragesAsync(Expression<Func<ShopStorage, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task<ShopStorage?> GetStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.ShopId == shopId && x.ProductId == productId, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY KEY: {{ ShopId: {0}, ProductId: {1} }}", shopId, productId);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<ShopStorage>> GetStorageByProductIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.ProductId == id, cancellationToken);
			_logger?.LogInformation("GET BY PRODUCT: {{ ProductId: {0} }}", id);
			return result;
		}

		public async Task<IEnumerable<ShopStorage>> GetStorageByShopIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.ShopId == id, cancellationToken);
			_logger?.LogInformation("GET BY SHOP: {{ ShopId: {0} }}", id);
			return result;
		}

		public async Task UpdateStorageAsync(ShopStorage item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UPDATE: {0}", item);
		}

		public async Task DeleteStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.ShopId == shopId && x.ProductId == productId,
				cancellationToken);
			_logger?.LogInformation("DELETE BY KEY: {{ ShopId: {0}, ProductId: {1} }}", shopId, productId);
		}
	}
}
