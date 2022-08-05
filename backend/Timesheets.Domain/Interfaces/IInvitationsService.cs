using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationService
    {
        Task Create(Invitation invitation);

        Task<Result<Invitation>> Get(string code);
    }
}