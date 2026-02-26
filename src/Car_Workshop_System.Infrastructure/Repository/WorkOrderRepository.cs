using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Car_Workshop_System.Infrastructure.Repository
{
	public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
	{
		public WorkOrderRepository(ApplicationDbContext dbContext) : base(dbContext) {}

		public async Task<IEnumerable<WorkOrder>> GetAllTechnicianWorkOrders(string technicianId)
		{
			return await _dbSet
				.Include(wo => wo.TechnicianAssignments)
				.Where(wo => wo.TechnicianAssignments
					.Any(ta => ta.TechnicianId == technicianId))
				.ToListAsync();
		}

		public async Task<WorkOrder> GetWithDetailsAsync(Guid id)
		{
			return await _dbSet
				.Include(wo => wo.Notes)
				.Include(wo => wo.TechnicianAssignments)
				.Where(wo => wo.Id == id)
				.SingleAsync();
		}

		public async Task<bool> IsTechnicianAssigned(Guid workOrderId,string technicianId)
		{
			return await _dbContext.TechnicianAssignments
				.AnyAsync(ta => ta.WorkOrderId == workOrderId &&
								ta.TechnicianId == technicianId);
				
		}
	}
}
