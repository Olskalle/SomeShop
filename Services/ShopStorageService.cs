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

        public ShopStorageService(IGenericRepository<ShopStorage> repository)
        {
            _repository = repository;
        }

		public async Task CreateStorageAsync(ShopStorage item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.UpdateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task DeleteStorageAsync(ShopStorage item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.RemoveAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}		
		}

		public async Task<IEnumerable<ShopStorage>> GetStoragesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<ShopStorage>> GetStoragesAsync(Expression<Func<ShopStorage, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(predicate, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<ShopStorage?> GetStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				var result = await _repository.GetAsync(x => x.ShopId == shopId && x.ProductId == productId,cancellationToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<ShopStorage>> GetStorageByProductIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(x => x.ProductId == id, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<ShopStorage>> GetStorageByShopIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(x => x.ShopId == id, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task UpdateStorageAsync(ShopStorage item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.UpdateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task DeleteStorageByKeyAsync(int shopId, int productId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.DeleteAsync(
					x => x.ShopId == shopId && x.ProductId == productId,
					cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
	}
}
