using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
    [Table("Batches")]
    public class Batch : AuditEntityBase
    {
        [Key]
        public string BatchId { get; set; }
        public string BatchName { get; set; }
        public string ExecFile { get; set; }
        public bool StartupStatus { get; set; }
        public string Args { get; set; }
    }
}
