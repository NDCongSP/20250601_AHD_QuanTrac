namespace Domain
{
    public enum EnumShipmentOrderStatus
    {
        Draft = 0,
        Open = 1,
        Picking = 2,
        Picked = 3,
        Packing = 4,
        Packed = 5,
        Completed = 6,
        Cancelled = 7,
        All = 8,//dùng cho việc tìm kiếm, chọn toàn bộ các trạng thái của shipment ở packing list master

        Delivered = 10,
        Received = 11,
        Putaway = 12,
        WaitingToPick = 13,
    }
}