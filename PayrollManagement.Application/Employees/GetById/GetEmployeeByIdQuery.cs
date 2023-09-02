using PayrollManagement.Application.Employees.Common;

namespace PayrollManagement.Application.Employees.GetById
{
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<ErrorOr<EmployeeResponseDto>>;
}
