using PayrollManagement.Domain.EmployeeErrors;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.Primitives;

namespace PayrollManagement.Application.Employees.Delete
{
    public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ErrorOr<Unit>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employeeId = new EmployeeId(command.Id);

            if ( await _employeeRepository.GetEmployeeByIdAsync(employeeId) is not Employee employee )
            {
                return Errors.Employee.EmployeeNotFound;
            }

            _employeeRepository.DeleteEmployee(employee);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
