using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Data Log.
    /// </summary>
    [Table("FT03")]
    public class FT03 : IGenericEntity, ITagsStationsDouble, ITagLocationInfo, ICalculatorValue
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Báo data log theo event tag valuchange hay theo timer.
        /// khi report thì where theo điều kiện này để lấy dữ liệu theo timer cho các giá trị value.
        /// còn không thì lấy theo event tag valuechange cho các thông số trạng thái kiểu bool.
        /// </summary>
        public bool LogBaseInterval { get; set; } = true;

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        /// <summary>
        /// Station Id.
        /// StationInfoModel.Id.
        /// </summary>
        [Display(Name = "Station Id")]
        public int StationId { get; set; }
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Station name.
        /// StationInfoModel.Name.
        /// </summary>
        [Display(Name = "Station Name")]
        public string? StationName { get; set; }
        public string? CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }

        //Tag station
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
        public double? Total_Fllow { get; set; } = 0;
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
        public double? Q_i_1 { get; set; } = 0;
        public double? Q_i_2 { get; set; } = 0;

        //Tag location information
        public double? Fllow_Ho { get; set; } = 0;
        public double? Fllow_Ho_Offset { get; set; } = 0;
        public double? Fllow_Ho_Final { get; set; } = 0;

        //Tag calculator value
        public double? API_Fllow_DauTieng { get; set; } = 0;
        public double? API_Fllow_BenSuc { get; set; } = 0;
        public double? API_Fllow_SonDai { get; set; } = 0;
        public double? API_Fllow_BinhNham { get; set; } = 0;

        public double? API_ChanDap { get; set; } = 0;
        public double? API_ThanhAn { get; set; } = 0;

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
        public double Q_i_total { get; set; } = 0;
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
        // mới thêm
        //public double? MNHL_CongSo1 { get; set; } = 0;
        //public double? MNHL_CongSo2 { get; set; } = 0;
        //public double? MNHL_CongSo3 { get; set; } = 0;
        //public double DoMoCua_a_CongSo1 { get; set; } = 0;
        //public double DoMoCua_a_CongSo2 { get; set; } = 0;
        //public double DoMoCua_a_CongSo3 { get; set; } = 0;
    }
}
