using Car_Workshop_System.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Workshop_System.Api.Controllers
{
	//TODO: refactor later, add authorization, pass dto as endpoint parameters

	[Route("api/workorders")]
	[ApiController]
	public class WorkOrderController : ControllerBase
	{
		private readonly IWorkOrderService _workOrderService;
		private readonly ITechnicianService _technicianService;

		public WorkOrderController(
			IWorkOrderService workOrderService,
			ITechnicianService technicianService)
		{
			_workOrderService = workOrderService;
			_technicianService = technicianService;
		}

		[HttpPost("workorders")]
		public async Task<IActionResult> Accept(
			string brand,
			int year,
			string? model = null,
			string? issueDescription = null)
		{
			await _workOrderService.AcceptWorkOrder(brand,year,model,issueDescription);

			return Ok("Work order accepted");
		}

		[HttpPatch("{workOrderId}/cancellation")]
		public async Task<IActionResult> Cancel(Guid workOrderId)
		{
			await _workOrderService.CancelWorkOrder(workOrderId);

			return Ok("Work order cancelled successfully.");
		}

		[HttpPost("{workOrderId}/technicians")]
		public async Task<IActionResult> AssignTechnician(Guid workOrderId,string technicianId)
		{
			await _workOrderService.AssignTechnician(workOrderId,technicianId);

			return Ok("Technician assigned succesfully.");
		}

		[HttpPost("{workOrderId}/notes")]
		public async Task<IActionResult> AddNote(Guid workOrderId,string text,string technicianId)
		{
			await _technicianService.AddNote(workOrderId,text,technicianId);

			return Ok("Note added successfully.");
		}

		[HttpPatch("{workOrderId}/status")]
		public async Task<IActionResult> ChangeStatus(Guid workOrderId,int status)
		{
			await _technicianService.ChangeStatus(workOrderId,status);

			return Ok("Status changed successfully.");
		}
	}
}
