using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;


namespace SomeShop.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private readonly IShopContext _context;
		ILogger<IGenericRepository<TEntity>> _logger;
		private DbSet<TEntity> entitySet;

		public GenericRepository(IShopContext context, ILogger<IGenericRepository<TEntity>> logger)
		{
			this._context = context;
			this._logger = logger;
			entitySet = context.Set<TEntity>();
		}

		public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_logger.LogInformation("CREATE {0} OF TYPE {1}", entity, typeof(TEntity));

			cancellationToken.ThrowIfCancellationRequested();

			await entitySet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("GET ITEMS OF TYPE {0}", typeof(TEntity));

			cancellationToken.ThrowIfCancellationRequested();

			return await Task.Run( () => 
				entitySet
					.AsNoTracking()
					.AsQueryable()
			);
		}

		public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken)
		{
			_logger.LogInformation("GET ITEMS OF TYPE {0} WHERE {1}", typeof(TEntity), func.Body);

			cancellationToken.ThrowIfCancellationRequested();

			return await Task.Run(() =>
				entitySet
					.AsNoTracking()
					.Where(func)
					.AsQueryable()
			);
		}

		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_logger.LogInformation("UPDATE {0} OF TYPE {1}", entity, typeof(TEntity));
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_logger.LogInformation("REMOVE {0} OF TYPE {1}", entity, typeof(TEntity));
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
		{
			_logger.LogInformation("DELETE ITEMS OF TYPE {0} WHERE {1}", typeof(TEntity), predicate.Body);

			cancellationToken.ThrowIfCancellationRequested();

			// FIX:  The provider for the source 'IQueryable' doesn't implement 'IAsyncQueryProvider'.
			//		 Only providers that implement 'IAsyncQueryProvider' can be used
			//		 for Entity Framework asynchronous operations.
			//		 # InMemory storage does not imply ExecuteUpdate and ExecuteDelete

			await entitySet.Where(predicate)
				.ExecuteDeleteAsync(cancellationToken);
			await _context.SaveChangesAsync();
		}

		public async Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, 
			params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			_logger.LogInformation("GET ITEMS OF TYPE {0} INCLUDE {1}", typeof(TEntity), includeExpressions);

			cancellationToken.ThrowIfCancellationRequested();

			return await IncludeAsync(includeExpressions);
		}

		public async Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
		{
			_logger.LogInformation("GET ITEMS OF TYPE {0} WHERE {1} INCLUDE {2}", typeof(TEntity), predicate.Body, includeProperties);

			cancellationToken.ThrowIfCancellationRequested();

			var query = await IncludeAsync(includeProperties);
			return query.Where(predicate)
				.AsQueryable();
		}

		private async Task<IQueryable<TEntity>> IncludeAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			IQueryable<TEntity> query = entitySet.AsNoTracking();
			return await Task.Run(() => includeExpressions
				.Aggregate(query,
					(current, includeProperty) => current.Include(includeProperty)));
		}
	}
}
