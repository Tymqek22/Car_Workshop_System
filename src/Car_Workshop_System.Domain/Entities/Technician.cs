namespace Car_Workshop_System.Domain.Entities
{
	public class Technician
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Phone { get; set; }

		public ICollection<Note>? Notes { get; set; }
		public ICollection<TechnicianAssignment>? TechnicianAssignments { get; set; }
	}
}
