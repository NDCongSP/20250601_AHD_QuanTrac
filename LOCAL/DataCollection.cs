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
using System.Windows.Forms.DataVisualization.Charting;

namespace RegistrationForm1
{
    public partial class DataCollection : Form
    {
        private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=RegistrationForm4;Integrated Security=True;TrustServerCertificate=True";
        private Timer dataCollectionTimer;
        private Random random = new Random();
        private bool isCollecting = false;
        private bool isFiltering = false; // Thêm biến để theo dõi trạng thái lọc
        private DateTime filterStartDate;  // Lưu ngày bắt đầu lọc
        private DateTime filterEndDate;    // Lưu ngày kết thúc lọc
        private User currentUser;
        private List<SensorReading> currentData = new List<SensorReading>();
        private List<ChartForm> openChartForms = new List<ChartForm>();

        public DataCollection(User loggedInUser)
        {
            this.currentUser = loggedInUser;
            InitializeComponent();
            SetupTimer();
            LoadInitialData();
        }

        private void DataCollection_Load(object sender, EventArgs e)
        {

        }

        private void SetupTimer()
        {
            // Timer thu thập dữ liệu mỗi 30 giây
            dataCollectionTimer = new Timer();
            dataCollectionTimer.Interval = 30000; // 30 seconds
            dataCollectionTimer.Tick += DataCollectionTimer_Tick;
        }

        private async void LoadInitialData()
        {
            // Tải dữ liệu ban đầu (100 bản ghi mới nhất)
            await LoadLatestData();
        }

        private async Task LoadLatestData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetLatestSensorData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecordCount", 100);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Đổi tên cột để hiển thị đẹp hơn
                            if (dt.Columns.Contains("Id"))
                                dt.Columns["Id"].ColumnName = "ID";
                            if (dt.Columns.Contains("Timestamp"))
                                dt.Columns["Timestamp"].ColumnName = "Thời gian";
                            if (dt.Columns.Contains("Temperature"))
                                dt.Columns["Temperature"].ColumnName = "Nhiệt độ (°C)";
                            if (dt.Columns.Contains("Voltage"))
                                dt.Columns["Voltage"].ColumnName = "Điện áp (V)";
                            if (dt.Columns.Contains("Current"))
                                dt.Columns["Current"].ColumnName = "Dòng điện (A)";
                            if (dt.Columns.Contains("Power"))
                                dt.Columns["Power"].ColumnName = "Công suất (W)";

                            dataGridView2.DataSource = dt;

                            // Lưu dữ liệu hiện tại để vẽ biểu đồ
                            UpdateCurrentData(dt);
                        }
                    }
                }

                lblstatus.Text = $"Đã tải {dataGridView2.Rows.Count} bản ghi mới nhất";
                lblstatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblstatus.Text = $"Lỗi tải dữ liệu: {ex.Message}";
                lblstatus.ForeColor = Color.Red;
            }
        }

        // Thêm method mới để load dữ liệu theo filter
        private async Task LoadFilteredData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"SELECT Id, Timestamp, Temperature, Voltage, [Current], Power 
                           FROM SensorData 
                           WHERE Timestamp >= @StartDate AND Timestamp <= @EndDate 
                           ORDER BY Timestamp DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = filterStartDate;
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = filterEndDate;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Đổi tên cột
                            if (dt.Columns.Contains("Id"))
                                dt.Columns["Id"].ColumnName = "ID";
                            if (dt.Columns.Contains("Timestamp"))
                                dt.Columns["Timestamp"].ColumnName = "Thời gian";
                            if (dt.Columns.Contains("Temperature"))
                                dt.Columns["Temperature"].ColumnName = "Nhiệt độ (°C)";
                            if (dt.Columns.Contains("Voltage"))
                                dt.Columns["Voltage"].ColumnName = "Điện áp (V)";
                            if (dt.Columns.Contains("Current"))
                                dt.Columns["Current"].ColumnName = "Dòng điện (A)";
                            if (dt.Columns.Contains("Power"))
                                dt.Columns["Power"].ColumnName = "Công suất (W)";

                            dataGridView2.DataSource = dt;
                            UpdateCurrentData(dt);
                        }
                    }
                }

                lblstatus.Text = $"Tìm thấy {dataGridView2.Rows.Count} bản ghi trong khoảng thời gian đã chọn (Cập nhật: {DateTime.Now:HH:mm:ss})";
                lblstatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblstatus.Text = $"Lỗi tải dữ liệu đã lọc: {ex.Message}";
                lblstatus.ForeColor = Color.Red;
            }
        }

        private void UpdateCurrentData(DataTable dt)
        {
            currentData.Clear();
            foreach (DataRow row in dt.Rows)
            {
                currentData.Add(new SensorReading
                {
                    Timestamp = Convert.ToDateTime(row[1]), // Cột thời gian
                    Temperature = Convert.ToDecimal(row[2]), // Cột nhiệt độ
                    Voltage = Convert.ToDecimal(row[3]),     // Cột điện áp
                    Current = Convert.ToDecimal(row[4]),     // Cột dòng điện
                    Power = Convert.ToDecimal(row[5])        // Cột công suất
                });
            }
            currentData.Reverse(); // Sắp xếp theo thời gian tăng dần
            UpdateOpenChartForms();
        }

        private void UpdateOpenChartForms()
        {
            // Loại bỏ các form đã đóng
            openChartForms.RemoveAll(form => form.IsDisposed);

            // Cập nhật các form còn lại
            foreach (var chartForm in openChartForms)
            {
                if (!chartForm.IsDisposed)
                {
                    chartForm.UpdateChartData(new List<SensorReading>(currentData));
                }
            }
        }

        private void btnRefreshCharts_Click(object sender, EventArgs e)
        {
            UpdateOpenChartForms();
        }

        // Override FormClosing để cleanup
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dừng timer khi đóng form
            if (dataCollectionTimer != null)
            {
                dataCollectionTimer.Stop();
                dataCollectionTimer.Dispose();
            }

            // Đóng tất cả chart forms
            foreach (var chartForm in openChartForms.ToList())
            {
                if (!chartForm.IsDisposed)
                {
                    chartForm.Close();
                }
            }
            openChartForms.Clear();

            base.OnFormClosing(e);
        }

        private async void DataCollectionTimer_Tick(object sender, EventArgs e)
        {
            await CollectSensorData();
        }

        private async Task CollectSensorData()
        {
            try
            {
                // Mô phỏng dữ liệu cảm biến
                double temperature = 20 + random.NextDouble() * 15; // 20-35°C
                double voltage = 11.5 + random.NextDouble() * 1.0;  // 11.5-12.5V
                double current = 2.0 + random.NextDouble() * 1.0;   // 2.0-3.0A
                double power = voltage * current;                    // P = U * I

                DateTime currentTime = DateTime.Now;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertSensorData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Temperature", Math.Round(temperature, 2));
                        cmd.Parameters.AddWithValue("@Voltage", Math.Round(voltage, 3));
                        cmd.Parameters.AddWithValue("@Current", Math.Round(current, 3));
                        cmd.Parameters.AddWithValue("@Power", Math.Round(power, 3));

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                // Kiểm tra xem có đang trong chế độ lọc hay không
                if (isFiltering)
                {
                    // Kiểm tra xem dữ liệu mới có nằm trong khoảng thời gian lọc không
                    if (currentTime >= filterStartDate && currentTime <= filterEndDate)
                    {
                        // Nếu dữ liệu mới nằm trong khoảng lọc, cập nhật hiển thị
                        await LoadFilteredData();
                        lblstatus.Text = $"Đã lưu dữ liệu mới trong khoảng lọc: {currentTime:HH:mm:ss}";
                        lblstatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        // Dữ liệu mới không nằm trong khoảng lọc, không cập nhật hiển thị
                        lblstatus.Text = $"Đã lưu dữ liệu mới (ngoài khoảng lọc): {currentTime:HH:mm:ss}";
                        lblstatus.ForeColor = Color.Orange;
                    }
                }
                else
                {
                    // Nếu không lọc, tải dữ liệu mới nhất như bình thường
                    await LoadLatestData();
                    lblstatus.Text = $"Đã lưu dữ liệu mới: {currentTime:HH:mm:ss}";
                    lblstatus.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblstatus.Text = $"Lỗi thu thập: {ex.Message}";
                lblstatus.ForeColor = Color.Red;
            }
        }

        private void btnCollection_Click(object sender, EventArgs e)
        {
            if (!isCollecting)
            {
                dataCollectionTimer.Start();
                btnCollection.Text = "Dừng thu thập";
                btnCollection.BackColor = Color.Red;
                isCollecting = true;
                lblstatus.Text = "Đang thu thập dữ liệu...";
                lblstatus.ForeColor = Color.Blue;
            }
            else
            {
                dataCollectionTimer.Stop();
                btnCollection.Text = "Bắt đầu thu thập";
                btnCollection.BackColor = Color.Green;
                isCollecting = false;
                lblstatus.Text = "Đã dừng thu thập";
                lblstatus.ForeColor = Color.Gray;
            }
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Đặt trạng thái lọc và lưu khoảng thời gian
                isFiltering = true;
                filterStartDate = dtpFromDate.Value.Date; // Bắt đầu từ 00:00:00
                filterEndDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1); // Kết thúc ở 23:59:59

                // Debug: Hiển thị khoảng thời gian lọc
                Console.WriteLine($"Lọc dữ liệu từ: {filterStartDate:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Đến: {filterEndDate:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Thời gian hiện tại: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

                // Gọi method load dữ liệu đã lọc
                await LoadFilteredData();

                // Thông báo cho người dùng biết đang trong chế độ lọc
                DateTime today = DateTime.Now.Date;
                string filterMessage = "";

                if (filterStartDate.Date <= today && filterEndDate.Date >= today)
                {
                    filterMessage = " (Bao gồm hôm nay - Dữ liệu real-time sẽ được cập nhật)";
                }
                else
                {
                    filterMessage = " (Không bao gồm hôm nay - Chỉ hiển thị dữ liệu lịch sử)";
                }

                lblstatus.Text = $"Tìm thấy {dataGridView2.Rows.Count} bản ghi trong khoảng thời gian đã chọn{filterMessage}";
                lblstatus.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                lblstatus.Text = $"Lỗi tìm kiếm: {ex.Message}";
                lblstatus.ForeColor = Color.Red;
            }
        }

        // Thêm button để tắt chế độ lọc và quay về hiển thị dữ liệu mới nhất
        private async void btnShowAll_Click(object sender, EventArgs e)
        {
            isFiltering = false;
            await LoadLatestData();
            lblstatus.Text = "Đã tắt chế độ lọc - Hiển thị dữ liệu mới nhất";
            lblstatus.ForeColor = Color.Green;
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            if (currentData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị biểu đồ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                ChartForm chartForm = new ChartForm(currentData);

                // Thêm vào danh sách tracking
                openChartForms.Add(chartForm);

                // Xử lý khi form bị đóng
                chartForm.FormClosed += (s, args) =>
                {
                    openChartForms.Remove(chartForm);
                };

                chartForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị biểu đồ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}