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
        private FrmMain _mainForm;
        private string connectionString => ConfigurationHelper.GetConnectionString();
        private BindingList<AlarmModel> alarmList = new BindingList<AlarmModel>();
     //   private Dictionary<string, string> lastValues = new Dictionary<string, string>();

        public FrmCanhBao(FrmMain frmMain)
        {
            InitializeComponent();
            //      Load += FrmCanhBao_Load;
            _mainForm = frmMain; // ✅ Gán trước khi sử dụng
            if (_mainForm != null)
            {
                
                //_mainForm.S1_Station_AlarmChanged += S1_Station_Alarm_ValueChanged;
                //_mainForm.S2_Station_AlarmChanged += S2_Station_Alarm_ValueChanged;
                //_mainForm.S3_Station_AlarmChanged += S3_Station_Alarm_ValueChanged;
                // Khởi tạo lastValues

                LoadInitialAlarms();

            }
            else
            {
                MessageBox.Show("FrmMain instance is null. Please check.");
            }

        }
       
        private void LoadInitialAlarms()
        {
            alarmList = new BindingList<AlarmModel>();
            dataGridViewAlarm.AutoGenerateColumns = true;
            dataGridViewAlarm.DataSource = alarmList;
            FormatGridAlarm();

            // ✅ Đọc trạng thái ban đầu các tag
            LoadInitialAlarmStatus();// Hàm này sẽ đọc trạng thái ban đầu từ PLC hoặc nguồn dữ liệu khác

        }
        private void FormatDgvHistory()
        {
            var dgv = DgvHistory;
            if (DgvHistory.Columns.Contains("CreateAt"))
            {
                DgvHistory.Columns["CreateAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();

            dgv.RowTemplate.Height = 30;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Đổi tên cột hiển thị (nếu cần)
            if (dgv.Columns["CreateAt"] != null) dgv.Columns["CreateAt"].HeaderText = "Thời Gian";
            if (dgv.Columns["Position"] != null) dgv.Columns["Position"].HeaderText = "Vị Trí";
            if (dgv.Columns["TagName"] != null) dgv.Columns["TagName"].HeaderText = "Tên Cảnh Báo";
            if (dgv.Columns["Door1_PressureHigh"] != null) dgv.Columns["Door1_PressureHigh"].HeaderText = "AS_H Cửa 1";
            if (dgv.Columns["Door1_PressureLow"] != null) dgv.Columns["Door1_PressureLow"].HeaderText = "AS_L Cửa 1";
            if (dgv.Columns["Door2_PressureHigh"] != null) dgv.Columns["Door2_PressureHigh"].HeaderText = "AS_H Cửa 2";
            if (dgv.Columns["Door2_PressureLow"] != null) dgv.Columns["Door2_PressureLow"].HeaderText = "AS_L Cửa 2";


            

            // Thêm các cột khác nếu cần đổi tên

        }
        private void LoadDataAlarm(DateTime startTime, DateTime endTime)
        {
     //       string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            string query = @"
        SELECT * FROM DataAlarm
        WHERE CreateAt BETWEEN @startTime AND @endTime
        ORDER BY CreateAt DESC"; // Hiển thị mới nhất lên trước

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@startTime", startTime);
                        cmd.Parameters.AddWithValue("@endTime", endTime);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DgvHistory.DataSource = dt;

                        FormatDgvHistory(); // Gọi hàm format nếu có
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadDataAlarm Error: {ex.Message}");
            }
        }
        private void LoadInitialAlarmStatus()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }        
            string status_S1_DC1 = _mainForm.GetS1_DC1_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 1", status_S1_DC1, "Trạm 1");
            string status_S1_DC2 = _mainForm.GetS1_DC2_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 2", status_S1_DC2, "Trạm 1");
            string status_S1_DC3 = _mainForm.GetS1_DC3_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 3", status_S1_DC3, "Trạm 1");
            // Trạng thái lổi Trạm 2
            string status_S2_DC1 = _mainForm.GetS2_DC1_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 1", status_S2_DC1, "Trạm 2");
            string status_S2_DC2 = _mainForm.GetS2_DC2_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 2", status_S2_DC2, "Trạm 2");
            string status_S2_DC3 = _mainForm.GetS2_DC3_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 3", status_S2_DC3, "Trạm 2");
            string status_S3_DC1 = _mainForm.GetS3_DC1_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 1", status_S3_DC1, "Trạm 3");
            string status_S3_DC2 = _mainForm.GetS3_DC2_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 2", status_S3_DC2, "Trạm 3");
            string status_S3_DC3 = _mainForm.GetS3_DC3_OverValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Quá Tải Động Cơ 3", status_S3_DC3, "Trạm 3");
            string status_Door1_PressureHigh = _mainForm.GetDoor1_PressureHighValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 1 Cao", status_Door1_PressureHigh, "Trạm 1");
            string status_Door1_PressureLow = _mainForm.GetDoor1_PressureLowValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 1 Thấp", status_Door1_PressureLow, "Trạm 1");
            string status_Door2_PressureHigh = _mainForm.GetDoor2_PressureHighValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 2 Cao", status_Door2_PressureHigh, "Trạm 1");
            string status_Door2_PressureLow = _mainForm.GetDoor2_PressureLowValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 2 Thấp", status_Door2_PressureLow, "Trạm 1");
            string status_Door3_PressureHigh = _mainForm.GetDoor3_PressureHighValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 3 Cao", status_Door3_PressureHigh, "Trạm 2");
            string status_Door3_PressureLow = _mainForm.GetDoor3_PressureLowValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 3 Thấp", status_Door3_PressureLow, "Trạm 2");
            string status_Door4_PressureHigh = _mainForm.GetDoor4_PressureHighValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 4 Cao", status_Door4_PressureHigh, "Trạm 2");
            string status_Door4_PressureLow = _mainForm.GetDoor4_PressureLowValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 4 Thấp", status_Door4_PressureLow, "Trạm 2");
            string status_Door5_PressureHigh = _mainForm.GetDoor5_PressureHighValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 5 Cao", status_Door5_PressureHigh, "Trạm 3");
            string status_Door5_PressureLow = _mainForm.GetDoor5_PressureLowValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 5 Thấp", status_Door5_PressureLow, "Trạm 3");
            string status_Door6_PressureHigh = _mainForm.GetDoor6_PressureHighValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 6 Cao", status_Door6_PressureHigh, "Trạm 3");
            string status_Door6_PressureLow = _mainForm.GetDoor6_PressureLowValue(); // trả về "1" hoặc "0"
            UpdateAlarmStatus("Áp Suất Cửa 6 Thấp", status_Door6_PressureLow, "Trạm 3");
            //string status_S1_Station_Alarm = _mainForm.GetS1_Station_AlarmValue(); // trả về "1" hoặc "0"
            //UpdateAlarmStatus("Lỗi Trạm 1", status_S1_Station_Alarm, "Trạm 1");
            //string status_S2_Station_Alarm = _mainForm.GetS2_Station_AlarmValue(); // trả về "1" hoặc "0"
            //UpdateAlarmStatus("Lỗi Trạm 2", status_S2_Station_Alarm, "Trạm 2");
            //string status_S3_Station_Alarm = _mainForm.GetS3_Station_AlarmValue(); // trả về "1" hoặc "0"
            //UpdateAlarmStatus("Lỗi Trạm 3", status_S3_Station_Alarm, "Trạm 3");


            //// ➡️ Tiếp tục cho các tag khác
        }
        //private void S1_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    string status = e.NewValue == "1" ? "1" : "0";

        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        UpdateAlarmStatus("Lỗi Trạm 1", status, "Trạm 1");
        //    });
        //}
        //private void S2_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    string status = e.NewValue == "1" ? "1" : "0";

        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        UpdateAlarmStatus("Lỗi Trạm 2", status, "Trạm 2");
        //    });
        //}
        //private void S3_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    string status = e.NewValue == "1" ? "1" : "0";

        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        UpdateAlarmStatus("Lỗi Trạm 3", status, "Trạm 3");
        //    });
        //}

        private void DataGridViewAlarm_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewAlarm.Rows[e.RowIndex].DataBoundItem is AlarmModel item)
            {
                if (item.Status == "1")
                {
                    dataGridViewAlarm.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dataGridViewAlarm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dataGridViewAlarm.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    dataGridViewAlarm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void UpdateAlarmStatus(string tagName, string status, string position)
        {
            var alarm = alarmList.FirstOrDefault(x => x.TagName == tagName && x.Position == position);

            if (status == "1")
            {
                if (alarm == null)
                {
                    alarmList.Add(new AlarmModel
                    {
                        Id = alarmList.Count + 1,
                        TagName = tagName,
                        Status = status,
                        Position = position,
                        CreateAt = DateTime.Now
                    });
                }
                else
                {
                    alarm.Status = status;
                    alarm.CreateAt = DateTime.Now;
                }
            }
            else
            {
                // Nếu status == "0", remove alarm khỏi danh sách (nếu muốn)
                if (alarm != null)
                {
                    alarmList.Remove(alarm);
                }
            }
        }

        private void FormatGridAlarm()
        {
            var dgv = dataGridViewAlarm;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;

            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();

            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;

            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;

            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;

            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ✅ Gán AutoGenerateColumns trước set header
            dgv.AutoGenerateColumns = true;

            // ✅ Đổi tên header cột nếu cột tồn tại
            if (dgv.Columns["Id"] != null) dgv.Columns["Id"].HeaderText = "STT";
            if (dgv.Columns["TagName"] != null) dgv.Columns["TagName"].HeaderText = "Tên Cảnh Báo";
            if (dgv.Columns["Status"] != null) dgv.Columns["Status"].HeaderText = "Trạng Thái";
            if (dgv.Columns["Position"] != null) dgv.Columns["Position"].HeaderText = "Vị Trí";
            if (dgv.Columns["CreateAt"] != null) dgv.Columns["CreateAt"].HeaderText = "Thời Gian";

            // ✅ Event format màu
            dgv.CellFormatting += DataGridViewAlarm_CellFormatting;
        }
        private void S3_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 3", status, "Trạm 3");
            });
        }
        private void S3_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 2", status, "Trạm 3");
            });
        }
        private void S3_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 1", status, "Trạm 3");
            });
        }


        //// Trạng thái lổi Trạm 2
        private void S2_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 3", status, "Trạm 2");
            });
        }
        private void S2_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 2", status, "Trạm 2");
            });
        }
        private void S2_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 1", status, "Trạm 2");
            });
        }
       
        private void S1_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 3", status, "Trạm 1");
            });
        }
        private void S1_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 2", status, "Trạm 1");
            });
        }
        private void S1_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";

            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Quá Tải Động Cơ 1", status, "Trạm 1");
            });
        }
        private void Door6_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 6 Thấp", status, "Trạm 3");
            });
        }
        private void Door6_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 6 Cao", status, "Trạm 3");
            });
        }
        private void Door5_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 5 Thấp", status, "Trạm 3");
            });
        }
        private void Door5_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 5 Cao", status, "Trạm 3");
            });
        }
        private void Door4_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 4 Thấp", status, "Trạm 2");
            });
        }
        private void Door4_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 4 Cao", status, "Trạm 2");
            });
        }
        private void Door3_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 3 Thấp", status, "Trạm 2");
            });
        }
        private void Door3_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 3 Cao", status, "Trạm 2");
            });
        }
        private void Door2_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 2 Thấp", status, "Trạm 1");
            });
        }
        private void Door2_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 2 Cao", status, "Trạm 1");
            });
        }
        private void Door1_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 1 Thấp", status, "Trạm 1");
            });
        }
        private void Door1_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string status = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                UpdateAlarmStatus("Áp Suất Cửa 1 Cao", status, "Trạm 1");
            });
        }

        private void bntLoad_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePickerStart.Value;
            DateTime end = dateTimePickerEnd.Value;

            LoadDataAlarm(start, end);
        }
    }
}



