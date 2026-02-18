using System.Linq.Expressions;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IRepository<T>
	{
		public Task<T> GetByIdAsync(object id);
		public Task<IEnumerable<T>> Get(Expression<Func<T,bool>> predicate,string includeProperties);
		public Task AddAsync(T entity);
		public Task UpdateAsync(T entity);
		public Task DeleteAsync(T entity);
		public Task SaveChangesAsync();
	}
}
