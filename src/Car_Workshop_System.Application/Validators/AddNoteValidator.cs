using Car_Workshop_System.Application.DTO;
using FluentValidation;

namespace Car_Workshop_System.Application.Validators
{
	public class AddNoteValidator : AbstractValidator<AddNoteDto>
	{
		public AddNoteValidator()
		{
			RuleFor(x => x.WorkOrderId)
				.NotEmpty()
				.WithMessage("WorkOrderId is required");

			RuleFor(x => x.Text)
				.NotEmpty()
				.WithMessage("Text is required");

			RuleFor(x => x.TechnicianId)
				.NotEmpty()
				.WithMessage("TechnicianId is required");
		}
	}
}
