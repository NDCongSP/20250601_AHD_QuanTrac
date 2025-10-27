using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("FT08")]
    public class FT08_FilesManagement : IGenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }

        public string GroupFolder { get; set; } = string.Empty;
        public string PathFile { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
