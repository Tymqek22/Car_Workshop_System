namespace Car_Workshop_System.Domain.Entities
{
	public class TechnicianAssignment
	{
		public Guid WorkOrderId { get; set; }
		public WorkOrder WorkOrder { get; set; }

		public Guid TechnicianId { get; set; }
		public Technician Technician { get; set; }
	}
}
