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
        public double DoMoCuaTran_h { get; set; } = 0;
        public int SoCuaMo { get; set; } = 1;
        public double HeSoCoHep_ALpha { get; set; } = 0;

        public double Q_CongSo1 { get; set; } = 0;
        public double Q_CongSo2 { get; set; } = 0;
        public double Q_CongSo3 { get; set; } = 0;
    }
}
