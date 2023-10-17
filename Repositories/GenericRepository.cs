using Microsoft.EntityFrameworkCore;

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
	}
}
