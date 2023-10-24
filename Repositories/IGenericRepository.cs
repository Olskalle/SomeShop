using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		void Create(TEntity entity);
		IQueryable<TEntity> Get();
		IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

		void Update(TEntity entity);
		void Remove(TEntity entity);
		IQueryable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeExpressions);
		IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties);

		Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken);
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
		Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
		Task DeleteAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken);
	}
}
