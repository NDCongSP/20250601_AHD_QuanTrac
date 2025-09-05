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
        private Chart chart;
        private TableLayoutPanel tableLayoutPanelMain; // TableLayoutPanel for layout management
        private Dictionary<string, Color> originalSeriesColors; // Dictionary to store original series colors
        private Dictionary<string, bool> seriesDisplayState; // Dictionary to track series display state
        private TextAnnotation tooltipAnnotation; // Annotation for custom tooltip display

        public FrmMucnuoc()
        {
            InitializeComponent();
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
            chart.MouseClick += Chart_MouseClick;
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
        }

        private void LoadDataAndDrawChart()
        {
            try
            {
                // X-axis data (Distance in km) - 39 points
                //List<double> xAxisData = new List<double>
                //{
                //    0, 1.5, 3.5, 5.5, 8.5, 9.5, 10.5, 12.5, 14.5, 16.5, 18.5, 20.6, 22.6, 24.6, 26.6, 28.6, 30.6, 34.7, 38.7, 42.7, 46.7, 50.7, 54.8,
                //    58.8, 62.8, 66.8, 70.8, 71.8, 73.8, 75.9, 80.9, 84.9, 86.9, 87.9, 91.9, 93.8, 101, 101, 101 // Added 39th value
                //};
                List<double> xAxisData = new List<double>
                {
                    0, 1.5, 3.5, 5.5, 8.5, 9.5, 10.5, 12.5, 14.5, 16.5, 18.5, 20.6, 22.6, 24.6, 26.6, 28.6, 30.6, 34.7, 38.7, 42.7, 46.7, 50.7, 54.8,
                    58.8, 62.8, 66.8, 70.8, 71.8, 73.8, 75.9, 80.9, 84.9, 86.9, 87.9, 91.9, 93.8, 101, 101, 101 // Added 39th value
                };

                // Y-axis data (Water level elevation in meters) for different flows
                List<List<double>> qValuesData = new List<List<double>>();
                // Ensure each list has 39 elements
                qValuesData.Add(new List<double> { 1.73, 1.66, 1.63, 1.55, 1.50, 1.49, 1.48, 1.46, 1.43, 1.43, 1.42, 1.41, 1.40, 1.39, 1.39, 1.38, 1.38, 1.37, 1.35, 1.34, 1.33, 1.31, 1.31, 1.33, 1.34, 1.36, 1.38, 1.41, 1.43, 1.44, 1.47, 1.49, 1.50, 1.51, 1.53, 1.54, 1.58, 1.58, 1.58 });
                qValuesData.Add(new List<double> { 2.46, 2.30, 2.19, 1.98, 1.83, 1.79, 1.77, 1.71, 1.66, 1.62, 1.57, 1.53, 1.50, 1.47, 1.46, 1.44, 1.44, 1.44, 1.41, 1.39, 1.38, 1.37, 1.36, 1.37, 1.37, 1.38, 1.40, 1.43, 1.45, 1.46, 1.47, 1.51, 1.52, 1.52, 1.54, 1.55, 1.58, 1.58, 1.58 });
                qValuesData.Add(new List<double> { 3.69, 3.39, 3.23, 2.99, 2.68, 2.62, 2.57, 2.42, 2.27, 2.13, 2.02, 1.86, 1.76, 1.61, 1.57, 1.53, 1.53, 1.52, 1.51, 1.50, 1.48, 1.45, 1.46, 1.46, 1.45, 1.44, 1.46, 1.48, 1.50, 1.50, 1.51, 1.54, 1.55, 1.56, 1.57, 1.58, 1.60, 1.60, 1.60 });
                qValuesData.Add(new List<double> { 4.52, 4.11, 3.95, 3.71, 3.31, 3.25, 3.20, 3.02, 2.84, 2.62, 2.46, 2.25, 2.08, 1.81, 1.72, 1.64, 1.64, 1.63, 1.61, 1.59, 1.56, 1.55, 1.55, 1.54, 1.52, 1.51, 1.53, 1.54, 1.55, 1.55, 1.55, 1.58, 1.59, 1.59, 1.60, 1.60, 1.62, 1.62, 1.62 });
                qValuesData.Add(new List<double> { 5.13, 4.65, 4.48, 4.23, 3.75, 3.69, 3.64, 3.45, 3.25, 3.01, 2.85, 2.64, 2.45, 2.08, 1.90, 1.76, 1.76, 1.74, 1.71, 1.68, 1.65, 1.63, 1.62, 1.61, 1.58, 1.59, 1.59, 1.60, 1.60, 1.60, 1.59, 1.61, 1.62, 1.62, 1.63, 1.63, 1.64, 1.64, 1.64 });
                qValuesData.Add(new List<double> { 5.57, 5.08, 4.88, 4.61, 4.09, 4.03, 3.98, 3.78, 3.57, 3.32, 3.17, 2.96, 2.74, 2.34, 2.11, 1.89, 1.88, 1.86, 1.82, 1.77, 1.74, 1.73, 1.71, 1.69, 1.66, 1.66, 1.65, 1.66, 1.66, 1.65, 1.63, 1.64, 1.65, 1.65, 1.65, 1.65, 1.66, 1.66, 1.66 });
                qValuesData.Add(new List<double> { 5.94, 5.44, 5.21, 4.93, 4.38, 4.32, 4.27, 4.06, 3.84, 3.58, 3.43, 3.22, 3.00, 2.56, 2.31, 2.02, 2.02, 1.99, 1.93, 1.87, 1.84, 1.83, 1.80, 1.77, 1.74, 1.73, 1.71, 1.69, 1.66, 1.66, 1.65, 1.66, 1.66, 1.65, 1.63, 1.64, 1.65, 1.65, 1.65 });
                qValuesData.Add(new List<double> { 6.51, 5.97, 5.73, 5.43, 4.83, 4.77, 4.72, 4.50, 4.27, 3.99, 3.83, 3.61, 3.37, 2.90, 2.67, 2.39, 2.37, 2.33, 2.19, 2.10, 2.03, 2.00, 1.95, 1.91, 1.89, 1.86, 1.84, 1.83, 1.82, 1.80, 1.76, 1.76, 1.75, 1.75, 1.74, 1.73, 1.72, 1.72, 1.72 });
                qValuesData.Add(new List<double> { 7.02, 6.44, 6.17, 5.85, 5.21, 5.15, 5.09, 4.86, 4.63, 4.33, 4.15, 3.92, 3.66, 3.18, 2.99, 2.75, 2.73, 2.67, 2.52, 2.34, 2.25, 2.18, 2.10, 2.04, 2.01, 1.96, 1.94, 1.94, 1.94, 1.92, 1.89, 1.85, 1.85, 1.84, 1.83, 1.82, 1.80, 1.77, 1.77 });
                qValuesData.Add(new List<double> { 9.70, 9.13, 8.83, 8.40, 7.56, 7.50, 7.41, 7.11, 6.88, 6.48, 6.28, 6.09, 5.83, 5.51, 5.41, 5.22, 5.16, 5.03, 4.82, 4.53, 4.29, 4.13, 3.79, 3.53, 3.36, 2.97, 2.80, 2.78, 2.69, 2.59, 2.46, 2.39, 2.35, 2.33, 2.26, 2.24, 2.18, 2.18, 2.18 });
                qValuesData.Add(new List<double> { 7.26, 5.39, 5.13, 4.02, 5.30, 6.59, 5.31, 4.02, 3.69, 3.32, 2.93, 2.45, 3.69, 2.85, 1.58, 1.78, 1.91, 1.82, 2.48, 1.47, 1.08, 1.68, 1.43, 2.32, 0.78, 1.64, 1.67, 1.71, 2.64, 0.88, 1.81, 1.51, 1.95, 2.11, 3.15, 1.63, 1.79, 1.79, 1.79 });
                qValuesData.Add(new List<double> { 4.42, 4.68, 4.99, 4.01, 4.91, 3.90, 3.98, 4.05, 3.22, 3.91, 2.74, 2.21, 2.80, 3.59, 1.98, 2.71, 1.94, 1.79, 1.66, 1.48, 1.49, 2.76, 2.23, 2.26, 2.73, 2.67, 1.35, 1.46, 2.43, 2.67, 2.58, 1.91, 1.68, 2.07, 2.20, 2.28, 2.28, 2.28, 2.28 });

                // Original series names
                string[] originalQSeriesNames = { "Q=50", "Q=100", "Q=200", "Q=300", "Q=400", "Q=500", "Q=600", "Q=800", "Q=1000", "Q=2800", "Bờ phải", "Bờ trái" };

                // Original series colors
                Color[] originalSeriesColorsArray = {
                    Color.Red, Color.DarkRed, Color.Pink, Color.DarkGreen, Color.Orange,
                    Color.Purple, Color.Brown, Color.Cyan, Color.Magenta, Color.Gray,
                    Color.Black, Color.Blue
                };

                // Check if the number of series names matches the number of data lists
                if (originalQSeriesNames.Length != qValuesData.Count)
                {
                    MessageBox.Show("Lỗi: Số lượng tên series gốc không khớp với số lượng danh sách dữ liệu Q gốc.", "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Clear all existing series before redrawing
                chart.Series.Clear();
                originalSeriesColors.Clear(); // Clear old colors when reloading data
                seriesDisplayState.Clear(); // Clear old display states

                // Iterate through each Q data list to create and add series to the chart
                for (int i = 0; i < qValuesData.Count; i++)
                {
                    Series series = new Series(originalQSeriesNames[i]);
                    series.ChartType = SeriesChartType.Spline; // Spline chart type (smooth line)
                    series.BorderWidth = 1; // Line thickness
                    series.MarkerStyle = MarkerStyle.Circle; // Marker style
                    series.MarkerSize = 6; // Marker size
                    Color currentColor = originalSeriesColorsArray[i % originalSeriesColorsArray.Length];
                    series.Color = currentColor; // Assign initial color
                    originalSeriesColors.Add(series.Name, currentColor); // Store original color
                    series.IsVisibleInLegend = true; // Legend always visible
                    series.Enabled = true; // Series always enabled for legend display and clicking

                    // Initialize series display state to TRUE (visible)
                    seriesDisplayState.Add(series.Name, true);

                    // Check if the number of X-axis points matches the current series' data points
                    if (xAxisData.Count != qValuesData[i].Count)
                    {
                        Console.WriteLine($"Cảnh báo: Số lượng điểm trục X ({xAxisData.Count}) không khớp với số lượng điểm dữ liệu Q {originalQSeriesNames[i]} ({qValuesData[i].Count}). Series này có thể không được vẽ đúng.");
                        continue;
                    }

                    // Add data points to the series
                    for (int j = 0; j < xAxisData.Count; j++)
                    {
                        DataPoint dp = new DataPoint(xAxisData[j], qValuesData[i][j]);
                        series.Points.Add(dp);
                    }
                    chart.Series.Add(series); // Add series to the chart
                }

                // --- Update: Add the "Mực Nước (6 điểm)" line, reading the latest data from SQL ---
                // X positions (Distance in km) corresponding to the 6 water level points.
                // IMPORTANT: Ensure this order matches the geographical/logical order of the water level locations queried from SQL.
                List<double> mucNuoc6PointsXData = new List<double> { 0, 25, 40, 60, 80, 101 }; // Updated X coordinates
                List<double> mucNuoc6PointsYData = new List<double>();

                // Connection string to your SQL Server database.
                // Replace "YourServerName", "YourDatabaseName", "YourUsername", "YourPassword" with your actual information.
                string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

                // SQL query to get the latest data.
                // Ensure table name and columns match your database.
                // IMPORTANT: The order of columns in the SELECT statement MUST MATCH the order you intend to map them to mucNuoc6PointsXData.
                // For example: If Fllow_Ho corresponds to X=0, Fllow_DauTieng to X=25, etc., then the SELECT order must be Fllow_Ho, Fllow_DauTieng,...
                // Replace 'CreateAt' with the actual name of your timestamp column.
                //  string query = "SELECT TOP 1 Fllow_Ho, Fllow_DauTieng, Fllow_BenSuc, Fllow_SonDai, Fllow_BinhNham, Fllow_TL_CDD FROM DataMucNuoc ORDER BY CreateAt DESC;";
                string query = "SELECT TOP 1 Fllow_BinhNham , Fllow_SonDai,Fllow_BenSuc, Fllow_DauTieng, Fllow_Ho, Fllow_TL_CDD FROM DataMucNuoc ORDER BY CreateAt DESC;";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read()) // Get the latest data row
                                {
                                    // Read each value and add it to the mucNuoc6PointsYData list in the order CORRESPONDING to mucNuoc6PointsXData.
                                    // If Fllow_Ho data is for X=0, it must be added first.
                                    // Use GetValue() and Convert.ToDouble() for more flexible data type conversion and NULL handling.
                                    mucNuoc6PointsYData.Add(reader.IsDBNull(reader.GetOrdinal("Fllow_TL_CDD")) ? double.NaN : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Fllow_TL_CDD"))));
                                    mucNuoc6PointsYData.Add(reader.IsDBNull(reader.GetOrdinal("Fllow_Ho")) ? double.NaN : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Fllow_Ho"))));
                                    mucNuoc6PointsYData.Add(reader.IsDBNull(reader.GetOrdinal("Fllow_SonDai")) ? double.NaN : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Fllow_SonDai"))));
                                    mucNuoc6PointsYData.Add(reader.IsDBNull(reader.GetOrdinal("Fllow_DauTieng")) ? double.NaN : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Fllow_DauTieng"))));
                                    mucNuoc6PointsYData.Add(reader.IsDBNull(reader.GetOrdinal("Fllow_BenSuc")) ? double.NaN : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Fllow_BenSuc"))));
                                    mucNuoc6PointsYData.Add(reader.IsDBNull(reader.GetOrdinal("Fllow_BinhNham")) ? double.NaN : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Fllow_BinhNham"))));
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy dữ liệu mới nhất cho đường 'Mực Nước (6 điểm)' trong cơ sở dữ liệu.", "Cảnh báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    // If no data from SQL, you can add default data so the chart still displays.
                                    mucNuoc6PointsYData.AddRange(new List<double> { 1.6, 1.5, 1.4, 1.3, 1.45, 1.6 }); // Example default values
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối hoặc truy vấn SQL: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // For illustration, I will add default data if there's a SQL error.
                    mucNuoc6PointsYData.AddRange(new List<double> { 1.6, 1.5, 1.4, 1.3, 1.45, 1.6 }); // Example default values
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi không xác định khi đọc dữ liệu mực nước: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // For illustration, I will add default data if there's another error.
                    mucNuoc6PointsYData.AddRange(new List<double> { 1.6, 1.5, 1.4, 1.3, 1.45, 1.6 }); // Example default values
                }
                // --- End Update ---

                Series mucNuoc6Series = new Series("MN");
                mucNuoc6Series.ChartType = SeriesChartType.Spline; // Spline chart type (smooth line)
                mucNuoc6Series.BorderWidth = 2; // Line thickness
                mucNuoc6Series.MarkerStyle = MarkerStyle.Diamond; // Diamond marker style
                mucNuoc6Series.MarkerSize = 8; // Larger marker size
                mucNuoc6Series.Color = Color.DarkGreen; // Dark green color
                originalSeriesColors.Add(mucNuoc6Series.Name, Color.DarkGreen); // Store original color
                mucNuoc6Series.IsVisibleInLegend = true; // Display in legend
                seriesDisplayState.Add(mucNuoc6Series.Name, true); // Set display state to visible

                // Ensure the number of X and Y points match before adding to the series
                if (mucNuoc6PointsXData.Count != mucNuoc6PointsYData.Count)
                {
                    MessageBox.Show("Lỗi: Số lượng điểm X cho 'Mực Nước (6 điểm)' không khớp với số lượng điểm Y được đọc từ SQL. Hãy kiểm tra truy vấn hoặc dữ liệu.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int j = 0; j < mucNuoc6PointsXData.Count; j++)
                    {
                        mucNuoc6Series.Points.Add(new DataPoint(mucNuoc6PointsXData[j], mucNuoc6PointsYData[j]));
                    }
                    chart.Series.Add(mucNuoc6Series); // Add the "Mực Nước (6 điểm)" series to the chart
                }
                // --- End Addition ---

                // Configure chart title
                chart.Titles.Clear();

                // Configure X and Y axis titles
                chart.ChartAreas[0].AxisX.Title = "Vị trí (km)";
                chart.ChartAreas[0].AxisY.Title = "Cao trình (m)";

                // Configure X and Y axis ranges
                chart.ChartAreas[0].AxisX.Minimum = 0;
                chart.ChartAreas[0].AxisX.Maximum = 101;
                chart.ChartAreas[0].AxisY.Minimum = -10;
                chart.ChartAreas[0].AxisY.Maximum = 28; // Adjusted max Y-axis for potential higher values

                // Get chart area for more detailed configuration
                ChartArea chartArea = chart.ChartAreas[0];
                chartArea.AxisX.CustomLabels.Clear(); // Clear old custom labels

                // Configure X-axis labels
                chartArea.AxisX.LabelStyle.Enabled = true;
                chartArea.AxisX.IsLabelAutoFit = false;
                chartArea.AxisX.Interval = 0; // Disable automatic interval
                chartArea.AxisX.MajorGrid.Enabled = false; // Disable major grid lines
                chartArea.AxisX.MajorTickMark.Enabled = false; // Disable major tick marks

                // Add vertical strip lines at each X-data point
                chartArea.AxisX.StripLines.Clear();
                foreach (double value in xAxisData)
                {
                    StripLine stripLine = new StripLine();
                    stripLine.IntervalOffset = value;
                    stripLine.StripWidth = 0.001; // Very small width to display as a thin line
                    stripLine.BackColor = Color.LightGray; // Color of the strip line
                    chartArea.AxisX.StripLines.Add(stripLine);
                }

                // Add custom numerical labels for the X-axis
                double labelNumericalRange = 0.5; // Range for numerical labels
                foreach (double value in xAxisData)
                {
                    CustomLabel numLabel = new CustomLabel();
                    numLabel.FromPosition = value - labelNumericalRange;
                    numLabel.ToPosition = value + labelNumericalRange;
                    numLabel.Text = value.ToString("N1"); // Format to 1 decimal place
                    numLabel.RowIndex = 0;
                    numLabel.LabelMark = LabelMarkStyle.None;
                    chartArea.AxisX.CustomLabels.Add(numLabel);
                }

                // Configure font style and angle for X-axis labels
                chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 7, FontStyle.Regular);
                chartArea.AxisX.LabelStyle.Angle = -90; // Rotate labels 90 degrees to avoid overlapping
                chartArea.AxisX.LabelStyle.ForeColor = Color.Black;
                chartArea.AxisX.LabelStyle.IsStaggered = false; // Disable staggered labels

                // Configure anti-aliasing for chart and text
                chart.AntiAliasing = AntiAliasingStyles.All;
                chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

                // Configure major grid and tick marks for Y-axis
                chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                chartArea.AxisY.MajorTickMark.Enabled = true;
                chartArea.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Black;
            }
            catch (Exception ex)
            {
                // Display error message box if there's an issue
                MessageBox.Show($"Đã xảy ra lỗi khi xử lý dữ liệu hoặc vẽ biểu đồ: {ex.Message}\nChi tiết: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Chart_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart.HitTest(e.X, e.Y);

            // If a legend item is clicked
            if (result.ChartElementType == ChartElementType.LegendItem)
            {
                Series series = result.Series;
                if (series != null)
                {
                    // Toggle the display state of the series in seriesDisplayState
                    seriesDisplayState[series.Name] = !seriesDisplayState[series.Name];

                    // Adjust series display state
                    if (seriesDisplayState[series.Name]) // If the series should be visible (turned on)
                    {
                        // Restore original color, border thickness, and marker size
                        if (originalSeriesColors.ContainsKey(series.Name))
                        {
                            series.Color = originalSeriesColors[series.Name]; // Restore original color in legend
                        }
                        series.BorderWidth = 2;
                        series.MarkerSize = 6;
                        // For "Mực Nước (6 điểm)" line, restore its specific marker type and size
                        if (series.Name == "Mực Nước (6 điểm)")
                        {
                            series.MarkerStyle = MarkerStyle.Diamond;
                            series.MarkerSize = 8;
                        }
                    }
                    else // If the series should be hidden (turned off)
                    {
                        // Hide the line on the chart, and dim the color in the legend
                        series.BorderWidth = 0; // No border visible on chart
                        series.MarkerSize = 0;  // No markers visible on chart
                        series.Color = Color.DarkGray; // Dim the color in the legend
                    }
                    chart.Invalidate(); // Redraw the chart
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
