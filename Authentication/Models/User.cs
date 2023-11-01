namespace SomeShop.Authentication.Models
{
	public class User
	{
		public long Id { get; set; }
		public string Login { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Email { get; set; } = null!;
		public bool IsEmailConfirmed { get; set; } = false;
		public Role Role { get; set; } = Role.Client;

		public override string ToString()
		{
			return $"{{ {Id}, {Email} }}";
		}
	}
}
