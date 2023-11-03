using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Authentication.Models;
using SomeShop.Authentication.Models.Dto;
using SomeShop.Authentication.Services;
using System.IdentityModel.Tokens.Jwt;

namespace SomeShop.Authentication.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _service;
		private readonly UserManager<User> _userManager;
		private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService service, 
			UserManager<User> userManager,
			ILogger<AuthenticationController> logger)
        {
			_service = service;
			_userManager = userManager;
			_logger = logger;
        }

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is not null) return BadRequest("User with this E-mail already exists");

			var newUser = new User() 
			{ 
				UserName = model.Name, 
				Email = model.Email 
			};

			var result = await _userManager.CreateAsync(newUser, model.Password);

			if (result is null || !result.Succeeded) return BadRequest("User was not created");

			

			return Ok();
		}

		[HttpPost("login/email")]
		public async Task<IActionResult> LoginByEmail(LoginModel model, CancellationToken cancellationToken)
		{
			var existingUser = await _userManager.FindByEmailAsync(model.Email);

			if (existingUser == null)
			{
				return BadRequest("Invalid login data");
			}

			var isPasswordCorrect = await _userManager.CheckPasswordAsync(existingUser, model.Password);
			if (!isPasswordCorrect) return BadRequest("Invalid login data");

			var result = await _service.GenerateToken(existingUser, cancellationToken);

			return Ok(result);
		}
		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshToken(RefreshTokenModel model, CancellationToken cancellationToken)
		{
			var result = await _service.VerifyToken(model, cancellationToken);

			if (result is null)
			{
				return BadRequest("Invalid token");
			}
			if (result.Errors.Any())
			{
				return BadRequest(result.Errors);
			}

			return Ok(result);
		}

		[HttpGet("test"), Authorize]
		public async Task<IActionResult> Test()
		{
			return await Task.FromResult(Ok());
		}
	}
}
