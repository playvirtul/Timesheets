using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Auth;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IInvitationsRepository _invitationRepository;

        public UsersService(IUsersRepository usersRepository, IInvitationsRepository invitationRepository)
        {
            _usersRepository = usersRepository;
            _invitationRepository = invitationRepository;
        }

        public async Task<Result<User>> AuthenticateUser(string email, string password)
        {
            var passwordHash = new Password(password).Hash();

            var user = await _usersRepository.Get(email, passwordHash);

            if (user == null)
            {
                return Result.Failure<User>("Incorrect email or password");
            }

            return user;
        }

        public async Task<Result<int>> Create(User userRequest, string code)
        {
            var user = await _usersRepository.Get(userRequest.Email);

            if (user != null)
            {
                return Result.Failure<int>("User with this email already exists");
            }

            await _invitationRepository.Delete(code);

            return await _usersRepository.Add(userRequest);
        }

        public async Task<Result<User>> Get(string email)
        {
            var user = await _usersRepository.Get(email);

            if (user == null)
            {
                return Result.Failure<User>("No user with this email");
            }

            return user;
        }

        public async Task<Result<User>> Get(int id)
        {
            var user = await _usersRepository.Get(id);

            if (user == null)
            {
                return Result.Failure<User>("No user with this id");
            }

            return user;
        }
    }
}