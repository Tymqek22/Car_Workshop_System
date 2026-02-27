using Car_Workshop_System.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Car_Workshop_System.Application.Validators
{
	public class ValidatorResolver : IValidatorResolver
	{
		private readonly IServiceProvider _serviceProvider;

		public ValidatorResolver(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IValidator<T> Get<T>()
		{
			return _serviceProvider.GetRequiredService<IValidator<T>>();
		}
	}
}
