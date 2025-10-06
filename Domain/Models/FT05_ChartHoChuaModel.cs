using System.Globalization;

namespace Domain.Models;

public class FT05_ChartHoChuaModel : FT05_ChartHoChua
{
    public DateTime Date
    {
        get
        {
            var date = DateTime.ParseExact(X_Value, "dd-MM", CultureInfo.InvariantCulture);
            var currentYear = DateTime.Now.Year;

            var targetYear = date.Month <= 6 ? currentYear + 1 : currentYear;

            if (date.Month == 1 && date.Day > 31)
            {
                return new DateTime(targetYear + 1, 1, date.Day - 31);
            }

            return new DateTime(targetYear, date.Month, date.Day);
        }
        set => X_Value = value.ToString("dd-MM");
    }
}
