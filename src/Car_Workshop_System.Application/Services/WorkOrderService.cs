using Car_Workshop_System.Application.DTO;
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

		public async Task AcceptWorkOrder(AcceptWorkOrderRequest request)
		{
			var workOrder = new WorkOrder
			{
				Brand = request.Brand,
				Model = request.Model,
				Year = request.Year,
				IssueDescription = request.IssueDescription,
				CreatedAt = DateTime.Now,
				Status = Status.Created
			};

			await _workOrderRepository.AddAsync(workOrder);
			await _workOrderRepository.SaveChangesAsync();
		}

		public async Task AssignTechnician(AssignTechnicianRequest request)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

			if (workOrder is null)
				throw new Exception("Work order doesn't exists.");

			if (await _identityService.GetUserById(request.TechnicianId) is null)
				throw new Exception("Technician doesn't exists.");

			var technicianAssignment = new TechnicianAssignment
			{
				WorkOrderId = workOrder.Id,
				TechnicianId = request.TechnicianId
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
