using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.Services
{
	public class TechnicianService : ITechnicianService
	{
		private readonly IRepository<WorkOrder> _workOrderRepository;
		private readonly IRepository<Note> _noteRepository;
		private readonly IIdentityService _identityService;

		public TechnicianService(
			IRepository<WorkOrder> workOrderRepository,
			IRepository<Note> noteRepository,
			IIdentityService identityService)
		{
			_workOrderRepository = workOrderRepository;
			_noteRepository = noteRepository;
			_identityService = identityService;
		}

		public async Task AddNote(Guid workOrderId,string text,string technicianId)
		{
			var workOrder = await _workOrderRepository.Get(wo => wo.Id == workOrderId,"Notes");

			if (workOrder is null)
				throw new Exception("Work order doesn't exist.");

			if (await _identityService.GetUserById(technicianId) is null)
				throw new Exception("Technician doesn't exist.");

			var note = new Note
			{
				Text = text,
				Date = DateTime.Now,
				WorkOrderId = workOrderId,
				TechnicianId = technicianId
			};

			workOrder.First().Notes.Add(note);

			await _workOrderRepository.SaveChangesAsync();
		}

		public async Task ChangeStatus(Guid workOrderId,Status status)
		{
			var workOrder = await _workOrderRepository.GetByIdAsync(workOrderId);

			if (workOrder is null)
				throw new Exception("Work order doesn't exist.");

			if (workOrder.Status == Status.Cancelled || (status - workOrder.Status != 1))
				throw new Exception("Status cannot be changed. Try to pick different status.");

			workOrder.Status = status;

			await _workOrderRepository.SaveChangesAsync();
		}
	}
}
