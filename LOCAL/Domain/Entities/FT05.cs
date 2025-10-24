using RegistrationForm1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Bảng dùng để điều khiển PLC từ web.
    /// </summary>
    [Table("FT05")]
    public class FT05 : GenericEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Model chứa các thông tin cần điều khiển.List< ControlPlcModel>.
        /// </summary>
        public string C000 { get; set; }
        public string? CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }
        public double X_Value { get;  set; }
    }
}
