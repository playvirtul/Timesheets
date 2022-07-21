﻿using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class TelegramEmlpoyeeDetails
    {
        [Required]
        [MinLength(TelegramInvitation.MIN_USERNAME_LENGTH)]
        [MaxLength(TelegramInvitation.MAX_USERNAME_LENGTH)]
        public string TelegramUserName { get; set; }

        [Required]
        [StringLength(Employee.MAX_FIRSTNAME_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(Employee.MAX_LASTNAME_LENGTH)]
        public string LastName { get; set; }

        [Required]
        public Position Position { get; set; }
    }
}