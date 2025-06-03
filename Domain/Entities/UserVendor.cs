using Domain.Commons;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class UserVendor: IDataKeyFilter
    {
        [Key]
        public string UserId { get; set; }
        public string VendorCode { get; set; }
        public string DataKey { get; set; }
    }
}