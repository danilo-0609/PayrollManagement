using FluentValidation;
namespace PayrollManagement.Application.Employees.Delete
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
