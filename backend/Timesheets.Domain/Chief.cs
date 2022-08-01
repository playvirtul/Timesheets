using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public record Chief : Employee
    {
        private Chief(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Chief, Array.Empty<Project>())
        {
        }

        public static Result<Chief> Create(int userId, string firstName, string lastName)
        {
            var validationResult = ValidationErrors(firstName, lastName);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Chief>(validationResult.Error);
            }

            return new Chief(userId, firstName, lastName);
        }
    }
}