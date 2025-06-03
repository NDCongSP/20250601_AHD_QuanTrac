using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ForecastedSalesData : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string ForecastedDate { get; set; }

        public string ForecastedChannel { get; set; }
        public float ForecastedSalesAmount { get; set; }

        public float ForecastedProfitAmount { get; set; }

        public string DataKey { get; set; }
    }
}
