namespace Car_Workshop_System.Application.DTO
{
	public class AssignTechnicianRequest
	{
		public Guid WorkOrderId { get; set; }
		public string TechnicianId { get; set; }
	}
}
