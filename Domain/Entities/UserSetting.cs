using Domain.Commons;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class UserSetting: IDataKeyFilter
    {
        [Key]
        public string UserId { get; set; }
        public string CurrencyCode { get; set; }
        public int PageLength { get; set; }
        public string DataKey { get; set; }
    }
}