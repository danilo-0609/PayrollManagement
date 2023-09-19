using System.Security.Cryptography;
using System.Text;

namespace PayrollManagement.Users.Domain.ValueObjects
{
    public partial record Password
    {
        private const int MaximumLength = 20;

        public string Value { get; private set; } = string.Empty;

        private Password(string value) => Value = value;

        public static Password? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > MaximumLength)
            {
                return null;
            }

            string passwordHash = GenerateSHA256(value);

            return new Password(passwordHash);
        }

        private static string GenerateSHA256(string passwordValue)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordValue));

            var passwordBuilder = new StringBuilder();

            foreach (var x in bytes)
            {
                passwordBuilder.Append(x.ToString());
            }

            return passwordBuilder.ToString();
        }
    }
}
