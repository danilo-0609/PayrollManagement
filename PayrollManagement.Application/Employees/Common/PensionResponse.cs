namespace PayrollManagement.Application.Employees.Common
{
    public record PensionResponse(
        decimal TotalPensionContribution, 
        bool MustContributeToPension);
}
