using System;

namespace Domain.Entities
{
    public interface GenericEntity
    {
        public string? CreateOperatorId { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? UpdateOperatorId { get; set; }

        public DateTime? UpdateAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
