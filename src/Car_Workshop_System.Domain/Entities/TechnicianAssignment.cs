namespace Car_Workshop_System.Domain.Entities
{
	public class TechnicianAssignment
	{
		public Guid WorkOrderId { get; set; }
		public WorkOrder WorkOrder { get; set; }

		public string TechnicianId { get; set; }
	}
}
