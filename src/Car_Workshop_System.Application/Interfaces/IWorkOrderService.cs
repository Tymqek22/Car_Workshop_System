using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Domain.Entities;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IWorkOrderService
	{
		public Task<IEnumerable<WorkOrderDto>> GetAllWorkOrders();
		public Task<WorkOrderDto> GetWorkOrderDetails(Guid workOrderId);
		public Task AcceptWorkOrder(AcceptWorkOrderDto request);
		public Task CancelWorkOrder(Guid workOrderId);
		public Task AssignTechnician(AssignTechnicianDto request);
	}
}
