namespace Timesheets.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User[]> Get();

        Task<User?> Get(string email);

        Task<int> Add(User user);
    }
}