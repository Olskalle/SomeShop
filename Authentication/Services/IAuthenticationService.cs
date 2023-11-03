using SomeShop.Authentication.Models;

namespace SomeShop.Authentication.Services
{
	public interface IAuthenticationService
	{
		public Task<string> GenerateToken(User user, CancellationToken cancellationToken);
	}
}
