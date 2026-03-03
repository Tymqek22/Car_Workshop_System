namespace Car_Workshop_System.Api.Requests
{
	public class UpdateWorkOrderRequest
	{
		public string Brand { get; set; }
		public string? Model { get; set; }
		public int Year { get; set; }
		public string? IssueDescription { get; set; }
		public List<string>? TechnicianIds { get; set; }
	}
}
