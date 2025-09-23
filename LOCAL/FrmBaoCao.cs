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

            cbStation.DataSource = stationItems;
            cbStation.DisplayMember = "DisplayName";
            cbStation.ValueMember = "Code";
            cbStation.SelectedIndex = 0;

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
            if (dataGridView1.DataSource == null) return;

            var data = ((IEnumerable<dynamic>)dataGridView1.DataSource).ToList();
            if (data.Count == 0) return;

            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM HH:mm";
            chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

            var distinctStations = data.Select(x => x.Trạm).Distinct().ToList();

            if (distinctStations.Count > 1)
            {
                // --- Trường hợp chọn Tất cả: vẽ mỗi trạm 2 line (Cửa 1 & Cửa 2) ---
                foreach (var station in distinctStations)
                {
                    var stationData = data.Where(x => x.Trạm == station).OrderBy(x => x.Thời_gian);

                    // Series cho Cửa 1
                    var seriesDoor1 = new System.Windows.Forms.DataVisualization.Charting.Series($"{station} - Độ mở cửa 1")
                    {
                        ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                        BorderWidth = 2,
                        Color = Color.Blue,
                        XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                    };

                    // Series cho Cửa 2
                    var seriesDoor2 = new System.Windows.Forms.DataVisualization.Charting.Series($"{station} - Độ mở cửa 2")
                    {
                        ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                        BorderWidth = 2,
                        Color = Color.Red,
                        XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                    };

                    foreach (var row in stationData)
                    {
                        seriesDoor1.Points.AddXY(row.Thời_gian, row.Độ_mở_cửa_1 ?? 0);
                        seriesDoor2.Points.AddXY(row.Thời_gian, row.Độ_mở_cửa_2 ?? 0);
                    }

                    chart1.Series.Add(seriesDoor1);
                    chart1.Series.Add(seriesDoor2);
                }

                chart1.Titles.Clear();
                chart1.Titles.Add("Biểu đồ độ mở cửa theo thời gian (Tất cả trạm)");
            }
            else
            {
                // --- Trường hợp chỉ 1 trạm: vẽ 2 series như trước ---
                var stationName = distinctStations.First();

                var series1 = new System.Windows.Forms.DataVisualization.Charting.Series($"{stationName} - Độ mở cửa 1")
                {
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    BorderWidth = 2,
                    Color = Color.Blue,
                    XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                };

                var series2 = new System.Windows.Forms.DataVisualization.Charting.Series($"{stationName} - Độ mở cửa 2")
                {
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    BorderWidth = 2,
                    Color = Color.Red,
                    XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
                };

                foreach (var row in data.OrderBy(x => x.Thời_gian))
                {
                    series1.Points.AddXY(row.Thời_gian, row.Độ_mở_cửa_1 ?? 0);
                    series2.Points.AddXY(row.Thời_gian, row.Độ_mở_cửa_2 ?? 0);
                }

                chart1.Series.Add(series1);
                chart1.Series.Add(series2);

                chart1.Titles.Clear();
                chart1.Titles.Add($"Biểu đồ độ mở cửa theo thời gian ({stationName})");
            }

            chart1.ChartAreas[0].RecalculateAxesScale();
        }
        private void DrawChartFromMucNuocData()
        {
            if (dataGridView1.DataSource == null) return;

            var data = ((IEnumerable<dynamic>)dataGridView1.DataSource).ToList();
            if (data.Count == 0) return;

            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM HH:mm";
            chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

            // --- Tạo series với màu sắc ---
            var seriesHo = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước hồ")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Blue,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            var seriesHoKQ = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước hồ KQ")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.DarkBlue,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            var seriesBenSuc = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước Bến Súc")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Green,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            var seriesDauTieng = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước Dầu Tiếng")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Red,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            var seriesSonDai = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước Sơn Đài")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Orange,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            var seriesBinhNham = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước Bình Nhâm")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Purple,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            var seriesTLCDD = new System.Windows.Forms.DataVisualization.Charting.Series("Mực nước TL CDD")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Brown,
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
            };

            foreach (var row in data.OrderBy(x => x.Thời_gian))
            {
                seriesHo.Points.AddXY(row.Thời_gian, row.Mực_nước_hồ ?? 0);
                seriesHoKQ.Points.AddXY(row.Thời_gian, row.Mực_nước_hồKQ ?? 0);
                seriesBenSuc.Points.AddXY(row.Thời_gian, row.Mực_nước_bến_súc ?? 0);
                seriesDauTieng.Points.AddXY(row.Thời_gian, row.Mực_nước_đầu_tiếng ?? 0);
                seriesSonDai.Points.AddXY(row.Thời_gian, row.Mực_nước_sơn_đài ?? 0);
                seriesBinhNham.Points.AddXY(row.Thời_gian, row.Mực_nước_bình_nhâm ?? 0);
                seriesTLCDD.Points.AddXY(row.Thời_gian, row.Mực_nước_tl_cdd ?? 0);
            }

            chart1.Series.Add(seriesHo);
            chart1.Series.Add(seriesHoKQ);
            chart1.Series.Add(seriesBenSuc);
            chart1.Series.Add(seriesDauTieng);
            chart1.Series.Add(seriesSonDai);
            chart1.Series.Add(seriesBinhNham);
            chart1.Series.Add(seriesTLCDD);

            chart1.Titles.Add("Biểu đồ mực nước theo thời gian");
            chart1.ChartAreas[0].RecalculateAxesScale();
        }


        //private void nbtTrend_Click(object sender, EventArgs e)
        //{
        //    DrawChartFromMucNuoc();
        //}


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

        private void bntDataVanHanh_Click(object sender, EventArgs e)
        {
            var selectedItem = cbStation.SelectedItem as StationItem;
            string selectedCode = selectedItem?.Code; // null = tất cả trạm

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
                                && (x.StationName == "Station_1" ||
                                    x.StationName == "Station_2" ||
                                    x.StationName == "Station_3"));

                if (!string.IsNullOrEmpty(selectedCode))
                {
                    query = query.Where(x => x.StationName == selectedCode);
                }

                var rawData = query
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        x.StationName,
                        x.CreateAt,
                        x.Door1_Aperture,
                        x.Door2_Aperture
                    })
                    .ToList();

                // Map sang tiếng Việt
                var mappedData = rawData.Select(x => new
                {
                    Trạm = x.StationName switch
                    {
                        "Station_1" => "Trạm 1",
                        "Station_2" => "Trạm 2",
                        "Station_3" => "Trạm 3",
                        _ => x.StationName
                    },
                    Thời_gian = x.CreateAt.Value,
                    Độ_mở_cửa_1 = x.Door1_Aperture,
                    Độ_mở_cửa_2 = x.Door2_Aperture
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

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filteredData;

                // Đổi tiêu đề cột
                if (dataGridView1.Columns.Contains("Trạm"))
                    dataGridView1.Columns["Trạm"].HeaderText = "Tên trạm";
                if (dataGridView1.Columns.Contains("Thời_gian"))
                    dataGridView1.Columns["Thời_gian"].HeaderText = "Thời gian";
                if (dataGridView1.Columns.Contains("Độ_mở_cửa_1"))
                    dataGridView1.Columns["Độ_mở_cửa_1"].HeaderText = "Độ mở cửa 1 (cm)";
                if (dataGridView1.Columns.Contains("Độ_mở_cửa_2"))
                    dataGridView1.Columns["Độ_mở_cửa_2"].HeaderText = "Độ mở cửa 2 (cm)";

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

        private void bntDataMucNuoc_Click(object sender, EventArgs e)
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
                        x.Fllow_Ho,
                        x.Fllow_Ho_Final,
                        x.API_Fllow_BenSuc,
                        x.API_Fllow_DauTieng,
                        x.API_Fllow_SonDai,
                        x.API_Fllow_BinhNham,
                        x.API_Fllow_TL_CDD
                    })
                    .ToList();

                // Map sang tiếng Việt
                var mappedData = rawData.Select(x => new
                {
                    Trạm = x.StationName switch
                    {
                        "Location_Info" => "Trạm mức nước",
                        
                        _ => x.StationName
                    },
                    Thời_gian = x.CreateAt.Value,
                    Mực_nước_hồ = x.Fllow_Ho,
                    Mực_nước_hồKQ = x.Fllow_Ho_Final,
                    Mực_nước_bến_súc = x.API_Fllow_BenSuc,
                    Mực_nước_đầu_tiếng = x.API_Fllow_DauTieng,
                    Mực_nước_sơn_đài = x.API_Fllow_SonDai,
                    Mực_nước_bình_nhâm = x.API_Fllow_BinhNham,
                    Mực_nước_tl_cdd = x.API_Fllow_TL_CDD

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

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filteredData;

                // Đổi tiêu đề cột
                if (dataGridView1.Columns.Contains("Trạm"))
                    dataGridView1.Columns["Trạm"].HeaderText = "Tên trạm";
                if (dataGridView1.Columns.Contains("Thời_gian"))
                    dataGridView1.Columns["Thời_gian"].HeaderText = "Thời gian";
                if (dataGridView1.Columns.Contains("Mực_nước_hồ"))
                    dataGridView1.Columns["Mực_nước_hồ"].HeaderText = "Mực nước hồ (cm)";
                if (dataGridView1.Columns.Contains("Mực_nước_hồKQ"))
                    dataGridView1.Columns["Mực_nước_hồKQ"].HeaderText = "Mực nước hồ KQ (cm)";
                if (dataGridView1.Columns.Contains("Mực_nước_bến_súc"))
                    dataGridView1.Columns["Mực_nước_bến_súc"].HeaderText = "Mực nước trạm bến súc (cm)";
                if (dataGridView1.Columns.Contains("Mực_nước_đầu_tiếng"))
                    dataGridView1.Columns["Mực_nước_đầu_tiếng"].HeaderText = "Mực nước trạm dầu tiếng (cm)";
                if (dataGridView1.Columns.Contains("Mực_nước_sơn_đài"))
                    dataGridView1.Columns["Mực_nước_sơn_đài"].HeaderText = "Mực nước trạm sơn đài (cm)";
                if (dataGridView1.Columns.Contains("Mực_nước_bình_nhâm"))
                    dataGridView1.Columns["Mực_nước_bình_nhâm"].HeaderText = "Mực nước trạm bình nhâm (cm)";
                if (dataGridView1.Columns.Contains("Mực_nước_tl_cdd"))
                    dataGridView1.Columns["Mực_nước_tl_cdd"].HeaderText = "Mực nước trạm tl cdd (cm)";


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

        private void bntData_Click(object sender, EventArgs e)
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
                        x.Q_cs1,
                        x.Q_cs2,
                        x.W1_ho,
                        x.W2_ho
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
                    Q_Cống_Số_1 = Math.Round(x.Q_cs1, 2),
                    Q_Cống_Số_2 = Math.Round(x.Q_cs2, 2),
                    W1_Hồ = Math.Round(x.W1_ho, 2),
                    W2_Hồ = Math.Round(x.W2_ho, 2)
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

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filteredData;

                // Đổi tiêu đề cột
                if (dataGridView1.Columns.Contains("Trạm"))
                    dataGridView1.Columns["Trạm"].HeaderText = "Tên trạm";
                if (dataGridView1.Columns.Contains("Thời_gian"))
                    dataGridView1.Columns["Thời_gian"].HeaderText = "Thời gian";
                if (dataGridView1.Columns.Contains("Q_Cống_Số_1"))
                    dataGridView1.Columns["Q_Cống_Số_1"].HeaderText = "Q cống số 1";
                if (dataGridView1.Columns.Contains("Q_Cống_Số_2"))
                    dataGridView1.Columns["Q_Cống_Số_2"].HeaderText = "Q cống số 2";
                if (dataGridView1.Columns.Contains("W1_Hồ"))
                    dataGridView1.Columns["W1_Hồ"].HeaderText = "W1 hồ";
                if (dataGridView1.Columns.Contains("W2_Hồ"))
                    dataGridView1.Columns["W2_Hồ"].HeaderText = "W2 hồ";
                


                if (filteredData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                 //   DrawChartFromMucNuocData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}

