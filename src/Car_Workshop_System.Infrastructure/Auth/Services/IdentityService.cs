using Car_Workshop_System.Application.Common;
using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Domain.Errors;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Car_Workshop_System.Infrastructure.Auth.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly JwtService _jwtService;

		public IdentityService(UserManager<ApplicationUser> userManager,JwtService jwtService)
		{
			_userManager = userManager;
			_jwtService = jwtService;
		}

		public async Task<Result<string>> GetUserById(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user is null)
				return Result<string>.Failure(new List<Error> { AuthErrors.UserNotFound });

			return Result<string>.Success(user.Id);
		}

		public async Task<Result<string>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByNameAsync(loginDto.Login);

			if (user is null)
				return Result<string>.Failure(new List<Error> { AuthErrors.UserNotFound });

			var valid = await _userManager.CheckPasswordAsync(user,loginDto.Password);

			if (!valid)
				return Result<string>.Failure(new List<Error> { AuthErrors.WrongPassword });

			var roles = await _userManager.GetRolesAsync(user);
			var token = _jwtService.GenerateToken(user,roles);

			return Result<string>.Success(token);
		}

		public async Task<Result> Register(RegisterDto registerDto)
		{
			var newUser = new ApplicationUser
			{
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
				UserName = registerDto.FirstName,
				Email = registerDto.Email
			};

			var userCreated = await _userManager.CreateAsync(newUser,registerDto.Password);

			if (!userCreated.Succeeded) {

				var errors = userCreated.Errors
					.Select(x => new Error(x.Code,x.Description))
					.ToList();

				return Result.Failure(errors);
			}

			var roleSigned = await _userManager.AddToRoleAsync(newUser,"Technician");

			if (!roleSigned.Succeeded)
				return Result.Failure(new List<Error> { AuthErrors.RoleAssignmentError });

			return Result.Success();
		}
	}
}
