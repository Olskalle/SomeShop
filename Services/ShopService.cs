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
			=> await _repository.CreateAsync(item, cancellationToken);

		public async Task DeleteShopAsync(Shop item, CancellationToken cancellationToken) 
			=> await _repository.RemoveAsync(item, cancellationToken);

		public async Task DeleteShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				var item = _repository.Get(x => x.Id == id)?.Single();
				if (item is null) throw new NullReferenceException();

				await _repository.RemoveAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<Shop?> GetShopByIdAsync(int id, CancellationToken cancellationToken)
		{
			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Shop>> GetShopsAsync(CancellationToken cancellationToken) 
			=> await _repository.GetAsync(cancellationToken);

		public async Task<IEnumerable<Shop>> GetShopsAsync(Expression<Func<Shop, bool>> predicate, CancellationToken cancellationToken) 
			=> await _repository.GetAsync(predicate, cancellationToken);

		public async Task UpdateShopAsync(Shop item, CancellationToken cancellationToken) 
			=> await _repository.UpdateAsync(item, cancellationToken);
	}
}
