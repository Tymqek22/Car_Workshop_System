using Car_Workshop_System.Api.Requests;
using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

		[Authorize(Roles = "Owner")]
		[HttpGet]
		public async Task<IActionResult> GetAllWorkOrders()
		{
			var workOrders = await _workOrderService.GetAllWorkOrders();

			return Ok(workOrders);
		}

		[Authorize(Roles = "Owner, Technician")]
		[HttpGet("{workOrderId}")]
		public async Task<IActionResult> GetWorkOrderDetails(Guid workOrderId)
		{
			var workOrder = await _workOrderService.GetWorkOrderDetails(workOrderId);

			if (workOrder is null)
				return NotFound();

			return Ok(workOrder);
		}

		[Authorize(Roles = "Technician")]
		[HttpGet("technicians/{technicianId}")]
		public async Task<IActionResult> GetAllTechnicianWorkOrders(string technicianId)
		{
			var workOrders = await _technicianService.GetAllTechniciansWorkOrders(technicianId);

			if (workOrders is null)
				return NotFound();

			return Ok(workOrders);
		}

		[Authorize(Roles = "Owner")]
		[HttpPost]
		public async Task<IActionResult> AcceptWorkOrder([FromBody] AcceptWorkOrderDto request)
		{
			var result = await _workOrderService.AcceptWorkOrder(request);

			if (!result.IsSuccess)
				return NotFound(result.Error);

			return NoContent();
		}

		[Authorize(Roles = "Owner")]
		[HttpPut("{workOrderId}/cancellation")]
		public async Task<IActionResult> CancelWorkOrder(Guid workOrderId)
		{
			var result = await _workOrderService.CancelWorkOrder(workOrderId);

			if (!result.IsSuccess)
				return NotFound(result.Error);

			return NoContent();
		}

		[Authorize(Roles = "Owner")]
		[HttpPost("{workOrderId}/technicians")]
		public async Task<IActionResult> AssignTechnician(Guid workOrderId,
			[FromBody] AssignTechnicianRequest request)
		{
			var dto = new AssignTechnicianDto
			{
				WorkOrderId = workOrderId,
				TechnicianId = request.TechnicianId
			};

			var result = await _workOrderService.AssignTechnician(dto);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return NoContent();
		}

		[Authorize(Roles = "Technician")]
		[HttpPost("{workOrderId}/notes")]
		public async Task<IActionResult> AddNote(Guid workOrderId,
			[FromBody] AddNoteRequest request)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var dto = new AddNoteDto
			{
				WorkOrderId = workOrderId,
				Text = request.Text,
				TechnicianId = userId
			};

			var result = await _technicianService.AddNote(dto);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return NoContent();
		}

		[Authorize(Roles = "Technician")]
		[HttpPut("{workOrderId}/status")]
		public async Task<IActionResult> ChangeStatus(Guid workOrderId,
			[FromBody] ChangeStatusRequest request)
		{
			var dto = new ChangeStatusDto
			{
				WorkOrderId = workOrderId,
				Status = request.Status
			};

			var result = await _technicianService.ChangeStatus(dto);

			if (!result.IsSuccess)
				return BadRequest(result.Error);

			return NoContent();
		}
	}
}
