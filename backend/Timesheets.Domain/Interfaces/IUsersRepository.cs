namespace Timesheets.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User[]> Get();

        Task<User?> Get(string email);

        Task<User?> Get(string email, string passwordHash);

        Task<int> Add(User user);
    }
}