using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Car_Workshop_System.Infrastructure.Auth.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public IdentityService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<string> GetUserById(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user is null)
				return null;

			return user.Id;
		}
	}
}
