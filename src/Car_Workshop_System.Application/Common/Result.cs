using Car_Workshop_System.Domain.Errors;

namespace Car_Workshop_System.Application.Common
{
	public class Result
	{
		public bool IsSuccess { get; }
		public Error? Error { get; }

		public Result(bool success, Error? error)
		{
			IsSuccess = success;
			Error = error;
		}

		public static Result Success() => new(true,null);
		public static Result Failure(Error error) => new(false,error);
	}
}
