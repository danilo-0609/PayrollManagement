using System.Text.RegularExpressions;

namespace PayrollManagement.Domain.ValueObjects
{
    public partial record PhoneNumber
    {
        private const int DefaultLength = 10;
        private const string Pattern = @"^3[0-9]{9}";

        public string Value { get; set; }


        private PhoneNumber(string value) => Value = value;

        public static PhoneNumber? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value))
            {
                return null;
            }

            return new PhoneNumber(value);
        }


        [GeneratedRegex(pattern: Pattern)]
        public static partial Regex PhoneNumberRegex();
    }
}
