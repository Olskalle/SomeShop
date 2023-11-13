namespace SomeShop.Authentication.Models.Dto
{
	public class RefreshTokenModel
	{
		public string Token { get; set; } = null!;
		public string RefreshToken { get; set; } = null!;
	}
}
