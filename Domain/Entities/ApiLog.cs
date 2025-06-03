using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ApiLogs")]
    public class ApiLog
    {
        public Guid Id { get; set; }
        public int CompanyId { get; set; }
        public string OrderIds { get; set; }
        public string CourierCode { get; set; }
        public string ApiEndpoint { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public int? StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
        public int? DurationMs { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
