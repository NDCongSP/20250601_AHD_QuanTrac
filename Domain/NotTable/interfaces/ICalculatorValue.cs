using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICalculatorValue
    {
        #region API
        /// <summary>
        /// trạm Dầu Tiếng.
        /// </summary>
        public double? API_Fllow_DauTieng { get; set; }

        /// <summary>
        /// trạm Bến Súc.
        /// </summary>
        public double? API_Fllow_BenSuc { get; set; }

        /// <summary>
        /// trạm sơn đài.
        /// </summary>
        public double? API_Fllow_SonDai { get; set; }

        /// <summary>
        /// Trạm bình nhâm.
        /// </summary>
        public double? API_Fllow_BinhNham { get; set; }

        //Giá trị tính toán từ API value

        /// <summary>
        /// Tinh toán dựa vào giá trị của API_Fllow_SonDai.
        /// chân đập = Sơn đài + (8.4 x 0.00001 x 7500).
        /// </summary>
        public double? API_ChanDap { get; set; }

        /// <summary>
        /// tính toán dựa vào giá trị của API_Fllow_BenSuc và API_Fllow_BinhNham.
        /// TH1 (bến súc > = bình nhâm : thanh an = bến súc + (8.4 x 0.00001 x 11150).
        /// TH2 (bến súc < bình nhâm : thanh an = bến súc - (8.4 x 0.00001 x 11150).
        /// </summary>
        public double? API_ThanhAn { get; set; }

        public double? API_Fllow_BinhNham2 { get; set; }
        public double? API_Fllow_TL_CDD { get; set; }
        public double? API_Fllow_HL_TXL { get; set; }

        //6100001 
        /// <summary>
        /// Prefix API_D lad sẽ hiển thị ở grid lượng mưa.
        /// </summary>
        public double? API_D_DM_HoDT { get; set; }
        public double? API_D_DM_HoDT_Total { get; set; }
        //6100002
        public double? API_D_MinhHoa { get; set; }
        public double? API_D_MinhHoa_Total { get; set; }
        //6100003
        public double? API_D_MinhTam { get; set; }
        public double? API_D_MinhTam_Total { get; set; }
        //6100004
        public double? API_D_LocThien { get; set; }
        public double? API_D_LocThien_Total { get; set; }
        //6100005
        public double? API_D_LocNinh { get; set; }
        public double? API_D_LocNinh_Total { get; set; }
        //6100006
        public double? API_D_LocThanh { get; set; }
        public double? API_D_LocThanh_Total { get; set; }
        //6100007
        public double? API_D_ThanhLuong { get; set; }
        public double? API_D_ThanhLuong_Total { get; set; }
        //6100008
        public double? API_D_TanHoa1 { get; set; }
        public double? API_D_TanHoa1_Total { get; set; }
        //6100009
        public double? API_D_TanHoa2 { get; set; }
        public double? API_D_TanHoa2_Total { get; set; }
        //6100010
        public double? API_D_KaTum { get; set; }
        public double? API_D_KaTum_Total { get; set; }
        //6100011
        public double? API_D_TanThanh { get; set; }
        public double? API_D_TanThanh_Total { get; set; }
        //6100012
        public double? API_D_DongBan { get; set; }
        public double? API_D_DongBan_Total { get; set; }
        //6100013
        public double? API_D_TanHa { get; set; }
        public double? API_D_TanHa_Total { get; set; }
        //6100014
        public double? API_D_Doi95 { get; set; }
        public double? API_D_Doi95_Total { get; set; }

        #endregion

        ////Calculator
        //public double? Total_Fllow { get; set; }
        //public double Q_Den { get; set; }
        //public double Q_Di { get; set; }
        //public double W_Ho { get; set; }
        //public double Qtr { get; set; }
        //public double LuuLuong { get; set; }
        //public double LuuLuongTong { get; set; }

        public double W1_ho { get; set; }
        public double W1_ho_old { get; set; }
        public double W2_ho { get; set; }
        public double W2_ho_old { get; set; }
        public double Q_den { get; set; }
        public double W_den { get; set; }
        public double Q_i_total { get; set; }
        public double Q_tr { get; set; }
        public double W_tr { get; set; }
        public double Q_cs1 { get; set; }
        public double W_cs1 { get; set; }
        public double Q_cs2 { get; set; }
        public double W_cs2 { get; set; }
        public double Q_cs3 { get; set; }
        public double W_cs3 { get; set; }
        public double Q_tt { get; set; }
        public double W_tt { get; set; }
        public double Q_di { get; set; }
        public double W_di { get; set; }
        public double Q_denta { get; set; }
    }
}
