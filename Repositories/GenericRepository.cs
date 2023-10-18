using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private DbContext context;
		private DbSet<TEntity> entitySet;

        public GenericRepository(DbContext context)
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

		public IEnumerable<TEntity> GetPage(int pageNumber, int pageSize)
		{
			return entitySet
				.AsNoTracking()
				.Skip((pageNumber - 1) * pageSize).Take(pageSize);
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
	}
}
