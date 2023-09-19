using FluentValidation;
using PayrollManagement.Users.Domain.Users;

namespace PayrollManagement.Users.Application.Users.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(rule => rule.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must be shorter than 255 words length")
                .MinimumLength(15).WithMessage("Email must be at least 15 words length");

            RuleFor(rule => rule.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password length must be at least 8 words length")
                .MaximumLength(30).WithMessage("The password length must be shorter than 30 words");
        }
    }
}
