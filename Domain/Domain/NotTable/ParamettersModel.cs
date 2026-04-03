using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ParamettersModel
    {
        public double HeSoLuuToc_Phi { get; set; } = 0.5;
        public double GiaToc_G { get; set; } = 0;
        public double CaoTrinhNguongTran_Zn { get; set; } = 0;
        public int SoCuaMo { get; set; } = 1;
        public double HeSoCoHep_ALpha { get; set; } = 0;

        public double Q_CongSo1 { get; set; } = 0;
        public double Q_CongSo2 { get; set; } = 0;
        public double Q_CongSo3 { get; set; } = 0;
        public double MNTL_CongSo1 { get; set; } = 0;
        public double MNTL_CongSo2 { get; set; } = 0;
        public double MNTL_CongSo3 { get; set; } = 0;
        public double MNHL_CongSo1 { get; set; } = 0;
        public double MNHL_CongSo2 { get; set; } = 0;
        public double MNHL_CongSo3 { get; set; } = 0;
        public double DoMoCua_a_CongSo1 { get; set; } = 0;
        public double DoMoCua_a_CongSo2 { get; set; } = 0;
        public double DoMoCua_a_CongSo3 { get; set; } = 0;

        /// <summary>
        /// Giá trị mức nước nhập tay.
        /// </summary>
        public double Fllow_HL_TXL { get; set; } = 0;
    }
}
