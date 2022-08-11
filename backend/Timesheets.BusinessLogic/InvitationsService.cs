using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class InvitationsService : IInvitationsService
    {
        private readonly IInvitationsRepository _invitationsRepository;

        public InvitationsService(IInvitationsRepository invitationRepository)
        {
            _invitationsRepository = invitationRepository;
        }

        public async Task<Result> Create(TelegramInvitation invitation)
        {
            return await _invitationsRepository.Add(invitation);
        }

        public async Task<Result<TelegramInvitation>> Get(string code)
        {
            var invitation = await _invitationsRepository.Get(code);

            if (invitation == null)
            {
                return Result.Failure<TelegramInvitation>("no invitation with this code");
            }

            return invitation;
        }
    }
}