using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Car_Workshop_System.Infrastructure.Repository
{
	public class Repository<T> : IRepository<T>
		where T : class
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task<IEnumerable<T>> Get(Func<T,bool> predicate = null,string includeProperties = "")
		{
			var query = _dbSet.Where(predicate).AsQueryable();


			foreach (var includeProperty in includeProperties.Split(
				new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {

				query = query.Include(includeProperty);
			}

			return await query.ToListAsync();
		}

		public async Task<T> GetByIdAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
		}
	}
}
