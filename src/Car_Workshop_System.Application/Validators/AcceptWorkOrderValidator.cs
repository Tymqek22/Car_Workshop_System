using Car_Workshop_System.Application.DTO;
using FluentValidation;

namespace Car_Workshop_System.Application.Validators
{
	public class AcceptWorkOrderValidator : AbstractValidator<AcceptWorkOrderDto>
	{
		public AcceptWorkOrderValidator()
		{
			RuleFor(x => x.Brand)
				.NotEmpty()
				.WithMessage("Brand is required");

			RuleFor(x => x.Year)
				.NotEmpty()
				.InclusiveBetween(1900,DateTime.UtcNow.Year)
				.WithMessage($"Year must be between 1900 and {DateTime.UtcNow.Year}");
		}
	}
}
