namespace PayrollManagement.Domain.ValueObjects
{
    public record Pension
    {
        private const decimal PensionContributionPercent = 0.12m;

        public decimal TotalPensionContribution { get; private set; }

        public bool MustContributeToPension { get; private set; }

        private Pension()
        {

        }

        private Pension(decimal totalPensionContribution, bool mustContributeToPension)
        {
            TotalPensionContribution = totalPensionContribution;
            MustContributeToPension = mustContributeToPension;
        }

        public static Pension CreatePensionStatement(int salary)
        {
        
            if (salary >= IntegralSalary.IntegralSalaryValue)
            {
                return new Pension()
                {
                    TotalPensionContribution = 0m,
                    MustContributeToPension = false
                };
            }

            return new Pension(PensionContributionPercent * salary, true);
        }

    }
}
