using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Charting = System.Windows.Forms.DataVisualization.Charting;
using Drawing = System.Drawing;
namespace RegistrationForm1
{
    public partial class FrmMucnuoc : Form
    {
        private Dictionary<string, System.Data.DataTable> excelDataSets;
        private string[] qValues = { "Qxa 2800", "Qxa 1000", "Qxa 600", "Qxa 400", "Qxa 300", "Qxa 200" };
        private ProgressBar progressBar;
        private Label statusLabel;
        public FrmMucnuoc()
        {
            InitializeComponent(); // Đảm bảo phương thức này được gọi để khởi tạo các điều khiển trên Form
            // Căn giữa biểu đồ trên form
        //   chart.Location = new Drawing.Point((this.ClientSize.Width - chart.Width) / 2, (this.ClientSize.Height - chart.Height) / 2);

            // Khởi tạo thanh tiến trình
            progressBar = new ProgressBar
            {
                Width = 300,
                Height = 20,
                Visible = false
            };
            this.Controls.Add(progressBar);

            // Khởi tạo nhãn trạng thái
            statusLabel = new Label
            {
                Width = 300,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Drawing.Font("Arial", 10),
                Visible = false,
                Text = "Đang tải: 0%"
            };
            this.Controls.Add(statusLabel);

            // Đặt vị trí ban đầu cho thanh tiến trình và nhãn trạng thái
            UpdateControlPositions();

            // Đăng ký sự kiện Resize để cập nhật vị trí khi form thay đổi kích thước
            this.Resize += FrmMucnuoc_Resize;

            SetupChart();
            InitializeComboBox();
            excelDataSets = new Dictionary<string, System.Data.DataTable>();
        }
        private void FrmMucnuoc_Resize(object sender, EventArgs e)
        {
            // Cập nhật vị trí của biểu đồ, thanh tiến trình và nhãn trạng thái khi form thay đổi kích thước
            UpdateControlPositions();
        }
        private void UpdateControlPositions()
        {
            // Căn giữa biểu đồ
          //  chart.Location = new Drawing.Point((this.ClientSize.Width - chart.Width) / 2, (this.ClientSize.Height - chart.Height) / 2);

            // Đặt thanh tiến trình ở dưới cùng, cách đáy form 50 pixel
            progressBar.Location = new Drawing.Point((this.ClientSize.Width - progressBar.Width) / 2, this.ClientSize.Height - 65);
            progressBar.BringToFront(); // Đảm bảo thanh tiến trình ở trên cùng

            // Đặt nhãn trạng thái ngay dưới thanh tiến trình
            statusLabel.Location = new Drawing.Point((this.ClientSize.Width - statusLabel.Width) / 2, this.ClientSize.Height - 40);
            statusLabel.BringToFront(); // Đảm bảo nhãn trạng thái ở trên cùng
        }
        private void InitializeComboBox()
        {
            comboBoxQ.Items.Clear();
            comboBoxQ.Items.AddRange(qValues);
            if (comboBoxQ.Items.Count > 0)
                comboBoxQ.SelectedIndex = 0;
        }
        private void SetupChart()
        {
            chart.BackColor = Drawing.Color.White;
            var chartArea = new Charting.ChartArea("MainArea") { BackColor = Drawing.Color.White };

            // Cấu hình trục X
            chartArea.AxisX.Title = "Khoảng cách (km)";
            chartArea.AxisX.TitleFont = new Drawing.Font("Arial", 14, Drawing.FontStyle.Bold);
            chartArea.AxisX.LabelStyle.Font = new Drawing.Font("Arial", 12, Drawing.FontStyle.Regular);
            chartArea.AxisX.MajorGrid.LineColor = Drawing.Color.LightGray;
            chartArea.AxisX.MajorGrid.LineDashStyle = Charting.ChartDashStyle.Solid;
            chartArea.AxisX.Interval = 5;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 150;

            // Cấu hình trục Y
            chartArea.AxisY.Title = "Mực nước (m)";
            chartArea.AxisY.TitleFont = new Drawing.Font("Arial", 14, Drawing.FontStyle.Bold);
            chartArea.AxisY.LabelStyle.Font = new Drawing.Font("Arial", 12, Drawing.FontStyle.Regular);
            chartArea.AxisY.MajorGrid.LineColor = Drawing.Color.LightGray;
            chartArea.AxisY.MajorGrid.LineDashStyle = Charting.ChartDashStyle.Solid;
            chartArea.AxisY.Interval = 3;
            chartArea.AxisY.Minimum = -25;
            chartArea.AxisY.Maximum = 12;
            chartArea.AxisY.LabelStyle.Format = "0";

            // Điều chỉnh InnerPlotPosition để tối ưu hóa không gian cho nhãn và chú thích
            chartArea.InnerPlotPosition.Auto = false;
            chartArea.InnerPlotPosition.X = 10;
            chartArea.InnerPlotPosition.Y = 10;
            chartArea.InnerPlotPosition.Width = 80;
            chartArea.InnerPlotPosition.Height = 70; // Giảm chiều cao để dành thêm không gian cho chú thích

            chart.ChartAreas.Add(chartArea);

            // Cấu hình chú giải
            var legend = new Charting.Legend("MainLegend")
            {
                Docking = Charting.Docking.Bottom,
                Alignment = StringAlignment.Center,
                LegendStyle = Charting.LegendStyle.Row,
                Font = new Drawing.Font("Arial", 12, Drawing.FontStyle.Regular)
            };
            chart.Legends.Add(legend);
        }
        
        private void UpdateComboBoxWithLoadedData()
        {
            comboBoxQ.Items.Clear();
            foreach (var kvp in excelDataSets)
                comboBoxQ.Items.Add(kvp.Key);
            if (comboBoxQ.Items.Contains("Qxa 2800"))
                comboBoxQ.SelectedItem = "Qxa 2800";
            else if (comboBoxQ.Items.Count > 0)
                comboBoxQ.SelectedIndex = 0;
        }

        private async Task LoadExcelData(string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;

            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("File không tồn tại: " + filePath);

                excelApp = new Excel.Application { Visible = false, DisplayAlerts = false };
                workbook = excelApp.Workbooks.Open(filePath);
                excelDataSets.Clear();

                var progress = new Progress<int>(value =>
                {
                    progressBar.Value = value;
                    int percentage = (int)((double)value / qValues.Length * 100);
                    statusLabel.Text = $"Đang tải: {percentage}%";
                });
                int sheetsProcessed = 0;

                foreach (string sheetName in qValues)
                {
                    var worksheet = FindWorksheetByName(workbook, sheetName);
                    if (worksheet == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Không tìm thấy sheet '{sheetName}'");
                        sheetsProcessed++;
                        ((IProgress<int>)progress).Report(sheetsProcessed);
                        continue;
                    }

                    var dt = LoadSheetData(worksheet, sheetName);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        excelDataSets.Add(sheetName, dt);
                        System.Diagnostics.Debug.WriteLine($"Đã thêm sheet '{sheetName}' với {dt.Rows.Count} dòng");
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

                    sheetsProcessed++;
                    ((IProgress<int>)progress).Report(sheetsProcessed);

                    // Tạm dừng ngắn để đảm bảo UI cập nhật
                    await Task.Delay(10);
                }

                if (excelDataSets.Count == 0)
                    throw new Exception("Không tìm thấy sheet nào có dữ liệu hợp lệ trong file Excel.");
            }
            finally
            {
                if (workbook != null) { workbook.Close(false); System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook); }
                if (excelApp != null) { excelApp.Quit(); System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp); }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private System.Data.DataTable LoadSheetData(Excel.Worksheet worksheet, string sheetName)
        {
            var dt = new System.Data.DataTable(sheetName);
            dt.Columns.Add("Distance", typeof(double));
            dt.Columns.Add("H", typeof(double));
            dt.Columns.Add("BoPhai", typeof(double));
            dt.Columns.Add("BoTrai", typeof(double));
            dt.Columns.Add("CaoDoDay", typeof(double));
            dt.Columns.Add("ViTri", typeof(string));

            var usedRange = worksheet.UsedRange;
            if (usedRange == null || usedRange.Rows.Count <= 1)
            {
                System.Diagnostics.Debug.WriteLine($"Sheet '{sheetName}' không có dữ liệu");
                return dt;
            }

            int startRow = worksheet.Cells[1, 1].Value?.ToString().ToLower().Contains("khoang") == true ? 2 : 1;
            for (int row = startRow; row <= usedRange.Rows.Count; row++)
            {
                var dataRow = dt.NewRow();
                bool hasValidData = false;

                if (TryParseAndSet(dataRow, "Distance", worksheet.Cells[row, 1].Value)) hasValidData = true;
                if (TryParseAndSet(dataRow, "H", worksheet.Cells[row, 2].Value)) hasValidData = true;
                if (usedRange.Columns.Count >= 3 && TryParseAndSet(dataRow, "BoPhai", worksheet.Cells[row, 3].Value)) hasValidData = true;
                if (usedRange.Columns.Count >= 4 && TryParseAndSet(dataRow, "BoTrai", worksheet.Cells[row, 4].Value)) hasValidData = true;
                if (usedRange.Columns.Count >= 5 && TryParseAndSet(dataRow, "CaoDoDay", worksheet.Cells[row, 5].Value)) hasValidData = true;
                if (usedRange.Columns.Count >= 6) dataRow["ViTri"] = worksheet.Cells[row, 6].Value?.ToString() ?? "";

                if (hasValidData)
                    dt.Rows.Add(dataRow);
            }

            System.Diagnostics.Debug.WriteLine($"Sheet '{sheetName}' có {dt.Rows.Count} dòng dữ liệu hợp lệ");
            return dt;
        }

        private Excel.Worksheet FindWorksheetByName(Excel.Workbook workbook, string targetName)
        {
            foreach (Excel.Worksheet worksheet in workbook.Worksheets)
            {
                string sheetName = worksheet.Name.Trim();
                if (string.Equals(sheetName, targetName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(sheetName.Replace(" ", ""), targetName.Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
                    return worksheet;
            }
            return null;
        }

        private bool TryParseAndSet(DataRow dataRow, string columnName, object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                dataRow[columnName] = DBNull.Value;
                return false;
            }

            string stringValue = value.ToString().Trim();
            if (double.TryParse(stringValue.Replace(',', '.'), out double result) ||
                double.TryParse(stringValue.Replace("−", "-"), out result))
            {
                dataRow[columnName] = result;
                return true;
            }

            System.Diagnostics.Debug.WriteLine($"Không thể phân tích giá trị '{stringValue}' cho cột {columnName}");
            dataRow[columnName] = DBNull.Value;
            return false;
        }

       

        private void UpdateChart()
        {
            chart.Series.Clear();
            chart.Annotations.Clear();
            chart.Titles.Clear();

            if (excelDataSets == null || excelDataSets.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Không có dữ liệu để vẽ biểu đồ");
                return;
            }

            string selectedQxa = comboBoxQ.SelectedItem?.ToString() ?? "Qxa 2800";

            var dataToPlot = excelDataSets.Where(kvp => kvp.Key == selectedQxa).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var kvp in dataToPlot)
            {
                string qValue = kvp.Key.Replace("Qxa ", "");
                chart.Titles.Add($"Đường mực nước lớn nhất dọc sông Sài Gòn ứng với kịch bản hồ Dầu Tiếng xả Q={qValue} m³/s");
                chart.Titles[0].Font = new Drawing.Font("Arial", 16, Drawing.FontStyle.Bold);

                Drawing.Color[] qxaColors = { Drawing.Color.Blue, Drawing.Color.Orange, Drawing.Color.Gray, Drawing.Color.Gold, Drawing.Color.DeepSkyBlue, Drawing.Color.Green };
                int colorIndex = 0;
                var seriesH = new Charting.Series($"H (m) - Q={qValue} m³/s")
                {
                    ChartType = Charting.SeriesChartType.Line,
                    Color = qxaColors[colorIndex % qxaColors.Length],
                    BorderWidth = 2,
                    MarkerStyle = Charting.MarkerStyle.Circle, // Thêm dấu chấm tại các điểm dữ liệu
                    MarkerSize = 6 // Kích thước dấu chấm
                };

                foreach (DataRow row in kvp.Value.Rows)
                {
                    if (!row.IsNull("Distance") && !row.IsNull("H"))
                        seriesH.Points.AddXY(row.Field<double>("Distance"), row.Field<double>("H"));
                }

                if (seriesH.Points.Count > 0)
                {
                    chart.Series.Add(seriesH);
                    colorIndex++;
                }
            }

            var referenceData = excelDataSets.Values.FirstOrDefault();
            if (referenceData != null)
            {
                AddReferenceSeries(referenceData);
                AddLocationAnnotations(referenceData);
            }

            // Điều chỉnh trục Y động dựa trên dữ liệu
            double maxH = dataToPlot.Values.SelectMany(dt => dt.AsEnumerable())
                .Where(row => !row.IsNull("H"))
                .Max(row => row.Field<double>("H"));
            double minH = dataToPlot.Values.SelectMany(dt => dt.AsEnumerable())
                .Where(row => !row.IsNull("CaoDoDay"))
                .Min(row => row.Field<double>("CaoDoDay"));
            chart.ChartAreas[0].AxisY.Maximum = Math.Ceiling(maxH / 3) * 3 + 3;
            chart.ChartAreas[0].AxisY.Minimum = Math.Floor(minH / 3) * 3 - 3;

            chart.Invalidate();
        }

        private void AddReferenceSeries(System.Data.DataTable referenceData)
        {
            var seriesBoTrai = new Charting.Series("Bờ trái (m)")
            {
                ChartType = Charting.SeriesChartType.Line,
                Color = Drawing.Color.DarkGreen,
                BorderWidth = 2,
                MarkerStyle = Charting.MarkerStyle.Circle, // Thêm dấu chấm tại các điểm dữ liệu
                MarkerSize = 6 // Kích thước dấu chấm
            };
            var seriesBoPhai = new Charting.Series("Bờ phải (m)")
            {
                ChartType = Charting.SeriesChartType.Line,
                Color = Drawing.Color.Purple,
                BorderWidth = 2,
                MarkerStyle = Charting.MarkerStyle.Circle, // Thêm dấu chấm tại các điểm dữ liệu
                MarkerSize = 6 // Kích thước dấu chấm
            };
            var seriesCaoDoDay = new Charting.Series("Cao độ đáy (m)")
            {
                ChartType = Charting.SeriesChartType.Line,
                Color = Drawing.Color.GreenYellow,
                BorderWidth = 2,
                MarkerStyle = Charting.MarkerStyle.Circle, // Thêm dấu chấm tại các điểm dữ liệu
                MarkerSize = 6 // Kích thước dấu chấm
            };

            foreach (DataRow row in referenceData.Rows)
            {
                if (!row.IsNull("Distance"))
                {
                    double distance = row.Field<double>("Distance");
                    if (!row.IsNull("BoTrai")) seriesBoTrai.Points.AddXY(distance, row.Field<double>("BoTrai"));
                    if (!row.IsNull("BoPhai")) seriesBoPhai.Points.AddXY(distance, row.Field<double>("BoPhai"));
                    if (!row.IsNull("CaoDoDay")) seriesCaoDoDay.Points.AddXY(distance, row.Field<double>("CaoDoDay"));
                }
            }

            chart.Series.Add(seriesBoTrai);
            chart.Series.Add(seriesBoPhai);
            chart.Series.Add(seriesCaoDoDay);
        }

        private void AddLocationAnnotations(System.Data.DataTable dataTable)
        {
            var seriesCaoDoDay = chart.Series.FirstOrDefault(s => s.Name == "Cao độ đáy (m)");
            if (seriesCaoDoDay == null) return;

            const double minDistanceThreshold = 8.0; // Khoảng cách tối thiểu để tránh chồng lấn
            const double verticalOffset = 3.0; // Độ lệch cơ bản theo chiều dọc
            const double verticalSpacing = 2.5; // Khoảng cách giữa các mức độ
            const double baseAnnotationHeight = 6.0; // Chiều cao cơ bản của LineAnnotation
            const double heightMultiplier = 3.5; // Hệ số tăng chiều cao (có thể điều chỉnh)
            var annotations = new List<Charting.TextAnnotation>();

            // Danh sách các vị trí cần hiển thị
            var requiredLocations = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Cầu Dầu Tiếng",
                "Cầu Bến Dược",
                "Láng The",
                "Thủ Dầu Một",
                "Cầu Phú Long",
                "Phú An",
                "Cửa S. Sài Gòn"
            };

            // Lọc các vị trí được chỉ định
            var locations = new List<(double Distance, double CaoDoDay, string ViTri)>();
            foreach (DataRow row in dataTable.Rows)
            {
                string viTri = row["ViTri"]?.ToString()?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(viTri) && !row.IsNull("Distance") && !row.IsNull("CaoDoDay"))
                {
                    if (requiredLocations.Any(req => viTri.IndexOf(req, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                   req.IndexOf(viTri, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        locations.Add((row.Field<double>("Distance"), row.Field<double>("CaoDoDay"), viTri));
                    }
                }
            }

            // Sắp xếp theo khoảng cách
            locations = locations.OrderBy(loc => loc.Distance).ToList();

            // Tính toán vị trí các chú thích để tránh chồng lấn
            var finalAnnotations = new List<(double Distance, double CaoDoDay, string ViTri, int Level)>();
            for (int i = 0; i < locations.Count; i++)
            {
                var current = locations[i];
                int level = 0;

                bool hasConflict = true;
                while (hasConflict)
                {
                    hasConflict = false;
                    foreach (var existing in finalAnnotations)
                    {
                        double distance = Math.Abs(current.Distance - existing.Distance);
                        if (distance < minDistanceThreshold && existing.Level == level)
                        {
                            hasConflict = true;
                            level++;
                            break;
                        }
                    }
                }

                finalAnnotations.Add((current.Distance, current.CaoDoDay, current.ViTri, level));
            }

            // Tạo các chú thích
            foreach (var item in finalAnnotations)
            {
                double yPosition = item.CaoDoDay + verticalOffset + (item.Level * verticalSpacing);

                var annotation = new Charting.TextAnnotation
                {
                    Text = item.ViTri,
                    AxisX = chart.ChartAreas[0].AxisX, // Gắn với trục X
                    AxisY = chart.ChartAreas[0].AxisY, // Gắn với trục Y
                    X = item.Distance,
                    Y = yPosition,
                    Font = new Drawing.Font("Arial", 9, Drawing.FontStyle.Bold),
                    ForeColor = Drawing.Color.Black,
                    AnchorX = item.Distance,
                    AnchorY = yPosition,
                    AllowMoving = false,
                    IsMultiline = false,
                    Alignment = ContentAlignment.BottomCenter,
                    BackColor = Drawing.Color.FromArgb(220, 255, 255, 255), // Màu nền trắng bán trong suốt
                    LineColor = Drawing.Color.Gray,
                    LineWidth = 1
                };

                // Tìm điểm gần nhất trên đường cao độ đáy
                var nearestCaoDoDayPoint = seriesCaoDoDay.Points.OrderBy(p => Math.Abs(p.XValue - item.Distance)).FirstOrDefault();
                if (nearestCaoDoDayPoint != null)
                {
                    annotation.AnchorDataPoint = nearestCaoDoDayPoint;

                    // Tính chiều cao tổng thể sau khi tăng
                    double totalHeight = baseAnnotationHeight * heightMultiplier;
                    double halfHeight = totalHeight / 2.0; // Chia đều lên và xuống

                    // Tính giới hạn trên và dưới của trục Y
                    double maxY = chart.ChartAreas[0].AxisY.Maximum; // Giá trị tối đa (12)
                    double minY = chart.ChartAreas[0].AxisY.Minimum; // Giá trị tối thiểu (-25)
                    double topY = item.CaoDoDay + halfHeight; // Điểm trên cùng
                    double bottomY = item.CaoDoDay - halfHeight; // Điểm dưới cùng

                    // Điều chỉnh chiều cao nếu vượt giới hạn
                    if (topY > maxY)
                    {
                        halfHeight = maxY - item.CaoDoDay;
                        totalHeight = 2.0 * halfHeight;
                    }
                    if (bottomY < minY)
                    {
                        halfHeight = item.CaoDoDay - minY;
                        totalHeight = 2.0 * halfHeight;
                    }

                    // Thêm đường thẳng dọc, kéo đều lên trên và xuống dưới
                    var lineAnnotation = new Charting.LineAnnotation
                    {
                        AxisX = chart.ChartAreas[0].AxisX, // Gắn với trục X
                        AxisY = chart.ChartAreas[0].AxisY, // Gắn với trục Y
                        X = item.Distance, // Tọa độ X tại điểm dữ liệu
                        Y = item.CaoDoDay - halfHeight, // Bắt đầu từ điểm dưới cùng
                        Height = totalHeight, // Chiều cao tổng thể
                        Width = 0, // Đường thẳng dọc
                        LineColor = Drawing.Color.DarkBlue,
                        LineWidth = 1,
                        StartCap = Charting.LineAnchorCapStyle.None, // Không có mũi tên ở đầu dưới
                        EndCap = Charting.LineAnchorCapStyle.Arrow, // Có mũi tên ở đầu trên
                        AnchorAlignment = ContentAlignment.TopCenter,
                        IsSizeAlwaysRelative = false // Sử dụng tọa độ dữ liệu tuyệt đối
                    };
                    chart.Annotations.Add(lineAnnotation);
                }

                chart.Annotations.Add(annotation);
            }
        }
        private void FrmMucnuoc_Load(object sender, EventArgs e)
        {
            chart.MouseMove += Chart_MouseMove;
        }
        private ToolTip chartToolTip = new ToolTip();
        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            var result = chart.HitTest(e.X, e.Y);

            if (result.ChartElementType == Charting.ChartElementType.DataPoint)
            {
                var point = result.Series.Points[result.PointIndex];
                string tooltipText = $"{result.Series.Name}\nX = {point.XValue:0.##}, Y = {point.YValues[0]:0.##}";

                chartToolTip.Show(tooltipText, chart, e.X + 15, e.Y - 15);
            }
            else
            {
                chartToolTip.Hide(chart);
            }
        }

        private async void btnLoadExcel_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|Excel files (*.xls)|*.xls|All files (*.*)|*.*",
                Title = "Chọn file Excel"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Hiển thị thanh tiến trình và nhãn trạng thái
                        progressBar.Visible = true;
                        statusLabel.Visible = true;
                        progressBar.Value = 0;
                        progressBar.Maximum = qValues.Length;
                        statusLabel.Text = "Đang tải: 0%";

                        await LoadExcelData(openFileDialog.FileName);
                        UpdateComboBoxWithLoadedData();
                        comboBoxQ.SelectedItem = "Qxa 2800"; // Đặt lựa chọn mặc định là Qxa 2800
                        UpdateChart();

                        string successMsg = $"Đã tải thành công {excelDataSets.Count} sheet dữ liệu:\n";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi đọc file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Diagnostics.Debug.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
                    }
                    finally
                    {
                        // Ẩn thanh tiến trình và nhãn trạng thái khi hoàn tất
                        progressBar.Visible = false;
                        statusLabel.Visible = false;
                    }
                }
            }
        }

        private void comboBoxQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }
    }
}
