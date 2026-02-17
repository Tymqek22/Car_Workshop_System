using Car_Workshop_System.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Workshop_System.Api.Controllers
{
	//TODO: refactor later

	[Route("api/[controller]")]
	[ApiController]
	public class WorkOrderController : ControllerBase
	{
		private readonly IWorkOrderService _workOrderService;

		public WorkOrderController(IWorkOrderService workOrderService)
		{
			_workOrderService = workOrderService;
		}

		[HttpPost("accept")]
		public async Task<IActionResult> Accept(
			string brand,
			int year,
			string? model = null,
			string? issueDestription = null)
		{
			await _workOrderService.AcceptWorkOrder(brand,year,model,issueDestription);

			return Ok("Work order accepted");
		}

		[HttpPut("cancel/{workOrderId}")]
		public async Task<IActionResult> Cancel(Guid workOrderId)
		{
			await _workOrderService.CancelWorkOrder(workOrderId);

			return Ok("Work order cancelled successfully.");
		}

		[HttpPost("assignTechnician/{workOrderId}/{technicianId}")]
		public async Task<IActionResult> AssignTechnician(Guid workOrderId, string technicianId)
		{
			await _workOrderService.AssignTechnician(workOrderId,technicianId);

			return Ok("Technician assigned succesfully.");
		}
	}
}
