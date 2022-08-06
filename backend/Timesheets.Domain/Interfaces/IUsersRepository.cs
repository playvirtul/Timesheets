namespace Timesheets.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User[]> Get();

        Task<User?> Get(string email);

        Task<Domain.User?> Get(int id);

        Task<User?> Get(string email, string passwordHash);

        Task<int> Add(User user);
    }
}