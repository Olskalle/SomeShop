using Microsoft.EntityFrameworkCore;
using SomeShop.Authentication.Models;

namespace SomeShop.Authentication.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IAuthenticationContext _context;
		private readonly ILogger<IUserRepository>? _logger;

        public UserRepository(IAuthenticationContext context, ILogger<IUserRepository> logger)
        {
			_context = context;
			_logger = logger;
        }
        public async Task AddUserAsync(User user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _context.Users.AddAsync(user, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			_logger?.LogInformation("[AUTH] NEW USER ADDED: {0}", user);
		}

		public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

			if (result is null) throw new NullReferenceException("No such user!");
			_logger?.LogInformation("[AUTH] GET USER BY E-MAIL: {0}", email);

			return result;
		}

		public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _context.Users.FirstOrDefaultAsync(x => x.Login == login, cancellationToken);

			if (result is null) throw new NullReferenceException("No such user!");
			_logger?.LogInformation("[AUTH] GET USER BY : {0}", login);

			return result;
		}
	}
}
