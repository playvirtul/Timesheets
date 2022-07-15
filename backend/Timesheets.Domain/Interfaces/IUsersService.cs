namespace Timesheets.Domain.Interfaces
{
    public interface IUsersService
    {
        Task<User?> AuthenticateUser(string email, string password);

        Task Register(string email, string password);
    }
}