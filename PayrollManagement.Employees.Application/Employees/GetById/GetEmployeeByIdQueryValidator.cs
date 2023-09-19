using FluentValidation;

namespace PayrollManagement.Application.Employees.GetById
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("The Id of the Employee is required");
        }
    }
}
