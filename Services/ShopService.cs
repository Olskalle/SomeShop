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
        public ShopService(IGenericRepository<Shop> repository)
        {
			_repository = repository;
        }

		public async Task CreateShopAsync(Shop item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.CreateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task DeleteShopAsync(Shop item, CancellationToken cancellationToken)
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

		public async Task DeleteShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.DeleteAsync(
					x => x.Id == id,
					cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<Shop?> GetShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<IEnumerable<Shop>> GetShopsAsync(CancellationToken cancellationToken)
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

		public async Task<IEnumerable<Shop>> GetShopsAsync(Expression<Func<Shop, bool>> predicate, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
			try
			{
				return await _repository.GetAsync(predicate, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task UpdateShopAsync(Shop item, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				await _repository.UpdateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
	}
}
