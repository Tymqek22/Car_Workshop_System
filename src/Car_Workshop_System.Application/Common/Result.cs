using Car_Workshop_System.Domain.Errors;

namespace Car_Workshop_System.Application.Common
{
	public class Result
	{
		public bool IsSuccess { get; }
		public List<Error>? Errors { get; }

		protected Result(bool success, List<Error>? errors)
		{
			IsSuccess = success;
			Errors = errors;
		}

		public static Result Success() => new(true,null);
		public static Result Failure(List<Error>? errors) => new(false,errors);
	}

	public class Result<T> : Result
	{
		public T? Value { get; }

		public Result(bool success, List<Error>? errors, T? value) : base(success,errors)
		{
			Value = value;
		}

		public static Result<T> Success(T value) => new(true,null,value);
		public static new Result<T> Failure(List<Error>? errors) => new(false,errors,default);
	}
}
