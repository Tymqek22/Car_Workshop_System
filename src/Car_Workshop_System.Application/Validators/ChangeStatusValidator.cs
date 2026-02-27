using Car_Workshop_System.Application.DTO;
using FluentValidation;

namespace Car_Workshop_System.Application.Validators
{
	public class ChangeStatusValidator : AbstractValidator<ChangeStatusDto>
	{
		public ChangeStatusValidator()
		{
			RuleFor(x => x.WorkOrderId)
				.NotEmpty()
				.WithMessage("WorkOrderId is required");

			RuleFor(x => x.Status)
				.NotEmpty()
				.InclusiveBetween(0,4)
				.WithMessage("Status must be between 0 and 4");
		}
	}
}
