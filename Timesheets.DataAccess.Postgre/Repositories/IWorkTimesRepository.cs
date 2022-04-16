using System.Threading.Tasks;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public interface IWorkTimesRepository
    {
        Task<int> Create(Domain.WorkTime newWorkTime);
        Task<Domain.WorkTime[]> Get();
    }
}