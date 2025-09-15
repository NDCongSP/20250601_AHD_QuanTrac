using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TagsStation : ITagsStationsDouble, ITagLocationInfo
    {
        public string Path { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; } = string.Empty;


        public bool? Remote { get; set; } = false;
        public bool? Local { get; set; } = false;
        public bool? Auto { get; set; } = false;
        public bool? Man { get; set; } = false;
        public bool? Local_Stop { get; set; } = false;
        public bool? DC1_Running { get; set; } = false;
        public bool? DC2_Running { get; set; } = false;
        public bool? DC3_Running { get; set; } = false;
        public bool? Door1_Opening { get; set; } = false;
        public bool? Door1_Closing { get; set; } = false;
        public bool? Door2_Opening { get; set; } = false;
        public bool? Door2_Closing { get; set; } = false;
        public bool? Doorlock1_Opening { get; set; } = false;
        public bool? Doorlock1_Closing { get; set; } = false;
        public bool? Doorlock2_Opening { get; set; } = false;
        public bool? Doorlock2_Closing { get; set; } = false;
        public bool? Door1_Open { get; set; } = false;
        public bool? Door1_Close { get; set; } = false;
        public bool? Door2_Open { get; set; } = false;
        public bool? Door2_Close { get; set; } = false;
        public bool? Doorlock1_1Open { get; set; } = false;
        public bool? Doorlock1_1Close { get; set; } = false;
        public bool? Doorlock1_2Open { get; set; } = false;
        public bool? Doorlock1_2Close { get; set; } = false;
        public bool? Doorlock2_1Open { get; set; } = false;
        public bool? Doorlock2_1Close { get; set; } = false;
        public bool? Doorlock2_2Open { get; set; } = false;
        public bool? Doorlock2_2Close { get; set; } = false;
        public bool? DC1_Over { get; set; } = false;
        public bool? DC2_Over { get; set; } = false;
        public bool? DC3_Over { get; set; } = false;
        public bool? Door1_PressureHigh { get; set; } = false;
        public bool? Door1_PressureLow { get; set; } = false;
        public bool? Door2_PressureHigh { get; set; } = false;
        public bool? Door2_PressureLow { get; set; } = false;
        public bool? Al_Door1 { get; set; } = false;
        public bool? Al_Door2 { get; set; } = false;
        public bool? Temp_Oil_High { get; set; } = false;
        public bool? Temp_Oil_Low { get; set; } = false;
        public bool? Lock1 { get; set; } = false;
        public bool? Lock2 { get; set; } = false;
        public bool? Door1_Open_Door { get; set; } = false;
        public bool? Door2_Open_Door { get; set; } = false;

        public double? HT_Cylinder1_1 { get; set; } = 0;
        public double? HT_Cylinder1_2 { get; set; } = 0;
        public double? HT_Cylinder2_1 { get; set; } = 0;
        public double? HT_Cylinder2_2 { get; set; } = 0;
        public double? Door1_Aperture { get; set; } = 0;
        public double? Door2_Aperture { get; set; } = 0;
        public double? S1_Temp_Oil { get; set; } = 0;
        public double? Pressure_Oil_Door1 { get; set; } = 0;
        public double? Pressure_Oil_Door2 { get; set; } = 0;
        public double? Fllow_Door1 { get; set; } = 0;
        public double? Fllow_Door2 { get; set; } = 0;
        public double? HT_Cylinder1_1_Offset { get; set; } = 0;
        public double? HT_Cylinder1_2_Offset { get; set; } = 0;
        public double? HT_Cylinder2_1_Offset { get; set; } = 0;
        public double? HT_Cylinder2_2_Offset { get; set; } = 0;
        public double? Door1_Aperture_Offset { get; set; } = 0;
        public double? Door2_Aperture_Offset { get; set; } = 0;
        public double? S1_Temp_Oil_Offset { get; set; } = 0;
        public double? Pressure_Oil_Door1_Offset { get; set; } = 0;
        public double? Pressure_Oil_Door2_Offset { get; set; } = 0;
        public double? Fllow_Door1_Offset { get; set; } = 0;
        public double? Fllow_Door2_Offset { get; set; } = 0;
        public double? HT_Cylinder1_1_Final { get; set; } = 0;
        public double? HT_Cylinder1_2_Final { get; set; } = 0;
        public double? HT_Cylinder2_1_Final { get; set; } = 0;
        public double? HT_Cylinder2_2_Final { get; set; } = 0;
        public double? Door1_Aperture_Final { get; set; } = 0;
        public double? Door2_Aperture_Final { get; set; } = 0;
        public double? S1_Temp_Oil_Final { get; set; } = 0;
        public double? Pressure_Oil_Door1_Final { get; set; } = 0;
        public double? Pressure_Oil_Door2_Final { get; set; } = 0;
        public double? Fllow_Door1_Final { get; set; } = 0;
        public double? Fllow_Door2_Final { get; set; } = 0;
        public double? Fllow_Ho { get; set; } = 0;
        public double? Fllow_Ho_Offset { get; set;} = 0;
        public double? Fllow_Ho_Final { get; set;} = 0;
        public double? Q_i { get; set; } = 0;
    }

    public class CalculatorValueModel : ICalculatorValue
    {
        public double? API_Fllow_DauTieng { get; set; } = 0;
        public double? API_Fllow_BenSuc { get; set; } = 0;
        public double? API_Fllow_SonDai { get; set; } = 0;
        public double? API_Fllow_BinhNham { get; set; } = 0;
        public double? API_Fllow_BinhNham2 { get; set; } = 0;
        public double? API_Fllow_TL_CDD { get; set; } = 0;
        public double? API_Fllow_HL_TXL { get; set; } = 0;

        public double? API_D_DM_HoDT { get; set; } = 0;
        public double? API_D_DM_HoDT_Total { get; set; } = 0;
        public double? API_D_MinhHoa { get; set; } = 0;
        public double? API_D_MinhHoa_Total { get; set; } = 0;
        public double? API_D_MinhTam { get; set; } = 0;
        public double? API_D_MinhTam_Total { get; set; } = 0;
        public double? API_D_LocThien { get; set; } = 0;
        public double? API_D_LocThien_Total { get; set; } = 0;
        public double? API_D_LocNinh { get; set; } = 0;
        public double? API_D_LocNinh_Total { get; set; } = 0;
        public double? API_D_LocThanh { get; set; } = 0;
        public double? API_D_LocThanh_Total { get; set; } = 0;
        public double? API_D_ThanhLuong { get; set; } = 0;
        public double? API_D_ThanhLuong_Total { get; set; } = 0;
        public double? API_D_TanHoa1 { get; set; } = 0;
        public double? API_D_TanHoa1_Total { get; set; } = 0;
        public double? API_D_TanHoa2 { get; set; } = 0;
        public double? API_D_TanHoa2_Total { get; set; } = 0;
        public double? API_D_KaTum { get; set; } = 0;
        public double? API_D_KaTum_Total { get; set; } = 0;
        public double? API_D_TanThanh { get; set; } = 0;
        public double? API_D_TanThanh_Total { get; set; } = 0;
        public double? API_D_DongBan { get; set; } = 0;
        public double? API_D_DongBan_Total { get; set; } = 0;
        public double? API_D_TanHa { get; set; } = 0;
        public double? API_D_TanHa_Total { get; set; } = 0;
        public double? API_D_Doi95 { get; set; } = 0;
        public double? API_D_Doi95_Total { get; set; } = 0;

        public double W1_ho { get; set; } = 0;
        public double W1_ho_old { get; set; } = 0;
        public double W2_ho { get; set; } = 0;
        public double W2_ho_old { get; set; } = 0;
        public double Q_den { get; set; } = 0;
        public double W_den { get; set; } = 0;
        public double Q_tr { get; set; } = 0;
        public double W_tr { get; set; } = 0;
        public double Q_cs1 { get; set; } = 0;
        public double W_cs1 { get; set; } = 0;
        public double Q_cs2 { get; set; } = 0;
        public double W_cs2 { get; set; } = 0;
        public double Q_cs3 { get; set; } = 0;
        public double W_cs3 { get; set; } = 0;
        public double Q_tt { get; set; } = 0;
        public double W_tt { get; set; } = 0;
        public double Q_di { get; set; } = 0;
        public double W_di { get; set; } = 0;
        public double Q_denta { get; set; } = 0;
        public double Q_i_total { get; set; } = 0;
    }
}