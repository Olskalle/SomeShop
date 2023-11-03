namespace SomeShop.Authentication.Models
{
	public class AuthResult
	{
		public string? Token { get; set; }
		public bool Successful { get; set; } = false;
		public List<string> Errors { get; set; } = new();
	}
}
