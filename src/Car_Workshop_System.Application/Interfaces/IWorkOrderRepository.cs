using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Domain.Entities;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IWorkOrderRepository : IRepository<WorkOrder>
	{
		public Task<IEnumerable<WorkOrder>> GetAllTechnicianWorkOrders(string technicianId);
	}
}
