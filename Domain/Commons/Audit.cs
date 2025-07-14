using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public class AuditEntityBase : IAudit, ICloneable
    {
        public string CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; } = false;

        public AuditEntityBase()
        {
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }
        public virtual object Clone()
        {
            return new AuditEntityBase
            {
               CreateAt = this.CreateAt,
               CreateOperatorId = this.CreateOperatorId,
               UpdateAt = this.UpdateAt,
               UpdateOperatorId = this.UpdateOperatorId,
               IsDeleted = this.IsDeleted
            };
        }

    }

    public abstract class UpdateAudit :IUpdateAudit
    {
        public string UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
    }

    public abstract class AddAudit : IAddAudit
    {
        public string CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
    }


}
