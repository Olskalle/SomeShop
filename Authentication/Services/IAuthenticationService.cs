using SomeShop.Authentication.Models;
using SomeShop.Authentication.Models.Dto;

namespace SomeShop.Authentication.Services
{
	public interface IAuthenticationService
	{
		public Task<AuthResult> GenerateToken(User user, CancellationToken cancellationToken);
		Task<AuthResult> VerifyToken(RefreshTokenModel model, CancellationToken cancellationToken);
	}
}
