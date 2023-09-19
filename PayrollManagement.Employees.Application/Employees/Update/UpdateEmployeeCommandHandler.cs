using Microsoft.EntityFrameworkCore;
using PayrollManagement.Application.Data;
using PayrollManagement.Application.Employees.Common;
using PayrollManagement.Domain.EmployeeErrors;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Application.Employees.Update
{
    public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ErrorOr<Unit>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationDbContext _dbContext;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IApplicationDbContext dbContext)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employeeId = new EmployeeId(command.Id);

            var existsEmployee = await _employeeRepository.ExistsAsync(employeeId);

            if (!existsEmployee)
            {
                return Error.NotFound("Employees.NotFound", "The employee with the provide Id was not found");
            }

            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            {
                return Error.Validation("Employees.PhoneNumber", "Phone number has not valid format.");
            }

            if (Address.Create(command.Country, command.City, command.Neighborhood, command.HouseAddress) is not Address address)
            {
                return Error.Validation("Employee.Address", "Address has not valid format.");
            }

            if (Email.Create(command.Email) is not Email email)
            {
                return Error.Validation("Employee.Email", "Email has not valid format.");
            }

            if (Pension.CreatePensionStatement(command.Salary) is not Pension pension)
            {
                return Error.Validation("Employee.Pension", "Salary for pension has not valid format");
            }

            if (Health.Create(command.Salary) is not Health health)
            {
                return Error.Validation("Employee.Health", "Salary for health has not valid format");
            }

            if (Bonus.Create(command.Salary) is not Bonus bonus)
            {
                return Error.Validation("Employee.Bonus", "Salary for bonus has not valid format");
            }

            #nullable disable
            var employeeFound = await _dbContext.Employees.FindAsync(employeeId);
            var joiningDate = employeeFound.EmploymentContract.JoiningDate;
            var endedContractTime = employeeFound.EmploymentContract.EndedContractTime;

            if (EmploymentContract.Create(command.EmploymentContractType, command.ContractTime, joiningDate, endedContractTime) is not EmploymentContract employmentContract)
            {
                return Error.Validation("Employee.EmploymentContract", "Employment contract has not valid format.");
            }

            var nextVacations = employeeFound.Vacations.NextVacations; 

            if (Vacations.Create(nextVacations) is not Vacations vacations)
            {
                return Error.Unexpected("EmployeeUpdate.Vacation", "Error with the vacation unexpected");
            }


            var employee = Employee.UpdateEmployee(
                command.Id,
                command.Name,
                command.LastName,
                command.CitizenshipId,
                command.Salary,
                email,
                vacations,
                pension,
                health,
                bonus,
                phoneNumber,
                address,
                employmentContract
                );

            _employeeRepository.UpdateEmployee(employee);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}


