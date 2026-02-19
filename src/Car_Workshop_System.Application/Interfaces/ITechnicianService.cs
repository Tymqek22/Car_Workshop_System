using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface ITechnicianService
	{
		public Task ChangeStatus(ChangeStatusDto request);
		public Task AddNote(AddNoteDto request);
	}
}
