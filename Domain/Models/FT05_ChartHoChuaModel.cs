using System.Globalization;

namespace Domain.Models;

public class FT05_ChartHoChuaModel : FT05_ChartHoChua
{
    //get set from X_Value format dd-MM
    public DateTime Date 
    {
        get => DateTime.ParseExact(X_Value, "dd-MM", CultureInfo.InvariantCulture);
        set => X_Value = value.ToString("dd-MM");
    }
}
