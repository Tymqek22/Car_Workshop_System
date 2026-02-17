namespace Car_Workshop_System.Application.Interfaces
{
	public interface IRepository<T>
	{
		public Task<T> GetByIdAsync(int id);
		public Task AddAsync(T entity);
		public Task UpdateAsync(T entity);
		public Task DeleteAsync(T entity);
		public Task SaveChangesAsync();
	}
}
