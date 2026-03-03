using Car_Workshop_System.Application.DTO;
using FluentValidation;

namespace Car_Workshop_System.Application.Validators
{
	public class UpdateWorkOrderValidator : AbstractValidator<UpdateWorkOrderDto>
	{
		public UpdateWorkOrderValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.WithMessage("Id is required.");

			RuleFor(x => x.Brand)
				.NotEmpty()
				.WithMessage("Brand is required.");

			RuleFor(x => x.Year)
				.NotEmpty()
				.InclusiveBetween(1950,DateTime.UtcNow.Year)
				.WithMessage($"Year must be between 1950 and {DateTime.UtcNow.Year}");
		}
	}
}
