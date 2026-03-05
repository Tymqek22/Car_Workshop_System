using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;

namespace Car_Workshop_System.Application.Interfaces
{
	public interface IIdentityService
	{
		public Task<Result> Register(RegisterDto registerDto);
		public Task<string> Login(LoginDto loginDto);
		public Task<string> GetUserById(string id);
	}
}
