namespace SomeShop.Authentication.Models.Dto
{
    public class AuthResult
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool Succeeded { get; set; } = false;
        public List<string> Errors { get; set; } = new();
    }
}
