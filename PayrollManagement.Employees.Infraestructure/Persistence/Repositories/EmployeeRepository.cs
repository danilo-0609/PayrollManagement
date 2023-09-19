using Microsoft.EntityFrameworkCore;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Infraestructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async void AddEmployee(Employee employee) => await _dbContext.Employees.AddAsync(employee);

        public void DeleteEmployee(Employee employee) => _dbContext.Employees.Remove(employee);

        public async Task<bool> ExistsAsync(EmployeeId id) => await _dbContext.Employees.AnyAsync(a => a.Id == id);

        public async Task<List<Employee>> GetAllEmployeesAsync() => await _dbContext.Employees.ToListAsync();

        public async Task<Employee?> GetEmployeeByIdAsync(EmployeeId id) => await _dbContext.Employees.SingleOrDefaultAsync(a => a.Id == id);

        public void UpdateEmployee(Employee employee) => _dbContext.Employees.Update(employee);

        public async Task<bool> IsEmailUniqueAsync(string email) => !await _dbContext.Employees.AnyAsync(c => c.Email == Email.Create(email));
    }
}
