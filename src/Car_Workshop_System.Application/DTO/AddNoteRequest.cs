namespace Car_Workshop_System.Application.DTO
{
	public class AddNoteRequest
	{
		public Guid WorkOrderId { get; set; }
		public string Text { get; set; }
		public string TechnicianId { get; set; }
	}
}
