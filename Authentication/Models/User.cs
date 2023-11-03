using Microsoft.AspNetCore.Identity;

namespace SomeShop.Authentication.Models
{
	public class User : IdentityUser
	{
		public override string ToString()
		{
			return $"{{ {Id}, {Email} }}";
		}
	}
}
