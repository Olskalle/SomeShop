using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private IShopContext context;
		private DbSet<TEntity> entitySet;

        public GenericRepository(IShopContext context)
        {
			this.context = context;
			entitySet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
		{
			entitySet.Add(entity);
			context.SaveChanges();
		}

		public IEnumerable<TEntity> Get()
		{
			return entitySet
				.AsNoTracking()
				.ToList();
		}

		public IEnumerable<TEntity> Get(Func<TEntity, bool> func)
		{
			return entitySet
				.AsNoTracking()
				.Where(func)
				.ToList();
		}

		public void Remove(TEntity entity)
		{
			entitySet.Remove(entity); 
			context.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			entitySet.Update(entity); 
			context.SaveChanges();
		}
		public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			return Include(includeExpressions).ToList();
		}

		public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = Include(includeProperties);
			return query.Where(predicate).ToList();
		}

		private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			IQueryable<TEntity> query = entitySet.AsNoTracking();
			return includeExpressions
				.Aggregate(query, 
					(current, includeProperty) => current.Include(includeProperty));
		}

		public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			//if (cancellationToken.IsCancellationRequested)
			//{
			//	throw new OperationCanceledException();
			//}

			//try
			//{
			//	await entitySet.AddAsync(entity);
			//	context.SaveChanges();
			//}
			//catch (OperationCanceledException)
			//{
			//	throw;
			//}
			await RunWithCancellationHandling(cancellationToken, 
				async () =>
				{
					await entitySet.AddAsync(entity);
					context.SaveChanges();
				});
		}

		public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken)
		{
			return await RunWithCancellationHandling(cancellationToken,
				async () =>
				{
					return await entitySet
						.AsNoTracking()
						.ToListAsync();
				});
		}

		public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			throw new NotImplementedException();
		}

		private async Task<T> RunWithCancellationHandling<T>(CancellationToken cancellationToken, Func<Task<T>> body)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				 return await body();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
		private async Task RunWithCancellationHandling(CancellationToken cancellationToken, Func<Task> body)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				await body();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
	}
}
