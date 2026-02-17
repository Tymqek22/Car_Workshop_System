using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.Services
{
	public class WorkOrderService : IWorkOrderService
	{
		private readonly IRepository<WorkOrder> _workOrderRepository;
		private readonly IRepository<TechnicianAssignment> _technicianAssignmentRepository;
		private readonly IIdentityService _identityService;

		public WorkOrderService(
			IRepository<WorkOrder> workOrderRepository,
			IRepository<TechnicianAssignment> technicianAssignmentRepository,
			IIdentityService identityService)
		{
			_workOrderRepository = workOrderRepository;
			_technicianAssignmentRepository = technicianAssignmentRepository;
			_identityService = identityService;
		}

		public async Task AcceptWorkOrder(
			string brand,
			int year,
			string? model = null,
			string? issueDestription = null)
		{
			var workOrder = new WorkOrder
			{
				Brand = brand,
				Model = model,
				Year = year,
				IssueDescription = issueDestription,
				CreatedAt = DateTime.Now,
				Status = Domain.Enums.Status.Created
			};

			await _workOrderRepository.AddAsync(workOrder);
			await _workOrderRepository.SaveChangesAsync();
		}

		public async Task AssignTechnician(WorkOrder workOrder,string technicianId)
		{
			if (await _identityService.GetUserById(technicianId) is null)
				throw new Exception("Technician doesn't exists.");

			var technicianAssignment = new TechnicianAssignment
			{
				WorkOrderId = workOrder.Id,
				TechnicianId = technicianId
			};

			await _technicianAssignmentRepository.AddAsync(technicianAssignment);
			await _technicianAssignmentRepository.SaveChangesAsync();
		}

		public async Task CancelWorkOrder(WorkOrder workOrder)
		{
			if (workOrder.Status == Status.Cancelled)
				throw new Exception("Work order is already cancelled.");

			workOrder.Status = Status.Cancelled;

			await _workOrderRepository.UpdateAsync(workOrder);
			await _workOrderRepository.SaveChangesAsync();
		}
	}
}
