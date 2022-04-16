using System;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class WorkTime
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int WorkingHours { get; set; }

        public DateTime Date { get; set; }
    }
}