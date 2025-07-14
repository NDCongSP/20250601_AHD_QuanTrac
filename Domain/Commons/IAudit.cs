using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public interface IUpdateAudit
    {
        string UpdateOperatorId { get; set; }
        DateTime? UpdateAt { get; set; }
    }

    public interface IAddAudit
    {
        string CreateOperatorId { get; set; }
        DateTime? CreateAt { get; set; }
    }
    public interface IDeleteAudit
    {
        bool? IsDeleted { get; set; }
    }

    public interface IAudit : IAddAudit,IUpdateAudit, IDeleteAudit
    {
        
    }

}
