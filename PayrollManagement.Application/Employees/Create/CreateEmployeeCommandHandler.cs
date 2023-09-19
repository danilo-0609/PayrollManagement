using PayrollManagement.Domain.EmployeeErrors;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Application.Employees.Create
{
    public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ErrorOr<Guid>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Guid>> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            {
                return Errors.Employee.PhoneNumberWithBadFormat;
            }  

            if (Address.Create(command.Country, command.City, command.Neighborhood, command.HouseAddress) 
                is not Address address)
            {
                return Errors.Employee.AddressWithBadFormat;
            }

            if (command.Salary == 0)
            {
                return Errors.Employee.SalaryEqualsToCeroError;
            }

            if (Email.Create(command.Email) is not Email email)
            {
                return Errors.Employee.EmailWithBadFormat;
            }

            var employmentContractCreate = EmploymentContract.Create(command.EmploymentContractType, command.ContractTime);
            var addressCreate = Address.Create(command.Country, command.City, command.Neighborhood, command.HouseAddress);
            var phoneNumberCreate = PhoneNumber.Create(command.PhoneNumber);
                var emailCreate = Email.Create(command.Email);


            if (phoneNumberCreate is null )
            {
                return Errors.Employee.PhoneNumberValueNull;
            }

            if (addressCreate is null)
            {
                return Errors.Employee.AddressValueNull;
            }

            if (employmentContractCreate is null)
            {
                return Errors.Employee.EmploymentContractValueNull;
            }

            if (emailCreate is null)
            {
                return Errors.Employee.EmailWithBadFormat;
            }


            var employee = new Employee(
                new EmployeeId(Guid.NewGuid()),
                command.Name,
                command.LastName,
                command.CitizenshipId,
                command.Salary,
                emailCreate,
                Vacations.Create(),
                Pension.CreatePensionStatement(command.Salary),
                Health.Create(command.Salary),
                Bonus.Create(command.Salary),
                phoneNumberCreate,
                addressCreate,
                employmentContractCreate
                );

             _employeeRepository.AddEmployee(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return employee.Id.Value;

        }
    }
}