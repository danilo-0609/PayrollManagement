using PayrollManagement.Application.Employees.Common;

namespace PayrollManagement.Application.Employees.GetAll
{
    public record GetAllEmployeeQuery() : IRequest<ErrorOr<IReadOnlyList<EmployeeResponseDto>>>;
}
