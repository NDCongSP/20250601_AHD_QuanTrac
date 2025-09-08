using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICalculatorValue
    {
        //API
        public double? Fllow_DauTieng { get; set; }
        public double? Fllow_BenSuc { get; set; }
        public double? Fllow_SonDai { get; set; }
        public double? Fllow_BinhNham { get; set; }
        public double? Fllow_BinhNham2 { get; set; }
        public double? Fllow_TL_CDD { get; set; }
        public double? Fllow_HL_TXL { get; set; }

        //Calculator
        public double? Total_Fllow { get; set; }
        public double Q_Den { get; set; }
        public double Q_Di { get; set; }
        public double W_Ho { get; set; }
        public double Qtr { get; set; }
        public double LuuLuong { get; set; }
        public double LuuLuongTong { get; set; }
    }
}
