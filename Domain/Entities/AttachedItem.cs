using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class AttachedItem : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int AttachedItemPriority { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsApplyAllTime { get; set; }
        public bool IsApplyAllChannels { get; set; }
        public bool IsApplyAllProductTypes { get; set; }
        public bool IsApplyAllCategories { get; set; }
        public string DataKey { get; set; }
        public int CompanyId { get; set; }
        public virtual ICollection<AttachedItemChannel> AttachedItemChannels { get; set; }
        public virtual ICollection<AttachedItemProductType> AttachedItemProductTypes { get; set; } 
        public virtual ICollection<AttachedItemCategory> AttachedItemCategories { get; set; } 
        public virtual ICollection<AttachedItemDetail> AttachedItemDetails { get; set; } 
    }
}
