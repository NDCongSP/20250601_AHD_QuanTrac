using Ahd.Core;
using Ahd.Winforms.Controls;
using DocumentFormat.OpenXml;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            dateTimePickerEnd.Value = DateTime.Now;
            // Đặt Start lùi 8 tiếng so với End
            dateTimePickerStart.Value = dateTimePickerEnd.Value.AddHours(-8);

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false; // Tạm dừng timer trong quá trình xử lý

                if (Globalvariable.RealtimeDisplays.Count == 0)
                {
                    return; // Nếu không có dữ liệu, thoát khỏi hàm
                }
                using var dbContext = new ApplicationDbContext();
                // Lấy bản ghi mới nhất cho từng loại cảnh báo tại các trạm "Station_1", "Station_2" và "Station_3"
                var latestAlarms = dbContext.FT04s
                    .Where(x => x.IsDeleted == false && (x.StationName == "Station_1" || x.StationName == "Station_2" || x.StationName == "Station_3"))
                    .AsEnumerable() // Chuyển sang xử lý trong bộ nhớ để group by
                    .GroupBy(x => new { x.StationName, x.TagName }) //  nhóm theo cả StationName và TagName
                    .Select(g => g.OrderByDescending(x => x.CreateAt).FirstOrDefault())
                    .ToList();

                // Lọc để chỉ lấy những cảnh báo đang hoạt động     
                var activeAlarms = latestAlarms
                    .Where(x => x != null && (
                         x.TagName == "Al_Door2" ||
                                x.TagName == "DC1_Over" ||
                                x.TagName == "DC2_Over" ||
                                x.TagName == "DC3_Over" ||
                                x.TagName == "Temp_Oil_High" ||
                                x.TagName == "Level_Oil_Low" ||
                                x.TagName == "Door1_PressureHigh" ||
                                x.TagName == "Door1_PressureLow")
                    )
                    .Where(x => x.Value == true)
                    .ToList();

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    // Cập nhật DataSource với danh sách các cảnh báo đang hoạt động
                    dataGridViewAlarm.DataSource = null;
                    dataGridViewAlarm.DataSource = activeAlarms.OrderByDescending(x => x.CreateAt).ToList();
                    // Định dạng và làm mới DataGridView
                    dataGridViewAlarm.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;
                    dataGridViewAlarm.AutoResizeColumns();
                    dataGridViewAlarm.Refresh();
                });
            }
            catch (Exception ex)
            {
                // Xử lý hoặc ghi lại lỗi nếu cần
                // Console.WriteLine(ex.Message);
            }
            finally
            {
                _timer.Enabled = true; // Bật lại timer
            }
        }




        public void SearchAlarmLogsByTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();

                // Danh sách trạm và tag cần theo dõi
                var stations = new[] { "Station_1", "Station_2", "Station_3" };
                var tags = new[]
                {
            "Al_Door1", "Al_Door2", "DC1_Over", "DC2_Over", "DC3_Over",
            "Temp_Oil_High", "Level_Oil_Low", "Door1_PressureHigh", "Door1_PressureLow"
        };

                // Lọc dữ liệu trong khoảng thời gian và theo danh sách tag
                var alarmLogs = dbContext.FT04s
                    .Where(x => (x.IsDeleted ?? false) == false)
                    .Where(x => x.CreateAt >= startTime && x.CreateAt <= endTime)
                    .Where(x => stations.Contains(x.StationName))
                    .Where(x => tags.Contains(x.TagName))
                    .Where(x => x.Value == true) // Chỉ lấy những log có trạng thái Alarm
                    .OrderBy(x => x.StationName)
                    .ThenBy(x => x.TagName)
                    .ThenBy(x => x.CreateAt)
                    .Select(x => new
                    {
                        Trạm = x.StationName,
                        Thiết_Bị = x.TagName,
                        Mô_Tả = x.Description,
                        Trạng_Thái = x.Value == true ? "Alarm" : "Normal",
                        Thời_Gian = x.CreateAt
                    })
                    .ToList();

                // Hiển thị trên DataGridView
                Globalvariable.InvokeIfRequired(this, () =>
                {
                    DgvHistory.DataSource = null;
                    DgvHistory.DataSource = alarmLogs;

                    DgvHistory.AutoResizeColumns();
                    DgvHistory.Refresh();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm log: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void ShowDeviceOnOffStates(DateTime startTime, DateTime endTime)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();

                // Danh sách trạm và tag thiết bị
                var stations = new[] { "Station_1", "Station_2", "Station_3" };
                var deviceTags = new[]
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

                // Lọc log trong khoảng thời gian tìm kiếm
                var logs = dbContext.FT04s
                    .Where(x => (x.IsDeleted ?? false) == false)
                    .Where(x => x.CreateAt >= startTime && x.CreateAt <= endTime)
                    .Where(x => stations.Contains(x.StationName))
                    .Where(x => deviceTags.Contains(x.TagName))
                    .OrderBy(x => x.StationName)
                    .ThenBy(x => x.TagName)
                    .ThenBy(x => x.CreateAt)
                    .ToList();

                var result = new List<dynamic>();

                // Lặp qua từng log để phát hiện trạng thái thay đổi
                foreach (var group in logs.GroupBy(x => new { x.StationName, x.TagName }))
                {
                    bool? lastState = null;

                    foreach (var log in group.OrderBy(x => x.CreateAt))
                    {
                        // Chỉ thêm khi trạng thái thay đổi (OFF -> ON hoặc ON -> OFF)
                        if (lastState == null || log.Value != lastState)
                        {
                            result.Add(new
                            {
                                Trạm = group.Key.StationName,
                                Thiết_Bị = group.Key.TagName,
                                Mô_Tả = log.Description,
                                Trạng_Thái = log.Value == true ? "ON" : "OFF",
                                Thời_Gian = log.CreateAt
                            });
                            lastState = log.Value;
                        }
                    }
                }

                // Gán dữ liệu vào DataGridView
                Globalvariable.InvokeIfRequired(this, () =>
                {
                    DgvHistory.DataSource = null;
                    DgvHistory.DataSource = result
                        .OrderBy(x => x.Trạm)
                        .ThenBy(x => x.Thiết_Bị)
                        .ThenByDescending(x => x.Thời_Gian)
                        .ToList();

                    DgvHistory.AutoResizeColumns();
                    DgvHistory.Refresh();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị trạng thái ON/OFF: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void bntLoad_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePickerStart.Value;
            DateTime end = dateTimePickerEnd.Value;

            SearchAlarmLogsByTime(start, end);
        }

        private void bntVh_Click(object sender, EventArgs e)
        {
            DateTime start1 = dateTimePickerStart.Value;
            DateTime end1 = dateTimePickerEnd.Value;

            ShowDeviceOnOffStates(start1, end1);
        }
    }
}



