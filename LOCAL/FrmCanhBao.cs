using Ahd.Core;
using Ahd.Winforms.Controls;
using DocumentFormat.OpenXml;
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
  //      private List<DataAlarmModel> alarmList = new List<DataAlarmModel>();
     //   private DataAlarmModel lastAlarm = null;    
    
        private DataTable alarmTable = null;
        private readonly Dictionary<string, string> lastValues = new Dictionary<string, string>();
        private readonly Dictionary<string, DataAlarmModel> activeAlarms = new Dictionary<string, DataAlarmModel>();
        private List<DataAlarmModel> alarmList = new List<DataAlarmModel>();
        private readonly string[] tagNames = new[]
        {
            "S1_DC1_Over", "S1_DC2_Over", "S1_DC3_Over",
            "S2_DC1_Over", "S2_DC2_Over", "S2_DC3_Over",
            "S3_DC1_Over", "S3_DC2_Over", "S3_DC3_Over",
            "Door1_PressureHigh", "Door1_PressureLow",
            "Door2_PressureHigh", "Door2_PressureLow",
            "Door3_PressureHigh", "Door3_PressureLow",
            "Door4_PressureHigh", "Door4_PressureLow",
            "Door5_PressureHigh", "Door5_PressureLow",
            "Door6_PressureHigh", "Door6_PressureLow"
        };


        public FrmCanhBao()
        {         
        InitializeComponent();
            Load += FrmCanhBao_Load;
        }        
        IAhdDriverConnector driver;
        private void FrmCanhBao_Load(object sender, EventArgs e)
        {
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
         
        }
        private void RemoveAlarmFromGrid(string tagName)
        {
            alarmList.RemoveAll(a => a.TagName == tagName);
            LoadAllAlarmsToGrid();
        }
        private void AddAlarmToGrid(DataAlarmModel alarm, string tagName)
        {
            // Xóa alarm cũ theo TagName
            alarmList.RemoveAll(a => a.TagName == tagName);
            // Thêm mới
            alarmList.Add(alarm);
            LoadAllAlarmsToGrid();
        }
        private DataAlarmModel TaoAlarmTuTag(string tagName, string value)
        {
            var alarm = new DataAlarmModel
            {
                CreateAt = DateTime.Now,
                DeviceCode = "DVC-01",
                Area = "Trạm số " + tagName.Substring(1, 1),
                Severity = "Cao",
                TagName = tagName // Gán vào TagName để so sánh
            };

            var prop = typeof(DataAlarmModel).GetProperty(tagName);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(alarm, value);
            }

            return alarm;
        }
        private void LoadAllAlarmsToGrid()
        {
            // Tạo DataTable hiển thị cảnh báo
            var table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Tên Cảnh Báo", typeof(string));
            table.Columns.Add("Vị Trí", typeof(string));
            table.Columns.Add("Trạng Thái", typeof(string));
            table.Columns.Add("Thời Gian", typeof(string));

            int stt = 1;

            // Duyệt danh sách alarm
            foreach (var alarm in alarmList)
            {
                var props = typeof(DataAlarmModel).GetProperties();
                foreach (var prop in props)
                {
                    // Bỏ qua các property không phải cảnh báo
                    if (prop.Name == "Id" || prop.Name == "CreateAt" ||
                        prop.Name == "DeviceCode" || prop.Name == "Area" ||
                        prop.Name == "Severity" || prop.Name == "TagName")
                        continue;

                    var value = prop.GetValue(alarm)?.ToString()?.ToUpper().Trim();

                    // Nếu có giá trị cảnh báo (CAO / THẤP / LỖI)
                    if (!string.IsNullOrWhiteSpace(value) &&
                        (value.Contains("CAO") || value.Contains("THẤP") || value.Contains("LỖI")))
                    {
                        // Lấy DisplayName tiếng Việt nếu có
                        var displayAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                              .FirstOrDefault() as DisplayNameAttribute;
                        string tenCanhBao = displayAttr != null ? displayAttr.DisplayName : prop.Name;

                        // Lấy vị trí từ tên biến
                        string viTri = LayViTriTuTenBien(prop.Name);

                        // Thêm dòng mới vào DataTable
                        table.Rows.Add(stt++, tenCanhBao, viTri, value, alarm.CreateAt.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                }
            }

            // Gán dữ liệu cho DataGridView
            dgvCanhBao.DataSource = table;

            // === Cấu hình hiển thị DataGridView ===

            // Auto size các cột
            dgvCanhBao.Columns["STT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCanhBao.Columns["Tên Cảnh Báo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCanhBao.Columns["Vị Trí"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCanhBao.Columns["Trạng Thái"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCanhBao.Columns["Thời Gian"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // ✅ Đổi màu hàng dữ liệu nếu có cảnh báo
            if (table.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvCanhBao.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                    }
                }
            }

            // ✅ Tuỳ chỉnh header
            dgvCanhBao.EnableHeadersVisualStyles = false; // Cho phép tuỳ chỉnh màu header
            dgvCanhBao.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dgvCanhBao.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCanhBao.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            dgvCanhBao.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Driver_Started(object sender, EventArgs e)
        {       // Gán sự kiện cho Tag ValueChanged của các tag cần theo dõi    
            // Giả sử driver đã kết nối sẵn, nếu chưa thì cần bắt sự kiện Driver_Started
            foreach (string tagName in tagNames)
            {
                var tag = ahdDriverConnector1.GetTag($"Local Station/Channel1/Device1/{tagName}");
                if (tag == null) continue;

                lastValues[tagName] = tag.Value?.ToString();

                string tagNameCopy = tagName;

                tag.ValueChanged += (s, ev) =>
                {
                    string newValue = ev.NewValue?.ToString();
                    string oldValue = lastValues.ContainsKey(tagNameCopy) ? lastValues[tagNameCopy] : null;

                    if (newValue == "1" && oldValue != "1")
                    {
                        lastValues[tagNameCopy] = newValue;

                        this.Invoke((MethodInvoker)delegate
                        {
                            var alarm = TaoAlarmTuTag(tagNameCopy, $"LỖI {tagNameCopy.Split('_')[1]}");
                            AddAlarmToGrid(alarm, tagNameCopy);
                            SaveAlarmToSql(alarm);
                        });
                    }
                    else if (newValue == "0" && oldValue == "1")
                    {
                        lastValues[tagNameCopy] = newValue;

                        this.Invoke((MethodInvoker)delegate
                        {
                            RemoveAlarmFromGrid(tagNameCopy);
                        });
                    }
                    else
                    {
                        lastValues[tagNameCopy] = newValue; // cập nhật giá trị dù không có thay đổi 0→1
                    }
                };

                // Kiểm tra trạng thái hiện tại nếu đang là 1
                if (tag.Value?.ToString() == "1")
                {
                    var alarm = TaoAlarmTuTag(tagName, $"LỖI {tagName.Split('_')[1]}");
                    AddAlarmToGrid(alarm, tagName);
                    SaveAlarmToSql(alarm);
                }
            }
        }

        // ✅ Hàm lưu cảnh báo vào SQL Server
        private void SaveAlarmToSql(DataAlarmModel alarm)
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var props = typeof(DataAlarmModel).GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name == "Id" || prop.Name == "CreateAt" ||
                        prop.Name == "DeviceCode" || prop.Name == "Area" ||
                        prop.Name == "Severity" || prop.Name == "TagName")
                        continue;

                    var value = prop.GetValue(alarm)?.ToString();
                    if (!string.IsNullOrWhiteSpace(value) &&
                        (value.ToUpper().Contains("CAO") ||
                         value.ToUpper().Contains("THẤP") ||
                         value.ToUpper().Contains("LỖI")))
                    {
                        var displayAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                              .FirstOrDefault() as DisplayNameAttribute;

                        string tenCanhBao = displayAttr != null ? displayAttr.DisplayName : prop.Name;

                        string query = @"INSERT INTO AlarmLogs 
                        (AlarmTime, DeviceCode, Area, AlarmName, AlarmStatus, Severity) 
                        VALUES (@time, @device, @area, @name, @status, @severity)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@time", alarm.CreateAt);
                            cmd.Parameters.AddWithValue("@device", alarm.DeviceCode ?? "Unknown");
                            cmd.Parameters.AddWithValue("@area", alarm.Area ?? "Unknown");
                            cmd.Parameters.AddWithValue("@name", tenCanhBao);
                            cmd.Parameters.AddWithValue("@status", value);
                            cmd.Parameters.AddWithValue("@severity", alarm.Severity ?? "Không xác định");
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        
        // ✅ Hàm tạo đối tượng DataAlarmModel từ 1 tag cụ thể
        public static DataAlarmModel CreateAlarmFromTag(string tagName, string status)
        {
            var alarm = new DataAlarmModel
            {
                CreateAt = DateTime.Now,
                DeviceCode = "DVC-01",
                Area = GetLocationFromPropertyName(tagName),
                Severity = "Cao"
            };

            var prop = typeof(DataAlarmModel).GetProperty(tagName);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(alarm, status);
            }

            return alarm;
        }
        // ✅ Các hàm phụ trợ

        //private static bool IsSystemProperty(string propName)
        //{
        //    return propName == "Id" || propName == "CreateAt" || propName == "DeviceCode" || propName == "Area" || propName == "Severity";
        //}

        private static string GetLocationFromPropertyName(string propName)
        {
            if (propName.Contains("Door1") || propName.Contains("Door2") || propName.Contains("S1_")) return "Trạm số 1";
            if (propName.Contains("Door3") || propName.Contains("Door4") || propName.Contains("S2_")) return "Trạm số 2";
            if (propName.Contains("Door5") || propName.Contains("Door6") || propName.Contains("S3_")) return "Trạm số 3";
            return "Không rõ";
        }

        private string LayViTriTuTenBien(string propName)
        {
            if (propName.Contains("Door1") || propName.Contains("Door2")) return "Trạm số 1";
            if (propName.Contains("Door3") || propName.Contains("Door4")) return "Trạm số 2";
            if (propName.Contains("Door5") || propName.Contains("Door6")) return "Trạm số 3";

            if (propName.Contains("S1_")) return "Trạm số 1";
            if (propName.Contains("S2_")) return "Trạm số 2";
            if (propName.Contains("S3_")) return "Trạm số 3";

            return "Không rõ";
        }
        private void LoadAlarmLogsFromSql()
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";
            //    string query = "SELECT Id, AlarmTime, DeviceCode, Area, AlarmName, AlarmStatus, Severity FROM AlarmLogs ORDER BY AlarmTime DESC";
            string query = "SELECT Id, AlarmTime,  Area, AlarmName, AlarmStatus FROM AlarmLogs ORDER BY AlarmTime DESC";
            DataTable table = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                conn.Open();
                adapter.Fill(table);
            }

            DgvHistory.DataSource = table;

            // Định dạng cột nếu cần
            DgvHistory.Columns["Id"].Visible = false;
            DgvHistory.Columns["AlarmTime"].HeaderText = "Thời Gian";
        //    DgvHistory.Columns["DeviceCode"].HeaderText = "Thiết Bị";
            DgvHistory.Columns["Area"].HeaderText = "Khu Vực";
            DgvHistory.Columns["AlarmName"].HeaderText = "Cảnh Báo";
            DgvHistory.Columns["AlarmStatus"].HeaderText = "Trạng Thái";
       //     DgvHistory.Columns["Severity"].HeaderText = "Mức Độ";

            DgvHistory.Columns["AlarmTime"].Width = 150;
            DgvHistory.Columns["AlarmName"].Width = 250;
            DgvHistory.Columns["AlarmStatus"].Width = 120;

            // Tô màu nếu cần
            foreach (DataGridViewRow row in DgvHistory.Rows)
            {
                if (!row.IsNewRow && row.Cells["AlarmStatus"].Value?.ToString().ToUpper().Contains("LỖI") == true)
                {
                   // row.DefaultCellStyle.ForeColor = Color.Red;
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
            // ✅ Tùy chỉnh màu tiêu đề
            DgvHistory.EnableHeadersVisualStyles = false; // Cho phép tùy chỉnh tiêu đề
            DgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            DgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            DgvHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void bntLoad_Click(object sender, EventArgs e)
        {
            LoadAlarmLogsFromSql();
        }
    }
}
