using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public partial class ArrivalInstruction : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime ScheduledArrivalDate { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string DataKey { get; set; }
    }
}