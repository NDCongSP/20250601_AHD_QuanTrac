using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ScadaUser")]
    public class ScadaUser : IGenericEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        public EnumPermissionScada? PermissionScada { get; set; }
        public string? CreateOperatorId {get;set;}
        public DateTime? CreateAt {get;set;}
        public string? UpdateOperatorId {get;set;}
        public DateTime? UpdateAt {get;set;}
        public bool? IsDeleted {get;set;}
    }
}
