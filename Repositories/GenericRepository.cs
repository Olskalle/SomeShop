using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SomeShop.Extensions;
using Microsoft.EntityFrameworkCore.Query;
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
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: CREATE", entity);

			cancellationToken.ThrowIfCancellationRequested();

			await entitySet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken)
		{
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: GET");

			cancellationToken.ThrowIfCancellationRequested();

			return await Task.Run( () => 
				entitySet
					.AsNoTracking()
					.AsQueryable()
			);
		}

		public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken)
		{
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: GET ({func})");

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
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: UPDATE ({entity})");
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: REMOVE ({entity})");
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken)
		{
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: DELETE ({predicate})");

			cancellationToken.ThrowIfCancellationRequested();

			// FIX:  The provider for the source 'IQueryable' doesn't implement 'IAsyncQueryProvider'.
			//		 Only providers that implement 'IAsyncQueryProvider' can be used
			//		 for Entity Framework asynchronous operations.
			await entitySet.Where(predicate)
				.AsQueryable()
				.ExecuteDeleteAsync(cancellationToken);
			await _context.SaveChangesAsync();
		}

		public async Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: GET INCLUDE ({includeExpressions})");

			cancellationToken.ThrowIfCancellationRequested();

			return await IncludeAsync(includeExpressions);
		}

		public async Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
		{
			_logger.Log(LogLevel.Information, $"EntitySet<{typeof(TEntity)}>: GET INCLUDE ({predicate}, {includeProperties})");

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
