using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text; // Required for StringBuilder
using System.Globalization; // Required for CultureInfo.InvariantCulture
using System.Data.SqlClient; // Required for SQL Server operations


namespace RegistrationForm1
{
    public partial class FrmMucnuoc : Form
    {
        // Lưu màu gốc của từng series để khôi phục khi bật lại
        private Dictionary<string, Color> originalSeriesColors = new Dictionary<string, Color>();
        private Dictionary<string, bool> seriesDisplayState = new Dictionary<string, bool>();


        private Chart chart;
        private TableLayoutPanel tableLayoutPanelMain; // TableLayoutPanel for layout management
        //private Dictionary<string, Color> originalSeriesColors; // Dictionary to store original series colors
        //private Dictionary<string, bool> seriesDisplayState; // Dictionary to track series display state
        private TextAnnotation tooltipAnnotation; // Annotation for custom tooltip display
        private Timer _timer = new Timer();

        public FrmMucnuoc()
        {
            InitializeComponent();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();
            // Set initial form title and size
            this.Text = "Biểu đồ Mực nước theo Khoảng cách và Lưu lượng";

            // Initialize dictionaries for original colors and display states
            originalSeriesColors = new Dictionary<string, Color>();
            seriesDisplayState = new Dictionary<string, bool>();

            // Initialize TableLayoutPanel
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelMain.Dock = DockStyle.Fill; // TableLayoutPanel fills the entire form
            tableLayoutPanelMain.ColumnCount = 1; // One column
            tableLayoutPanelMain.RowCount = 2; // Two rows

            // Configure row heights: Top row (for controls) 38%, Bottom row (for chart) 62%
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 38F)); // Top row (can be used for other controls)
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 62F)); // Bottom row for the chart

            this.Controls.Add(tableLayoutPanelMain); // Add TableLayoutPanel to the form

            // Initialize chart
            chart = new Chart();
            // Set Dock property to Fill so the chart automatically fills and resizes with its container
            chart.Dock = DockStyle.Fill;
            // Add the chart directly to the second row of the TableLayoutPanel
            tableLayoutPanelMain.Controls.Add(chart, 0, 1); // Column 0, Row 1

            // Add chart area
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Add and configure Legend
            chart.Legends.Add("Legend1");
            chart.Legends["Legend1"].Docking = Docking.Top;
            chart.Legends["Legend1"].Alignment = StringAlignment.Center;
            chart.Legends["Legend1"].IsTextAutoFit = false;
            chart.Legends["Legend1"].TextWrapThreshold = 100;
            chart.Legends["Legend1"].Font = new Font("Arial", 9, FontStyle.Regular);

            // Assign mouse events to the chart
         //   chart.MouseClick += Chart_MouseClick;
            chart.MouseMove += Chart_MouseMove;
            chart.MouseLeave += Chart_MouseLeave;

            // Configure custom tooltip annotation
            tooltipAnnotation = new TextAnnotation();
            tooltipAnnotation.IsMultiline = true;
            tooltipAnnotation.Visible = false;
            tooltipAnnotation.AllowMoving = false;
            tooltipAnnotation.AnchorAlignment = ContentAlignment.TopLeft;
            tooltipAnnotation.ForeColor = Color.Yellow;
            tooltipAnnotation.BackColor = Color.Blue;
            tooltipAnnotation.LineColor = Color.White;
            tooltipAnnotation.LineWidth = 3;
            tooltipAnnotation.Font = new Font("Arial", 10, FontStyle.Regular);
            chart.Annotations.Add(tooltipAnnotation);

            // Load data and draw chart
            LoadDataAndDrawChart();
            ConfigureChart();
            // Gắn sự kiện click vào legend
            chart.MouseDown += Chart_MouseDown;
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
                            //  _labAPIBinhnham2.Text = location.CalculatorValue.Fllow_BinhNham2.ToString();
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

        private void LoadDataAndDrawChart()
        {
            try
            {
                // 1. Dọn chart trước khi vẽ
                chart.Series.Clear();
                chart.ChartAreas[0].AxisX.CustomLabels.Clear();

                // --- 1. Khai báo dữ liệu Q cố định ---
                List<double> xAxisData = new List<double>
        {
            0, 1.5, 3.5, 5.5, 8.5, 9.5, 10.5, 12.5, 14.5, 16.5, 18.5,
            20.6, 22.6, 24.6, 26.6, 28.6, 30.6, 34.7, 38.7, 42.7, 46.7, 50.7,
            54.8, 58.8, 62.8, 66.8, 70.8, 71.8, 73.8, 75.9, 80.9, 84.9,
            86.9, 87.9, 91.9, 93.8, 101, 101, 101
        };

                List<string> xLabels = new List<string>
        {
            "Bờ Trái", "Cống 1", "Cống 2", "Cống 3", "Cống 4",
            "Điểm 6", "Điểm 7", "Điểm 8", "Điểm 9", "Điểm 10",
            "Điểm 11", "Điểm 12", "Điểm 13", "Điểm 14", "Điểm 15",
            "Điểm 16", "Điểm 17", "Điểm 18", "Điểm 19", "Điểm 20",
            "Điểm 21", "Điểm 22", "Điểm 23", "Điểm 24", "Điểm 25",
            "Điểm 26", "Điểm 27", "Điểm 28", "Điểm 29", "Điểm 30",
            "Điểm 31", "Điểm 32", "Điểm 33", "Điểm 34", "Điểm 35",
            "Điểm 36", "Điểm 37", "Điểm 38"
        };

                List<List<double>> qValuesData = new List<List<double>>
        {
            new List<double> { 4.52, 4.11, 3.95, 3.71, 3.31, 3.25, 3.20, 3.02, 2.84, 2.62, 2.46, 2.25, 2.08, 1.81, 1.72, 1.64, 1.64, 1.63, 1.61, 1.59, 1.56, 1.55, 1.55, 1.54, 1.52, 1.51, 1.53, 1.54, 1.55, 1.55, 1.55, 1.58, 1.59, 1.59, 1.60, 1.60, 1.62, 1.62, 1.62 },
            new List<double> { 5.13, 4.65, 4.48, 4.23, 3.75, 3.69, 3.64, 3.45, 3.25, 3.01, 2.85, 2.64, 2.45, 2.08, 1.90, 1.76, 1.76, 1.74, 1.71, 1.68, 1.65, 1.63, 1.62, 1.61, 1.58, 1.59, 1.59, 1.60, 1.60, 1.60, 1.59, 1.61, 1.62, 1.62, 1.63, 1.63, 1.64, 1.64, 1.64 },
            new List<double> { 5.94, 5.44, 5.21, 4.93, 4.38, 4.32, 4.27, 4.06, 3.84, 3.58, 3.43, 3.22, 3.00, 2.56, 2.31, 2.02, 2.02, 1.99, 1.93, 1.87, 1.84, 1.83, 1.80, 1.77, 1.74, 1.73, 1.71, 1.69, 1.66, 1.66, 1.65, 1.66, 1.66, 1.65, 1.63, 1.64, 1.65, 1.65, 1.65 },
            new List<double> { 9.70, 9.13, 8.83, 8.40, 7.56, 7.50, 7.41, 7.11, 6.88, 6.48, 6.28, 6.09, 5.83, 5.51, 5.41, 5.22, 5.16, 5.03, 4.82, 4.53, 4.29, 4.13, 3.79, 3.53, 3.36, 2.97, 2.80, 2.78, 2.69, 2.59, 2.46, 2.39, 2.35, 2.33, 2.26, 2.24, 2.18, 2.18, 2.18 },
            new List<double> { 7.26, 5.39, 5.13, 4.02, 5.30, 6.59, 5.31, 4.02, 3.69, 3.32, 2.93, 2.45, 3.69, 2.85, 1.58, 1.78, 1.91, 1.82, 2.48, 1.47, 1.08, 1.68, 1.43, 2.32, 0.78, 1.64, 1.67, 1.71, 2.64, 0.88, 1.81, 1.51, 1.95, 2.11, 3.15, 1.63, 1.79, 1.79, 1.79 },
            new List<double> { 4.42, 4.68, 4.99, 4.01, 4.91, 3.90, 3.98, 4.05, 3.22, 3.91, 2.74, 2.21, 2.80, 3.59, 1.98, 2.71, 1.94, 1.79, 1.66, 1.48, 1.49, 2.76, 2.23, 2.26, 2.73, 2.67, 1.35, 1.46, 2.43, 2.67, 2.58, 1.91, 1.68, 2.07, 2.20, 2.28, 2.28, 2.28, 2.28 }
        };

                string[] seriesNames = { "Q=300", "Q=400", "Q=600", "Q=2800", "Bờ phải", "Bờ trái" };
                Color[] seriesColors = { Color.Red, Color.DarkRed, Color.Pink, Color.DarkGreen, Color.Orange, Color.Purple };

                if (seriesNames.Length != qValuesData.Count)
                {
                    MessageBox.Show("Số lượng tên series không khớp số lượng dữ liệu Q.", "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // 3. Chuẩn bị nhãn X (nếu muốn thay bằng text)
                var customLabels = xAxisData.Select(x => x.ToString("0.0")).ToList();
                // 4. Cấu hình trục X để hiển thị đầy đủ
                ConfigureXAxis(chart, xAxisData, customLabels);



                // 5. Vẽ từng series
                for (int i = 0; i < qValuesData.Count; i++)
                {
                    AddSeries(chart, seriesNames[i], seriesColors[i % seriesColors.Length], xAxisData, qValuesData[i]);
                }

                // --- 5. Lấy dữ liệu API mới nhất ---
                using var dbContext = new ApplicationDbContext();
                var latest = dbContext.FT03s
                    .Where(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.CreateAt)
                    .FirstOrDefault();

                List<double> mucNuoc6PointsXData = new List<double> { 0, 25, 40, 101 };
                List<double> mucNuoc6PointsYData = new();

                if (latest != null)
                {
                    mucNuoc6PointsYData.Add(latest.API_Fllow_SonDai ?? double.NaN);
                    mucNuoc6PointsYData.Add(latest.API_Fllow_DauTieng ?? double.NaN);
                    mucNuoc6PointsYData.Add(latest.API_Fllow_BenSuc ?? double.NaN);
                    mucNuoc6PointsYData.Add(latest.API_Fllow_BinhNham ?? double.NaN);
                }

                if (mucNuoc6PointsYData.Any(y => !double.IsNaN(y)))
                    AddSeries(chart, "MN", Color.DarkGreen, mucNuoc6PointsXData, mucNuoc6PointsYData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý dữ liệu hoặc vẽ biểu đồ:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddSeries(Chart chart, string name, Color color, List<double> xData, List<double> yData, SeriesChartType type = SeriesChartType.Spline)
        {
            if (xData.Count != yData.Count)
            {
                Console.WriteLine($"⚠ {name}: số lượng X={xData.Count} ≠ Y={yData.Count}");
                return;
            }

            Series s = new Series(name)
            {
                ChartType = type,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 4,
                Color = color,
                IsVisibleInLegend = true,
                Enabled = true,
                ToolTip = "#SERIESNAME: X=#VALX, Y=#VALY"
            };

            for (int i = 0; i < xData.Count; i++)
                s.Points.AddXY(xData[i], yData[i]);

            chart.Series.Add(s);

            // Lưu màu gốc & trạng thái hiển thị
            originalSeriesColors[name] = color;
            seriesDisplayState[name] = true;
        }
        // Hàm tiện ích cấu hình trục X
        /// Cấu hình trục X hiển thị đầy đủ các giá trị + nhãn
        /// </summary>
        private void ConfigureXAxis(Chart chart, List<double> xAxisData, List<string> xLabels = null)
        {
            var chartArea = chart.ChartAreas[0];

            chartArea.AxisX.Minimum = xAxisData.Min();
            chartArea.AxisX.Maximum = xAxisData.Max();
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.MajorTickMark.Enabled = false;

            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.IsStaggered = false;
            chartArea.AxisX.Interval = 1;

            chartArea.AxisX.CustomLabels.Clear();
            chartArea.AxisX.StripLines.Clear();

            double halfStep = 0.2;

            for (int i = 0; i < xAxisData.Count; i++)
            {
                double value = xAxisData[i];
                string labelText = value.ToString("0.##");

                // Nếu có danh sách nhãn và đủ phần tử, thêm tên bên dưới
                if (xLabels != null && i < xLabels.Count)
                    labelText += "\n" + xLabels[i];

                chartArea.AxisX.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = value - halfStep,
                    ToPosition = value + halfStep,
                    Text = labelText,
                    RowIndex = 0,
                    LabelMark = LabelMarkStyle.None
                });

                chartArea.AxisX.StripLines.Add(new StripLine
                {
                    IntervalOffset = value,
                    StripWidth = 0.0001,
                    BackColor = Color.LightGray
                });
            }

            chartArea.AxisX.LabelStyle.Angle = -90;
            chartArea.AxisX.LabelStyle.Font = new Font("Arial", 7, FontStyle.Regular);
            chartArea.AxisX.LabelStyle.ForeColor = Color.Black;
        }
        private void ConfigureChart()
        {
            var chartArea = chart.ChartAreas[0];
            chart.Titles.Clear();

            chartArea.AxisX.Title = "Vị trí (km)";
            chartArea.AxisY.Title = "Cao trình (m)";
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 101;
            chartArea.AxisY.Minimum = -2;
            chartArea.AxisY.Maximum = 10;

            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.MajorTickMark.Enabled = false;
            chartArea.AxisX.LabelStyle.Enabled = true;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisX.Interval = 0;

            chartArea.AxisX.LabelStyle.Font = new Font("Arial", 7, FontStyle.Regular);
            chartArea.AxisX.LabelStyle.Angle = -90;
            chartArea.AxisX.LabelStyle.ForeColor = Color.Black;
            chartArea.AxisX.LabelStyle.IsStaggered = false;

            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorTickMark.LineColor = Color.Black;
        }
        private void Chart_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.LegendItem)
            {
                Series clickedSeries = result.Series;
                if (clickedSeries != null)
                {
                    string name = clickedSeries.Name;

                    // Đảo trạng thái hiển thị
                    seriesDisplayState[name] = !seriesDisplayState[name];

                    if (seriesDisplayState[name])
                    {
                        // Bật lại màu gốc
                        clickedSeries.Color = originalSeriesColors[name];
                        clickedSeries.BorderWidth = 2;
                    }
                    else
                    {
                        // "Tắt" bằng cách làm trong suốt
                        clickedSeries.Color = Color.Transparent;
                        clickedSeries.BorderWidth = 0;
                    }

                    chart.Invalidate(); // refresh chart
                }
            }
        }
        
        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            // Reset all series to normal state before checking for highlight
            foreach (Series s in chart.Series)
            {
                if (seriesDisplayState.ContainsKey(s.Name))
                {
                    if (seriesDisplayState[s.Name]) // If series should be visible
                    {
                        s.BorderWidth = 2;
                        s.MarkerSize = 6;
                        s.Color = originalSeriesColors[s.Name]; // Restore original color for legend item
                        // For "Mực Nước (6 điểm)" line, restore its specific marker type and size
                        if (s.Name == "Mực Nước (6 điểm)")
                        {
                            s.MarkerStyle = MarkerStyle.Diamond;
                            s.MarkerSize = 8;
                        }
                        else
                        {
                            s.MarkerStyle = MarkerStyle.Circle; // Ensure other lines use Circle style
                        }
                    }
                    else // If series should be hidden
                    {
                        s.BorderWidth = 0;
                        s.MarkerSize = 0;
                        s.Color = Color.DarkGray; // Dim the color in the legend
                    }
                }
            }

            HitTestResult result = chart.HitTest(e.X, e.Y);

            // If the mouse moves within the plotting area or over a data point
            if (result.ChartElementType == ChartElementType.PlottingArea || result.ChartElementType == ChartElementType.DataPoint)
            {
                // Get the X-value corresponding to the mouse position on the X-axis
                double currentX = chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

                StringBuilder tooltipContent = new StringBuilder();
                tooltipContent.Append($"X: {currentX.ToString("N1", CultureInfo.InvariantCulture)} km"); // X-value on the first line

                // Flag to check if any series is displayed and has data for the tooltip
                bool hasRelevantTooltipData = false;

                // Iterate through all series to get interpolated Y-values
                foreach (Series series in chart.Series)
                {
                    // Only display tooltip for series that are ENABLED and VISIBLE on the chart
                    if (series.Enabled && seriesDisplayState.ContainsKey(series.Name) && seriesDisplayState[series.Name])
                    {
                        double interpolatedY = GetInterpolatedY(series, currentX);
                        if (!double.IsNaN(interpolatedY))
                        {
                            tooltipContent.AppendLine(); // Add a new line before each series data line
                            tooltipContent.Append($"{series.Name}: {interpolatedY.ToString("N2", CultureInfo.InvariantCulture)} m");
                            hasRelevantTooltipData = true; // Mark as having tooltip data
                        }
                    }
                }

                // If at least one line is showing data in the tooltip, display the tooltip
                if (hasRelevantTooltipData)
                {
                    // Update tooltipAnnotation position and content
                    tooltipAnnotation.Text = tooltipContent.ToString();
                    // Convert mouse pixel coordinates to relative (0-100%) coordinates of the Chart control
                    tooltipAnnotation.X = ((double)e.X / chart.Width) * 100 + 1; // +1 for a small offset
                    tooltipAnnotation.Y = ((double)e.Y / chart.Height) * 100 + 1; // +1 for a small offset

                    tooltipAnnotation.Visible = true; // Show tooltip

                    // Update tooltip text and border color based on the hovered line's color
                    if (result.ChartElementType == ChartElementType.DataPoint && result.Series != null && seriesDisplayState[result.Series.Name])
                    {
                        result.Series.BorderWidth = 5; // Highlight line
                        result.Series.MarkerSize = 10; // Highlight marker
                        // For "Mực Nước (6 điểm)" line, highlight with its specific marker
                        if (result.Series.Name == "Mực Nước (6 điểm)")
                        {
                            result.Series.MarkerStyle = MarkerStyle.Diamond;
                            result.Series.MarkerSize = 12; // Larger size when highlighted
                        }
                        else
                        {
                            result.Series.MarkerStyle = MarkerStyle.Circle; // Ensure other lines use Circle style
                        }

                        tooltipAnnotation.ForeColor = result.Series.Color; // Text color matches line
                        tooltipAnnotation.LineColor = result.Series.Color; // Border color matches line
                    }
                    else // If not hovering over a specific DataPoint or line is hidden, revert to default tooltip colors
                    {
                        tooltipAnnotation.ForeColor = Color.Black; // Default text color from constructor
                        tooltipAnnotation.LineColor = Color.Black;  // Default border color from constructor
                    }
                }
                else
                {
                    tooltipAnnotation.Visible = false; // Hide tooltip if no data to display
                }

                chart.Invalidate(); // Redraw chart to display tooltip
            }
            else
            {
                tooltipAnnotation.Visible = false; // Hide tooltip if mouse is not over the chart
                chart.Invalidate();
            }
        }
        private void Chart_MouseLeave(object sender, EventArgs e)
        {
            // Reset all series to normal state
            foreach (Series s in chart.Series)
            {
                if (seriesDisplayState.ContainsKey(s.Name))
                {
                    if (seriesDisplayState[s.Name]) // If series should be visible
                    {
                        s.BorderWidth = 2;
                        s.MarkerSize = 6;
                        s.Enabled = true; // Ensure series is enabled on chart
                        s.Color = originalSeriesColors[s.Name]; // Restore original color for legend item
                        // For "Mực Nước (6 điểm)" line, restore its specific marker type and size
                        if (s.Name == "Mực Nước (6 điểm)")
                        {
                            s.MarkerStyle = MarkerStyle.Diamond;
                            s.MarkerSize = 8;
                        }
                        else
                        {
                            s.MarkerStyle = MarkerStyle.Circle; // Ensure other lines use Circle style
                        }
                    }
                    else // If series should be hidden
                    {
                        s.BorderWidth = 0;
                        s.MarkerSize = 0;
                        s.Enabled = false; // Completely hide the line on the chart
                        s.Color = Color.DarkGray; // Dim the color in the legend
                    }
                }
            }
            tooltipAnnotation.Visible = false;
            chart.Invalidate();
        }
        private double GetInterpolatedY(Series series, double xValue)
        {
            DataPoint prevPoint = null;
            DataPoint nextPoint = null;

            // Find the two closest data points surrounding xValue
            foreach (DataPoint p in series.Points)
            {
                if (p.XValue <= xValue)
                {
                    prevPoint = p;
                }
                else
                {
                    nextPoint = p;
                    break;
                }
            }

            // Handle edge cases
            if (prevPoint == null && nextPoint == null) return double.NaN; // No data points
            if (prevPoint == null) return nextPoint.YValues[0]; // xValue is smaller than all points
            if (nextPoint == null) return prevPoint.YValues[0]; // xValue is larger than all points

            // If xValue matches one of the points
            if (prevPoint.XValue == xValue) return prevPoint.YValues[0];
            if (nextPoint.XValue == xValue) return nextPoint.YValues[0];
            if (prevPoint.XValue == nextPoint.XValue) return prevPoint.YValues[0]; // Avoid division by zero

            // Perform linear interpolation
            double slope = (nextPoint.YValues[0] - prevPoint.YValues[0]) / (nextPoint.XValue - prevPoint.XValue);
            double interpolatedY = prevPoint.YValues[0] + slope * (xValue - prevPoint.XValue);
            return interpolatedY;
        }



    }
}
