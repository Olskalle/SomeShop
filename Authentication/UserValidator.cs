using FluentValidation;
using SomeShop.Authentication.Models;

namespace SomeShop.Authentication
{
	public class UserValidator : AbstractValidator<User>
	{
        public UserValidator()
        {
			RuleFor(user => user.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email address.");

			RuleFor(user => user.Password)
				.NotEmpty().WithMessage("Password is required.")
				.Length(8, 20).WithMessage("Password must be between 8 and 20 characters.");

			RuleFor(user => user.Login)
				.NotEmpty().WithMessage("Login is required.")
				.Length(10, 16).WithMessage("Login must be between 10 and 16 characters.");
		}
	}
}
