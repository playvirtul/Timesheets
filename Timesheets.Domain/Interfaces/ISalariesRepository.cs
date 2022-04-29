namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesRepository
    {
        Task<Salary[]> Get();
    }
}