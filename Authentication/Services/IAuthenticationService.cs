using SomeShop.Authentication.Models;

namespace SomeShop.Authentication.Services
{
	public interface IAuthenticationService
	{
		public string GenerateToken(User user);
		public Task AddUserAsync(User user, string password, CancellationToken cancellationToken);
		public Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
		//public Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
		Task<bool> ValidateUserPassword(User user, string password);
	}
}
