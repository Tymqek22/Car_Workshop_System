namespace Car_Workshop_System.Application.DTO
{
	public class ChangeStatusRequest
	{
		public Guid WorkOrderId { get; set; }
		public int Status { get; set; }
	}
}
