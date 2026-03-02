using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IWorkOrderService
	{
		public Task<IEnumerable<WorkOrderDto>> GetAllWorkOrders();
		public Task<WorkOrderDto> GetWorkOrderDetails(Guid workOrderId);
		public Task<Result> AcceptWorkOrder(AcceptWorkOrderDto request);
		public Task<Result> CancelWorkOrder(Guid workOrderId);
		public Task<Result> AssignTechnician(AssignTechnicianDto request);
		public Task<Result> UpdateWorkOrder(UpdateWorkOrderDto request);
	}
}
