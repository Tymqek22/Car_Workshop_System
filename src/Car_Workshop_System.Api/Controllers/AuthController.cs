using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
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
		//to refactor leater
		private readonly IIdentityService _identityService;

		public AuthController(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto request)
		{
			var result = await _identityService.Register(request);

			if (!result.IsSuccess)
				return BadRequest();

			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto request)
		{
			var result = await _identityService.Login(request);

			if (result == string.Empty)
				return Unauthorized();

			return Ok(result);
		}
	}
}
