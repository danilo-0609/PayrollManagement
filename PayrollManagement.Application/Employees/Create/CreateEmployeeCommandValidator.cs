using FluentValidation;
using FluentValidation.Validators;
using PayrollManagement.Domain.Employees;
using System.Collections.Generic;

namespace PayrollManagement.Application.Employees.Create
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator(IEmployeeRepository _employeeRepository)
        {
            RuleFor(d => d.Name)
               .NotEmpty().WithMessage("Name is required")
               .MaximumLength(50).WithMessage("Name must be shorter than 50 words length")
               .MinimumLength(3).WithMessage("Name must be at least 3 words length");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Name must be shorter than 50 words length")
                .MinimumLength(3).WithMessage("Last name must be at least 3 words length");

            RuleFor(d => d.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Length(10).WithMessage("Phone number must be 10 digits length")
                .Matches(@"^3[0-9]{9}").WithMessage("Invalid phone number format")
                .WithName("Phone number");

            RuleFor(d => d.Salary)
                .GreaterThan(800000).WithMessage("Salary must be greater than 800.000 pesos");

            RuleFor(d => d.Country)
                .NotEmpty().WithMessage("Country is required")
                .MinimumLength(3).WithMessage("Country must be at least 4 words length")
                .MaximumLength(60).WithMessage("Country must be shorter than 60 words length");

            RuleFor(d => d.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(60).WithMessage("City must be shorter than 60 words length")
                .MinimumLength(10).WithMessage("City must be at least 10 words length");

            RuleFor(d => d.EmploymentContractType)
                .NotEmpty().WithMessage("Employment contract type is required")
                .MinimumLength(15).WithMessage("Employment contract type must be at least 15 words length")
                .MaximumLength(100).WithMessage("Employment contract type must be shorter than 100 words length");

            // Email must be unique throughout the entire system
            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must be shorter than 255 words length")
                .MinimumLength(15).WithMessage("Email must be at least 15 words length")
                .MustAsync(async (email, _) =>
                {
                    bool isUnique = await _employeeRepository.IsEmailUniqueAsync(email);

                    return isUnique;
                }).WithMessage("The email must be unique.");

            RuleFor(d => d.Neighborhood)
                .NotEmpty().WithMessage("Neighborhood is required")
                .MaximumLength(50).WithMessage("Neighborhood must be shorter than 50 words length")
                .MinimumLength(3).WithMessage("Neighborhood must be at least 3 words length");

            RuleFor(d => d.HouseAddress)
                .NotEmpty().WithMessage("House address is required")
                .MinimumLength(15).WithMessage("House address must be at least 10 words length")
                .MaximumLength(50).WithMessage("House address must be shorter than 50 words length");

            RuleFor(d => d.CitizenshipId)
                .NotEmpty().WithMessage("Citizenship id is required")
                .MinimumLength(6).WithMessage("Citizenship id must be at least 6 digits length")
                .MaximumLength(10).WithMessage("Citizenship id must be shorter than 10 digits length");
        }
    }
}
