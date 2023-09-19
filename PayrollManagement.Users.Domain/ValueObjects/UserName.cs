namespace PayrollManagement.Users.Domain.ValueObjects
{
    public partial record UserName
    {
        private const int MinimumLength = 7;
        private const int MaximumLength = 20;

        //I must apply a pattern here to validate if the value received is valid for
        //create a new user name...

        public string Value { get; private set; } = string.Empty;

        private UserName(string value) => Value = value;

        public static UserName? Create(string value)
        {
            if (string.IsNullOrEmpty(value) 
                || value.Length < MinimumLength 
                || value.Length > MaximumLength )
            {
                return null;
            }

            return new UserName(value);
        }
    }
}
