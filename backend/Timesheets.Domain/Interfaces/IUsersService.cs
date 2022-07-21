namespace Timesheets.Domain.Interfaces
{
    public interface IUsersService
    {
        Task<User?> AuthenticateUser(string email, string password);

        Task<int> Create(User newUser, string code);

        Task<User?> Get(string email);
    }
}