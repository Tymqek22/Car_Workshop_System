using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Application.Mappings;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Domain.Enums;
using Car_Workshop_System.Domain.Errors;
using FluentValidation;

namespace Car_Workshop_System.Application.Services
{
	public class WorkOrderService : IWorkOrderService
	{
		//TODO: rich domain model implementation

		private readonly IWorkOrderRepository _workOrderRepository;
		private readonly IRepository<TechnicianAssignment> _technicianAssignmentRepository;
		private readonly IIdentityService _identityService;
		private readonly IValidatorResolver _validatorResolver;

		public WorkOrderService(
			IWorkOrderRepository workOrderRepository,
			IRepository<TechnicianAssignment> technicianAssignmentRepository,
			IIdentityService identityService,
			IValidatorResolver validatorResolver)
		{
			_workOrderRepository = workOrderRepository;
			_technicianAssignmentRepository = technicianAssignmentRepository;
			_identityService = identityService;
			_validatorResolver = validatorResolver;
		}

		public async Task<Result> AcceptWorkOrder(AcceptWorkOrderDto request)
		{
			var validator = _validatorResolver.Get<AcceptWorkOrderDto>();
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid) {

				var errors = validationResult.Errors
					.Select(x => new Error(x.ErrorCode,x.ErrorMessage));

				return Result.Failure(errors.First());
			}

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

			return Result.Success();
		}

		public async Task<Result> AssignTechnician(AssignTechnicianDto request)
		{
			var validator = _validatorResolver.Get<AssignTechnicianDto>();
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid) {

				var errors = validationResult.Errors
					.Select(x => new Error(x.ErrorCode,x.ErrorMessage));

				return Result.Failure(errors.First());
			}

			var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

			if (workOrder is null)
				return Result.Failure(WorkOrderErrors.OrderNotFound);

			if (await _identityService.GetUserById(request.TechnicianId) is null)
				return Result.Failure(WorkOrderErrors.TechnicianNotFound);

			var technicianAssignment = new TechnicianAssignment
			{
				WorkOrderId = workOrder.Id,
				TechnicianId = request.TechnicianId
			};

			if (await _workOrderRepository.IsTechnicianAssigned(workOrder.Id,request.TechnicianId))
				return Result.Failure(WorkOrderErrors.TechnicianAlreadyAssigned);

			await _technicianAssignmentRepository.AddAsync(technicianAssignment);
			await _technicianAssignmentRepository.SaveChangesAsync();

			return Result.Success();
		}

		public async Task<Result> CancelWorkOrder(Guid workOrderId)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(workOrderId);

			if (workOrder is null)
				return Result.Failure(WorkOrderErrors.OrderNotFound);

			if (workOrder.Status == Status.Cancelled)
				return Result.Failure(WorkOrderErrors.AlreadyCancelled);

			workOrder.Status = Status.Cancelled;

			await _workOrderRepository.UpdateAsync(workOrder);
			await _workOrderRepository.SaveChangesAsync();

			return Result.Success();
		}

		public async Task<IEnumerable<WorkOrderDto>> GetAllWorkOrders()
		{
			var workOrders = await _workOrderRepository.Get(null,"");

			return workOrders.Select(wo => wo.MapToDto());
		}

		public async Task<WorkOrderDto> GetWorkOrderDetails(Guid workOrderId)
		{
			var workOrder = await _workOrderRepository.GetWithDetailsAsync(workOrderId);

			return workOrder.MapToDto();
		}
	}
}
