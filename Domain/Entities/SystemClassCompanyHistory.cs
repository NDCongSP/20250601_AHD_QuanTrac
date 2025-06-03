
using Domain.Commons;
using System;

namespace Domain.Entities
{
    public class SystemClassCompanyHistory : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int SystemClassCompanyId { get; set; }
        public int CompanyId { get; set; }
        public string LargeClassCode { get; set; }
        public string SmallClassCode { get; set; }
        public string LargeClassCodeName { get; set; }
        public string SmallClassCodeName { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public int? Numeric1 { get; set; }
        public double? Numeric2 { get; set; }
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Remarks { get; set; }
        public string DataKey { get; set; }
    }
}
