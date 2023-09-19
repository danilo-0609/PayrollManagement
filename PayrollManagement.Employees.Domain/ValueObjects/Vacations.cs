namespace PayrollManagement.Domain.ValueObjects
{
    public record Vacations
    {
        public DateTime NextVacations { get; private set; }

        private Vacations(DateTime nextVacations)
        {
            NextVacations = nextVacations;
        }

        public static Vacations Create(DateTime nextVacations)
        {
            return new Vacations(nextVacations);
        }

        public static Vacations Create()
        {
            return new Vacations(DateTime.UtcNow.AddYears(1));
        }

    }
}
