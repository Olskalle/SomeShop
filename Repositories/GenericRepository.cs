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

			cancellationToken.ThrowIfCancellationRequested();

			await entitySet.AddAsync(entity, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation("CREATE {0} OF TYPE {1}", entity, typeof(TEntity));
		}

		public async Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken)
		{

			cancellationToken.ThrowIfCancellationRequested();

			var result = entitySet
					.AsNoTracking()
					.AsQueryable();
			_logger.LogInformation("GET ITEMS OF TYPE {0}", typeof(TEntity));
			return await Task.FromResult(result);
		}

		public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken)
		{

			cancellationToken.ThrowIfCancellationRequested();
			
			var result = entitySet
				.AsNoTracking()
				.Where(func)
				.AsQueryable();
			_logger.LogInformation("GET ITEMS OF TYPE {0} WHERE {1}", typeof(TEntity), func.Body);
			return await Task.FromResult(result);
		}

		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Update(entity);
			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation("UPDATE {0} OF TYPE {1}", entity, typeof(TEntity));
		}

		public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Remove(entity);
			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation("REMOVE {0} OF TYPE {1}", entity, typeof(TEntity));
		}

		public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
		{

			cancellationToken.ThrowIfCancellationRequested();

			// InMemory storage does not imply ExecuteUpdate and ExecuteDelete
			await entitySet.Where(predicate)
				.ExecuteDeleteAsync(cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			_logger.LogInformation("DELETE ITEMS OF TYPE {0} WHERE {1}", typeof(TEntity), predicate.Body);
		}

		public async Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, 
			params Expression<Func<TEntity, object>>[] includeExpressions)
		{

			cancellationToken.ThrowIfCancellationRequested();

			var result = await IncludeAsync(cancellationToken, includeExpressions);
			_logger.LogInformation("GET ITEMS OF TYPE {0} INCLUDE {1}", typeof(TEntity), includeExpressions);
			return result;
		}

		public async Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
		{

			cancellationToken.ThrowIfCancellationRequested();

			var query = await IncludeAsync(cancellationToken, includeProperties);
			var result = query.Where(predicate)
				.AsQueryable();
			_logger.LogInformation("GET ITEMS OF TYPE {0} WHERE {1} INCLUDE {2}", typeof(TEntity), predicate.Body, includeProperties);
			return result;
		}

		private async Task<IQueryable<TEntity>> IncludeAsync(CancellationToken cancellationToken, 
			params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			cancellationToken.ThrowIfCancellationRequested();

			IQueryable<TEntity> query = entitySet.AsNoTracking();
			var result = includeExpressions
				.Aggregate(query,
					(current, includeProperty) => current.Include(includeProperty));
			return await Task.FromResult(result);
		}
	}
}
