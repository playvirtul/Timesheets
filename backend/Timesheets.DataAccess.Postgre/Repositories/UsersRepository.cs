using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Domain.User newUser)
        {
            var user = _mapper.Map<Domain.User, User>(newUser);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task<Domain.User[]> Get()
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .ToArrayAsync();

            var users = _mapper.Map<User[], Domain.User[]>(usersEntities);

            return users;
        }

        public async Task<Domain.User?> Get(int userId)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (userEntity == null)
            {
                return null;
            }

            var user = _mapper.Map<User, Domain.User>(userEntity);

            return user;
        }
    }
}