namespace PayrollManagement.Application.Employees.Delete
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
