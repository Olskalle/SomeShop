using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SomeShop.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SomeShop.Authentication.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthenticationService>? _logger;

        public AuthenticationService(IConfiguration configuration,
			UserManager<User> userManager,
			ILogger<AuthenticationService>? logger)
        {
			_logger = logger;
			_configuration = configuration;
        }

		public async Task<string> GenerateToken(User user, CancellationToken cancellationToken)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim> 
			{
				new Claim("Id", user.Id),
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
				signingCredentials: credentials
			);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			return await Task.FromResult(tokenString);
		}
	}
}
