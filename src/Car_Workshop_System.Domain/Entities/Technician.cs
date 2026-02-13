namespace Car_Workshop_System.Domain.Entities
{
	public class Technician
	{
		public Guid Id { get; set; }
		public string? Specialization { get; set; }

		public string UserId { get; set; }

		public ICollection<Note>? Notes { get; set; }
		public ICollection<TechnicianAssignment>? TechnicianAssignments { get; set; }
	}
}
