using PayrollManagement.Application.Employees.Common;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.ValueObjects;
using System.Runtime.CompilerServices;

namespace PayrollManagement.Application.Employees.GetAll
{
    public sealed class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeeQuery, ErrorOr<IReadOnlyList<EmployeeResponseDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<IReadOnlyList<EmployeeResponseDto>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();

            return employees.Select(employee => new EmployeeResponseDto(
                employee.Id.Value,
                employee.Name,
                employee.LastName,
                employee.CitizenshipId,
                employee.Salary,
                new VacationResponse(employee.Vacations.NextVacations),
                new PensionResponse(employee.Pension.TotalPensionContribution, employee.Pension.MustContributeToPension),
                new HealthResponse(employee.Health.TotalHealthContribution, employee.Health.MustContributeToHealth),
                new BonusResponse(employee.Bonus.SalaryBonus),
                employee.PhoneNumber.ToString(),
                new AddressResponse(employee.Address.Country, employee.Address.City, employee.Address.Neighborhood, employee.Address.HouseAddress),
                new EmploymentContractResponse(employee.EmploymentContract.EmploymentContractType, employee.EmploymentContract.JoiningDate, employee.EmploymentContract.EndedContractTime)
                )).ToList();
        }
    }
}
