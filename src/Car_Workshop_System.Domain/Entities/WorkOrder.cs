using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Domain.Entities
{
	public class WorkOrder
	{
		public Guid Id { get; set; }
		public string Brand { get; set; }
		public string? Model { get; set; }
		public int Year { get; set; }
		public string? IssueDescription { get; set; }
		public DateTime CreatedAt { get; set; }
		public Status Status { get; set; }
		
		public ICollection<Note>? Notes { get; set; }
		public ICollection<TechnicianAssignment>? TechnicianAssignments { get; set; }
	}
}
