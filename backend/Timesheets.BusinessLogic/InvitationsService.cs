using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class InvitationsService : IInvitationService
    {
        private readonly IInvitationsRepository _invitationsRepository;

        public InvitationsService(IInvitationsRepository invitationRepository)
        {
            _invitationsRepository = invitationRepository;
        }

        public async Task Create(Invitation invitation)
        {
            await _invitationsRepository.Add(invitation);
        }

        public async Task<Result<Invitation>> Get(string code)
        {
            var invitation = await _invitationsRepository.Get(code);

            if (invitation == null)
            {
                return Result.Failure<Invitation>("no invitation with this code");
            }

            return invitation;
        }
    }
}