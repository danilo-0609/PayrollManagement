using System.Runtime.InteropServices;

namespace PayrollManagement.Domain.ValueObjects
{
    public partial record Role
    {
        public static string Admin { get; } = "Admin";

        public static string User { get; } = "User";        
    }
}
