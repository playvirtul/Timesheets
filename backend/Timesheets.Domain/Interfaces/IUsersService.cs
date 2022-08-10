using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IUsersService
    {
        Task<Result<User>> AuthenticateUser(string email, string password);

        Task<Result<int>> Create(User newUser, string code);

        Task<Result<User>> Get(string email);

        Task<Result<User>> Get(int id);

        Task<User[]> Get();
    }
}