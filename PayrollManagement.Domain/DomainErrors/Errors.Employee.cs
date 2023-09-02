using ErrorOr;

namespace PayrollManagement.Domain.EmployeeErrors
{
    public static partial class Errors
    {
        public static class Employee
        {
            public static Error SalaryEqualsToCeroError =>
                Error.Validation("Employee.Salart", "Employee salary can't be equals to 0");

            public static Error PhoneNumberWithBadFormat =>
                Error.Validation("Employee.PhoneNumber", "The phone number format received is not correct");

            public static Error AddressWithBadFormat =>
                Error.Validation("Employee.Address", "The address format received is not correct");

            public static Error EmailWithBadFormat =>
                Error.Validation("Employee.Email", "The email format received is not correct");

            public static Error EmailDoesNotUnique =>
                Error.Validation("Employee.Email", "The Email must be unique");

            public static Error PhoneNumberValueNull =>
                Error.Validation("Employee.PhoneNumber", "The value for the phone number is empty or null");

            public static Error AddressValueNull =>
                Error.Validation("Employee.Address", "The value for the address is empty or null");

            public static Error EmploymentContractValueNull =>
                Error.Validation("Employee.EmploymentContract", "The value for the employment contract is empty or null");

            public static Error EmailValueNull =>
               Error.Validation("Employee.Email", "The value for the email is empty or null");

            public static Error EmployeeNotFound =>
                Error.NotFound("Employee.NotFound", "The employee was not found");
        }

    }
}
