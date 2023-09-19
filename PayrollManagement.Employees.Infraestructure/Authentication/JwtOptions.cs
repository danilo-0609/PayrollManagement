namespace PayrollManagement.Infraestructure.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; init; } = "PayrollManagementApp";

        public string Audience { get; init; } = "PayrollManagementService";

        public string SecretKey { get; init; } = "SuperSecretKeyForPayrollManagement";

    }
}
