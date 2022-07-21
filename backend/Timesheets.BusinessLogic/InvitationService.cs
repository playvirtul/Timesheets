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

        public async Task<Invitation?> Get(string code)
        {
            return await _invitationRepository.Get(code);
        }
    }
}