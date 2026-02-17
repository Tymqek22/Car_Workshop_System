using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.Services
{
	public class WorkOrderService : IWorkOrderService
	{
		//TODO: refactor to use dto as a parameters, rich domain model implementation, result pattern

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

		public async Task AssignTechnician(Guid workOrderId,string technicianId)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(workOrderId);

			if (workOrder is null)
				throw new Exception("Work order doesn't exists.");

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

		public async Task CancelWorkOrder(Guid workOrderId)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(workOrderId);

			if (workOrder is null)
				throw new Exception("Work order doesn't exists.");

			if (workOrder.Status == Status.Cancelled)
				throw new Exception("Work order is already cancelled.");

			workOrder.Status = Status.Cancelled;

			await _workOrderRepository.UpdateAsync(workOrder);
			await _workOrderRepository.SaveChangesAsync();
		}
	}
}
