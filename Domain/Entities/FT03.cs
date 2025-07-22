using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Data Log.
    /// </summary>
    [Table("FT03")]
    public class FT03 : TagsModel
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Station Id.
        /// StationInfoModel.Id.
        /// </summary>
        [Display(Name = "Station Id")]
        public int StationId { get; set; }

        /// <summary>
        /// Station name.
        /// StationInfoModel.Name.
        /// </summary>
        [Display(Name = "Station Name")]
        public string? StationName { get; set; }

    }
}
