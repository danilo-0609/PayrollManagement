namespace PayrollManagement.Domain.ValueObjects
{
    public record Health
    {
        private const decimal HealthContributionPercent = 0.085m;

        public decimal TotalHealthContribution { get; private set; }

        public bool MustContributeToHealth { get; private set; }

        private Health()
        {

        }

        private Health(decimal totalHealthContribution, bool mustContributeToHealth)
        {
            TotalHealthContribution = totalHealthContribution;
            MustContributeToHealth = mustContributeToHealth;
        }

        public static Health Create(int salary)
        {
            if (salary >= IntegralSalary.IntegralSalaryValue)
            {
                return new Health(0m, false);
            }

            var totalHealthContribution = salary * HealthContributionPercent;
            var mustContributeToHealth = true;

            return new Health(totalHealthContribution, mustContributeToHealth);
        }
    }
}
