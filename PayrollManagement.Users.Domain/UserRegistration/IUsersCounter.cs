namespace PayrollManagement.Users.Domain.UserRegistration
{
    public interface IUsersCounter
    {
        int CountUsersWithLogin(string login);
    }
}
