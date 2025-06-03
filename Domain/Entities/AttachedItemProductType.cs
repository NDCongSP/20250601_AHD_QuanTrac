using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AttachedItemProductType : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int AttachedItemId { get; set; } 
        public int ProductTypeId { get; set; }
        public string DataKey { get; set; }
        public int CompanyId { get; set; }

        // Navigation property to access the parent AttachedItem
        public virtual AttachedItem AttachedItem { get; set; }
    }
}
