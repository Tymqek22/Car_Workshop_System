using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Car_Workshop_System.Infrastructure.Auth.Services
{
	public class JwtService
	{
		private readonly IConfiguration _config;

		public JwtService(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateToken(ApplicationUser user, IList<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role,r)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

			var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

			var tokenDestriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddHours(8),
				SigningCredentials = creds,
				Issuer = _config["Jwt:Issuer"],
				Audience = _config["Jwt:Audience"]
			};

			var tokenHandler = new JsonWebTokenHandler();

			return tokenHandler.CreateToken(tokenDestriptor);
		}
	}
}
