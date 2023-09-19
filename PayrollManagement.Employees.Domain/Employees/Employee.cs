using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.ValueObjects;
using System.Globalization;

namespace PayrollManagement.Domain.Employees
{
    public sealed class Employee : AggregateRoot
    {

        public EmployeeId Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string FullName => $"{Name} {LastName}";

        public string CitizenshipId { get; private set; } = string.Empty;

        public Email Email { get; set; }

        public int Salary { get; private set; }

        public Vacations Vacations { get; private set; }

        public Pension Pension { get; private set; }

        public Health Health { get; private set; }

        public Bonus Bonus { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Address Address { get; private set; }

        public EmploymentContract EmploymentContract { get; private set; }


        public Employee(EmployeeId id,
            string name,
            string lastName,
            string citizenshipId,
            int salary,
            Email email,
            Vacations vacations,
            Pension pension,
            Health health,
            Bonus bonus,
            PhoneNumber phoneNumber,
            Address address,
            EmploymentContract employmentContract)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CitizenshipId = citizenshipId;
            Salary = salary;
            Email = email;
            Vacations = vacations;
            Pension = pension;
            Health = health;
            Bonus = bonus;
            PhoneNumber = phoneNumber;
            Address = address;
            EmploymentContract = employmentContract;
        }

        public Employee(
            EmployeeId id, string name, string lastName, string citizenshipId, Email email, int salary, Pension pension, Health health, Bonus bonus, PhoneNumber phoneNumber, Address address, EmploymentContract employmentContract)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CitizenshipId = citizenshipId;
            Email = email;
            Salary = salary;
            Pension = pension;
            Health = health;
            Bonus = bonus;
            PhoneNumber = phoneNumber;
            Address = address;
            EmploymentContract = employmentContract;
        }

        private Employee()
        {

        }

        public static Employee UpdateEmployee(Guid id, string name, string lastName, string citizenshipId,
                            int salary, Email email, Vacations vacations, Pension pension, 
                            Health health, Bonus bonus, PhoneNumber phoneNumber, 
                            Address address, EmploymentContract employmentContract)
        {
            return new Employee(new EmployeeId(id), name, lastName, citizenshipId, salary, email, vacations, pension, health, bonus, phoneNumber, address, employmentContract);
        }

        public static Employee UpdateEmployee(Guid id, string name, string lastName, string citizenshipId,
                            int salary, Email email, Pension pension,
                            Health health, Bonus bonus, PhoneNumber phoneNumber,
                            Address address, EmploymentContract employmentContract)
        {
            return new Employee(new EmployeeId(id), name, lastName, citizenshipId, email, salary, pension, health, bonus, phoneNumber, address, employmentContract);
        }
    }
}
