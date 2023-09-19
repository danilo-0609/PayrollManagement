using FluentValidation;

namespace PayrollManagement.Application.Users.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
