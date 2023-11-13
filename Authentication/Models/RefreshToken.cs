using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeShop.Authentication.Models
{
	public class RefreshToken
	{
		public int Id { get; set; }
		public string UserId { get; set; } = null!;
		public string Token { get; set; } = null!;
		public string JwtId { get; set; } = null!;
		public bool IsUsed { get; set; } = false;
		public bool IsRevoked { get; set; } = false;
		public DateTime AddedDate { get; set; } = DateTime.UtcNow;
		public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);

		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }
	}
}
