using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Domain.Commons;

namespace Domain.Entities;

[Table("Tasks")]
public partial class TaskModel : AuditEntityBase, IDataKeyFilter
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string OrderId { get; set; }
    
    public string DeliveryId { get; set; }

    public string ChannelCode { get; set; }

    public string? Title { get; set; }

    public int? Status { get; set; }

    public int Priority { get; set; }

    public string? Assignee { get; set; }

    public int TaskType { get; set; }

    public string? TaskContent { get; set; }
    public string? ImageUrl { get; set; }

    public int? FdaRegistrationId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
    public bool IsTaskCreatedByBatch { get; set; }

    public string? DataKey { get; set; }

    public virtual ICollection<TaskChatHistory> TaskChatHistories { get; set; } = new List<TaskChatHistory>();
    public Guid ReceiptOrderLineId { get; set; }
    public string ReceiptNo { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string LotNo { get; set; }
    public EnumProductErrorStatus? StatusProductError { get; set; }
    /// <summary>
    /// List model ImageNameBase64
    /// </summary>
    public string? ImagesBase64 { get; set; }

}