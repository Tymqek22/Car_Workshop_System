using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Car_Workshop_System.Infrastructure.Repository
{
	public class TechnicianAssignmentRepository : Repository<TechnicianAssignment>, ITechnicianAssignmentRepository
	{
		public TechnicianAssignmentRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task DeleteManyAsync(Expression<Func<TechnicianAssignment,bool>> predicate)
		{
			var toRemove = await _dbSet
				.Where(predicate)
				.ToListAsync();

			_dbSet.RemoveRange(toRemove);
		}
	}
}
