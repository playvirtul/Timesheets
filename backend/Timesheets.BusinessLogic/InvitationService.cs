using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task Create(Invitation invitation)
        {
            await _invitationRepository.Add(invitation);
        }

        public async Task<Result<Invitation>> Get(string code)
        {
            var invitation = await _invitationRepository.Get(code);

            if (invitation == null)
            {
                return Result.Failure<Invitation>("no invitation with this code");
            }

            return invitation;
        }
    }
}