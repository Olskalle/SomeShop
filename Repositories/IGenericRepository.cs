using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
		Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken);
		Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken);
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
		Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
		Task DeleteAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken);
		Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, 
			params Expression<Func<TEntity, object>>[] includeExpressions);
		Task<IQueryable<TEntity>> GetWithIncludeAsync(CancellationToken cancellationToken, 
			Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties);
	}
}
