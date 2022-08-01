using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public record Manager : Employee
    {
        private Manager(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Manager, Array.Empty<Project>())
        {
        }

        public static Result<Manager> Create(int userId, string firstName, string lastName)
        {
            var validationResult = ValidationErrors(firstName, lastName);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Manager>(validationResult.Error);
            }

            return new Manager(userId, firstName, lastName);
        }
    }
}