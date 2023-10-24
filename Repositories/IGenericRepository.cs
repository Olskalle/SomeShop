using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		async Task Create(TEntity entity);
		IQueryable<TEntity>> Get();
		IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> expression);

		async Task Update(TEntity entity);
		async Task Remove(TEntity entity);
		IQueryable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeExpressions);
		IQueryable<TEntity>> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties);

		Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
		Task<async Task<IEnumerable<TEntity>>> GetAsync(CancellationToken cancellationToken);
		Task<async Task<IEnumerable<TEntity>>> GetAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken);
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
		Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
	}
}
