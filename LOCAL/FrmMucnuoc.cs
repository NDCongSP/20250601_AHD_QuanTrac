using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace RegistrationForm1
{
    public partial class FrmMucnuoc : Form
    {
        private Timer _timer = new Timer();
        private ToolTip tooltip = new ToolTip();

        public FrmMucnuoc()
        {
            InitializeComponent();
            LoadChart();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();


        }
        private void LoadChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();
            chart1.Titles.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Title = "Vị trí (km)";
            area.AxisY.Title = "Mực nước (m)";
            area.AxisY.Minimum = -2; 
            area.AxisY.Maximum = 10;  
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisX.Interval = 5;
            area.AxisX.LabelStyle.Angle = -90;  // 🔄 Xoay nhãn song song trục Y
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            area.AxisX.IsLabelAutoFit = false;
            chart1.ChartAreas.Add(area);

            Legend legend = new Legend("Legend1")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center
            };
            chart1.Legends.Add(legend);

            chart1.Titles.Add("Diễn biến mực nước trên sông sài gòn");
            chart1.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // === Dữ liệu ===
            var data = new List<(double KCach, double BoPhai, double BoTrai, double Q300, double Q400, double Q600, double Q800, double Q2800, string Vitri)>
 {
  (     0,   7.26, 4.42, 4.517, 5.129, 5.938, 6.509, 9.697, "Chân đập --"),
        (1.5, 5.39, 4.68, 4.109, 4.65, 5.441, 5.973, 9.132, "Cầu Mới --"),
        (3.5, 5.13, 4.99, 3.95, 4.478, 5.207, 5.729, 8.832, "Km 3.5 --"),
        (5.5, 4.02, 4.01, 3.713, 4.225, 4.927, 5.43, 8.397, "Km 5.5 --"),
        (7.5, 5.3,  7.91, 3.313, 3.753, 4.381, 4.834, 7.559, "Trạm Sơn Đài --"),
        (8.5, 5.3,  7.91, 3.313, 3.753, 4.381, 4.834, 7.559 ,"Cầu bến củi --"),
        (9.5, 6.59, 3.9,  3.247, 3.688, 4.316, 4.768, 7.497, "Trạm Dầu Tiếng --"),
        (10.5, 5.305, 3.975, 3.203, 3.643, 4.267, 4.716, 7.412, "Km 10.5 --"),
        (12.5, 4.02, 4.05, 3.018, 3.45, 4.059, 4.496, 7.114, "Km 12.5 --"),
        (14.5, 3.69, 3.22, 2.835, 3.246, 3.836, 4.273, 6.884, "Km 14.5 --"),
        (16.5, 3.32, 3.91, 2.622, 3.008, 3.58, 3.994, 6.483, "Km 16.5 --"),
        (18.5, 2.93, 2.74, 2.459, 2.851, 3.431, 3.833, 6.283, "Km 18.5 --"),
        (20.6, 2.45, 2.21, 2.245, 2.635, 3.223, 3.614, 6.085, "Km 20.6 --"),
        (22.6, 3.69, 2.8,  2.082, 2.445, 2.997, 3.368, 5.825, "Km 22.6 --"),
        (24.6, 2.85, 3.59, 1.811, 2.075, 2.557, 2.901, 5.507, "Km 24.6 --"),
        (26.6, 1.58, 1.98, 1.72, 1.899, 2.309, 2.672, 5.409, "Km 26.6 --"),
        (28.6, 1.78, 2.71, 1.642, 1.759, 2.023, 2.391, 5.222, "Km 28.6 --"),
        (30.6, 1.91, 1.94, 1.638, 1.755, 2.017, 2.373, 5.157, "Cầu Thạnh An --"),
        (34.7, 1.82, 1.79, 1.627, 1.741, 1.994, 2.326, 5.03, "Bến Dược --"),
        (38.7, 2.48, 1.66, 1.614, 1.713, 1.926, 2.188, 4.824, "Km 38.7 --"),
        (42.7, 1.47, 1.48, 1.592, 1.682, 1.873, 2.097, 4.53, "Trạm Bến Súc --"),
        (46.7, 1.08, 1.49, 1.563, 1.647, 1.838, 2.031, 4.293, "Km 46.7 --"),
        (50.7, 1.68, 2.76, 1.545, 1.629, 1.825, 1.996, 4.129, "Km 50.7 --"),
        (54.8, 1.43, 2.23, 1.547, 1.622, 1.801, 1.949, 3.789, "Km 54.8 --"),
        (58.8, 2.32, 2.26, 1.538, 1.607, 1.77, 1.908, 3.527, "Km 58.8 --"),
        (62.8, 0.78, 2.73, 1.519, 1.581, 1.738, 1.887, 3.355, "Km 62.8 --"),
        (66.8, 1.64, 2.67, 1.513, 1.587, 1.725, 1.856, 2.972, "Km 66.8 --"),
        (70.8, 1.67, 1.35, 1.525, 1.592, 1.718, 1.841, 2.804, "Láng The --"),
        (71.8, 1.71, 1.46, 1.541, 1.601, 1.717, 1.833, 2.783, "Km 71.8 --"),
        (73.8, 2.64, 2.43, 1.549, 1.603, 1.709, 1.817, 2.689, "Km 73.8 --"),
        (75.9, 0.88, 2.67, 1.547, 1.597, 1.695, 1.796, 2.592, "Thị Tính --"),
        (80.9, 1.81, 2.58, 1.552, 1.592, 1.667, 1.755, 2.461, "Km 80.9 --"),
        (84.9, 1.51, 1.91, 1.58, 1.613, 1.678, 1.756, 2.393, "Km 84.9 --"),
        (86.9, 1.95, 1.68, 1.587, 1.618, 1.678, 1.752, 2.35, "Km 86.9 --"),
        (87.9, 2.11, 2.07, 1.592, 1.622, 1.679, 1.751, 2.333, "Phú Cường --"),
        (91.9, 3.15, 2.2,  1.602, 1.627, 1.677, 1.74, 2.256, "Km 91.9 --"),
        (93.8, 1.63, 2.28, 1.603, 1.626, 1.672, 1.732, 2.24, "Rạch Tra --"),
        (97,  1.79, 2.07, 1.623, 1.64, 1.674, 1.717, 2.178, "Trạm bình nhâm --"),
        (101, 1.79, 2.07, 1.623, 1.64, 1.674, 1.717, 2.178, "Phú Long --")
 };


            // === Series ===
            var sBoPhai = CreateSeries("Bờ phải", Color.SaddleBrown);
            var sBoTrai = CreateSeries("Bờ trái", Color.DimGray);
            var s300 = CreateSeries("Q=300", Color.Blue);
            var s400 = CreateSeries("Q=400", Color.Green);
            var s600 = CreateSeries("Q=600", Color.Orange);
            var s800 = CreateSeries("Q=800", Color.Red);
            var s2800 = CreateSeries("Q=2800", Color.Purple);

            chart1.Series.Add(sBoPhai);
            chart1.Series.Add(sBoTrai);
            chart1.Series.Add(s300);
            chart1.Series.Add(s400);
            chart1.Series.Add(s600);
            chart1.Series.Add(s800);
            chart1.Series.Add(s2800);

            // === Thêm dữ liệu ===
            foreach (var d in data)
            {
                sBoPhai.Points.AddXY(d.KCach, d.BoPhai);
                sBoTrai.Points.AddXY(d.KCach, d.BoTrai);
                s300.Points.AddXY(d.KCach, d.Q300);
                s400.Points.AddXY(d.KCach, d.Q400);
                s600.Points.AddXY(d.KCach, d.Q600);
                s800.Points.AddXY(d.KCach, d.Q800);
                s2800.Points.AddXY(d.KCach, d.Q2800);

                // 🏷️ Nhãn vị trí song song trục Y
                var label = new CustomLabel(d.KCach - 1, d.KCach + 1, d.Vitri, 0, LabelMarkStyle.None);
                area.AxisX.CustomLabels.Add(label);
            }
            // === Thêm line mực nước API từ SQL ===
            try
            {
                using var dbContext = new ApplicationDbContext();

                var latest = dbContext.FT03s
                    .AsNoTracking()
                    .Where(x => (x.IsDeleted ?? false) == false && x.CreateAt.HasValue)
                    .OrderByDescending(x => x.CreateAt)
                    .FirstOrDefault();

                if (latest == null)
                {
                    MessageBox.Show("Không có dữ liệu mực nước mới nhất trong bảng FT03s.");
                    return;
                }

                Color apiColor = Color.DeepSkyBlue;

                var apiPoints = new List<(string Name, double KCach, double? Value)>
    {
        ("API_ChanDap", 0, latest.API_ChanDap),
        ("API_Fllow_SonDai", 7.5, latest.API_Fllow_SonDai),
        ("API_Fllow_DauTieng", 9.5, latest.API_Fllow_DauTieng),
        ("API_Fllow_ThanhAn", 30.6, latest.API_ThanhAn),
        ("API_Fllow_BenSuc", 42.7, latest.API_Fllow_BenSuc),
        ("API_Fllow_BinhNham", 97, latest.API_Fllow_BinhNham),
        ("", 101, 1.117)
    };

                // Lọc điểm hợp lệ
                var validPoints = apiPoints.Where(p => p.Value.HasValue).ToList();

                // 🔹 Series vùng tô (Area)
                Series sApiFill = new Series("Vùng mực nước")
                {
                    ChartType = SeriesChartType.Area,
                    Color = Color.FromArgb(80, apiColor),
                    BorderColor = Color.Transparent,
                    IsVisibleInLegend = false,
                    ["PointWidth"] = "1"
                };

                // 🔹 Series đường mực nước (Spline)
                Series sApiLine = new Series("Mực nước trên sông Sài Gòn")
                {
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 3,
                    Color = apiColor,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 8,
                    MarkerColor = apiColor,
                    LegendText = "Mực nước trên sông Sài Gòn"
                };

                var mainArea = chart1.ChartAreas["MainArea"];
                mainArea.AxisY.Crossing = -2; // Dời trục X xuống -2

                // --- Vẽ vùng ---
                // 1️⃣ Thêm toàn bộ các điểm mực nước thật (đường trên)
                foreach (var p in validPoints)
                    sApiFill.Points.AddXY(p.KCach, p.Value.Value);

                // 2️⃣ Thêm ngược lại các điểm đáy (đường dưới, Y = -2)
                for (int i = validPoints.Count - 1; i >= 0; i--)
                    sApiFill.Points.AddXY(validPoints[i].KCach, -2);

                // --- Vẽ đường spline mực nước ---
                foreach (var p in validPoints)
                    sApiLine.Points.AddXY(p.KCach, p.Value.Value);

                // --- Thêm vào biểu đồ ---
                chart1.Series.Add(sApiFill);
                chart1.Series.Add(sApiLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải mực nước API: " + ex.ToString());
            }



            // --- Tooltip chung khi rê chuột ---
            chart1.GetToolTipText += (s, e) =>
            {
                if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
                {
                    var point = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = $"{e.HitTestResult.Series.Name}\nK={point.XValue} km\nH={point.YValues[0]:0.00} m";
                }
            };

            // 🟡 Hiển thị tooltip tổng hợp khi rê chuột
            chart1.MouseMove += Chart1_MouseMove;


            // 🎨 Sự kiện click legend => làm mờ/hiện line
            chart1.MouseClick += Chart1_MouseClick;
        }
        // === Ẩn tooltip khi form mất focus hoặc bị ẩn ===
        // ... (Các hàm OnDeactivate, OnVisibleChanged, OnFormClosing, OnShown giữ nguyên)
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            tooltip.Hide(chart1);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!this.Visible)
            {
                tooltip.Hide(chart1);
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            tooltip.Hide(chart1);
            _timer.Stop();
            base.OnFormClosing(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _timer.Start();
        }

        private Series CreateSeries(string name, Color color)
        {
            var s = new Series(name)
            {
                 ChartType = SeriesChartType.Line,
               // ChartType = SeriesChartType.SplineArea,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 5,
                ChartArea = "MainArea",
                Color = color,

                // 💡 Đảm bảo tên hiển thị đúng
                LegendText = name,
            };

            // 🔑 QUAN TRỌNG: Ép buộc kiểu hiển thị Legend là hình chữ nhật tô màu
            // Thuộc tính này ghi đè lên kiểu Legend mặc định của SeriesChartType.Line
            s["LegendMarkerStyle"] = "Area"; // <--- ĐÂY LÀ CHÌA KHÓA

            // Lưu màu gốc vào Tag
            s.Tag = color;
            return s;
        }

        // 🟡 Hiển thị tooltip tất cả giá trị tại X gần nhất (có màu line tương ứng)
        // ... (Hàm Chart1_MouseMove giữ nguyên)
        private void Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var area = chart1.ChartAreas[0];

            try
            {
                double xVal = area.AxisX.PixelPositionToValue(pos.X);

                if (xVal < area.AxisX.Minimum || xVal > area.AxisX.Maximum)
                {
                    tooltip.Hide(chart1);
                    return;
                }

                // 🔹 Lọc ra tất cả X hợp lệ từ các series hiển thị (trừ Area)
                var allX = chart1.Series
                    .Where(s => s.Enabled && s.Points.Count > 0 && s.ChartType != SeriesChartType.Area)
                    .SelectMany(s => s.Points.Select(p => p.XValue))
                    .Distinct()
                    .OrderBy(v => v)
                    .ToList();

                if (!allX.Any())
                {
                    tooltip.Hide(chart1);
                    return;
                }

                double closestX = allX.OrderBy(v => Math.Abs(v - xVal)).First();

                // 🔹 Duyệt tất cả series hiển thị (trừ Area)
                var lines = chart1.Series
                    .Where(s => s.Enabled && s.ChartType != SeriesChartType.Area &&
                                s.Points.Any(p => Math.Abs(p.XValue - closestX) < 0.001))
                    .Select(s =>
                    {
                        var p = s.Points.First(pt => Math.Abs(pt.XValue - closestX) < 0.001);

                        // Lấy màu hiển thị (ưu tiên MarkerColor hoặc Color chính)
                        Color displayColor = s.MarkerColor.A > 0 ? s.MarkerColor : s.Color;
                        string colorSymbol = GetColorSymbol(displayColor);

                        return $"{colorSymbol} {s.LegendText.PadRight(12)}: {p.YValues[0]:0.###} m";
                    })
                    .ToList();

                if (lines.Any())
                {
                    string text = $"Vị trí: {closestX:0.###} km\n" + string.Join("\n", lines);
                    tooltip.Show(text, chart1, e.X + 15, e.Y - 20);
                }
                else
                {
                    tooltip.Hide(chart1);
                }
            }
            catch
            {
                tooltip.Hide(chart1);
            }
        }

        // 🌈 Hàm trả về ký hiệu màu tương ứng
        private string GetColorSymbol(Color color)
        {
            // Không cần kiểm tra Alpha (s.Color.A) vì Line không bị thay đổi màu/Alpha khi dùng s.Enabled
            if (color == Color.Blue) return "🟦";
            if (color == Color.Green) return "🟩";
            if (color == Color.Orange) return "🟧";
            if (color == Color.Red) return "🟥";
            if (color == Color.Purple) return "🟪";
            if (color == Color.SaddleBrown) return "🟫";
            if (color == Color.DimGray) return "⚫";
            if (color == Color.DeepSkyBlue) return "🔵"; // Thêm màu cho API
            return "⬜"; // mặc định
        }

        private void Chart1_MouseClick(object sender, MouseEventArgs e)
        {
            _timer.Enabled = false;

            try
            {
                var result = chart1.HitTest(e.X, e.Y);
                if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    Series s = result.Series;

                    // Nếu Tag chưa chứa màu gốc thì lưu lại
                    if (!(s.Tag is Color))
                        s.Tag = s.Color;

                    Color originalColor = (Color)s.Tag;

                    bool isCurrentlyVisible = s.Color.A != 0; // hiển thị khi alpha != 0

                    if (isCurrentlyVisible)
                    {
                        // Ẩn: làm trong suốt mọi thành phần hiển thị nhưng GIỮ Legend
                        s.Color = Color.FromArgb(0, s.Color);            // vùng/shape
                        s.MarkerColor = Color.FromArgb(0, s.MarkerColor);
                        s.BorderColor = Color.FromArgb(0, s.BorderColor);

                        // Nếu là Area và bạn muốn ẩn vùng nhưng vẫn giữ border khi visible, 
                        // ở đây ta đã đặt BorderColor trong suốt.
                        s.IsValueShownAsLabel = false;

                        // Option: để người dùng thấy mục legend "mờ", bạn có thể thay đổi LegendText:
                        // s.LegendText = s.LegendText + " (ẩn)";
                    }
                    else
                    {
                        // Hiện lại: khôi phục màu gốc từ Tag
                        s.Color = originalColor;

                        // Nếu Series là vùng mà bạn dùng alpha thấp cho vùng, khôi phục alpha vùng:
                        if (s.Name == "Mực nước trên sông sài gòn")
                        {
                            s.Color = Color.FromArgb(80, originalColor); // vùng bán trong suốt
                            s.BorderColor = originalColor;
                            s.MarkerColor = originalColor;
                        }
                        else
                        {
                            s.MarkerColor = originalColor;
                            s.BorderColor = originalColor;
                        }

                        s.IsValueShownAsLabel = false;

                        // Nếu trước kia bạn thay đổi LegendText khi ẩn -> phục hồi:
                        // s.LegendText = s.LegendText.Replace(" (ẩn)", "");
                    }

                    chart1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xử lý Legend Click: {ex.Message}");
            }
            finally
            {
                _timer.Enabled = true;
            }
        }




        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false; // Tạm dừng timer trong quá trình xử lý

                if (Globalvariable.RealtimeDisplays.Count == 0)
                    return; // Nếu không có dữ liệu, thoát khỏi hàm

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    var location = Globalvariable.RealtimeDisplays?.FirstOrDefault(loc => loc.LocationId == 1);
                    if (location != null)
                    {
                        foreach (var item in location.Stations)
                        {
                            if (item.Path == "Local Station/DauTieng/S71500/Station_1")
                            {

                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_2")
                            {
                                //_labALDoor1_Station2.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station2.Text = item.Al_Door2.ToString();
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_3")
                            {
                                //_labALDoor1_Station3.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station3.Text = item.Al_Door2.ToString();
                            }

                            _labFllow_ho.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho.ToString();
                            _labAPISondai.Text = location.CalculatorValue.API_Fllow_SonDai.ToString();
                            _labAPIBensuc.Text = location.CalculatorValue.API_Fllow_BenSuc.ToString();
                            _labAPIDautieng.Text = location.CalculatorValue.API_Fllow_DauTieng.ToString();
                            _labAPIBinhnham.Text = location.CalculatorValue.API_Fllow_BinhNham.ToString();
                            //  _labAPIBinhnham2.Text = location.CalculatorValue.Fllow_BinhNham2.ToString();
                            _labAPICDD.Text = location.CalculatorValue.API_Fllow_TL_CDD.ToString();


                        }

                    }


                });

            }

            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Enabled = true;
            }
        }




        private void bntMap_Click(object sender, EventArgs e)
        {
            FrmMap Map = new FrmMap();
            Map.ShowDialog();

        }
    }
}