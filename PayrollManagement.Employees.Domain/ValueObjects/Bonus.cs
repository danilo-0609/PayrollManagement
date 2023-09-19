namespace PayrollManagement.Domain.ValueObjects
{
    public record Bonus
    {
        private const decimal BonusPercent = 0.5m;

        public decimal SalaryBonus { get; private set; }

        private Bonus()
        {

        }

        private Bonus(decimal salaryBonus)
        {
            SalaryBonus = salaryBonus;
        }

        public static Bonus Create(int salary)
        {
            if (salary >= IntegralSalary.IntegralSalaryValue)
            {
                return new Bonus(0m);
            }

            var salaryBonus = BonusPercent * salary;

            return new Bonus(salaryBonus);
        }
    }
}
