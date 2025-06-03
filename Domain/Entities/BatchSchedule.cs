using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
    [Table("BatchSchedules")]
    public class BatchSchedule : AuditEntityBase
    {
        [Key]
        public long BatchScheduleId { get; set; }
        public int CompanyId { get; set; }
        public string BatchId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ScheduleDivision { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public char ScheduleType { get; set; }
        public char? DayOfWeek { get; set; }
    }

}
