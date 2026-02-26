using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface ITechnicianService
	{
		public Task<Result> ChangeStatus(ChangeStatusDto request);
		public Task<Result> AddNote(AddNoteDto request);
		public Task<IEnumerable<WorkOrderDto>> GetAllTechniciansWorkOrders(string technicianId);
	}
}
