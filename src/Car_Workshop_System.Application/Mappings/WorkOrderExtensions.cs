using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Domain.Entities;

namespace Car_Workshop_System.Application.Mappings
{
	public static class WorkOrderExtensions
	{
		public static WorkOrderDto MapToDto(this WorkOrder workOrder)
		{
			var dto = new WorkOrderDto
			{
				Id = workOrder.Id,
				Brand = workOrder.Brand,
				Model = workOrder.Model,
				Year = workOrder.Year,
				IssueDescription = workOrder.IssueDescription,
				CreatedAt = workOrder.CreatedAt,
				Status = workOrder.Status
			};

			return dto;
		}
	}
}
