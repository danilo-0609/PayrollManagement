using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Infraestructure.Persistence.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(
                 employeeId => employeeId.Value,
                 value => new EmployeeId(value))
                .HasColumnName("EmployeeId")
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasColumnName("Last Name")
                .HasMaxLength(50);

            builder.Ignore(c => c.FullName);

            builder.Property(c => c.CitizenshipId)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("CitizenshipId");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasConversion(
                email => email.Value,
                value => Email.Create(value)!)
                .HasColumnName("Email")
                .HasMaxLength(255);

            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.Salary)
                .IsRequired()
                .HasColumnName("Salary")
                .HasMaxLength(100);

            builder.OwnsOne(c => c.Vacations, vacationsBuilder =>
            {
                vacationsBuilder.Property(d => d.NextVacations)
                                .IsRequired()
                                .HasColumnName("Vacation");
            });

            builder.OwnsOne(c => c.Pension, pensionBuilder =>
            {
                pensionBuilder.Property(d => d.MustContributeToPension)
                    .IsRequired()
                    .HasColumnName("MustContributeToPension");

                pensionBuilder.Property(d => d.TotalPensionContribution)
                        .IsRequired()
                        .HasColumnName("TotalPensionContribution")
                        .HasColumnType("decimal(18, 2)");
            });

            builder.OwnsOne(c => c.Health, healthBuilder =>
            {
                healthBuilder.Property(d => d.MustContributeToHealth)
                    .IsRequired()
                    .HasColumnName("MustContributeToHealth");

                healthBuilder.Property(d => d.TotalHealthContribution)
                        .IsRequired()
                        .HasColumnName("TotalHealthContribution")
                        .HasColumnType("decimal(18, 2)");
            });

            builder.OwnsOne(c => c.Bonus, bonusBuilder =>
            {
                bonusBuilder.Property(d => d.SalaryBonus)
                        .IsRequired()
                        .HasColumnName("SalaryBonus")
                        .HasColumnType("decimal(18, 2)");
            });

            builder.Property(c => c.PhoneNumber).HasConversion(
            phoneNumber => phoneNumber.Value,
            value => PhoneNumber.Create(value)!)
            .HasMaxLength(10);

            builder.OwnsOne(c => c.Address, addressBuilder =>
            {
                addressBuilder.Property(d => d.Country)
                    .IsRequired()
                    .HasColumnName("Country")
                    .HasMaxLength(60);

                addressBuilder.Property(d => d.City)
                    .IsRequired()
                    .HasColumnName("City")
                    .HasMaxLength(60);

                addressBuilder.Property(d => d.Neighborhood)
                    .IsRequired()
                    .HasColumnName("Neighborhood")
                    .HasMaxLength(50);

                addressBuilder.Property(d => d.HouseAddress)
                    .IsRequired()
                    .HasColumnName("HouseAddress")
                    .HasMaxLength(50);
            });

            builder.OwnsOne(c => c.EmploymentContract, employmentContractBuilder =>
            {
                employmentContractBuilder.Property(d => d.JoiningDate)
                    .IsRequired()
                    .HasColumnName("JoiningDate");

                employmentContractBuilder.Property(d => d.EndedContractTime)
                    .IsRequired(false)
                    .HasColumnName("EndedContractTime");
            });
        }
    }
}
