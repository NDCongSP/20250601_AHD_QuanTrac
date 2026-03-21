using System.Globalization;

namespace Domain.Models;

public class FT05_ChartHoChuaModel : FT05_ChartHoChua
{
    public DateTime Date
    {
        get
        {
            if (string.IsNullOrWhiteSpace(X_Value))
                return default;

            var date = DateTime.ParseExact(X_Value.Trim(), "M/d", CultureInfo.InvariantCulture);
            var now = DateTime.Now;

            var baseYear = now.Month >= 7 ? now.Year : now.Year - 1;
            var targetYear = date.Month >= 7 ? baseYear : baseYear + 1;

            return new DateTime(targetYear, date.Month, date.Day);
        }
        set => X_Value = value.ToString("M/d", CultureInfo.InvariantCulture);
    }
}
