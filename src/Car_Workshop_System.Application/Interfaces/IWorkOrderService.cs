using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Domain.Entities;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IWorkOrderService
	{
		public Task AcceptWorkOrder(AcceptWorkOrderRequest request);
		public Task CancelWorkOrder(Guid workOrderId);
		public Task AssignTechnician(AssignTechnicianRequest request);
	}
}
