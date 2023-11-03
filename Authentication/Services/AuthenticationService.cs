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
		private readonly UserManager<User> _userManager;
		//private readonly RoleManager<User> _roleManager;
		private readonly ILogger<AuthenticationService>? _logger;

        public AuthenticationService(IConfiguration configuration,
			UserManager<User> userManager, 
			//RoleManager<User> roleManager, 
			ILogger<AuthenticationService>? logger)
        {
			_userManager = userManager;
			//_roleManager = roleManager;
			_logger = logger;
			_configuration = configuration;
        }

		public string GenerateToken(User user)
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

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task AddUserAsync(User user, string password, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _userManager.CreateAsync(user, password);
			_logger?.LogInformation("NEW USER ADDED");
		}

		public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _userManager.FindByEmailAsync(email);
			_logger?.LogInformation("GET USER BY EMAIL");
			return result;
		}

		//public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
		//{
		//	cancellationToken.ThrowIfCancellationRequested();

		//	var result = await _userManager.FindByLoginAsync(login);
		//	_logger?.LogInformation("GET USER BY LOGIN");
		//	return result;
		//}

		public async Task<bool> ValidateUserPassword(User user, string password)
		{
			var checkResult = await _userManager.CheckPasswordAsync(user, password);
			return checkResult;
		}
	}
}
