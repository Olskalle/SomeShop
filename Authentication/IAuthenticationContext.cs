using Microsoft.EntityFrameworkCore;
using SomeShop.Authentication.Models;

namespace SomeShop.Authentication
{
	public interface IAuthenticationContext
	{
		DbSet<User> Users { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken); 
	}
}
