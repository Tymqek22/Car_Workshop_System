namespace Car_Workshop_System.Application.DTO
{
	public class AcceptWorkOrderDto
	{
		public string Brand { get; set; }
		public int Year { get; set; }
		public string? Model { get; set; }
		public string? IssueDescription { get; set; }
	}
}
