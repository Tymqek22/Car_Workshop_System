using Car_Workshop_System.Api.Requests;
using Car_Workshop_System.Application.DTO;
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

		[HttpGet]
		public async Task<IActionResult> GetAllWorkOrders()
		{
			var workOrders = await _workOrderService.GetAllWorkOrders();

			return Ok(workOrders);
		}

		[HttpGet("{workOrderId}")]
		public async Task<IActionResult> GetWorkOrderDetails(Guid workOrderId)
		{
			var workOrder = await _workOrderService.GetWorkOrderDetails(workOrderId);

			return Ok(workOrder);
		}

		[HttpGet("technicians/{technicianId}")]
		public async Task<IActionResult> GetAllTechnicianWorkOrders(string technicianId)
		{
			var workOrders = await _technicianService.GetAllTechniciansWorkOrders(technicianId);

			return Ok(workOrders);
		}

		[HttpPost]
		public async Task<IActionResult> Accept([FromBody] AcceptWorkOrderDto request)
		{
			await _workOrderService.AcceptWorkOrder(request);

			return Ok("Work order accepted");
		}

		[HttpPatch("{workOrderId}/cancellation")]
		public async Task<IActionResult> Cancel(Guid workOrderId)
		{
			await _workOrderService.CancelWorkOrder(workOrderId);

			return Ok("Work order cancelled successfully.");
		}

		[HttpPost("{workOrderId}/technicians")]
		public async Task<IActionResult> AssignTechnician(Guid workOrderId,
			[FromBody] AssignTechnicianRequest request)
		{
			var dto = new AssignTechnicianDto
			{
				WorkOrderId = workOrderId,
				TechnicianId = request.TechnicianId
			};

			await _workOrderService.AssignTechnician(dto);

			return Ok("Technician assigned succesfully.");
		}

		[HttpPost("{workOrderId}/notes")]
		public async Task<IActionResult> AddNote(Guid workOrderId,
			[FromBody] AddNoteRequest request)
		{
			var dto = new AddNoteDto
			{
				WorkOrderId = workOrderId,
				Text = request.Text,
				TechnicianId = request.TechnicianId
			};

			await _technicianService.AddNote(dto);

			return Ok("Note added successfully.");
		}

		[HttpPatch("{workOrderId}/status")]
		public async Task<IActionResult> ChangeStatus(Guid workOrderId,
			[FromBody] ChangeStatusRequest request)
		{
			var dto = new ChangeStatusDto
			{
				WorkOrderId = workOrderId,
				Status = request.Status
			};

			await _technicianService.ChangeStatus(dto);

			return Ok("Status changed successfully.");
		}
	}
}
