using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class TelegramUsersRepository : ITelegramUsersRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public TelegramUsersRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(Domain.Telegram.TelegramUser telegramUser)
        {
            var telegramUserEntity = _mapper.Map<Domain.Telegram.TelegramUser, TelegramUser>(telegramUser);

            var user = await _context.TelegramUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ChatId == telegramUser.ChatId);

            if (user != null)
            {
                return default;
            }

            await _context.TelegramUsers.AddAsync(telegramUserEntity);

            await _context.SaveChangesAsync();

            return telegramUserEntity.Id;
        }

        public async Task<Domain.Telegram.TelegramUser?> Get(string userName)
        {
            var userEntity = await _context.TelegramUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (userEntity == null)
            {
                return null;
            }

            return _mapper.Map<TelegramUser, Domain.Telegram.TelegramUser>(userEntity);
        }
    }
}