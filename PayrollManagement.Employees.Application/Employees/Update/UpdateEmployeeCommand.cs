namespace PayrollManagement.Application.Employees.Update
{
   public record UpdateEmployeeCommand(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string CitizenshipId,
        int Salary,
        string PhoneNumber,
        string Country,
        string City,
        string Neighborhood,
        string HouseAddress,
        string EmploymentContractType,
        int ContractTime
       ) : IRequest<ErrorOr<Unit>>;
}
