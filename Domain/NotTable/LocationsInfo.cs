namespace Domain
{
    /// <summary>
    /// nhiều vị trí ở các khu vực địa lý khác nhau.
    /// </summary>
    public class LocationsInfo : List<LocationInfoModel>
    {

    }

    /// <summary>
    /// trong 1 vị trí có thể có nhiều trạm khác nhau.
    /// </summary>
    public class LocationInfoModel
    {
        /// <summary>
        /// Id của vị trí.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Tên của vị trí.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Mô tả về vị trí.
        /// </summary>
        public string? Description { get; set; }

        public List<StationInfoModel>? Stations { get; set; } = new List<StationInfoModel>();
    }

    public class StationInfoModel
    {
        /// <summary>
        /// Id cuar trạm.
        /// </summary>
        public int? Id { get; set; }

        public string? Name { get; set; }
        public TagsStation? Tags { get; set; }
        /// <summary>
        /// Lưu thông tin tag path, để phục vụ cho sự kiện tagCHanged.
        /// </summary>
        public string? Path { get; set; }
    }
}
