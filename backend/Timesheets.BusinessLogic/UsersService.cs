using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Auth;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IInvitationRepository _invitationRepository;

        public UsersService(IUsersRepository usersRepository, IInvitationRepository invitationRepository)
        {
            _usersRepository = usersRepository;
            _invitationRepository = invitationRepository;
        }

        public async Task<User?> AuthenticateUser(string email, string password)
        {
            var hashPassword = new Password(password).Hash();

            var users = await _usersRepository.Get();
            var user = users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashPassword);

            return user;
        }

        public async Task<int> Create(User newUser, string code)
        {
            await _invitationRepository.Delete(code);

            return await _usersRepository.Add(newUser);
        }

        public async Task<User?> Get(string email)
        {
            return await _usersRepository.Get(email);
        }
    }
}