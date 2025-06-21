using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmCanhBao : Form
    {
        private List<DataAlarmModel> alarmList = new List<DataAlarmModel>();
        private DataAlarmModel lastAlarm = null;
        public FrmCanhBao()
        {
           
        InitializeComponent();
        }
        
        private void CheckAndLogAlarm(DataAlarmModel currentAlarm)
        {
            if (lastAlarm == null || IsAlarmChanged(lastAlarm, currentAlarm))
            {
                SaveAlarmToSql(currentAlarm);
                LoadAlarmToGrid(currentAlarm);
                lastAlarm = currentAlarm;
            }
        }
        private void LoadAlarmToGrid(DataAlarmModel alarm)
        {
            var table = new DataTable();
            table.Columns.Add("Thời Gian", typeof(string));
            table.Columns.Add("Tên Cảnh Báo", typeof(string));
            table.Columns.Add("Trạng Thái", typeof(string));

            var props = typeof(DataAlarmModel).GetProperties();

            foreach (var prop in props)
            {
                if (prop.Name == "Id" || prop.Name == "CreateAt") continue;

                var value = prop.GetValue(alarm)?.ToString();
                if (value == "ON")
                {
                    var displayAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                          .FirstOrDefault() as DisplayNameAttribute;

                    string tenCanhBao = displayAttr != null ? displayAttr.DisplayName : prop.Name;
                    table.Rows.Add(alarm.CreateAt.ToString("dd/MM/yyyy HH:mm:ss"), tenCanhBao, value);
                }
            }
        }
        private bool IsAlarmChanged(DataAlarmModel a, DataAlarmModel b)
        {
            var props = typeof(DataAlarmModel).GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name == "Id" || prop.Name == "CreateAt") continue;

                var aVal = prop.GetValue(a)?.ToString();
                var bVal = prop.GetValue(b)?.ToString();

                if (aVal != bVal) return true;
            }
            return false;
        }
        private void SaveAlarmToSql(DataAlarmModel alarm)
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var props = typeof(DataAlarmModel).GetProperties();
                foreach (var prop in props)
                {
                    // Dùng cách tương thích C# 7.3
                    if (prop.Name == "Id" || prop.Name == "CreateAt" ||
                        prop.Name == "DeviceCode" || prop.Name == "Area" || prop.Name == "Severity")
                        continue;

                    var value = prop.GetValue(alarm)?.ToString();
                    if (value == "ON")
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
        private void FrmCanhBao_Load(object sender, EventArgs e)
        {
            var alarm = new DataAlarmModel
            {
                CreateAt = DateTime.Now,
                DeviceCode = "DVC-01",
                Area = "Cửa số 1",
                Severity = "Cao",
                Door1_PressureHigh = "ON",
                DC2_Over = "ON"
            };

            SaveAlarmToSql(alarm);
            LoadAlarmToGrid(alarm); // nếu bạn vẫn muốn hiển thị ra DGV
        }
    }
}
