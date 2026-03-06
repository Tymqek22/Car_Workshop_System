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

		private readonly IWorkOrderRepository _workOrderRepository;
		private readonly IIdentityService _identityService;
		private readonly IValidatorResolver _validatorResolver;

		public WorkOrderService(
			IWorkOrderRepository workOrderRepository,
			IIdentityService identityService,
			IValidatorResolver validatorResolver)
		{
			_workOrderRepository = workOrderRepository;
			_identityService = identityService;
			_validatorResolver = validatorResolver;
		}

		public async Task<Result> AcceptWorkOrder(AcceptWorkOrderDto request)
		{
			var validator = _validatorResolver.Get<AcceptWorkOrderDto>();
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid) {

				var errors = validationResult.Errors
					.Select(x => new Error(x.ErrorCode,x.ErrorMessage))
					.ToList();

				return Result.Failure(errors);
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
					.Select(x => new Error(x.ErrorCode,x.ErrorMessage))
					.ToList();

				return Result.Failure(errors);
			}

			var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.WorkOrderId);

			if (workOrder is null)
				return Result.Failure(new List<Error> { WorkOrderErrors.OrderNotFound });

			if (await _identityService.GetUserById(request.TechnicianId) is null)
				return Result.Failure(new List<Error> { WorkOrderErrors.TechnicianNotFound });

			var technicianAssignment = new TechnicianAssignment
			{
				WorkOrderId = workOrder.Id,
				TechnicianId = request.TechnicianId
			};

			if (await _workOrderRepository.IsTechnicianAssigned(workOrder.Id,request.TechnicianId))
				return Result.Failure(new List<Error> { WorkOrderErrors.TechnicianAlreadyAssigned });

			workOrder.TechnicianAssignments.Add(technicianAssignment);

			await _workOrderRepository.SaveChangesAsync();

			return Result.Success();
		}

		public async Task<Result> CancelWorkOrder(Guid workOrderId)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(workOrderId);

			if (workOrder is null)
				return Result.Failure(new List<Error> { WorkOrderErrors.OrderNotFound });

			if (workOrder.Status == Status.Cancelled)
				return Result.Failure(new List<Error> { WorkOrderErrors.AlreadyCancelled });

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

		public async Task<Result> UpdateWorkOrder(UpdateWorkOrderDto request)
		{
			var validator = _validatorResolver.Get<UpdateWorkOrderDto>();
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid) {

				var errors = validationResult.Errors
					.Select(e => new Error(e.ErrorCode,e.ErrorMessage))
					.ToList();

				return Result.Failure(errors);
			}

			var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.Id);

			if (workOrder is null)
				return Result.Failure(new List<Error> { WorkOrderErrors.OrderNotFound });

			workOrder.Brand = request.Brand;
			workOrder.Model = request.Model;
			workOrder.Year = request.Year;
			workOrder.IssueDescription = request.IssueDescription;

			var newTechIds = request.TechnicianIds;

			var currentTechIds = workOrder.TechnicianAssignments
				.Select(ta => ta.TechnicianId)
				.ToList();

			var techsToRemove = workOrder.TechnicianAssignments
				.Where(ta => !newTechIds.Contains(ta.TechnicianId))
				.ToList();

			foreach (var t in techsToRemove) {

				workOrder.TechnicianAssignments.Remove(t);
			}

			var techsToAdd = newTechIds
				.Where(t => !currentTechIds.Contains(t))
				.ToList();

			foreach (var t in techsToAdd) {

				workOrder.TechnicianAssignments.Add(new TechnicianAssignment
				{
					WorkOrderId = workOrder.Id,
					TechnicianId = t
				});
			}

			await _workOrderRepository.SaveChangesAsync();

			return Result.Success();
		}
	}
}
