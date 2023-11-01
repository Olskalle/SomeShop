using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Authentication.Models;
using SomeShop.Authentication.Services;
using System.IdentityModel.Tokens.Jwt;

namespace SomeShop.Authentication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _service;
		private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService service, ILogger<AuthenticationController> logger)
        {
			_service = service;
			_logger = logger;
        }

		[HttpPost("register")]
		public async Task<IActionResult> Register(User user, CancellationToken cancellationToken)
		{
			await _service.AddUserAsync(user, cancellationToken);
			return Ok();
		}

		[HttpPost("login/email")]
		public async Task<IActionResult> LoginByEmail(string email, string password, CancellationToken cancellationToken)
		{
			var existingUser = await _service.GetUserByEmailAsync(email, cancellationToken);

			if (existingUser == null || existingUser.Password != password)
			{
				return BadRequest("Invalid login data");
			}

			var token = _service.GenerateToken(existingUser);

			return Ok(token);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(string login, string password, CancellationToken cancellationToken)
		{
			var existingUser = await _service.GetUserByLoginAsync(login, cancellationToken);

			if (existingUser == null || existingUser.Password != password)
			{
				return BadRequest("Invalid login data");
			}

			var token = _service.GenerateToken(existingUser);

			return Ok(token);
		}

		[HttpGet("test"), Authorize]
		public IActionResult Test()
		{
			return Ok();
		}
	}
}
