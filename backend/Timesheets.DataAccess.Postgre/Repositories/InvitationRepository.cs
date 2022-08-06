using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class InvitationRepository : IInvitationsRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public InvitationRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Domain.TelegramInvitation newInvitation)
        {
            var invitation = _mapper.Map<Domain.TelegramInvitation, TelegramInvitation>(newInvitation);

            _context.TelegramInvitations.Add(invitation);

            await _context.SaveChangesAsync();
        }

        public async Task<Domain.TelegramInvitation?> Get(string code)
        {
            var invitationEntity = await _context.TelegramInvitations
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Code == code);

            if (invitationEntity == null)
            {
                return null;
            }

            var invitation = _mapper.Map<TelegramInvitation, Domain.TelegramInvitation>(invitationEntity);

            return invitation;
        }

        public async Task Delete(string code)
        {
            var invitationEntity = await _context.TelegramInvitations
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Code == code);

            if (invitationEntity != null)
            {
                _context.TelegramInvitations.Remove(invitationEntity);
            }

            await _context.SaveChangesAsync();
        }
    }
}