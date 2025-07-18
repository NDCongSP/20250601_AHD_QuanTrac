using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Bảng chứa giá trị hiển thị web realtime Display.
    /// </summary>
    [Table("FT02")]
    public class FT02: GenericEntity

    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// giá trị hiển thị realtime của trạm.
        /// chính là class RealtimeDisplays (List<RealtimeDisplayModel>).
        /// </summary>
        public string? C000 { get; set; }
    }
}
