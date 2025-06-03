using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MstSystemClass
    {
        [Key]
        public int Id { get; set; }
        public string TableName { get; set; }
        public string StatusTittle { get; set; }
        public int  StatusValue { get; set; }
        public string JWording { get; set; }
        public string EWording { get; set; }
        public string Description { get; set; }
    }
}
