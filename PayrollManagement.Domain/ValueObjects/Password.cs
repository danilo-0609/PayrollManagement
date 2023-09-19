using System.Security.Cryptography;

namespace PayrollManagement.Domain.ValueObjects
{
    public partial record Password
    {
        private const int MaximumLength = 20;

        public string Value { get; private set; } = string.Empty;

        private Password(string value)
        {
            Value = value;
        }

        public static Password? Create(string value)
        {
            if (value.Length > MaximumLength)
            {
                return null;
            }

            string passwordHash = GenerateHash256(value);
            return new Password(passwordHash);
        }

        private static string GenerateHash256(string passwordValue)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordValue));

            var passwordBuilder = new StringBuilder();

            foreach (var s in bytes)
            {
                passwordBuilder.Append(s.ToString());
            }

            return passwordBuilder.ToString();
        }


        
    }
}
