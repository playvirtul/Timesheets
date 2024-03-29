﻿using System.Text;
using Timesheets.Domain.Auth;

namespace Timesheets.Domain
{
    public abstract record Invitation
    {
        public string Code { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Position Position { get; }

        public Role Role { get;  }

        public Invitation(string firstName, string lastName, Position position, Role role)
        {
            Code = GenerateCode();
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Role = role;
        }

        protected string GenerateCode()
        {
            var guidStr = Guid.NewGuid().ToString();

            var guidBytes = Encoding.UTF8.GetBytes(guidStr);

            return Convert.ToBase64String(guidBytes);
        }
    }
}