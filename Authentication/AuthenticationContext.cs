using Microsoft.EntityFrameworkCore;
using SomeShop.Authentication.Models;

namespace SomeShop.Authentication
{
	public class AuthenticationContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
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
			});
		}
	}
}
