using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SomeShop.Extensions;


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

		public IQueryable<TEntity> Get()
		{
			return entitySet
				.AsNoTracking()
				.AsQueryable();
		}

		public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> func)
		{
			return entitySet
				.AsNoTracking()
				.Where(func)
				.AsQueryable();
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
		public IQueryable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeExpressions)
		{
			return Include(includeExpressions).AsQueryable();
		}

		public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = Include(includeProperties);
			return query.Where(predicate)
				.AsQueryable();
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
			cancellationToken.ThrowIfCancellationRequested();

			await entitySet.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await entitySet
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await this.Get(func)
				.ToListAsync();
		}

		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Update(entity);
			await context.SaveChangesAsync();
		}

		public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			entitySet.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await entitySet.Where(predicate)
				.AsQueryable()
				.ExecuteDeleteAsync(cancellationToken);
			context.SaveChanges();
		}
	}
}
