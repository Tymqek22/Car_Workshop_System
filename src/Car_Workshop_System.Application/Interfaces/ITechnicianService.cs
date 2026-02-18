using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface ITechnicianService
	{
		public Task ChangeStatus(Guid workOrderId,Status status);
		public Task AddNote(Guid workOrderId,string text,string technicianId);
	}
}
