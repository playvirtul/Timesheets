using AutoMapper;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class TelegramUserRepository : ITelegramUserRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public TelegramUserRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(Domain.Telegram.TelegramUser telegramUser)
        {
            var telegramUserEntity = _mapper.Map<Domain.Telegram.TelegramUser, TelegramUser>(telegramUser);

            _context.TelegramUsers.Add(telegramUserEntity);

            await _context.SaveChangesAsync();

            return telegramUserEntity.Id;
        }
    }
}