using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("InventTransferLines")]
    public class InventTransferLine : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid InventTransferId { get; set; }
        public string TransferNo { get; set; }
        public double? Qty { get; set; } = 0;
        public int UnitId { get; set; }
        public EnumInvenTransferStatus Status { get; set; } = EnumInvenTransferStatus.InProcess;
        public string FromBin { get; set; }
        public string ToBin { get; set; }
        public string FromLotNo { get; set; }
        public string ToLotNo { get; set; }
        public string ProductCode { get; set; }
    }
}
