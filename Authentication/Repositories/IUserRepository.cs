using SomeShop.Authentication.Models;

namespace SomeShop.Authentication.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
		Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
		Task AddUserAsync(User user, CancellationToken cancellationToken);
	}
}
