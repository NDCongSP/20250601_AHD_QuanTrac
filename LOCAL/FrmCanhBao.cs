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
        private string connectionString => ConfigurationHelper.GetConnectionString();
      

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
                    return; // Nếu không có dữ liệu, thoát khỏi hàm

                using var dbContext = new ApplicationDbContext();

                var dataAlarm = dbContext.FT04s
                    .Where(x => x.IsDeleted == false)
                    .GroupBy(a => new { a.LocationId, a.StationId })
                    .Select(g => new
                    {
                        LocationId = g.Key.LocationId,
                        StationId = g.Key.StationId,
                        LocationName = g.FirstOrDefault().LocationName,
                        StationName = g.FirstOrDefault().StationName,
                        Items = g.ToList(),
                    })
                    .ToList();

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    var result = dataAlarm.FirstOrDefault(x => x.StationName == "Station_1")
                        .Items
                        .Where(x => x.TagName.Contains("1")
                           //  .Where(x => x.TagName==("Al_Door1")
                           || ((x.TagName == "DC1_Running") || (x.TagName == "DC2_Running") || (x.TagName == "DC3_Running"))
                                   || ((x.TagName == "DC1_Over") || (x.TagName == "DC2_Over") || (x.TagName == "DC3_Over"))
                                   || ((x.TagName == "Door1_Opening") || (x.TagName == "Door1_Closing") || (x.TagName == "Door1_Open"))
                                     || ((x.TagName == "Door1_Close") || (x.TagName == "Door1_PressureHigh") || (x.TagName == "Door1_PressureLow"))
                        )
                    .GroupBy(x => x.TagName) // group theo Tên Thiết Bị
                    .SelectMany(g => g
                        .OrderByDescending(x => x.CreateAt) // sắp xếp theo Thời Gian giảm dần
                        .Take(2)) // lấy 2 bản ghi mới nhất
                    .ToList();


                    dataGridViewAlarm.DataSource = null;
                    dataGridViewAlarm.DataSource = dataAlarm.FirstOrDefault(x => x.StationName == "Station_1")
                        .Items
                        .Where(x => x.TagName == ("Al_Door1")
                           || ((x.TagName == "DC1_Running") || (x.TagName == "DC2_Running") || (x.TagName == "DC3_Running"))
                                   || ((x.TagName == "DC1_Over") || (x.TagName == "DC2_Over") || (x.TagName == "DC3_Over"))
                                   || ((x.TagName == "Door1_Opening") || (x.TagName == "Door1_Closing") || (x.TagName == "Door1_Open"))
                                     || ((x.TagName == "Door1_Close") || (x.TagName == "Door1_PressureHigh") || (x.TagName == "Door1_PressureLow"))
                        )
                        .GroupBy(x => x.TagName) // group theo Tên Thiết Bị
                        .SelectMany(g => g
                                    .OrderByDescending(x => x.CreateAt) // sắp xếp theo Thời Gian giảm dần
                                    .Take(1)
                                   ) // lấy 2 bản ghi mới nhất
                        .OrderByDescending(x => x.CreateAt)
                        .ToList();

                    dataGridViewAlarm.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;
                    dataGridViewAlarm.AutoResizeColumns();
                    dataGridViewAlarm.Refresh();
                



                });
        }
            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Enabled = true;
            }
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
       
        

       
       
       
       

       
       
       
      
      
    
      
       

        private void bntLoad_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePickerStart.Value;
            DateTime end = dateTimePickerEnd.Value;

            LoadDataAlarm(start, end);
        }
    }
}



