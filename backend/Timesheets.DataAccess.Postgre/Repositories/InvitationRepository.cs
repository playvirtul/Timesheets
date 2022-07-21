using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public InvitationRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Domain.Invitation newInvitation)
        {
            var invitation = _mapper.Map<Domain.Invitation, Invitation>(newInvitation);

            _context.Invitations.Add(invitation);

            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Invitation?> Get(string code)
        {
            var invitationEntity = await _context.Invitations
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Code == code);

            if (invitationEntity == null)
            {
                return null;
            }

            var invitation = _mapper.Map<Invitation, Domain.TelegramInvitation>(invitationEntity);

            return invitation;
        }

        public async Task Delete(string code)
        {
            var invitationEntity = await _context.Invitations
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Code == code);

            if (invitationEntity != null)
            {
                _context.Invitations.Remove(invitationEntity);
            }

            await _context.SaveChangesAsync();
        }
    }
}