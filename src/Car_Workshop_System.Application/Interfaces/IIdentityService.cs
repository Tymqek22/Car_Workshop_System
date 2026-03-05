using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IIdentityService
	{
		public Task<Result> Register(RegisterDto registerDto);
		public Task<Result<string>> Login(LoginDto loginDto);
		public Task<Result<string>> GetUserById(string id);
	}
}
