namespace Car_Workshop_System.Application.DTO
{
	public class UpdateWorkOrderDto
	{
		public Guid Id { get; set; }
		public string Brand { get; set; }
		public string? Model { get; set; }
		public int Year { get; set; }
		public string? IssueDescription { get; set; }
		public List<string>? TechnicianIds { get; set; }
	}
}
