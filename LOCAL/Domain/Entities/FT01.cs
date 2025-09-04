
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
    /// Bảng chứa các thông tin cấu hình về lò của hệ thống, số lượng lò và tên lò.
    /// </summary>
    [Table("FT01")]
    public class FT01 : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// List chứa thông tin cài đặt chung của toàn hệ thống.
        /// Chính là ConfigModel.
        /// </summary>
        [Display(Name = "Systems config")]
        public string? C000 { get; set; }

        /// <summary>
        /// Chứa các thông tin config cho từng trạm.
        /// chính là class LocationsModel (List<LocationInfoModel>).
        /// </summary>
        [Display(Name = "Stations config")]
        public string? C001 { get; set; }
        public string? CreateOperatorId { get ; set ; }
        public DateTime? CreateAt { get ; set ; }
        public string? UpdateOperatorId { get ; set ; }
        public DateTime? UpdateAt { get ; set ; }
        public bool? IsDeleted { get ; set ; }
    }
}
