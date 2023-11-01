using Microsoft.IdentityModel.Tokens;
using SomeShop.Authentication.Models;
using SomeShop.Authentication.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SomeShop.Authentication.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IConfiguration _configuration;
		private readonly IUserRepository _repository;
		private readonly ILogger<AuthenticationService>? _logger;

        public AuthenticationService(IConfiguration configuration, 
			IUserRepository repository, 
			ILogger<AuthenticationService>? logger)
        {
			_repository = repository;
			_logger = logger;
			_configuration = configuration;
        }

		public string GenerateToken(User user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };

			var token = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task AddUserAsync(User user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.AddUserAsync(user, cancellationToken);
			_logger?.LogInformation("NEW USER ADDED");
		}

		public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetUserByEmailAsync(email, cancellationToken);
			_logger?.LogInformation("GET USER BY EMAIL");
			return result;
		}

		public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetUserByLoginAsync(login, cancellationToken);
			_logger?.LogInformation("GET USER BY LOGIN");
			return result;
		}
	}

	public interface IAuthenticationService
	{
		public string GenerateToken(User user);
		public Task AddUserAsync(User user, CancellationToken cancellationToken);
		public Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
		public Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
	}
}
