namespace PayrollManagement.Application.Employees.Common
{
    public record AddressResponse(
        string Country, 
        string City, 
        string Neighborhood, 
        string HouseAddress); 
}
