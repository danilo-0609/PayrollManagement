namespace PayrollManagement.Domain.Employees
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(EmployeeId id);
        Task<bool> ExistsAsync(EmployeeId id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
