﻿namespace Timesheets.DataAccess.Postgre.Entities
{
    public class TelegramUser
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public long ChatId { get; set; }
    }
}