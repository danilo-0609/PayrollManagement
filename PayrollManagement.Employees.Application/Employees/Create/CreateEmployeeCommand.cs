namespace PayrollManagement.Application.Employees.Create
{
    public record CreateEmployeeCommand(
        string Name, 
        string LastName,
        string CitizenshipId,
        string Email, 
        int Salary,
        string PhoneNumber,
        string Country,
        string City,
        string Neighborhood,
        string HouseAddress,
        string EmploymentContractType,
        int ContractTime
        ) : IRequest<ErrorOr<Guid>>;
}
