using Domain.Commons;

namespace Domain.Entities
{
    public class SalesData : IDataKeyFilter
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int CompanyId { get; set; }
        public string Channel { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderYear { get; set; }
        public int OrderMonth { get; set; }
        public int OrderQty { get; set; }
        public double CostOfSales { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }
        public string Currency { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerId { get; set; }
        public string RepeatPurchase { get; set; }
        public string DataKey { get; set; }

    }
}