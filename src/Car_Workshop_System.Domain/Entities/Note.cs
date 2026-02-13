namespace Car_Workshop_System.Domain.Entities
{
	public class Note
	{
		public Guid Id { get; set; }
		public string? Text { get; set; }
		public DateTime Date { get; set; }

		public Guid? WorkOrderId { get; set; }
		public WorkOrder WorkOrder { get; set; }

		public Guid? TechnicianId { get; set; }
		public Technician Technician { get; set; }
	}
}
