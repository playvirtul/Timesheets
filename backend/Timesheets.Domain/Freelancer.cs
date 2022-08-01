using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public record Freelancer : Employee
    {
        private Freelancer(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Freelancer, Array.Empty<Project>())
        {
        }

        public static Result<Freelancer> Create(int userId, string firstName, string lastName)
        {
            var validationResult = ValidationErrors(firstName, lastName);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Freelancer>(validationResult.Error);
            }

            return new Freelancer(userId, firstName, lastName);
        }
    }
}