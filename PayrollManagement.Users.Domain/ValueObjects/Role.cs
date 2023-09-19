namespace PayrollManagement.Users.Domain.ValueObjects
{
    public partial record Role
    {
        private const string Admin = "Admin";
        private const string User = "User";

        public string Value { get; private set; }

        private Role(string value) => Value = value;

        public static Role? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !RoleIsValid(value))
            {
                return null;
            }

            return new Role(value);
        }

        public static bool RoleIsValid(string value)
        {
            if (value == Admin || value == User)
            {
                return true;
            }

            return false;
        }
    }
}
