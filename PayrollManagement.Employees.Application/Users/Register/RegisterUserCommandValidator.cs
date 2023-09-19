using FluentValidation;
using PayrollManagement.Domain.Users;

namespace PayrollManagement.Application.Users.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IUserRepository _userRepository)
        {
            RuleFor(rule => rule.UserName)
                .NotEmpty().WithMessage("The username can't be empty.")
                .MinimumLength(6).WithMessage("The username length must be at least 6 words length")
                .MaximumLength(30).WithMessage("The username length must be shorter than 30 words");

            RuleFor(rule => rule.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must be shorter than 255 words length")
                .MinimumLength(15).WithMessage("Email must be at least 15 words length")
                .MustAsync(async (email, _) =>
                {
                    bool isUnique = await _userRepository.UserEmailExistsAsync(email);

                    return isUnique;

                }).WithMessage("The email must be unique.");

            RuleFor(rule => rule.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password length must be at least 8 words length")
                .MaximumLength(30).WithMessage("The password length must be shorter than 30 words");
        }
    }
}
