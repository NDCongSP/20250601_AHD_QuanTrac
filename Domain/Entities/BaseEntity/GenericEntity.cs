namespace Domain.Entities
{
    public class GenericEntity
    {
        public string? CreateOperatorId { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? UpdateOperatorId { get; set; }

        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
