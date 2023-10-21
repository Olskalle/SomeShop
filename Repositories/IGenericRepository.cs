using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		void Create(TEntity entity);
		IEnumerable<TEntity> Get();
		IEnumerable<TEntity> Get(Func<TEntity, bool> func);
		void Update(TEntity entity);
		void Remove(TEntity entity);
		IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeExpressions);
		IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties);

		Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> func, CancellationToken cancellationToken);
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
		Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeExpressions);
		Task<IEnumerable<TEntity>> GetWithIncludeAsync(Func<TEntity, bool> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties);
	}
}
