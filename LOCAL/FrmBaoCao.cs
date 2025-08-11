using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using Dapper;
using GemBox.Spreadsheet;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;

//using System.Drawing; // vẫn dùng cho Image nhưng không bị lỗi font

namespace RegistrationForm1
{       
    public partial class FrmBaoCao : Form
    {
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public FrmBaoCao()
        {
            InitializeComponent();
            Load += FrmBaoCao_Load;
        }

        private void FrmBaoCao_Load(object sender, EventArgs e)
        { // Thiết lập chế độ báo cáo mặc định

            cbxExportType.Items.AddRange(new string[] { "Excel", "PDF" });
            cbxExportType.SelectedIndex = 1;

            cbTimeRange.Items.AddRange(new string[] { "10 phút", "15 phút", "30 phút", "60 phút", "1 Ngày", "Tất cả" });
            cbTimeRange.SelectedIndex = 1;

            dtFrom.Format = DateTimePickerFormat.Custom;
            dtFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtTo.Format = DateTimePickerFormat.Custom;
            dtTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dtTo.Value = DateTime.Now;
            dtFrom.Value = DateTime.Now.AddMinutes(-120);

        }
        
        private void bntDataVanHanh_Click(object sender, EventArgs e)
        {
            //  string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            string selected = cbTimeRange.SelectedItem?.ToString() ?? "Tất cả";
            DateTime fromPicker = dtFrom.Value;
            DateTime toPicker = dtTo.Value;

            if (fromPicker >= toPicker)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fromTime = fromPicker;
            DateTime toTime = toPicker;

            if (selected != "Tất cả")
            {
                int minutes = selected.Contains("1 Ngày") ? 1440 :
                              selected.Contains("60") ? 60 :
                              selected.Contains("30") ? 30 :
                              selected.Contains("15") ? 15 :
                              selected.Contains("10") ? 10 : 0;

                DateTime recentFrom = DateTime.Now.AddMinutes(-minutes);
                DateTime recentTo = DateTime.Now;

                // Giới hạn từ recentFrom - recentTo nhưng không vượt ngoài fromPicker - toPicker
                fromTime = recentFrom < fromPicker ? fromPicker : recentFrom;
                toTime = recentTo > toPicker ? toPicker : recentTo;

                if (fromTime >= toTime)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT Id, CreateAt, Fllow_Ho,Fllow_DauTieng, Door1_Aperture, Door2_Aperture, Door3_Aperture, Door4_Aperture, Door5_Aperture,
                        Door6_Aperture,Fllow_Door1,Fllow_Door2,Fllow_Door3,Fllow_Door4,Fllow_Door5,Fllow_Door6, Total_Fllow
                FROM DataVanHanh
                WHERE CreateAt BETWEEN @FromTime AND @ToTime
                ORDER BY CreateAt DESC";

                    var data = conn.Query<Vanhanh>(query, new
                    {
                        FromTime = fromTime,
                        ToTime = toTime
                    }).ToList();

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = data;

                    if (data.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DrawChartFromVanHanhData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Báo cáo");

            worksheet.Style.Font.FontName = "Arial";
            int rowStart = 7;

            // === 1. Logo ===
            string logoPath = @"D:\Logo\logo.png"; // Thay bằng đường dẫn đúng của bạn
            if (File.Exists(logoPath))
            {
                try
                {
                    worksheet.AddPicture(logoPath)
                             .MoveTo(worksheet.Cell("A1"))
                             .WithPlacement(XLPicturePlacement.FreeFloating)
                             .Scale(0.5);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi chèn logo: " + ex.Message);
                }
            }

            // === 2. Tiêu đề báo cáo ===
            var titleRange = worksheet.Range("C1:E1");
            titleRange.Merge();
            titleRange.Value = "BÁO CÁO HOẠT ĐỘNG HỆ THỐNG";
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.FontSize = 16;
            titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // === 3. Thông tin công ty ===
            worksheet.Cell("C2").Value = "CÔNG TY TNHH MTV KHAI THÁC THUỶ LỢI MIỀN NAM";
            worksheet.Cell("C3").Value = "178 Nguyễn Văn Thương, Quận Bình Thạnh, TP.HCM";
            worksheet.Range("C2:C3").Style.Font.Bold = true;
            worksheet.Range("C2:C3").Style.Font.FontSize = 11;

            worksheet.Cell("F2").Value = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            worksheet.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            // === 4. Header bảng ===
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                var cell = worksheet.Cell(rowStart, i + 1);
                cell.Value = dgv.Columns[i].HeaderText;
                cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                cell.Style.Font.Bold = true;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            // === 5. Dữ liệu bảng ===
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    var value = dgv.Rows[i].Cells[j].Value?.ToString();
                    var cell = worksheet.Cell(rowStart + 1 + i, j + 1);
                    cell.Value = value;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
            }

            // === 6. Điều chỉnh độ rộng cột (giới hạn tối đa) ===
            int maxColumnWidth = 30;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                var column = worksheet.Column(i + 1);
                column.AdjustToContents();
                if (column.Width > maxColumnWidth)
                {
                    column.Width = maxColumnWidth;
                }
            }

            // === 7. Footer ===
            int footerRow = rowStart + dgv.Rows.Count + 3;
            var footer = worksheet.Range(footerRow, 1, footerRow, 3);
            footer.Merge();
            footer.Value = "Người lập báo cáo:";
            footer.Style.Font.Italic = true;
            footer.Style.Font.Bold = true;
            footer.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var note = worksheet.Range(footerRow + 3, 1, footerRow + 3, 5);
            note.Merge();
            note.Value = "Ghi chú: Dữ liệu được xuất từ hệ thống tự động.";
            note.Style.Font.FontSize = 10;
            note.Style.Font.Italic = true;

            // === 8. Lưu file ===
            workbook.SaveAs(filePath);
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
                        ExportToExcel(dataGridView1, sfd.FileName); // Gọi hàm với filePath truyền vào; // hàm bạn đã có
                    }
                    else if (selectedType == "PDF")
                    {
                        ExportDataGridViewToPDF(dataGridView1, filePath); // hàm mới đã xử lý font + logo + công ty
                    }
                }
            }

        }

        private void DrawChartFromVanHanhData()
        {
            if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để vẽ biểu đồ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Xóa cũ
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear(); // Đặt lại legends

            // Thêm legend trước khi gán vào Series
            chart1.Legends.Add(new Legend("Legend"));

            // Thêm chart area
            chart1.ChartAreas.Add("Main");
            var area = chart1.ChartAreas["Main"];
            area.AxisX.LabelStyle.Format = "HH:mm\ndd/MM";
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.Title = "Thời gian";
            area.AxisY.Title = "Giá trị (m)";
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            // Thêm tiêu đề
            chart1.Titles.Add("Biểu đồ vận hành cánh cửa & mực nước hồ");
            // Ánh xạ tên tiếng Anh → tiếng Việt
            Dictionary<string, string> seriesLabels = new Dictionary<string, string>
            {
                { "Fllow_Ho", "Mực nước hồ" },
                { "Fllow_DauTieng", "Mực nước TV dầu tiếng" },               
                { "Door1_Aperture", "Độ mở cửa 1" },
                { "Door2_Aperture", "Độ mở cửa 2" },
                { "Door3_Aperture", "Độ mở cửa 3" },
                { "Door4_Aperture", "Độ mở cửa 4" },
                { "Door5_Aperture", "Độ mở cửa 5" },
                { "Door6_Aperture", "Độ mở cửa 6" },
                { "Total_Fllow", "Tổng lưu lượng" }

            };

                    string[] seriesNames = {
                "Fllow_Ho", "Fllow_DauTieng",
                "Door1_Aperture", "Door2_Aperture",
                "Door3_Aperture", "Door4_Aperture",
                "Door5_Aperture", "Door6_Aperture","Total_Fllow"
             };

            foreach (string seriesName in seriesNames)
            {
                if (!dataGridView1.Columns.Contains(seriesName)) continue; // Kiểm tra nếu cột không tồn tại
                string seriesLabel = seriesLabels.ContainsKey(seriesName) ? seriesLabels[seriesName] : seriesName;
                var series = new Series(seriesLabel) // gán tên tiếng Việt làm hiển thị
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    XValueType = ChartValueType.DateTime,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 4,
                    Legend = "Legend"
                };

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    var timeObj = row.Cells["CreateAt"].Value;
                    var valueObj = row.Cells[seriesName].Value;
                    if (DateTime.TryParse(timeObj?.ToString(), out DateTime time) &&
                        double.TryParse(valueObj?.ToString(), out double value))
                    {
                        series.Points.AddXY(time, value);
                    }
                }
                chart1.Series.Add(series);
            }
        }
        private void DrawChartFromMucNuoc()
        {
            if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để vẽ biểu đồ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Xóa cũ
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear(); // Đặt lại legends
            // Thêm legend trước khi gán vào Series
            chart1.Legends.Add(new Legend("Legend"));
            // Thêm chart area
            chart1.ChartAreas.Add("Main");
            var area = chart1.ChartAreas["Main"];
            area.AxisX.LabelStyle.Format = "HH:mm\ndd/MM";
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.Title = "Thời gian";
            area.AxisY.Title = "Giá trị (m)";
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            // Thêm tiêu đề
            chart1.Titles.Add("Biểu đồ  mực nước hồ");
            // Ánh xạ tên tiếng Anh → tiếng Việt
            Dictionary<string, string> seriesLabels = new Dictionary<string, string>
            {
                { "Fllow_Ho", "Mực nước hồ" },
                { "Fllow_DauTieng", "Mực Nước TV Dầu Tiếng" },
                { "Fllow_BenSuc", "Mực Nước Bến Súc" },
                { "Fllow_SonDai", "Mực Nước Sơn Đài" },
                 { "Fllow_BinhNham", "Mực Nước Bình Nhâm " }
            };
                  string[] seriesNames = {
                    "Fllow_Ho", "Fllow_DauTieng",
                    "Fllow_BenSuc", "Fllow_SonDai","Fllow_BinhNham"
              };
            

            foreach (string seriesName in seriesNames)
            {
                if (!dataGridView1.Columns.Contains(seriesName)) continue; // Kiểm tra nếu cột không tồn tại

                string seriesLabel = seriesLabels.ContainsKey(seriesName) ? seriesLabels[seriesName] : seriesName;

                var series = new Series(seriesLabel) // gán tên tiếng Việt làm hiển thị
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    XValueType = ChartValueType.DateTime,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 4,
                    Legend = "Legend"
                };

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    var timeObj = row.Cells["CreateAt"].Value;
                    var valueObj = row.Cells[seriesName].Value;

                    if (DateTime.TryParse(timeObj?.ToString(), out DateTime time) &&
                        double.TryParse(valueObj?.ToString(), out double value))
                    {
                        series.Points.AddXY(time, value);
                    }
                }

                chart1.Series.Add(series);
            }
        }

        
        //private void nbtTrend_Click(object sender, EventArgs e)
        //{
        //    DrawChartFromMucNuoc();
        //}

        private void bntDataMucNuoc_Click(object sender, EventArgs e)
        {
       //     string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            string selected = cbTimeRange.SelectedItem?.ToString() ?? "Tất cả";
            DateTime fromPicker = dtFrom.Value;
            DateTime toPicker = dtTo.Value;

            if (fromPicker >= toPicker)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fromTime = fromPicker;
            DateTime toTime = toPicker;

            // Nếu người dùng chọn "5 phút", "10 phút" v.v...
            if (selected != "Tất cả")
            {
                int minutes = 0;
                if (selected.Contains("10")) minutes = 10;
                else if (selected.Contains("15")) minutes = 15;
                else if (selected.Contains("30")) minutes = 30;
                else if (selected.Contains("60")) minutes = 60;
                else if (selected.Contains("1 Ngày")) minutes = 1440; // 1 ngày = 1440 phút

                DateTime recentFrom = DateTime.Now.AddMinutes(-minutes);
                DateTime recentTo = DateTime.Now;

                // Giới hạn trong khoảng dtFrom - dtTo
                fromTime = recentFrom < fromPicker ? fromPicker : recentFrom;
                toTime = recentTo > toPicker ? toPicker : recentTo;

                if (fromTime >= toTime)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM DataMucNuoc WHERE CreateAt BETWEEN @FromTime AND @ToTime ORDER BY CreateAt DESC";

                var data = conn.Query<DataMucNuocModel>(query, new
                {
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

                dataGridView1.DataSource = data;

                if (data.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DrawChartFromMucNuoc(); // Gọi hàm vẽ biểu đồ từ dữ liệu mực nước
            }
        }
        private void ExportChartAsImage()
        {
            if (chart1.Series.Count == 0)
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
                        chart1.SaveImage(sfd.FileName, format);

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

        
    }
}
