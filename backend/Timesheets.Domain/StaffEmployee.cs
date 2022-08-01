using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public record StaffEmployee : Employee
    {
        private StaffEmployee(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.StaffEmployee, Array.Empty<Project>())
        {
        }

        public static Result<StaffEmployee> Create(int userId, string firstName, string lastName)
        {
            var validationResult = ValidationErrors(firstName, lastName);

            if (validationResult.IsFailure)
            {
                return Result.Failure<StaffEmployee>(validationResult.Error);
            }

            return new StaffEmployee(userId, firstName, lastName);
        }
    }
}