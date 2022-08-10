namespace Timesheets.WorkTimeReporter
{
    public record BotConfiguration
    {
        public string BotToken { get; init; } = default!;
    }
}