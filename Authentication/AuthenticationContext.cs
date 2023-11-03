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

		public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("InMemory");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<RefreshToken>(t =>
			{
				t.Property("UserId").IsRequired();
				t.Property("Token").IsRequired();
				t.Property("JwtId").IsRequired();
			});
		}
	}
}
