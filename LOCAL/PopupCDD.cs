using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RegistrationForm1
{
    public partial class PopupCDD : Form
    {
        public PopupCDD()
        {
            InitializeComponent();
            cbFrequency.Items.AddRange(new object[] { "Tất cả", "10 phút", "15 phút", "30 phút", "60 phút", "1 Ngày" });
            cbFrequency.SelectedIndex = 0;
            cbFrequency.SelectedIndexChanged += cbFrequency_SelectedIndexChanged;

            chartMucNuocCDD.GetToolTipText += ChartMucNuoc_GetToolTipText;

        }
        private void DrawChart(List<dynamic> data)
        {
            chartMucNuocCDD.Series.Clear();
            chartMucNuocCDD.ChartAreas.Clear();

            var chartArea = new ChartArea("Cống dẫn dòng");
            chartArea.AxisX.LabelStyle.Format = "HH:mm";
            chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea.AxisY.Title = "Mực nước (cm)";
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

            // ⚙️ Thêm khoảng cách 1 giờ cho trục X
            chartArea.AxisX.IntervalType = DateTimeIntervalType.Hours;
            chartArea.AxisX.Interval = 1; // mỗi 1 giờ hiển thị 1 vạch

            // Nếu dữ liệu có ngày giờ đầy đủ, ta có thể set min/max để căn chuẩn trục X
            if (data.Any())
            {
                DateTime minTime = data.Min(d => (DateTime)d.Thời_gian);
                DateTime maxTime = data.Max(d => (DateTime)d.Thời_gian);

                chartArea.AxisX.Minimum = minTime.ToOADate();
                chartArea.AxisX.Maximum = maxTime.ToOADate();
            }

            chartMucNuocCDD.ChartAreas.Add(chartArea);

            var series = new Series("Cống dẫn dòng")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6,
                XValueType = ChartValueType.DateTime // ⚙️ Cực kỳ quan trọng để trục X hiểu là dữ liệu thời gian
            };

            foreach (var item in data)
            {
                series.Points.AddXY(item.Thời_gian, item.Mực_nước_CDD);
            }

            chartMucNuocCDD.Series.Add(series);

            // Tắt crosshair line — chỉ hiển thị tooltip
            chartMucNuocCDD.ChartAreas[0].CursorX.IsUserEnabled = false;
            chartMucNuocCDD.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            chartMucNuocCDD.ChartAreas[0].CursorY.IsUserEnabled = false;
            chartMucNuocCDD.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;

            // Bật tooltip cho từng điểm
            chartMucNuocCDD.Series[0].ToolTip = "⏰ #VALX{dd/MM/yyyy HH:mm}\n🌊 #VALY{F2} cm";
        }

        private void PopupCDD_Load(object sender, EventArgs e)
        {
            LoadAndDisplayData();
        }

        
        private void LoadAndDisplayData()
        {
            // Lấy dữ liệu 24 giờ gần nhất tính từ lúc click truy xuất
            DateTime toTime = DateTime.Now;
            DateTime fromTime = toTime.AddHours(-24);

            string selectedFrequency = cbFrequency.SelectedItem?.ToString() ?? "Tất cả";

            try
            {
                using var dbContext = new ApplicationDbContext();

                var rawData = dbContext.FT03s
                    .AsNoTracking()
                    .Where(x => (x.IsDeleted ?? false) == false
                                && x.CreateAt.HasValue
                                && x.CreateAt.Value >= fromTime
                                && x.CreateAt.Value <= toTime
                                && x.StationName == "Location_Info")
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        Thời_gian = x.CreateAt.Value,
                        Mực_nước_CDD = x.API_Fllow_TL_CDD
                    })
                    .ToList();

                // Ánh xạ dữ liệu
                var mappedData = rawData
                    .Select(x => new
                    {
                        Thời_gian = x.Thời_gian,
                        Mực_nước_CDD = x.Mực_nước_CDD
                    })
                    .ToList();

                // Lọc theo tần suất
                int frequencyMinutes = selectedFrequency switch
                {
                    "10 phút" => 10,
                    "15 phút" => 15,
                    "30 phút" => 30,
                    "60 phút" => 60,
                    "1 Ngày" => 1440,
                    _ => 0
                };

                var filteredData = new List<dynamic>();

                if (frequencyMinutes > 0)
                {
                    DateTime? lastTime = null;
                    foreach (var row in mappedData.OrderBy(x => x.Thời_gian))
                    {
                        if (lastTime == null || (row.Thời_gian - lastTime.Value).TotalMinutes >= frequencyMinutes)
                        {
                            filteredData.Add(row);
                            lastTime = row.Thời_gian;
                        }
                    }
                }
                else
                {
                    filteredData = mappedData.Cast<dynamic>().ToList();
                }

                // Gán dữ liệu vào DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filteredData;

                if (dataGridView1.Columns.Contains("Thời_gian"))
                    dataGridView1.Columns["Thời_gian"].HeaderText = "Thời gian";
                if (dataGridView1.Columns.Contains("Mực_nước_CDD"))
                    dataGridView1.Columns["Mực_nước_CDD"].HeaderText = "Mực nước CDD (cm)";

                if (filteredData.Count > 0)
                    DrawChart(filteredData);
                else
                    chartMucNuocCDD.Series.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ChartMucNuoc_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                var point = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                DateTime time = DateTime.FromOADate(point.XValue);
                double value = point.YValues[0];

                e.Text = $"⏰ {time:dd/MM/yyyy HH:mm}\n🌊 {value:F2} cm";
            }
        }

      
        private void cbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAndDisplayData();
        }
    }
}

