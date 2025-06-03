using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;
using Domain.Entities;

public class TaskChatHistory : AuditEntityBase, IDataKeyFilter
{
    public int Id { get; set; }                     
    public int TaskId { get; set; }                
    public string ChatContent { get; set; }                  
    public string? DataKey { get; set; }             
    public virtual TaskModel Task { get; set; }
    public bool IsModified { get; set; }
}
