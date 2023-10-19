using System.Linq.Expressions;

namespace SomeShop.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		void Create(TEntity entity);
		IEnumerable<TEntity> Get();
		IEnumerable<TEntity> Get(Func<TEntity, bool> func);
		IEnumerable<TEntity> GetPage(int pageNumber, int pageSize);
		void Update(TEntity entity);
		void Remove(TEntity entity);
		IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeExpressions);
		IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties);
	}
}
