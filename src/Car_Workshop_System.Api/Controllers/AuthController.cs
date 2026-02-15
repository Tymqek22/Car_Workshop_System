using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Car_Workshop_System.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Car_Workshop_System.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly JwtService _jwtService;

		public AuthController(UserManager<ApplicationUser> userManager, JwtService jwtService)
		{
			_userManager = userManager;
			_jwtService = jwtService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(string firstName, string lastName, string login, string password)
		{
			var newUser = new ApplicationUser
			{
				FirstName = firstName,
				LastName = lastName,
				UserName = login,
				Email = login
			};

			var userCreated = await _userManager.CreateAsync(newUser,password);

			if (!userCreated.Succeeded)
				return BadRequest();

			var roleSigned = await _userManager.AddToRoleAsync(newUser,"Technician");

			if (!roleSigned.Succeeded)
				return BadRequest();

			return Ok(newUser);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(string login, string password)
		{
			var user = await _userManager.FindByNameAsync(login);

			if (user is null)
				return Unauthorized();

			var valid = await _userManager.CheckPasswordAsync(user,password);

			if (!valid)
				return Unauthorized();

			var roles = await _userManager.GetRolesAsync(user);
			var token = _jwtService.GenerateToken(user,roles);

			return Ok(new { token });
		}
	}
}
