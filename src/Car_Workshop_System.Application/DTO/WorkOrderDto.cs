using Car_Workshop_System.Domain.Enums;

namespace Car_Workshop_System.Application.DTO
{
	public class WorkOrderDto
	{
		public Guid Id { get; set; }
		public string Brand { get; set; }
		public string? Model { get; set; }
		public int Year { get; set; }
		public string? IssueDescription { get; set; }
		public DateTime CreatedAt { get; set; }
		public Status Status { get; set; }
	}
}
