using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Helpers;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<User?> AuthenticateUser(string email, string password)
        {
            var hashPassword = HashPasswordHelper.HashPassword(password);

            var users = await _usersRepository.Get();
            var user = users.FirstOrDefault(u => u.Email == email && u.HashPassword == hashPassword);

            return user;
        }

        public async Task Register(string email, string password)
        {
            var hashPassword = HashPasswordHelper.HashPassword(password);
        }
    }
}