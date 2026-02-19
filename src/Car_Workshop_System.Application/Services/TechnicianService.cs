using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.Services
{
	public class TechnicianService : ITechnicianService
	{
		private readonly IRepository<WorkOrder> _workOrderRepository;
		private readonly IIdentityService _identityService;

		public TechnicianService(
			IRepository<WorkOrder> workOrderRepository,
			IRepository<Note> noteRepository,
			IIdentityService identityService)
		{
			_workOrderRepository = workOrderRepository;
			_identityService = identityService;
		}

		public async Task AddNote(AddNoteDto request)
		{
			var workOrder = await _workOrderRepository.Get(wo => wo.Id == request.WorkOrderId,"Notes");

			if (workOrder is null)
				throw new Exception("Work order doesn't exist.");

			if (await _identityService.GetUserById(request.TechnicianId) is null)
				throw new Exception("Technician doesn't exist.");

			var note = new Note
			{
				Text = request.Text,
				Date = DateTime.Now,
				WorkOrderId = request.WorkOrderId,
				TechnicianId = request.TechnicianId
			};

			workOrder.First().Notes.Add(note);

			await _workOrderRepository.SaveChangesAsync();
		}

		public async Task ChangeStatus(ChangeStatusDto request)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

			if (workOrder is null)
				throw new Exception("Work order doesn't exist.");

			if (workOrder.Status == Status.Cancelled || ((Status)request.Status - workOrder.Status != 1))
				throw new Exception("Status cannot be changed. Try to pick different status.");

			workOrder.Status = (Status)request.Status;

			await _workOrderRepository.SaveChangesAsync();
		}
	}
}
