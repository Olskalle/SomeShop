using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SomeShop.Authentication.Models;

namespace SomeShop.Authentication
{
	public class AuthenticationContext : IdentityDbContext<User>
	{
		private readonly IConfiguration _configuration;
		public AuthenticationContext(IConfiguration configuration, DbContextOptions<AuthenticationContext> options)
			: base(options)
		{
			_configuration = configuration;
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("InMemory");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//modelBuilder.Entity<User>(u =>
			//{
			//	u.HasData(new User()
			//	{
			//		Id=1,
			//		Login = "Olskalle",
			//		Password = "12345",
			//		Email = "forwhomthebelltolls@mail.com"
			//	});
			//});
		}

		
	}
}
