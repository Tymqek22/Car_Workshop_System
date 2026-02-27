using Car_Workshop_System.Application.DTO;
using FluentValidation;

namespace Car_Workshop_System.Application.Validators
{
	public class AssignTechnicianValidator : AbstractValidator<AssignTechnicianDto>
	{
		public AssignTechnicianValidator()
		{
			RuleFor(x => x.WorkOrderId)
				.NotEmpty()
				.WithMessage("WorkOrderId is required");

			RuleFor(x => x.TechnicianId)
				.NotEmpty()
				.WithMessage("TechnicianId is required");
		}
	}
}
