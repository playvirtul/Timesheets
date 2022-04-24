using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class WorkTime
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        //[Column("WorkingHours")]
        public int WorkingHours { get; set; }

        public DateTime Date { get; set; }
    }
}