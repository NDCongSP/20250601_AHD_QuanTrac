using Application.DTOs;
using Application.Models;

namespace UI.Core
{
    public class MasterTransferToDetails
    {
        public WarehousePackingListDto TransferToPackingDetail { get; set; }

        public WarehousePickingDTO TransferToPickingDetail { get; set; }

        public string ShipmentNo { get; set; } = string.Empty;

        public string RoleId { get; set; } = string.Empty;

        public SupplierTenantDTO SupplierInfo { get; set; }
        public List<string> PickNoList { get; set;}
        public string PickNoJoin { get; set; } = string.Empty;
    }
}
