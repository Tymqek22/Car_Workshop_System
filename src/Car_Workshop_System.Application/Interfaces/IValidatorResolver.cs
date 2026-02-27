using FluentValidation;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IValidatorResolver
	{
		IValidator<T> Get<T>();
	}
}
