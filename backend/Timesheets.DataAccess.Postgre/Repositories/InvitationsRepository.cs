using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class InvitationsRepository : IInvitationsRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public InvitationsRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Add(Domain.TelegramInvitation newInvitation)
        {
            var existInvitation = await _context
                .TelegramInvitations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == newInvitation.UserName);

            if (existInvitation != null)
            {
                return Result.Failure("You can't send multiple invitations to the same user");
            }

            var invitation = _mapper.Map<Domain.TelegramInvitation, TelegramInvitation>(newInvitation);

            await _context.TelegramInvitations.AddAsync(invitation);

            await _context.SaveChangesAsync();

            return Result.Success();
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

        public async Task Accept(string code)
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