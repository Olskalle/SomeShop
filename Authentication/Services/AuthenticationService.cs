using Microsoft.IdentityModel.Tokens;
using SomeShop.Authentication.Models;
using SomeShop.Authentication.Models.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SomeShop.Authentication.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IConfiguration _configuration;
		private readonly AuthenticationContext _context;
		private readonly UserManager<User> _userManager;
		private readonly TokenValidationParameters _validationParameters;
		private readonly ILogger<AuthenticationService>? _logger;

		public AuthenticationService(IConfiguration configuration,
			AuthenticationContext context,
			UserManager<User> userManager,
			TokenValidationParameters validationParameters,
			ILogger<AuthenticationService>? logger)
		{
			_configuration = configuration;
			_validationParameters = validationParameters;
			_context = context;
			_userManager = userManager;
			_logger = logger;
		}

		public async Task<AuthResult> GenerateToken(User user, CancellationToken cancellationToken)
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
				expires: DateTime.UtcNow.AddMinutes(1),
				signingCredentials: credentials
			);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			var refreshToken = CreateRefreshToken(token, user);

			await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return new AuthResult()
			{
				Token = tokenString,
				RefreshToken = refreshToken.Token,
				Succeeded = true
			};
		}

		public async Task<AuthResult> VerifyToken(RefreshTokenModel model, CancellationToken cancellationToken)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var token = jwtTokenHandler.ValidateToken(model.Token, _validationParameters, out var validatedToken);

			if (validatedToken is JwtSecurityToken)
			{
				var jwtSecurityToken = validatedToken as JwtSecurityToken;

				var result = jwtSecurityToken!.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

				if (result == false)
				{
					var authResult = new AuthResult();
					authResult.Errors.Add("Invalid security algorithm");
					return authResult;
				}
			}

			// Get expiry timestamp
			var expiryClaim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp);
			var utcExpiryDate = (expiryClaim is not null)
				? long.Parse(expiryClaim.Value)
				: default;

			var expDate = UnixTimeStampToDateTime(utcExpiryDate);

			if (expDate > DateTime.UtcNow)
			{
				return new AuthResult()
				{
					Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
					Succeeded = false
				};
			}

			var storedRefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);

			if (storedRefreshToken == null)
			{
				return new AuthResult()
				{
					Errors = new List<string>() { "Refresh token does not exist" },
					Succeeded = false
				};
			}

			if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
			{
				return new AuthResult()
				{
					Errors = new List<string>() { "Token has been expired" },
					Succeeded = false
				};
			}

			if (storedRefreshToken.IsUsed)
			{
				return new AuthResult()
				{
					Errors = new List<string>() { "Token has been used" },
					Succeeded = false
				};
			}

			if (storedRefreshToken.IsRevoked)
			{
				return new AuthResult()
				{
					Errors = new List<string>() { "Token has been revoked" },
					Succeeded = false
				};
			}

			var jti = token.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

			// check the id that the recieved token has against the id saved in the db
			if (storedRefreshToken.JwtId != jti)
			{
				return new AuthResult()
				{
					Errors = new List<string>() { "Token doenst match saved token" },
					Succeeded = false
				};
			}

			storedRefreshToken.IsUsed = true;
			_context.RefreshTokens.Update(storedRefreshToken);
			await _context.SaveChangesAsync();

			var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
			return await GenerateToken(dbUser, cancellationToken);
		}

		private RefreshToken CreateRefreshToken(JwtSecurityToken token, User user)
		{
			return new RefreshToken()
			{
				JwtId = token.Id,
				IsUsed = false,
				UserId = user.Id,
				Token = RandomString(100) + Guid.NewGuid()
			};
		}

		private string RandomString(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}
	}
}
