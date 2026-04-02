using Ahd.Core;
using Ahd.Winforms.Controls;
using DocumentFormat.OpenXml;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;

namespace RegistrationForm1
{

    public partial class FrmCanhBao : Form
    {
        private Timer _timer = new Timer();

        public FrmCanhBao()
        {
            InitializeComponent();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();
            // Đặt thời gian End là hiện tại
            dtpEnd.Value = DateTime.Now;
            // Đặt Start lùi 8 tiếng so với End
            dtpStart.Value = dtpEnd.Value.AddHours(-8);

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false; // Tạm dừng timer trong khi xử lý

                if (Globalvariable.RealtimeDisplays.Count == 0)
                    return;

                using var dbContext = new ApplicationDbContext();

                // Danh sách tag cảnh báo cần theo dõi
                var alarmTags = new[]
                {
            "Al_Door1", "Al_Door2",
            "DC1_Over", "DC2_Over", "DC3_Over",
            "Temp_Oil_High", "Level_Oil_Low",
            "Door1_PressureHigh", "Door1_PressureLow",
            "Door2_PressureHigh", "Door2_PressureLow"
        };

                // Lấy bản ghi mới nhất cho từng (StationName, TagName)
                var latestAlarms = dbContext.FT04s
                    .Where(x => x.IsDeleted == false &&      // ✅ Sửa lỗi bool? bằng so sánh rõ ràng
                                (x.StationName == "Station_1" ||
                                 x.StationName == "Station_2" ||
                                 x.StationName == "Station_3") &&
                                alarmTags.Contains(x.TagName))
                    .GroupBy(x => new { x.StationName, x.TagName })
                    .Select(g => g.OrderByDescending(x => x.CreateAt).FirstOrDefault())
                    .Where(x => x.Value == true) // chỉ lấy cảnh báo đang hoạt động
                    .OrderByDescending(x => x.CreateAt)
                    .ToList();

                // Cập nhật giao diện (trên thread UI)
                Globalvariable.InvokeIfRequired(this, () =>
                {
                    dataGridViewAlarm.DataSource = null;
                    dataGridViewAlarm.DataSource = latestAlarms;

                    if (dataGridViewAlarm.Columns.Contains(nameof(FT04.CreateAt)))
                        dataGridViewAlarm.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;

                    dataGridViewAlarm.AutoResizeColumns();
                    dataGridViewAlarm.Refresh();
                });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi — giúp debug dễ hơn
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timer_Tick error: {ex.Message}");
            }
            finally
            {
                _timer.Enabled = true; // Luôn bật lại timer dù có lỗi
            }
        }



        public void SearchAlarmLogsByTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();

                string[] tags =
                {
            "Al_Door1", "Al_Door2", "DC1_Over", "DC2_Over", "DC3_Over",
            "Temp_Oil_High", "Level_Oil_Low",
            "Door1_PressureHigh", "Door1_PressureLow",
            "Door2_PressureHigh", "Door2_PressureLow"
        };

                string selectedStation = cboStation?.SelectedItem?.ToString();

                var query = dbContext.FT04s.Where(x =>
                    (x.IsDeleted ?? false) == false &&
                    x.CreateAt >= startTime && x.CreateAt <= endTime &&
                    tags.Contains(x.TagName) &&
                    x.Value == true);

                if (!string.IsNullOrEmpty(selectedStation) && selectedStation != "Tất cả")
                {
                    query = query.Where(x => x.StationName == selectedStation);
                }

                var alarmLogs = query
                    .OrderBy(x => x.StationName)
                    .ThenBy(x => x.TagName)
                    .ThenBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        Thời_Gian = x.CreateAt,
                        Trạm = x.StationName,
                        Thiết_Bị = x.TagName,
                        Trạng_Thái = "Alarm",
                        Mô_Tả = x.Description
                    })
                    .ToList();

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    DgvHistory.DataSource = null;
                    DgvHistory.DataSource = alarmLogs;
                    DgvHistory.AutoResizeColumns();
                    DgvHistory.Refresh();

                    if (alarmLogs.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy bản ghi nào trong khoảng thời gian đã chọn.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm log: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStationsToComboBox()
        {
            try
            {
                using var db = new ApplicationDbContext();
                var stationNames = db.FT04s
                    .Select(x => x.StationName)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                cboStation.Items.Clear();
                cboStation.Items.Add("Tất cả");
                cboStation.Items.AddRange(stationNames.ToArray());
                cboStation.SelectedIndex = 0;
            }
            catch
            {
                // Nếu DB lỗi thì dùng danh sách tĩnh
                cboStation.Items.AddRange(new[] { "Tất cả", "Station_1", "Station_2", "Station_3" });
                cboStation.SelectedIndex = 0;
            }

            // 🟩 Thêm đoạn này NGAY SAU khi nạp xong dữ liệu
            cboStation.SelectedIndexChanged += (s, e) =>
            {
                // Gọi lại tìm kiếm mỗi khi chọn trạm khác
                SearchAlarmLogsByTime(dtpStart.Value, dtpEnd.Value);

            };
        }
        private void LoadStationsToComboBox1()
        {
            try
            {
                using var db = new ApplicationDbContext();
                var stationNames = db.FT04s
                    .Select(x => x.StationName)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                cboStation1.Items.Clear();
                cboStation1.Items.Add("Tất cả");
                cboStation1.Items.AddRange(stationNames.ToArray());
                cboStation1.SelectedIndex = 0;
            }
            catch
            {
                // Nếu DB lỗi thì dùng danh sách tĩnh
                cboStation1.Items.AddRange(new[] { "Tất cả", "Station_1", "Station_2", "Station_3" });
                cboStation1.SelectedIndex = 0;
            }

            // 🟩 Thêm đoạn này NGAY SAU khi nạp xong dữ liệu
            cboStation1.SelectedIndexChanged += (s, e) =>
            {
                // Gọi lại tìm kiếm mỗi khi chọn trạm khác
                ShowDeviceOnOffStates(dtpStart1.Value, dtpEnd1.Value);

            };
        }


        public void ShowDeviceOnOffStates(DateTime startTime, DateTime endTime)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();

                // Danh sách tag thiết bị
                string[] deviceTags =
                {
            "DC1_Running", "DC2_Running", "DC3_Running",
            "Door1_Opening", "Door1_Closing",
            "Door2_Opening", "Door2_Closing",
            "Doorlock1_Opening", "Doorlock1_Closing",
            "Doorlock2_Opening", "Doorlock2_Closing",
            "Door1_Open", "Door1_Close",
            "Door2_Open", "Door2_Close",
            "Doorlock1_1Open","Doorlock1_1Close",
            "Doorlock1_2Open","Doorlock1_2Close",
            "Doorlock2_1Open","Doorlock2_1Close",
            "Doorlock2_2Open","Doorlock2_2Close"
        };

                // Lấy station được chọn
                string selectedStation = cboStation1?.SelectedItem?.ToString();

                // Truy vấn log thiết bị
                var query = dbContext.FT04s.Where(x =>
                    (x.IsDeleted ?? false) == false &&
                    x.CreateAt >= startTime && x.CreateAt <= endTime &&
                    deviceTags.Contains(x.TagName));

                // Nếu chọn 1 trạm -> thêm điều kiện lọc
                if (!string.IsNullOrEmpty(selectedStation) && selectedStation != "Tất cả")
                {
                    query = query.Where(x => x.StationName == selectedStation);
                }

                var logs = query
                    .OrderBy(x => x.StationName)
                    .ThenBy(x => x.TagName)
                    .ThenBy(x => x.CreateAt)
                    .AsNoTracking()
                    .ToList();

                var result = new List<dynamic>();

                // Xử lý trạng thái ON
                foreach (var group in logs.GroupBy(x => new { x.StationName, x.TagName }))
                {
                    bool? lastState = null;

                    foreach (var log in group)
                    {
                        if (log.Value == true && (lastState == null || log.Value != lastState))
                        {
                            result.Add(new
                            {
                                Thời_Gian = log.CreateAt,
                                Trạm = group.Key.StationName,
                                Thiết_Bị = group.Key.TagName,
                                Trạng_Thái = "ON",
                                Mô_Tả = log.Description
                            });
                        }

                        lastState = log.Value;
                    }
                }

                // Gán DataGridView
                Globalvariable.InvokeIfRequired(this, () =>
                {
                    DgvDeviceStatus.DataSource = null;

                    var sorted = result
                        .OrderBy(x => x.Trạm)
                        .ThenBy(x => x.Thiết_Bị)
                        .ThenByDescending(x => x.Thời_Gian)
                        .ToList();

                    DgvDeviceStatus.DataSource = sorted;

                    if (DgvDeviceStatus.Columns["Thời_Gian"] != null)
                        DgvDeviceStatus.Columns["Thời_Gian"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

                    DgvDeviceStatus.AutoResizeColumns();
                    DgvDeviceStatus.Refresh();

                    if (sorted.Count == 0)
                    {
                        MessageBox.Show("Không có trạng thái ON trong khoảng thời gian đã chọn.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị trạng thái ON: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




       

        

        

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DgvHistory == null || DgvHistory.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lấy thông tin bộ lọc hiện tại
                string selectedStation = cboStation?.SelectedItem?.ToString() ?? "Tất cả";
                string fromTime = dtpStart.Value.ToString("dd/MM/yyyy HH:mm");
                string toTime = dtpEnd.Value.ToString("dd/MM/yyyy HH:mm");

                // Hộp thoại lưu file
                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"AlarmLogs_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                };

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                // ====== TẠO FILE EXCEL ======
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Alarm Logs");

                int row = 1; // Dòng hiện tại trong Excel

                // 🔹 Dòng 1: Tiêu đề chính
                ws.Cell(row, 1).Value = "BÁO CÁO ALARM LOG";
                ws.Range(row, 1, row, DgvHistory.Columns.Count).Merge();
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Cell(row, 1).Style.Font.FontSize = 16;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                row += 2;

                // 🔹 Dòng 2: Thông tin thời gian
                ws.Cell(row, 1).Value = $"Từ ngày: {fromTime}";
                ws.Cell(row, 2).Value = $"Đến ngày: {toTime}";
                ws.Row(row).Style.Font.Italic = true;
                row++;

                // 🔹 Dòng 3: Trạm
                ws.Cell(row, 1).Value = $"Trạm: {selectedStation}";
                ws.Row(row).Style.Font.Italic = true;
                row += 2;

                // ====== GHI DỮ LIỆU ======
                // Tiêu đề cột
                for (int i = 0; i < DgvHistory.Columns.Count; i++)
                {
                    ws.Cell(row, i + 1).Value = DgvHistory.Columns[i].HeaderText;
                    ws.Cell(row, i + 1).Style.Font.Bold = true;
                    ws.Cell(row, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    ws.Cell(row, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(row, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Dữ liệu
                for (int i = 0; i < DgvHistory.Rows.Count; i++)
                {
                    for (int j = 0; j < DgvHistory.Columns.Count; j++)
                    {
                        ws.Cell(row + 1 + i, j + 1).Value = DgvHistory.Rows[i].Cells[j].Value?.ToString();
                        ws.Cell(row + 1 + i, j + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }

                // Tự căn độ rộng cột
                ws.Columns().AdjustToContents();

                // Freeze tiêu đề bảng để cuộn không mất
                ws.SheetView.FreezeRows(row);

                // Lưu file
                wb.SaveAs(sfd.FileName);

                MessageBox.Show($"Xuất Excel thành công!\nĐường dẫn: {sfd.FileName}",
                    "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportDeviceStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (DgvDeviceStatus == null || DgvDeviceStatus.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lấy thông tin bộ lọc hiện tại
                string selectedStation = cboStation1?.SelectedItem?.ToString() ?? "Tất cả";
                string fromTime = dtpStart1.Value.ToString("dd/MM/yyyy HH:mm");
                string toTime = dtpEnd1.Value.ToString("dd/MM/yyyy HH:mm");

                // Hộp thoại lưu file
                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"DeviceStatus_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                };

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                // ====== TẠO FILE EXCEL ======
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("DeviceStatus");

                int row = 1; // Dòng hiện tại trong Excel

                // 🔹 Dòng 1: Tiêu đề chính
                ws.Cell(row, 1).Value = "BÁO CÁO TRẠNG THÁI HOẠT ĐỘNG CỦA THIẾT BỊ";
                ws.Range(row, 1, row, DgvDeviceStatus.Columns.Count).Merge();
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Cell(row, 1).Style.Font.FontSize = 16;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                row += 2;

                // 🔹 Dòng 2: Thông tin thời gian
                ws.Cell(row, 1).Value = $"Từ ngày: {fromTime}";
                ws.Cell(row, 2).Value = $"Đến ngày: {toTime}";
                ws.Row(row).Style.Font.Italic = true;
                row++;

                // 🔹 Dòng 3: Trạm
                ws.Cell(row, 1).Value = $"Trạm: {selectedStation}";
                ws.Row(row).Style.Font.Italic = true;
                row += 2;

                // ====== GHI DỮ LIỆU ======
                // Tiêu đề cột
                for (int i = 0; i < DgvDeviceStatus.Columns.Count; i++)
                {
                    ws.Cell(row, i + 1).Value = DgvDeviceStatus.Columns[i].HeaderText;
                    ws.Cell(row, i + 1).Style.Font.Bold = true;
                    ws.Cell(row, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    ws.Cell(row, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(row, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Dữ liệu
                for (int i = 0; i < DgvDeviceStatus.Rows.Count; i++)
                {
                    for (int j = 0; j < DgvHistory.Columns.Count; j++)
                    {
                        ws.Cell(row + 1 + i, j + 1).Value = DgvHistory.Rows[i].Cells[j].Value?.ToString();
                        ws.Cell(row + 1 + i, j + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }

                // Tự căn độ rộng cột
                ws.Columns().AdjustToContents();

                // Freeze tiêu đề bảng để cuộn không mất
                ws.SheetView.FreezeRows(row);

                // Lưu file
                wb.SaveAs(sfd.FileName);

                MessageBox.Show($"Xuất Excel thành công!\nĐường dẫn: {sfd.FileName}",
                    "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bntLoad_Click(object sender, EventArgs e)
        {
            var startTime = dtpStart.Value;
            var endTime = dtpEnd.Value;

            if (endTime <= startTime)
            {
                MessageBox.Show("Thời gian kết thúc phải lớn hơn thời gian bắt đầu!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SearchAlarmLogsByTime(startTime, endTime);
        }

        private void FrmCanhBao_Load(object sender, EventArgs e)
        {
            LoadStationsToComboBox();
            LoadStationsToComboBox1();
        }

        private void bntVh_Click(object sender, EventArgs e)
        {
           
            var startTime1 = dtpStart1.Value;
            var endTime1 = dtpEnd1.Value;

            if (endTime1 <= startTime1)
            {
                MessageBox.Show("Thời gian kết thúc phải lớn hơn thời gian bắt đầu!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowDeviceOnOffStates(startTime1, endTime1);
        }
    }
}



