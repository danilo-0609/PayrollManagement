using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Application.Employees.Create;
using PayrollManagement.Application.Employees.Delete;
using PayrollManagement.Application.Employees.GetAll;
using PayrollManagement.Application.Employees.GetById;
using PayrollManagement.Application.Employees.Update;
using PayrollManagement.Domain.EmployeeErrors;
using System.Runtime.CompilerServices;

namespace PayrollManagement.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class EmployeeController : ApiController
    {
        private readonly ISender _mediator;

        public EmployeeController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employeeResult = await _mediator.Send(new GetAllEmployeeQuery());

            return employeeResult.Match(
                employees => Ok(employees),
                errors => Problem(errors)
                );
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(id));

            return employeeResult.Match(
                employee => Ok(employee),
                errors => Problem(errors)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var createEmployeeResult = await _mediator.Send(command);

            return createEmployeeResult.Match(
                employeeId => Created(nameof(employeeId), employeeId),
                errors => Problem(errors)
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    Error.Validation("Employee.UpdateInvalid", "The request Id does not match with the URl Id")
                };

                return Problem(errors);
            }

            var updateEmployeeResult = await _mediator.Send(command);

            return updateEmployeeResult.Match(
                employeeUpdate => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var deleteEmployeeResult = await _mediator.Send(new DeleteEmployeeCommand(id));

            return deleteEmployeeResult.Match(
                deleteEmployee => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}
