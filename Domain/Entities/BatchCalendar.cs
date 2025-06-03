using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
    public class BatchCalendar
    {
        [Key]
        public string Date { get; set; }
        public char ScheduleType { get; set; }
    }

}
