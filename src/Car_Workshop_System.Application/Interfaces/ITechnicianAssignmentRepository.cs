using Car_Workshop_System.Domain.Entities;
using System.Linq.Expressions;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface ITechnicianAssignmentRepository : IRepository<TechnicianAssignment>
	{
		public Task DeleteManyAsync(Expression<Func<TechnicianAssignment,bool>> predicate);
	}
}
