using System;

namespace Domain.Models
{
    public class FT03DataPoint
    {
        public string X_Value { get; set; } = string.Empty;
        public double? Value { get; set; }
        public DateTime Date { get; set; }
    }
}
