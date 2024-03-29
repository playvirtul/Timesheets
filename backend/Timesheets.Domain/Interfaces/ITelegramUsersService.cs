﻿using CSharpFunctionalExtensions;
using Timesheets.Domain.Telegram;

namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramUsersService
    {
        Task<int> Create(TelegramUser telegramUser);

        Task<Result<TelegramUser>> Get(string username);
    }
}