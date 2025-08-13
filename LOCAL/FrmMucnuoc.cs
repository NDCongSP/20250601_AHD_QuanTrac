using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;

namespace RegistrationForm1
{
    public partial class FrmMucnuoc : Form
    {
        private Chart chart;
        private ChartArea ca;

        public FrmMucnuoc()
        {
            InitializeComponent(); // Đảm bảo phương thức này được gọi để khởi tạo các điều khiển trên Form
            // Căn giữa biểu đồ trên form
        //   chart.Location = new Drawing.Point((this.ClientSize.Width - chart.Width) / 2, (this.ClientSize.Height - chart.Height) / 2);
 

            SetupChart();
          
        }
        
        
        
        private void SetupChart()
        {
            
        }
        
        

        
       

        

    }
}
