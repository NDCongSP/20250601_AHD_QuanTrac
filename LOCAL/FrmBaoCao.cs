using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace RegistrationForm1
{       
    public partial class FrmBaoCao : Form
    {
        private string currentDataType = "MucNuoc"; // Giá trị mặc định ban đầu
        // --- Khai báo stationMapping ở đây để toàn bộ class dùng được ---
        private readonly Dictionary<string, string> stationMapping = new Dictionary<string, string>
    {
        { "Station_1", "Trạm 1" },
        { "Station_2", "Trạm 2" },
        { "Station_3", "Trạm 3" }
        // Thêm các trạm khác ở đây
    };

        public FrmBaoCao()
        {
            InitializeComponent();
            Load += FrmBaoCao_Load;
           cbFrequency.SelectedIndexChanged += cbFrequency_SelectedIndexChanged;
        }
     
        private void FrmBaoCao_Load(object sender, EventArgs e)
        { // Thiết lập chế độ báo cáo mặc định

            cbxExportType.Items.AddRange(new string[] { "Excel", "PDF" });
            cbxExportType.SelectedIndex = 1;

            dtFrom.Format = DateTimePickerFormat.Custom;
            dtFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            dtTo.Format = DateTimePickerFormat.Custom;
            dtTo.CustomFormat = "dd/MM/yyyy HH:mm";

            dtTo.Value = DateTime.Now;
            dtFrom.Value = DateTime.Now.AddMinutes(-120);

            using var dbContext = new ApplicationDbContext();

            // Lấy danh sách các trạm từ DB (chỉ 3 trạm)
            var stations = dbContext.FT03s
                .Where(x => (x.IsDeleted ?? false) == false &&
                            (x.StationName == "Station_1" ||
                             x.StationName == "Station_2" ||
                             x.StationName == "Station_3"))
                .Select(x => x.StationName)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            // Ánh xạ sang danh sách StationItem (hiển thị tiếng Việt)
            var stationItems = new List<StationItem>
    {
        new StationItem { Code = null, DisplayName = "Tất cả" }
    };

            foreach (var st in stations)
            {
                stationItems.Add(new StationItem
                {
                    Code = st,
                    DisplayName = st switch
                    {
                        "Station_1" => "Trạm 1",
                        "Station_2" => "Trạm 2",
                        "Station_3" => "Trạm 3",
                        _ => st
                    }
                });
            }

            // --- B3: Load tần suất ---
            cbFrequency.Items.Clear();
            cbFrequency.Items.AddRange(new string[]
            {
        "Tất cả",
        "10 phút",
        "15 phút",
        "30 phút",
        "60 phút",
        "1 Ngày"
            });
            cbFrequency.SelectedIndex = 2;
          

        }
       

        public class StationItem
        {
            public string Code { get; set; }        // Station_1, Station_2, ...
            public string DisplayName { get; set; } // Tên tiếng Việt: Trạm 1, Trạm 2

            public override string ToString()
            {
                return DisplayName;
            }
        }

        private void ExportDataGridViewToPDF(DataGridView dgv, string filePath)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất PDF!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 40f, 20f);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // 1. Load font Unicode (Arial hoặc font .ttf từ hệ thống)
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var titleFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                var headerFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                var normalFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                var italicFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC);

                // 2. Logo
                string logoPath = @"D:\Logo\logo.png"; // Thay bằng đường dẫn logo thực tế
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(80f, 80f);
                    logo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(logo);
                }

                // 3. Thông tin công ty
                Paragraph companyInfo = new Paragraph
        {
            new Chunk("CÔNG TY TNHH MTV KHAI THÁC THUỶ LỢI MIỀN NAM\n", headerFont),
            new Chunk("178 Nguyễn Văn Thương, Q. Bình Thạnh, TP.HCM\n", normalFont),
            new Chunk("Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n\n", normalFont)
        };
                companyInfo.Alignment = Element.ALIGN_LEFT;
                doc.Add(companyInfo);

                // 4. Tiêu đề chính
                Paragraph title = new Paragraph("BÁO CÁO DỮ LIỆU VẬN HÀNH", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                doc.Add(title);

                // 5. Bảng dữ liệu
                PdfPTable table = new PdfPTable(dgv.Columns.Count)
                {
                    WidthPercentage = 100
                };

                // Header bảng
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, headerFont))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }

                // Dữ liệu từng dòng
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellText = cell.Value?.ToString() ?? "";
                        table.AddCell(new Phrase(cellText, normalFont));
                    }
                }

                doc.Add(table);

                // 6. Footer
                Paragraph footer = new Paragraph("\nNgười lập báo cáo: ....................................", italicFont)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingBefore = 30f
                };
                doc.Add(footer);

                Paragraph note = new Paragraph("Ghi chú: Dữ liệu được xuất từ hệ thống tự động.", italicFont)
                {
                    SpacingBefore = 15f
                };
                doc.Add(note);

                MessageBox.Show("Xuất PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }

        private void ExportToExcel(DataGridView dgv, string filePath)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Báo cáo");
                ws.Style.Font.FontName = "Arial";

                int startRow = 8; // vị trí header bảng

                // === 1. LOGO góc trái ===
                string logoPath = @"D:\Logo\logo.png"; // Đổi đường dẫn thật
                if (File.Exists(logoPath))
                {
                    try
                    {
                        ws.AddPicture(logoPath)
                          .MoveTo(ws.Cell("A1"))
                          .WithPlacement(XLPicturePlacement.FreeFloating)
                          .Scale(0.5);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi chèn logo: " + ex.Message);
                    }
                }

                // === 2. THÔNG TIN CÔNG TY (bên trái) ===
                var companyRange = ws.Range("C1", "H1");
                companyRange.Merge();
                companyRange.Value = "CÔNG TY TNHH MTV KHAI THÁC THUỶ LỢI MIỀN NAM";
                companyRange.Style.Font.Bold = true;
                companyRange.Style.Font.FontSize = 11;

                var addressRange = ws.Range("C2", "H2");
                addressRange.Merge();
                addressRange.Value = "178 Nguyễn Văn Thương, Quận Bình Thạnh, TP.HCM";
                addressRange.Style.Font.Italic = true;
                addressRange.Style.Font.FontSize = 8;

                var contactRange = ws.Range("C3", "H3");
                contactRange.Merge();
                contactRange.Value = "Điện thoại: (028) 38 998 888 | Website: www.thuyloimiennam.vn";
                contactRange.Style.Font.FontSize = 8;

                // === 3. NGÀY XUẤT (bên phải) ===
                ws.Cell("I1").Value = "Ngày xuất:";
                ws.Cell("J1").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                ws.Range("I1:J1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                // === 4. TIÊU ĐỀ CHÍNH ===
                var title = ws.Range("C5:J5");
                title.Merge();
                title.Value = "BÁO CÁO HOẠT ĐỘNG HỆ THỐNG";
                title.Style.Font.Bold = true;
                title.Style.Font.FontSize = 16;
                title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                // === 5. Cố định độ rộng cột tiêu đề (fix lỗi giãn cột C & H) ===
                ws.Column("C").Width = 25;
                ws.Column("H").Width = 25;
                ws.Columns("A:H").Style.Alignment.WrapText = true;

                // === 6. Header bảng ===
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    var cell = ws.Cell(startRow, i + 1);
                    cell.SetValue(dgv.Columns[i].HeaderText);
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(221, 235, 247);
                    cell.Style.Font.Bold = true;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // === 7. Dữ liệu ===
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        var value = dgv.Rows[i].Cells[j].Value;
                        var cell = ws.Cell(startRow + 1 + i, j + 1);
                        cell.SetValue(value?.ToString() ?? "");
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }

                // === 8. Viền bảng ===
                var tableRange = ws.Range(startRow, 1, startRow + dgv.Rows.Count, dgv.Columns.Count);
                tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                // === 9. AutoFit riêng vùng dữ liệu (không ảnh hưởng tiêu đề) ===
                int maxWidth = 40;
                for (int i = 1; i <= dgv.Columns.Count; i++)
                {
                    var col = ws.Column(i);
                    col.AdjustToContents(startRow, startRow + dgv.Rows.Count);
                    if (col.Width > maxWidth) col.Width = maxWidth;
                }

                // === 10. Freeze Header ===
                ws.SheetView.FreezeRows(startRow);

                // === 11. FOOTER ===
                int footerRow = startRow + dgv.Rows.Count + 3;

                var footerLeft = ws.Range(footerRow, 1, footerRow, 3);
                footerLeft.Merge();
                footerLeft.Value = "Người lập báo cáo";
                footerLeft.Style.Font.Bold = true;
                footerLeft.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var footerRight = ws.Range(footerRow, 6, footerRow, 8);
                footerRight.Merge();
                footerRight.Value = "Người kiểm duyệt";
                footerRight.Style.Font.Bold = true;
                footerRight.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // chừa khoảng trống ký tên
                ws.Row(footerRow + 1).Height = 25;

                var note = ws.Range(footerRow + 3, 1, footerRow + 3, dgv.Columns.Count);
                note.Merge();
                note.Value = "Ghi chú: Dữ liệu được trích xuất tự động từ hệ thống SCADA – chỉ mang tính tham khảo.";
                note.Style.Font.FontSize = 10;
                note.Style.Font.Italic = true;

                // === 12. Định dạng trang in ===
                ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                ws.PageSetup.AdjustTo(90);
                ws.PageSetup.CenterHorizontally = true;

                // === 13. Lưu file ===
                workbook.SaveAs(filePath);
            }

            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bntExportExcelOrPdf_Click(object sender, EventArgs e)
        {
            if (cbxExportType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn kiểu xuất (Excel hoặc PDF)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedType = cbxExportType.SelectedItem.ToString();

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = (selectedType == "Excel") ? "Excel Workbook|*.xlsx" : "PDF Document|*.pdf";
               // sfd.Title = "Chọn nơi lưu " + selectedType;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;

                    if (selectedType == "Excel")
                    {
                        ExportToExcel(Dgv, sfd.FileName); // Gọi hàm với filePath truyền vào; // hàm bạn đã có
                    }
                    else if (selectedType == "PDF")
                    {
                        ExportDataGridViewToPDF(Dgv, filePath); // hàm mới đã xử lý font + logo + công ty
                    }
                }
            }

        }

        private void DrawChartFromVanHanhData()
        {
            if (Dgv.DataSource == null) return;

            var data = ((IEnumerable<dynamic>)Dgv.DataSource).ToList();
            if (data.Count == 0) return;

            var chart = ChartMucnuoc;
            chart.Series.Clear();
            chart.Titles.Clear();

            var area = chart.ChartAreas[0];
            area.AxisX.LabelStyle.Format = "dd/MM HH:mm";
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            area.AxisY.Title = "Giá trị (cm/mm)";

            // 🔹 Bật zoom & scroll
            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.AxisY.ScaleView.Zoomable = true;
            area.AxisX.ScrollBar.IsPositionedInside = true;
            area.AxisY.ScrollBar.IsPositionedInside = true;

            // --- Series cho Độ mở cửa (6 cửa) ---
            for (int i = 1; i <= 6; i++)
            {
                string columnName = $"Độ_mở_cửa_{i}";
                if (data.First().GetType().GetProperty(columnName) == null)
                    continue;

                var series = new System.Windows.Forms.DataVisualization.Charting.Series($"Cửa {i}")
                {
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    BorderWidth = 2,
                    XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                };

                foreach (var row in data)
                {
                    var value = (double?)row.GetType().GetProperty(columnName)?.GetValue(row, null);
                    if (value.HasValue)
                        series.Points.AddXY(row.Thời_gian, value.Value);
                }

                chart.Series.Add(series);
            }

            // --- Series cho HT xy lanh (6 cửa x 2 xy lanh) ---
            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    string columnName = $"HT_Xy_Lanh{i}_{j}";
                    if (data.First().GetType().GetProperty(columnName) == null)
                        continue;

                    var series = new System.Windows.Forms.DataVisualization.Charting.Series($"HT xy lanh {i}_{j}")
                    {
                        ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                        BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash,
                        BorderWidth = 1,
                        XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                    };

                    foreach (var row in data)
                    {
                        var value = (double?)row.GetType().GetProperty(columnName)?.GetValue(row, null);
                        if (value.HasValue)
                            series.Points.AddXY(row.Thời_gian, value.Value);
                    }

                    chart.Series.Add(series);
                }
            }

            chart.Titles.Add("Biểu đồ độ mở cửa & HT xy lanh theo thời gian (6 cửa - 3 trạm)");
            chart.ChartAreas[0].RecalculateAxesScale();

            // 🔹 Gắn sự kiện zoom bằng wheel (chỉ gắn 1 lần)
            chart.MouseWheel -= Chart_MouseWheelZoom;
            chart.MouseWheel += Chart_MouseWheelZoom;
        }

        private void DrawChartFromMucNuocData()
        {
            if (Dgv.DataSource == null) return;

            var data = ((IEnumerable<dynamic>)Dgv.DataSource).ToList();
            if (data.Count == 0) return;

            var chart = ChartMucnuoc;
            chart.Series.Clear();
            chart.Titles.Clear();

            var area = chart.ChartAreas[0];
            area.AxisX.LabelStyle.Format = "dd/MM HH:mm";
            area.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            area.AxisX.LabelStyle.Angle = -45;

            // ✅ Bật zoom & scroll
            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;

            area.CursorX.LineColor = Color.Transparent;
            area.CursorY.LineColor = Color.Transparent;

            area.AxisX.ScaleView.Zoomable = true;
            area.AxisY.ScaleView.Zoomable = true;
            area.AxisX.ScrollBar.IsPositionedInside = true;
            area.AxisY.ScrollBar.IsPositionedInside = true;

            // --- Tạo series ---
            var colors = new Dictionary<string, Color>
    {
        { "Mực nước hồ", Color.Blue },
        { "Mực nước Bến Súc", Color.Green },
        { "Mực nước Dầu Tiếng", Color.Red },
        { "Mực nước Sơn Đài", Color.Orange },
        { "Mực nước Bình Nhâm", Color.Purple },
        { "Mực nước TL CDD", Color.Brown }
    };

            var seriesList = colors.ToDictionary(
                kv => kv.Key,
                kv => new System.Windows.Forms.DataVisualization.Charting.Series(kv.Key)
                {
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    BorderWidth = 2,
                    Color = kv.Value,
                    XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                });

            foreach (var row in data.OrderBy(x => x.Thời_gian))
            {
                var time = row.Thời_gian;
                void AddPoint(System.Windows.Forms.DataVisualization.Charting.Series s, double? value)
                {
                    var idx = s.Points.AddXY(time, value ?? 0);
                    s.Points[idx].ToolTip = $"{s.Name}\nThời gian: {time:dd/MM HH:mm}\nGiá trị: {value:0.00}";
                }

                AddPoint(seriesList["Mực nước hồ"], row.Mực_nước_hồ);
                AddPoint(seriesList["Mực nước Bến Súc"], row.Mực_nước_bến_súc);
                AddPoint(seriesList["Mực nước Dầu Tiếng"], row.Mực_nước_đầu_tiếng);
                AddPoint(seriesList["Mực nước Sơn Đài"], row.Mực_nước_sơn_đài);
                AddPoint(seriesList["Mực nước Bình Nhâm"], row.Mực_nước_bình_nhâm);
                AddPoint(seriesList["Mực nước TL CDD"], row.Mực_nước_tl_cdd);
            }

            foreach (var s in seriesList.Values)
                chart.Series.Add(s);

            chart.Titles.Add("Biểu đồ mực nước theo thời gian");
            chart.ChartAreas[0].RecalculateAxesScale();

            // ✅ Reset zoom khi double-click
            chart.DoubleClick -= Chart_DoubleClickReset;
            chart.DoubleClick += Chart_DoubleClickReset;

            // ✅ Zoom bằng con lăn chuột
            chart.MouseWheel -= Chart_MouseWheelZoom;
            chart.MouseWheel += Chart_MouseWheelZoom;
        }

        // --- Hàm reset zoom ---
        private void Chart_DoubleClickReset(object sender, EventArgs e)
        {
            var chart = sender as System.Windows.Forms.DataVisualization.Charting.Chart;
            foreach (var area in chart.ChartAreas)
            {
                area.AxisX.ScaleView.ZoomReset();
                area.AxisY.ScaleView.ZoomReset();
            }
        }

        // --- Hàm zoom bằng con lăn ---
        private void Chart_MouseWheelZoom(object sender, MouseEventArgs e)
        {
            var chart = sender as System.Windows.Forms.DataVisualization.Charting.Chart;
            var area = chart.ChartAreas[0];

            try
            {
                double xMin = area.AxisX.ScaleView.ViewMinimum;
                double xMax = area.AxisX.ScaleView.ViewMaximum;
                double posX = area.AxisX.PixelPositionToValue(e.Location.X);

                if (e.Delta < 0)
                {
                    // Lăn xuống → zoom out
                    area.AxisX.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0)
                {
                    // Lăn lên → zoom in
                    double zoomFactor = 0.5; // tỉ lệ zoom
                    double newMin = posX - (xMax - xMin) * zoomFactor / 2;
                    double newMax = posX + (xMax - xMin) * zoomFactor / 2;
                    area.AxisX.ScaleView.Zoom(newMin, newMax);
                }
            }
            catch { }
        }





        private void ExportChartAsImage()
        {
            if (ChartMucnuoc.Series.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất biểu đồ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                sfd.Title = "Lưu biểu đồ dưới dạng hình ảnh";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Xác định định dạng ảnh
                        var format = sfd.FileName.EndsWith(".jpg") ? ChartImageFormat.Jpeg : ChartImageFormat.Png;

                        // Lưu biểu đồ
                        ChartMucnuoc.SaveImage(sfd.FileName, format);

                        MessageBox.Show("Xuất biểu đồ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất biểu đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void bntExportBieuDo_Click(object sender, EventArgs e)
        {
            ExportChartAsImage();
        }
        private void LoadDataVanHanh()
        {
            string selectedFrequency = cbFrequency.SelectedItem?.ToString() ?? "Tất cả";
            DateTime fromTime = dtFrom.Value;
            DateTime toTime = dtTo.Value;

            if (fromTime >= toTime)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var dbContext = new ApplicationDbContext();

                // --- Lấy dữ liệu cả 3 trạm ---
                var rawData = dbContext.FT03s
                    .Where(x => (x.IsDeleted ?? false) == false
                                && x.CreateAt.HasValue
                                && x.CreateAt.Value >= fromTime
                                && x.CreateAt.Value <= toTime
                                && (x.StationName == "Station_1" ||
                                    x.StationName == "Station_2" ||
                                    x.StationName == "Station_3"))
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        x.StationName,
                        x.CreateAt,
                        x.Door1_Aperture_Final,
                        x.Door2_Aperture_Final,
                        x.HT_Cylinder1_1_Final,
                        x.HT_Cylinder1_2_Final,
                        x.HT_Cylinder2_1_Final,
                        x.HT_Cylinder2_2_Final,
                        x.Q_i_1,
                        x.Q_i_2,
                        x.Q_tr

                    })
                    .ToList();

                // --- Gom 3 trạm thành 1 dòng theo cùng thời điểm ---
                var groupedData = rawData
                    .GroupBy(x => x.CreateAt.Value)
                    .Select(g =>
                    {
                        var station1 = g.FirstOrDefault(x => x.StationName == "Station_1");
                        var station2 = g.FirstOrDefault(x => x.StationName == "Station_2");
                        var station3 = g.FirstOrDefault(x => x.StationName == "Station_3");

                        return new
                        {
                            Thời_gian = g.Key,
                            Độ_mở_cửa_1 = station1?.Door1_Aperture_Final,
                            Độ_mở_cửa_2 = station1?.Door2_Aperture_Final,
                            Độ_mở_cửa_3 = station2?.Door1_Aperture_Final,
                            Độ_mở_cửa_4 = station2?.Door2_Aperture_Final,
                            Độ_mở_cửa_5 = station3?.Door1_Aperture_Final,
                            Độ_mở_cửa_6 = station3?.Door2_Aperture_Final,
                            HT_Xy_Lanh1_1 = station1?.HT_Cylinder1_1_Final,
                            HT_Xy_Lanh1_2 = station1?.HT_Cylinder1_2_Final,
                            HT_Xy_Lanh2_1 = station1?.HT_Cylinder2_1_Final,
                            HT_Xy_Lanh2_2 = station1?.HT_Cylinder2_2_Final,
                            HT_Xy_Lanh3_1 = station2?.HT_Cylinder1_1_Final,
                            HT_Xy_Lanh3_2 = station2?.HT_Cylinder1_2_Final,
                            HT_Xy_Lanh4_1 = station2?.HT_Cylinder2_1_Final,
                            HT_Xy_Lanh4_2 = station2?.HT_Cylinder2_2_Final,
                            HT_Xy_Lanh5_1 = station3?.HT_Cylinder1_1_Final,
                            HT_Xy_Lanh5_2 = station3?.HT_Cylinder1_2_Final,
                            HT_Xy_Lanh6_1 = station3?.HT_Cylinder2_1_Final,
                            HT_Xy_Lanh6_2 = station3?.HT_Cylinder2_2_Final,
                            Qi_1 = station1?.Q_i_1,
                            Qi_2 = station1?.Q_i_2,
                            Qi_3 = station2?.Q_i_1,
                            Qi_4 = station2?.Q_i_2,
                            Qi_5 = station3?.Q_i_1,
                            Qi_6 = station3?.Q_i_2,
                            Q_Tổng = station1?.Q_tr


                        };
                    })
                    .OrderBy(x => x.Thời_gian)
                    .ToList();

                // --- Lọc tần suất ---
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
                DateTime? lastTime = null;

                foreach (var row in groupedData)
                {
                    if (frequencyMinutes == 0 || lastTime == null ||
                        (row.Thời_gian - lastTime.Value).TotalMinutes >= frequencyMinutes)
                    {
                        filteredData.Add(row);
                        lastTime = row.Thời_gian;
                    }
                }

                // --- Hiển thị ---
                Dgv.DataSource = null;
                Dgv.AutoGenerateColumns = true;
                Dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                Dgv.DataSource = filteredData;

                // --- Đổi tiêu đề cột ---
                Dgv.Columns["Thời_gian"].HeaderText = "Thời gian";
                for (int i = 1; i <= 6; i++)
                {
                    if (Dgv.Columns.Contains($"Độ_mở_cửa_{i}"))
                        Dgv.Columns[$"Độ_mở_cửa_{i}"].HeaderText = $"Độ mở cửa {i} (cm)";
                }
                for (int i = 1; i <= 3; i++)
                {
                    if (Dgv.Columns.Contains($"HT_Xy_Lanh{i}_1"))
                        Dgv.Columns[$"HT_Xy_Lanh{i}_1"].HeaderText = $"HT xy lanh {i}_1 (mm)";
                    if (Dgv.Columns.Contains($"HT_Xy_Lanh{i}_2"))
                        Dgv.Columns[$"HT_Xy_Lanh{i}_2"].HeaderText = $"HT xy lanh {i}_2 (mm)";
                }

                if (filteredData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                        DrawChartFromVanHanhData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void bntDataVanHanh_Click(object sender, EventArgs e)
        {
            currentDataType = "VanHanh";
            LoadDataVanHanh();
        }


        private void bntDataMucNuoc_Click(object sender, EventArgs e)
        {
            currentDataType = "MucNuoc";
            LoadDataMucNuoc();
        }
        private void LoadDataMucNuoc()
        {

            string selectedFrequency = cbFrequency.SelectedItem?.ToString() ?? "Tất cả";
            DateTime fromTime = dtFrom.Value;
            DateTime toTime = dtTo.Value;

            if (fromTime >= toTime)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var dbContext = new ApplicationDbContext();

                // --- Lấy dữ liệu chỉ 3 trạm ---
                var query = dbContext.FT03s
                    .Where(x => (x.IsDeleted ?? false) == false
                                && x.CreateAt.HasValue
                                && x.CreateAt.Value >= fromTime
                                && x.CreateAt.Value <= toTime
                                && (x.StationName == "Location_Info"));


                var rawData = query
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        x.StationName,
                        x.CreateAt,
                        x.API_Fllow_TL_CDD,
                        x.Fllow_Ho_Final,
                        x.API_Fllow_SonDai,
                        x.API_Fllow_DauTieng,
                        x.API_Fllow_BenSuc,
                        x.API_Fllow_BinhNham


                    })
                    .ToList();

                // Map sang tiếng Việt MN HỒ (TL-CDD)   (m)
                var mappedData = rawData.Select(x => new
                {
                    Trạm = x.StationName switch
                    {
                        "Location_Info" => "Trạm mức nước",

                        _ => x.StationName
                    },
                    Thời_gian = x.CreateAt.Value,
                    Mực_nước_tl_cdd = x.API_Fllow_TL_CDD,
                    Mực_nước_hồ = x.Fllow_Ho_Final,
                    Mực_nước_sơn_đài = x.API_Fllow_SonDai,
                    Mực_nước_đầu_tiếng = x.API_Fllow_DauTieng,
                    Mực_nước_bến_súc = x.API_Fllow_BenSuc,
                    Mực_nước_bình_nhâm = x.API_Fllow_BinhNham


                }).ToList();

                // Lọc tần suất
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
                    // Nhóm theo Trạm trước
                    var grouped = mappedData
                        .GroupBy(x => x.Trạm)
                        .OrderBy(g => g.Key);

                    foreach (var group in grouped)
                    {
                        DateTime? lastTime = null;
                        foreach (var row in group.OrderBy(x => x.Thời_gian))
                        {
                            if (lastTime == null || (row.Thời_gian - lastTime.Value).TotalMinutes >= frequencyMinutes)
                            {
                                filteredData.Add(row);
                                lastTime = row.Thời_gian;
                            }
                        }
                    }

                    // Sắp xếp lại toàn bộ dữ liệu sau khi gộp
                    filteredData = filteredData.OrderBy(x => x.Thời_gian).ToList();
                }
                else
                {
                    filteredData = mappedData.Cast<dynamic>().ToList();
                }

                Dgv.DataSource = null;
                Dgv.DataSource = filteredData;


                // Ẩn cột Trạm
                if (Dgv.Columns.Contains("Trạm"))
                    Dgv.Columns["Trạm"].Visible = false;

                // Đổi tiêu đề cột
                if (Dgv.Columns.Contains("Thời_gian"))
                    Dgv.Columns["Thời_gian"].HeaderText = "Thời gian";
                if (Dgv.Columns.Contains("Mực_nước_tl_cdd"))
                    Dgv.Columns["Mực_nước_tl_cdd"].HeaderText = "MN HỒ (TL-CDD)   (m)";
                if (Dgv.Columns.Contains("Mực_nước_hồ"))
                    Dgv.Columns["Mực_nước_hồ"].HeaderText = "MN HỒ (TL_TXL) (m)";
                if (Dgv.Columns.Contains("Mực_nước_sơn_đài"))
                    Dgv.Columns["Mực_nước_sơn_đài"].HeaderText = "TRẠM SƠN ĐÀI    (m)";
                if (Dgv.Columns.Contains("Mực_nước_đầu_tiếng"))
                    Dgv.Columns["Mực_nước_đầu_tiếng"].HeaderText = "TRẠM TV DẦU TIẾNG  (m)";
                if (Dgv.Columns.Contains("Mực_nước_bến_súc"))
                    Dgv.Columns["Mực_nước_bến_súc"].HeaderText = "TRẠM BẾN SÚC  (m)";
                if (Dgv.Columns.Contains("Mực_nước_bình_nhâm"))
                    Dgv.Columns["Mực_nước_bình_nhâm"].HeaderText = "TRẠM BÌNH NHÂM  (m)";


                if (filteredData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                     DrawChartFromMucNuocData();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataTongHop()
        {
            string selectedFrequency = cbFrequency.SelectedItem?.ToString() ?? "Tất cả";
            DateTime fromTime = dtFrom.Value;
            DateTime toTime = dtTo.Value;

            if (fromTime >= toTime)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var dbContext = new ApplicationDbContext();

                // --- Lấy dữ liệu chỉ 3 trạm ---
                var query = dbContext.FT03s
                    .Where(x => (x.IsDeleted ?? false) == false
                                && x.CreateAt.HasValue
                                && x.CreateAt.Value >= fromTime
                                && x.CreateAt.Value <= toTime
                                && (x.StationName == "Location_Info"));


                var rawData = query
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        x.StationName,
                        x.CreateAt,
                        x.Fllow_Ho_Final,
                        x.W1_ho,
                        x.Q_den,
                        x.W_den,
                        x.Q_tr,
                        x.W_tr,
                        x.Q_cs1,
                        x.W_cs1,
                        x.Q_cs2,
                        x.W_cs2,
                        x.Q_cs3,
                        x.W_cs3,
                        x.Q_tt,
                        x.W_tt,
                        x.Q_di,
                        x.W_di

                    })
                    .ToList();

                // Map sang tiếng Việt + làm tròn 2 chữ số thập phân
                var mappedData = rawData.Select(x => new
                {
                    Trạm = x.StationName switch
                    {
                        "Location_Info" => "Trạm mức nước",
                        _ => x.StationName
                    },
                    Thời_gian = x.CreateAt.Value,
                    Mực_nước_hồ = Math.Round(Convert.ToDouble(x.Fllow_Ho_Final ?? 0), 2),
                    W_Hồ = Math.Round(x.W1_ho, 2),
                    Q_đến = Math.Round(x.Q_den, 2),
                    W_đến = Math.Round(x.W_den, 2),
                    Q_tràn = Math.Round(x.Q_tr, 2),
                    W_tràn = Math.Round(x.W_tr, 2),
                    Q_cống_sô1 = Math.Round(x.Q_cs1, 2),
                    W_cống_sô1 = Math.Round(x.W_cs1, 2),
                    Q_cống_sô2 = Math.Round(x.Q_cs2, 2),
                    W_cống_sô2 = Math.Round(x.W_cs2, 2),
                    Q_cống_sô3 = Math.Round(x.Q_cs3, 2),
                    W_cống_sô3 = Math.Round(x.W_cs3, 2),
                    Q_tổn_thất = Math.Round(x.Q_tt, 2),
                    W_tổn_thất = Math.Round(x.W_tt, 2),
                    Q_đi = Math.Round(x.Q_di, 2),
                    W_đi = Math.Round(x.W_di, 2),



                }).ToList();

                // Lọc tần suất
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
                    // Nhóm theo Trạm trước
                    var grouped = mappedData
                        .GroupBy(x => x.Trạm)
                        .OrderBy(g => g.Key);

                    foreach (var group in grouped)
                    {
                        DateTime? lastTime = null;
                        foreach (var row in group.OrderBy(x => x.Thời_gian))
                        {
                            if (lastTime == null || (row.Thời_gian - lastTime.Value).TotalMinutes >= frequencyMinutes)
                            {
                                filteredData.Add(row);
                                lastTime = row.Thời_gian;
                            }
                        }
                    }

                    // Sắp xếp lại toàn bộ dữ liệu sau khi gộp
                    filteredData = filteredData.OrderBy(x => x.Thời_gian).ToList();
                }
                else
                {
                    filteredData = mappedData.Cast<dynamic>().ToList();
                }

                Dgv.DataSource = null;
                Dgv.DataSource = filteredData;

                // Ẩn cột Trạm
                if (Dgv.Columns.Contains("Trạm"))
                    Dgv.Columns["Trạm"].Visible = false;

                // Đổi tiêu đề cột
                if (Dgv.Columns.Contains("Thời_gian"))
                    Dgv.Columns["Thời_gian"].HeaderText = "Thời gian";
                if (Dgv.Columns.Contains("Mực_nước_hồ"))
                    Dgv.Columns["Mực_nước_hồ"].HeaderText = "Mực nước hồ";
                if (Dgv.Columns.Contains("W_Hồ"))
                    Dgv.Columns["W_Hồ"].HeaderText = "W hồ";
                if (Dgv.Columns.Contains("Q_đến"))
                    Dgv.Columns["Q_đến"].HeaderText = "Q đến";
                if (Dgv.Columns.Contains("W_đến"))
                    Dgv.Columns["W_đến"].HeaderText = "W đến";
                if (Dgv.Columns.Contains("Q_tràn"))
                    Dgv.Columns["Q_tràn"].HeaderText = "Q tràn";
                if (Dgv.Columns.Contains("W_tràn"))
                    Dgv.Columns["W_tràn"].HeaderText = "W tràn";
                if (Dgv.Columns.Contains("Q_cống_sô1"))
                    Dgv.Columns["Q_cống_sô1"].HeaderText = "Q cống số 1";
                if (Dgv.Columns.Contains("W_cống_sô1"))
                    Dgv.Columns["W_cống_sô1"].HeaderText = "W cống số 1";
                if (Dgv.Columns.Contains("Q_cống_sô2"))
                    Dgv.Columns["Q_cống_sô2"].HeaderText = "Q cống số 2";
                if (Dgv.Columns.Contains("W_cống_sô2"))
                    Dgv.Columns["W_cống_sô2"].HeaderText = "W cống số 2";
                if (Dgv.Columns.Contains("Q_cống_sô3"))
                    Dgv.Columns["Q_cống_sô3"].HeaderText = "Q cống số 3";
                if (Dgv.Columns.Contains("W_cống_sô3"))
                    Dgv.Columns["W_cống_sô3"].HeaderText = "W cống số 3";
                if (Dgv.Columns.Contains("Q_tổn_thất"))
                    Dgv.Columns["Q_tổn_thất"].HeaderText = "Q tổn thất";
                if (Dgv.Columns.Contains("W_tổn_thất"))
                    Dgv.Columns["W_tổn_thất"].HeaderText = "W tổn thất";
                if (Dgv.Columns.Contains("Q_đi"))
                    Dgv.Columns["Q_đi"].HeaderText = "Q đi";
                if (Dgv.Columns.Contains("W_đi"))
                    Dgv.Columns["W_đi"].HeaderText = "W đi";




                if (filteredData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DrawChartFromTongHop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawChartFromTongHop()
        {
            if (Dgv.DataSource == null) return;

            // Lấy dữ liệu gốc từ DataGridView
            var dataList = (Dgv.DataSource as IEnumerable<object>)?.ToList();
            if (dataList == null || dataList.Count == 0) return;

            var chart = ChartMucnuoc;
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();

            // --- Khu vực biểu đồ ---
            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("Tổng hợp");
            chartArea.AxisX.LabelStyle.Format = "HH:mm\ndd/MM";
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea.AxisY.Title = "Giá trị (m³/s hoặc m)";
            chartArea.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;

            // 🔹 Bật zoom & scroll
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisY.ScaleView.Zoomable = true;

            // 🔹 Bỏ line đỏ khi zoom
            chartArea.CursorX.LineColor = Color.Transparent;
            chartArea.CursorY.LineColor = Color.Transparent;

            chartArea.AxisX.ScrollBar.IsPositionedInside = true;
            chartArea.AxisY.ScrollBar.IsPositionedInside = true;

            chart.ChartAreas.Add(chartArea);

            // --- Định nghĩa các đại lượng cần vẽ ---
            var seriesDefinitions = new Dictionary<string, string>
    {
        {"Mực_nước_hồ", "Mực nước hồ (m)"},
        {"Q_đến", "Q đến (m³/s)"},
        {"Q_tràn", "Q tràn (m³/s)"},
        {"Q_cống_sô1", "Q cống số 1 (m³/s)"},
        {"Q_cống_sô2", "Q cống số 2 (m³/s)"},
        {"Q_cống_sô3", "Q cống số 3 (m³/s)"},
        {"Q_tổn_thất", "Q tổn thất (m³/s)"},
        {"Q_đi", "Q đi (m³/s)"}
    };

            var props = dataList.First().GetType().GetProperties();

            // --- Vẽ từng đại lượng ---
            foreach (var def in seriesDefinitions)
            {
                var propValue = props.FirstOrDefault(p => p.Name == def.Key);
                var timeProp = props.FirstOrDefault(p => p.Name == "Thời_gian");

                if (propValue == null || timeProp == null)
                    continue;

                var series = new System.Windows.Forms.DataVisualization.Charting.Series(def.Value)
                {
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    BorderWidth = 2,
                    XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                };

                var ordered = dataList
                    .Select(d => new
                    {
                        Time = (DateTime)timeProp.GetValue(d),
                        Value = Convert.ToDouble(propValue.GetValue(d))
                    })
                    .OrderBy(x => x.Time);

                foreach (var item in ordered)
                {
                    int idx = series.Points.AddXY(item.Time, item.Value);
                    series.Points[idx].ToolTip =
                        $"{def.Value}\nThời gian: {item.Time:dd/MM HH:mm}\nGiá trị: {item.Value:0.00}";
                }

                chart.Series.Add(series);
            }

            chart.Titles.Add("Biểu đồ tổng hợp lưu lượng & mực nước theo thời gian");
            chart.ChartAreas[0].RecalculateAxesScale();

            // 🔹 Zoom bằng cuộn chuột
            chart.MouseWheel -= Chart_MouseWheelZoom;
            chart.MouseWheel += Chart_MouseWheelZoom;
        }


        private void bntData_Click(object sender, EventArgs e)
        {
            currentDataType = "TongHop";
            LoadDataTongHop();
        }
        


        private void LoadQTM()
        {
            string selectedFrequency = cbFrequency.SelectedItem?.ToString() ?? "Tất cả";
            DateTime fromTime = dtFrom.Value;
            DateTime toTime = dtTo.Value;

            if (fromTime >= toTime)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var dbContext = new ApplicationDbContext();

                // --- Lấy dữ liệu chỉ 3 trạm ---
                var query = dbContext.FT03s
                    .Where(x => (x.IsDeleted ?? false) == false
                                && x.CreateAt.HasValue
                                && x.CreateAt.Value >= fromTime
                                && x.CreateAt.Value <= toTime
                                && (x.StationName == "Location_Info"));


                // --- Map sang tiếng Việt và chuyển dạng dữ liệu ---
                var rawData = query
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        x.CreateAt,
                        x.API_D_DM_HoDT,
                        x.API_D_DM_HoDT_Total,
                        x.API_D_MinhHoa,
                        x.API_D_MinhHoa_Total,
                        x.API_D_MinhTam,
                        x.API_D_MinhTam_Total,
                        x.API_D_LocThien,
                        x.API_D_LocThien_Total,
                        x.API_D_LocNinh,
                        x.API_D_LocNinh_Total,
                        x.API_D_LocThanh,
                        x.API_D_LocThanh_Total,
                        x.API_D_ThanhLuong,
                        x.API_D_ThanhLuong_Total,
                        x.API_D_TanHoa1,
                        x.API_D_TanHoa1_Total,
                        x.API_D_TanHoa2,
                        x.API_D_TanHoa2_Total,
                        x.API_D_KaTum,
                        x.API_D_KaTum_Total,
                        x.API_D_TanThanh,
                        x.API_D_TanThanh_Total,
                        x.API_D_DongBan,
                        x.API_D_DongBan_Total,
                        x.API_D_TanHa,
                        x.API_D_TanHa_Total
                    })
                    .ToList();

                // --- Chuyển dạng dữ liệu thành 3 cột: Thời gian - Tên - Tức thời - Tổng tích luỹ ---
                var list = new List<dynamic>();

                foreach (var row in rawData)
                {
                    DateTime time = row.CreateAt ?? DateTime.MinValue;
                    list.Add(new { Thời_gian = time, Tên = "Đầu mối Hồ Dầu Tiếng", Tức_thời = row.API_D_DM_HoDT, Tổng_tích_lũy = row.API_D_DM_HoDT_Total });
                    list.Add(new { Thời_gian = time, Tên = "Minh Hoà", Tức_thời = row.API_D_MinhHoa, Tổng_tích_lũy = row.API_D_MinhHoa_Total });
                    list.Add(new { Thời_gian = time, Tên = "Minh Tâm", Tức_thời = row.API_D_MinhTam, Tổng_tích_lũy = row.API_D_MinhTam_Total });
                    list.Add(new { Thời_gian = time, Tên = "Lộc Thiện", Tức_thời = row.API_D_LocThien, Tổng_tích_lũy = row.API_D_LocThien_Total });
                    list.Add(new { Thời_gian = time, Tên = "Lộc Ninh", Tức_thời = row.API_D_LocNinh, Tổng_tích_lũy = row.API_D_LocNinh_Total });
                    list.Add(new { Thời_gian = time, Tên = "Lộc Thành", Tức_thời = row.API_D_LocThanh, Tổng_tích_lũy = row.API_D_LocThanh_Total });
                    list.Add(new { Thời_gian = time, Tên = "Thanh Lương", Tức_thời = row.API_D_ThanhLuong, Tổng_tích_lũy = row.API_D_ThanhLuong_Total });
                    list.Add(new { Thời_gian = time, Tên = "Tân Hoà 1", Tức_thời = row.API_D_TanHoa1, Tổng_tích_lũy = row.API_D_TanHoa1_Total });
                    list.Add(new { Thời_gian = time, Tên = "Tân Hoà 2", Tức_thời = row.API_D_TanHoa2, Tổng_tích_lũy = row.API_D_TanHoa2_Total });
                    list.Add(new { Thời_gian = time, Tên = "Kà Tum", Tức_thời = row.API_D_KaTum, Tổng_tích_lũy = row.API_D_KaTum_Total });
                    list.Add(new { Thời_gian = time, Tên = "Tân Thành", Tức_thời = row.API_D_TanThanh, Tổng_tích_lũy = row.API_D_TanThanh_Total });
                    list.Add(new { Thời_gian = time, Tên = "Đồng Ban", Tức_thời = row.API_D_DongBan, Tổng_tích_lũy = row.API_D_DongBan_Total });
                    list.Add(new { Thời_gian = time, Tên = "Tân Hà", Tức_thời = row.API_D_TanHa, Tổng_tích_lũy = row.API_D_TanHa_Total });
                }



                // Lọc tần suất
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
                    // Nhóm theo Trạm trước
                    var grouped = list
                       .GroupBy(x => x.Tên)
                       .OrderBy(g => g.Key);

                    foreach (var group in grouped)
                    {
                        DateTime? lastTime = null;
                        foreach (var row in group.OrderBy(x => x.Thời_gian))
                        {
                            if (lastTime == null || (row.Thời_gian - lastTime.Value).TotalMinutes >= frequencyMinutes)
                            {
                                filteredData.Add(row);
                                lastTime = row.Thời_gian;
                            }
                        }
                    }

                    // Sắp xếp lại toàn bộ dữ liệu sau khi gộp
                    filteredData = filteredData.OrderBy(x => x.Thời_gian).ToList();
                }
                else
                {
                    filteredData = list.OrderBy(x => x.Thời_gian).ToList();
                }
                // --- Hiển thị lên DataGridView ---
                Dgv.DataSource = null;
                Dgv.DataSource = filteredData;

                Dgv.Columns["Thời_gian"].HeaderText = "Thời gian";
                Dgv.Columns["Tên"].HeaderText = "Tên trạm";
                Dgv.Columns["Tức_thời"].HeaderText = "Tức thời (mm)";
                Dgv.Columns["Tổng_tích_lũy"].HeaderText = "Tổng tích luỹ (mm)";


                if (filteredData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DrawChartFromQTM();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DrawChartFromQTM(string mode = "Tức_thời")
        {
            if (Dgv.DataSource == null) return;

            var data = ((IEnumerable<dynamic>)Dgv.DataSource).ToList();
            if (data.Count == 0) return;

            var chart = ChartMucnuoc;
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();

            // --- Cấu hình chung ---
            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("QTM");
            chartArea.AxisX.LabelStyle.Format = "dd/MM HH:mm";
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea.AxisY.Title = mode == "Tức_thời" ? "Tức thời (mm)" : "Tổng tích luỹ (mm)";
            chartArea.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;

            // 🔹 Bật zoom và scroll, nhưng ẩn line đỏ cursor
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.LineWidth = 0; // ❌ Ẩn line đỏ trục X
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.LineWidth = 0; // ❌ Ẩn line đỏ trục Y
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisY.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = true;
            chartArea.AxisY.ScrollBar.IsPositionedInside = true;

            chart.ChartAreas.Add(chartArea);

            // --- Nhóm dữ liệu theo Tên trạm ---
            var grouped = data.GroupBy(x => x.Tên);

            foreach (var group in grouped)
            {
                var series = new System.Windows.Forms.DataVisualization.Charting.Series(group.Key)
                {
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    BorderWidth = 2,
                    XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                };

                foreach (var row in group.OrderBy(x => x.Thời_gian))
                {
                    double value = 0;
                    if (mode == "Tức_thời")
                        value = Convert.ToDouble(row.Tức_thời ?? 0);
                    else
                        value = Convert.ToDouble(row.Tổng_tích_lũy ?? 0);

                    int pointIndex = series.Points.AddXY(row.Thời_gian, value);
                    series.Points[pointIndex].ToolTip =
                        $"{group.Key}\nThời gian: {row.Thời_gian:dd/MM HH:mm}\nGiá trị: {value:0.00} mm";
                }

                chart.Series.Add(series);
            }

            chart.Titles.Add(mode == "Tức_thời"
                ? "Biểu đồ Lưu lượng tức thời (mm) theo thời gian"
                : "Biểu đồ Tổng lượng tích luỹ (mm) theo thời gian");

            chart.ChartAreas[0].RecalculateAxesScale();

            // 🔹 Zoom bằng cuộn chuột
            chart.MouseWheel -= Chart_MouseWheelZoom;
            chart.MouseWheel += Chart_MouseWheelZoom;
        }

        private void bntQTM_Click(object sender, EventArgs e)
        {
            currentDataType = "QTM";
            LoadQTM();
        }
        private void cbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu form chưa load xong hoặc combo chưa có giá trị thì bỏ qua
            if (cbFrequency.SelectedIndex < 0)
                return;

            switch (currentDataType)
            {
                case "MucNuoc":
                    LoadDataMucNuoc();
                    break;

                case "VanHanh":
                    LoadDataVanHanh();
                    break;

                case "QTM":
                    LoadQTM();
                    break;

                case "TongHop":
                    LoadDataTongHop();
                    break;

                default:
                    // Nếu không xác định được thì mặc định load lại Mực nước
                    LoadDataMucNuoc();
                    break;
            }
        }

        
    }
    
}

