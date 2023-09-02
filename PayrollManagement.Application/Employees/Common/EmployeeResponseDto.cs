namespace PayrollManagement.Application.Employees.Common
{
    public record EmployeeResponseDto(
        Guid Id, 
        string FullName, 
        string Email, 
        string CitizenshipId,
        int Salary, 
        VacationResponse Vacation, 
        PensionResponse Pension, 
        HealthResponse Health, 
        BonusResponse Bonus, 
        string PhoneNumber, 
        AddressResponse Address, 
        EmploymentContractResponse EmploymentContract);
}
