using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Car_Workshop_System.Infrastructure.Auth.Identity.DbInitializer
{
	public static class DbInitializer
	{
		public static async Task InitializeDb(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string[] roles = { "Owner","Technician" };
			string userName = "Admin";
			string password = "Admin123!";

			foreach (var role in roles) {

				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
			}

			var user = await userManager.FindByNameAsync(userName);

			if (user is null) {

				user = new ApplicationUser
				{
					UserName = userName,
					FirstName = "Admin",
					LastName = "Admin"
				};

				await userManager.CreateAsync(user,password);
			}

			if (!await userManager.IsInRoleAsync(user,roles[0])) {

				await userManager.AddToRoleAsync(user,roles[0]);
			}
		}
	}
}
