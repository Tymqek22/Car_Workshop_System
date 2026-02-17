using Car_Workshop_System.Domain.Entities;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IWorkOrderService
	{
		public Task AcceptWorkOrder(string brand,int year, string? model = null, string? issueDestription = null);
		public Task CancelWorkOrder(Guid workOrderId);
		public Task AssignTechnician(Guid workOrderId,string technicianId);
	}
}
