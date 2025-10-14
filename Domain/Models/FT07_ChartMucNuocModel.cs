namespace Domain.Models;

public class FT07_ChartMucNuocModel : FT07_ChartMucNuoc
{
    public double? Z_ThucValue { get; set; }
    public FT07_ChartMucNuocModel(){}
    public FT07_ChartMucNuocModel(FT07_ChartMucNuoc item)
    {
        this.Id = item.Id;
        this.Index = item.Index;
        this.X_Prefix = item.X_Prefix;
        this.X_Value = item.X_Value;
        this.BoPhai = item.BoPhai;
        this.BoTrai = item.BoTrai;
        this.Q300 = item.Q300;
        this.Q400 = item.Q400;
        this.Q600 = item.Q600;
        this.Q2800 = item.Q2800;
        this.Z_Thuc = item.Z_Thuc;
    }
}
