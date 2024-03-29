﻿namespace Timesheets.API
{
    public record BotConfiguration
    {
        public string BotToken { get; init; } = default!;

        public string HostAddress { get; init; } = default!;
    }
}