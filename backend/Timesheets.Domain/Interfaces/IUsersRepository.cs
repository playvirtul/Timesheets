namespace Timesheets.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User[]> Get();

        Task<User?> Get(int userId);

        Task Add(User user);
    }
}