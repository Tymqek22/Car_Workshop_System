using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Car_Workshop_System.Infrastructure.Auth.Identity.DbInitializer
{
	public static class DbInitializer
	{
		public static async Task InitializeDb(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string[] roles = { "Owner","Technician" };

			foreach (var role in roles) {

				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}
}
