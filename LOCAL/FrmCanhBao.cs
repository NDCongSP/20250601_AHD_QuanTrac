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
        
      

        public FrmCanhBao(FrmMain frmMain)
        {
            InitializeComponent();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();


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

                // Lấy tất cả các bản ghi cảnh báo trong khoảng thời gian đã chọn
                var alarmLogs = dbContext.FT04s
                    .Where(x => x.IsDeleted == false && x.CreateAt >= startTime && x.CreateAt <= endTime)
                    .Where(x => (x.StationName == "Station_1" || x.StationName == "Station_2" || x.StationName == "Station_3"))
                    .Where(x => (x.TagName == "Al_Door1" ||
                                x.TagName == "Al_Door2" ||
                                x.TagName == "DC1_Over" ||
                                x.TagName == "DC2_Over" ||
                                x.TagName == "DC3_Over" ||
                                x.TagName == "Temp_Oil_High"||
                                x.TagName == "Level_Oil_Low" ||
                                x.TagName == "Door1_PressureHigh" ||
                                x.TagName == "Door1_PressureLow")
                    )
                    .OrderByDescending(x => x.CreateAt) // Sắp xếp theo thời gian mới nhất
                    .ToList();

                // Cập nhật DataSource của DataGridView
                Globalvariable.InvokeIfRequired(this, () =>
                {
                    DgvHistory.DataSource = null;
                    DgvHistory.DataSource = alarmLogs;
                    DgvHistory.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;
                    DgvHistory.AutoResizeColumns();
                    DgvHistory.Refresh();
                });
            }
            catch (Exception ex)
            {
                // Xử lý hoặc ghi lại lỗi tìm kiếm
                // Console.WriteLine(ex.Message);
            }
        }



        private void bntLoad_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePickerStart.Value;
            DateTime end = dateTimePickerEnd.Value;

            SearchAlarmLogsByTime(start, end);
        }
    }
}



