using Microsoft.EntityFrameworkCore;
using SomeShop.Authentication.Models;

namespace SomeShop.Authentication
{
	public class AuthenticationContext : DbContext, IAuthenticationContext
	{
		private readonly IConfiguration _configuration;
        public AuthenticationContext(IConfiguration configuration)
        {
			_configuration = configuration;
			Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("InMemory");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(u =>
			{
				u.HasKey(x => x.Id);
				u.Property(x => x.Id).ValueGeneratedOnAdd();
				u.Property(x => x.Login).IsRequired();
				u.Property(x => x.Password).IsRequired();
				u.Property(x => x.Email).IsRequired();

				u.HasIndex(x => x.Login).IsUnique();
				u.HasIndex(x => x.Email).IsUnique();

				u.HasData(new User()
				{
					Id=1,
					Login = "Olskalle",
					Password = "12345",
					Email = "forwhomthebelltolls@mail.com"
				});
			});
		}

		
	}
}
