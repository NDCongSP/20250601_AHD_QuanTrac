using System;

using Domain.Commons;

namespace Domain.Entities
{
    public class ProductStatus : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public string StatusProduct { get; set; }
        public string Description { get; set; }
        public string DataKey { get; set; }
    }
}