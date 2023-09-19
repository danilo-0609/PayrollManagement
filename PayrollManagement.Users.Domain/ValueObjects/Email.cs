using PayrollManagement.BuildingBlocks.Domain.Primitives;
using System.Text.RegularExpressions;

namespace PayrollManagement.Users.Domain.ValueObjects
{
    public partial record Email
    {
        private const string Pattern = @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/";

        public string Value { get; private set; } = string.Empty;

        private Email(string value) => Value = value;
        
        public static Email? Create(string value)
        {
            if ( !string.IsNullOrEmpty(value) && EmailRegex().IsMatch(value) )
            {
                return new Email(value);
            }

            return null;
        }

        [GeneratedRegex(Pattern)]
        public static partial Regex EmailRegex();
    }
}
