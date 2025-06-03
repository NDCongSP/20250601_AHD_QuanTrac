using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("InventTransfer")]
    public class InventTransfer : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string TransferNo { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }
        public DateOnly? TransferDate { get; set; }
        public EnumInvenTransferStatus Status { get; set; } = EnumInvenTransferStatus.InProcess;
        public string? PersonInCharge { get; set; }
        public int TenantId { get; set; }
        public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
        public string? HHTInfo { get; set; } = string.Empty;
    }
}
