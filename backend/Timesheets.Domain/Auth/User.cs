namespace Timesheets.Domain
{
    public class User
    {
        public string Email { get; set; }

        public string HashPassword { get; set; }

        public Position Position { get; set; }
    }
}