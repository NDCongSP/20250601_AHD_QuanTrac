using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FdaRegistration : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string ChannelCode { get; set; }

        public string OrderId { get; set; }
        public string DeliveryId { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string PriorNoticeConfirmationNumber { get; set; }

        public string DataKey { get; set; }
    }
}
