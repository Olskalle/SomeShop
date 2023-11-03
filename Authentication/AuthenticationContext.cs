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
			SeedUsers();
		}

		private void SeedUsers()
		{
			var admin = new User()
			{
				UserName = "5uper4dmin",
				Email = "admin@supe.ru",
				EmailConfirmed = true
			};
			this.Users.Add(admin);
		}
	}
}
