using PayrollManagement.Application.Employees.Common;
using PayrollManagement.Domain.EmployeeErrors;
using PayrollManagement.Domain.Employees;

namespace PayrollManagement.Application.Employees.GetById
{
    public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, ErrorOr<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<EmployeeResponseDto>> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var employeeMatch = await _employeeRepository.GetEmployeeByIdAsync(new EmployeeId(query.Id));

            if (employeeMatch is not Employee employee)
            {
                return Errors.Employee.EmployeeNotFound;
            }

            return new EmployeeResponseDto(
                employeeMatch.Id.Value,
                employeeMatch.Name,
                employeeMatch.LastName,
                employeeMatch.CitizenshipId,
                employeeMatch.Salary,
                new VacationResponse(employeeMatch.Vacations.NextVacations),
                new PensionResponse(employeeMatch.Pension.TotalPensionContribution, employeeMatch.Pension.MustContributeToPension),
                new HealthResponse(employeeMatch.Health.TotalHealthContribution, employeeMatch.Health.MustContributeToHealth),
                new BonusResponse(employeeMatch.Bonus.SalaryBonus),
                employeeMatch.PhoneNumber.ToString(),
                new AddressResponse(employeeMatch.Address.Country, employeeMatch.Address.City, employeeMatch.Address.Neighborhood, employeeMatch.Address.HouseAddress),
                new EmploymentContractResponse(employeeMatch.EmploymentContract.EmploymentContractType, employeeMatch.EmploymentContract.JoiningDate, employeeMatch.EmploymentContract.EndedContractTime)
                );
        }
    }
}
