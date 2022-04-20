﻿using System;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public WorkTime[] WorkTimes { get; set; } = Array.Empty<WorkTime>();
    }
}