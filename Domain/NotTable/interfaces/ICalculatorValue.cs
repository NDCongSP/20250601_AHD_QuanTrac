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
        public double? API_Fllow_DauTieng { get; set; }
        public double? API_Fllow_BenSuc { get; set; }
        public double? API_Fllow_SonDai { get; set; }
        public double? API_Fllow_BinhNham { get; set; }
        public double? API_Fllow_BinhNham2 { get; set; }
        public double? API_Fllow_TL_CDD { get; set; }
        public double? API_Fllow_HL_TXL { get; set; }

        //6100001
        public double? API_DM_HoDT { get; set; }
        //6100002
        public double? API_MinhHoa { get; set; }
        //6100003
        public double? API_MinhTam { get; set; }
        //6100004
        public double? API_LocThien { get; set; }
        //6100005
        public double? API_LocNinh { get; set; }
        //6100006
        public double? API_LocThanh { get; set; }
        //6100007
        public double? API_ThanhLuong { get; set; }
        //6100008
        public double? API_TanHoa1 { get; set; }
        //6100009
        public double? API_TanHoa2 { get; set; }
        //6100010
        public double? API_KaTum { get; set; }
        //6100011
        public double? API_TanThanh { get; set; }
        //6100012
        public double? API_DongBan { get; set; }
        //6100013
        public double? API_TanHa { get; set; }
        //6100014
        public double? API_Doi95 { get; set; }

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
