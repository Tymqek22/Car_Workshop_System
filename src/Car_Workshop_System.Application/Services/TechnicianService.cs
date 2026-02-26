using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Application.Mappings;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Domain.Enums;
using Car_Workshop_System.Domain.Errors;

namespace Car_Workshop_System.Application.Services
{
	public class TechnicianService : ITechnicianService
	{
		private readonly IWorkOrderRepository _workOrderRepository;
		private readonly IIdentityService _identityService;

		public TechnicianService(
			IWorkOrderRepository workOrderRepository,
			IIdentityService identityService)
		{
			_workOrderRepository = workOrderRepository;
			_identityService = identityService;
		}

		public async Task<Result> AddNote(AddNoteDto request)
		{
			var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.WorkOrderId);

			if (workOrder is null)
				return Result.Failure(WorkOrderErrors.OrderNotFound);

			if (await _identityService.GetUserById(request.TechnicianId) is null)
				return Result.Failure(WorkOrderErrors.TechnicianNotFound);

			var note = new Note
			{
				Text = request.Text,
				Date = DateTime.Now,
				WorkOrderId = request.WorkOrderId,
				TechnicianId = request.TechnicianId
			};

			workOrder.Notes.Add(note);

			await _workOrderRepository.SaveChangesAsync();

			return Result.Success();
		}

		public async Task<Result> ChangeStatus(ChangeStatusDto request)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

			if (workOrder is null)
				return Result.Failure(WorkOrderErrors.OrderNotFound);

			if (workOrder.Status == Status.Cancelled || ((Status)request.Status - workOrder.Status != 1))
				return Result.Failure(WorkOrderErrors.WrongStatusPicked);

			workOrder.Status = (Status)request.Status;

			await _workOrderRepository.SaveChangesAsync();

			return Result.Success();
		}

		public async Task<IEnumerable<WorkOrderDto>> GetAllTechniciansWorkOrders(string technicianId)
		{
			var techniciansOrders = await _workOrderRepository.GetAllTechnicianWorkOrders(technicianId);

			return techniciansOrders.Select(to => to.MapToDto());
		}
	}
}
