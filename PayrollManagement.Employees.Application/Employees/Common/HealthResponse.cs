namespace PayrollManagement.Application.Employees.Common
{
    public record HealthResponse(
        decimal TotalHealthContribution, 
        bool MustContributeToHealth);
}
