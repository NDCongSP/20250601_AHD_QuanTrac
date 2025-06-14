using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using static RegistrationForm1.Chartstatistics;
using System.Reflection;


namespace RegistrationForm1
{
    public partial class DeviceStatusManager : Form
    {
        private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=RegistrationForm4;Integrated Security=True;TrustServerCertificate=True";
        private User currentUser;
        private Timer statusChangeTimer;
        private Timer alertHideTimer;
        public DeviceStatusManager(User loggedInUser)
        {
            this.currentUser = loggedInUser;

            InitializeComponent();
            InitializeDatabase();
            LoadDeviceData();
            SetupAlertSystem();
            ConfigureWindowControls();
            


        }
        private void ConfigureWindowControls()
        {
         
            this.Resize += OnFormResize;

        }
        private void SetupContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem noteSelectedMenuItem = new ToolStripMenuItem("Ghi chú cho dòng đã chọn");
            noteSelectedMenuItem.Click += NoteSelectedMenuItem_Click;

            ToolStripMenuItem noteAllMenuItem = new ToolStripMenuItem("Ghi chú cho tất cả thiết bị");
            noteAllMenuItem.Click += NoteAllMenuItem_Click;

            // THÊM MỚI: Menu item xóa ghi chú
            ToolStripMenuItem deleteNoteMenuItem = new ToolStripMenuItem("Xóa ghi chú");
            deleteNoteMenuItem.Click += DeleteNoteMenuItem_Click;

            contextMenu.Items.AddRange(new ToolStripItem[] {
        noteSelectedMenuItem,
        noteAllMenuItem,
        new ToolStripSeparator(), // Thêm đường phân cách
        deleteNoteMenuItem
    });

            dataGridView2.ContextMenuStrip = contextMenu;
        }
        // Xử lý sự kiện khi người dùng cố gắng đóng form
      

        // Xử lý sự kiện khi form được resize
        private void OnFormResize(object sender, EventArgs e)
        {
            // Xử lý khi cửa sổ thay đổi kích thước
            if (this.WindowState == FormWindowState.Minimized)
            {
                // Có thể ẩn form xuống system tray
                this.ShowInTaskbar = false;
                // Hoặc thực hiện các hành động khác khi minimize
            }
        }

        // Xử lý sự kiện khi trạng thái cửa sổ thay đổi


        public void CloseWindow()
        {
            this.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void InitializeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Tạo database
                    string createDbQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DeviceDB')
                        CREATE DATABASE DeviceDB";

                    using (SqlCommand cmd = new SqlCommand(createDbQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // Tạo bảng và nhập vào sample data
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeviceStatus' AND xtype='U')
                        CREATE TABLE DeviceStatus (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            DeviceId NVARCHAR(100) NOT NULL,
                            DeviceName NVARCHAR(100) NOT NULL,
                            Status INT NOT NULL CHECK (Status IN (0,1)),
                            Position INT NOT NULL CHECK (Position IN (1,2,3)),
                            LastUpdated DATETIME DEFAULT GETDATE(),
                            AlertTime DATETIME NULL,
                            PreviousStatus INT NULL
                        )";

                    using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Thêm PreviousStatus nếu nó không có tồn tại
                    string addColumnQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('DeviceStatus') AND name = 'PreviousStatus')
                        ALTER TABLE DeviceStatus ADD PreviousStatus INT NULL";

                    using (SqlCommand cmd = new SqlCommand(addColumnQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Nhập vào sample data nếu ko có 
                    string checkDataQuery = "SELECT COUNT(*) FROM DeviceStatus";
                    using (SqlCommand cmd = new SqlCommand(checkDataQuery, conn))
                    {
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            InsertSampleData(conn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertSampleData(SqlConnection conn)
        {
            Random rand = new Random();
            for (int i = 1; i <= 100; i++)
            {
                int status = rand.Next(0, 2);
                int position = rand.Next(1,4);
                string insertQuery = @"
                    INSERT INTO DeviceStatus (DeviceId, DeviceName, Status, Position,  LastUpdated, PreviousStatus) 
                    VALUES (@DeviceId, @DeviceName, @Status, @Position, @LastUpdated, @PreviousStatus)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@DeviceId", $"DH{i:D3}");
                    cmd.Parameters.AddWithValue("@DeviceName", $"Đồng hồ số {i}");
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now.AddMinutes(-rand.Next(0, 1440)));
                    cmd.Parameters.AddWithValue("@PreviousStatus", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadDeviceData()
        {
            try
            {
                SaveScrollPosition();

                // Suspend layout để giảm flickering
                dataGridView2.SuspendLayout();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT Id, DeviceId as [Mã thiết bị], DeviceName as [Tên thiết bị], 
                               CASE WHEN Status = 1 THEN N'Hoạt động' ELSE N'Dừng' END as [Trạng thái],
                               Status, Position as [vị trí], LastUpdated as [Cập nhật cuối], AlertTime as [Thời gian cảnh báo],
                        Note as [Ghi chú], NoteTime as [Thời gian ghi chú], NoteBy as [Người ghi chú]
                        FROM DeviceStatus 
                        ORDER BY DeviceId";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView2.DataSource = dt;

                    // Giấu  Id and Status cột
                    if (dataGridView2.Columns["Id"] != null)
                        dataGridView2.Columns["Id"].Visible = false;
                    if (dataGridView2.Columns["Status"] != null)
                        dataGridView2.Columns["Status"].Visible = false;

                    // Format datetime columns
                    if (dataGridView2.Columns["Cập nhật cuối"] != null)
                        dataGridView2.Columns["Cập nhật cuối"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    if (dataGridView2.Columns["Thời gian cảnh báo"] != null)
                        dataGridView2.Columns["Thời gian cảnh báo"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    if (dataGridView2.Columns["Thời gian ghi chú"] != null)
                        dataGridView2.Columns["Thời gian ghi chú"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dataGridView2.ResumeLayout();
                // Thêm kiểm tra trước khi gọi BeginInvoke
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        RestoreScrollPosition();
                    }));
                }
                else
                {
                    // Nếu handle chưa tạo, gọi trực tiếp
                    RestoreScrollPosition();
                }
            }
        }
        private int savedScrollPosition = -1;
        private bool isManualRefresh = false;

        private void SaveScrollPosition()
        {
            try
            {
                if (dataGridView2.FirstDisplayedScrollingRowIndex >= 0)
                {
                    savedScrollPosition = dataGridView2.FirstDisplayedScrollingRowIndex;
                }
            }
            catch
            {
               
            }
        }

        private void RestoreScrollPosition()
        {
            try
            {
                if (savedScrollPosition >= 0 &&
                    savedScrollPosition < dataGridView2.Rows.Count &&
                    dataGridView2.IsHandleCreated)
                {
                    dataGridView2.FirstDisplayedScrollingRowIndex = savedScrollPosition;
                }
            }
            catch
            {
                
            }
        }
        private void AddNoteToDevice(int deviceId, string note)
        {
            try
            {
                if (statusChangeTimer != null)
                {
                    statusChangeTimer.Stop();
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = @"
                UPDATE DeviceStatus 
                SET Note = @Note, NoteTime = GETDATE(), NoteBy = @NoteBy 
                WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.Parameters.AddWithValue("@NoteBy", currentUser?.FullName ?? "Unknown");
                        cmd.Parameters.AddWithValue("@Id", deviceId);
                        cmd.ExecuteNonQuery();
                    }

                    // THÊM MỚI: Lấy DeviceId để cập nhật màu
                    string getDeviceIdQuery = "SELECT DeviceId FROM DeviceStatus WHERE Id = @Id";
                    string deviceCode;
                    using (SqlCommand cmd = new SqlCommand(getDeviceIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", deviceId);
                        deviceCode = cmd.ExecuteScalar()?.ToString();
                    }

                    // Cập nhật màu cho thiết bị đã được ghi chú
                    if (!string.IsNullOrEmpty(deviceCode))
                    {
                        UpdateDeviceRowColorAfterNote(deviceCode);
                    }
                }

                LoadDeviceData();
                MessageBox.Show("Ghi chú đã được lưu thành công!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu ghi chú: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNoteToMultipleDevices(List<int> deviceIds, string note)
        {
            try
            {
                if (statusChangeTimer != null)
                {
                    statusChangeTimer.Stop();
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy danh sách DeviceId trước khi update
                    string deviceIdList = string.Join(",", deviceIds);
                    string getDeviceCodesQuery = $"SELECT DeviceId FROM DeviceStatus WHERE Id IN ({deviceIdList})";
                    List<string> deviceCodes = new List<string>();

                    using (SqlCommand cmd = new SqlCommand(getDeviceCodesQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                deviceCodes.Add(reader["DeviceId"].ToString());
                            }
                        }
                    }

                    // Update ghi chú
                    string updateQuery = $@"
                UPDATE DeviceStatus 
                SET Note = @Note, NoteTime = GETDATE(), NoteBy = @NoteBy 
                WHERE Id IN ({deviceIdList})";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.Parameters.AddWithValue("@NoteBy", currentUser?.FullName ?? "Unknown");
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Cập nhật màu cho tất cả thiết bị đã được ghi chú
                        foreach (string deviceCode in deviceCodes)
                        {
                            UpdateDeviceRowColorAfterNote(deviceCode);
                        }

                        MessageBox.Show($"Đã thêm ghi chú cho {rowsAffected} thiết bị!", "Thông báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadDeviceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu ghi chú: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddNoteToAllDevices(string note)
        {
            try
            {
                if (statusChangeTimer != null)
                {
                    statusChangeTimer.Stop();
                }
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thêm ghi chú cho tất cả thiết bị?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Lấy danh sách tất cả DeviceId trước khi update
                        string getAllDeviceCodesQuery = "SELECT DeviceId FROM DeviceStatus";
                        List<string> allDeviceCodes = new List<string>();

                        using (SqlCommand cmd = new SqlCommand(getAllDeviceCodesQuery, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    allDeviceCodes.Add(reader["DeviceId"].ToString());
                                }
                            }
                        }

                        string updateQuery = @"
                    UPDATE DeviceStatus 
                    SET Note = @Note, NoteTime = GETDATE(), NoteBy = @NoteBy";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Note", note);
                            cmd.Parameters.AddWithValue("@NoteBy", currentUser?.FullName ?? "Unknown");
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Cập nhật màu cho tất cả thiết bị
                            foreach (string deviceCode in allDeviceCodes)
                            {
                                UpdateDeviceRowColorAfterNote(deviceCode);
                            }

                            MessageBox.Show($"Đã thêm ghi chú cho {rowsAffected} thiết bị!", "Thông báo",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    LoadDeviceData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu ghi chú: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteNotesFromDevices(List<int> deviceIds)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string deviceIdList = string.Join(",", deviceIds);
                    string updateQuery = $@"
                UPDATE DeviceStatus 
                SET Note = NULL, NoteTime = NULL, NoteBy = NULL 
                WHERE Id IN ({deviceIdList})";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        string message = deviceIds.Count == 1 ?
                            "Đã xóa ghi chú thành công!" :
                            $"Đã xóa ghi chú của {rowsAffected} thiết bị!";

                        MessageBox.Show(message, "Thông báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadDeviceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa ghi chú: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteAllNotes()
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có CHẮC CHẮN muốn xóa ghi chú của TẤT CẢ thiết bị?\nHành động này không thể hoàn tác!",
                    "Cảnh báo - Xóa tất cả ghi chú",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string updateQuery = @"
                    UPDATE DeviceStatus 
                    SET Note = NULL, NoteTime = NULL, NoteBy = NULL";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show($"Đã xóa ghi chú của {rowsAffected} thiết bị!", "Thông báo",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    LoadDeviceData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa tất cả ghi chú: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void NoteSelectedMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một thiết bị để thêm ghi chú!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string title = dataGridView2.SelectedRows.Count == 1 ?
                "Ghi chú cho thiết bị đã chọn" :
                $"Ghi chú cho {dataGridView2.SelectedRows.Count} thiết bị đã chọn";

            using (NoteForm noteForm = new NoteForm())
            {
                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    List<int> selectedDeviceIds = new List<int>();

                    foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
                    {
                        int deviceId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                        selectedDeviceIds.Add(deviceId);
                    }

                    if (selectedDeviceIds.Count == 1)
                    {
                        AddNoteToDevice(selectedDeviceIds[0], noteForm.NoteText);
                    }
                    else
                    {
                        AddNoteToMultipleDevices(selectedDeviceIds, noteForm.NoteText);
                    }
                }
            }
        }
      
        private void NoteAllMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn thêm ghi chú cho TÁT CẢ thiết bị?",
                "Xác nhận ghi chú tất cả",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                using (NoteForm noteForm = new NoteForm())
                {
                    if (noteForm.ShowDialog() == DialogResult.OK)
                    {
                        AddNoteToAllDevices(noteForm.NoteText);
                    }
                }
            }
        }
        private void DeleteNoteMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một thiết bị để xóa ghi chú!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string confirmMessage;
            if (dataGridView2.SelectedRows.Count == 1)
            {
                string deviceName = dataGridView2.SelectedRows[0].Cells["Tên thiết bị"].Value?.ToString() ?? "";
                confirmMessage = $"Bạn có chắc chắn muốn xóa ghi chú của thiết bị '{deviceName}'?";
            }
            else
            {
                confirmMessage = $"Bạn có chắc chắn muốn xóa ghi chú của {dataGridView2.SelectedRows.Count} thiết bị đã chọn?";
            }

            DialogResult result = MessageBox.Show(confirmMessage, "Xác nhận xóa ghi chú",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                List<int> selectedDeviceIds = new List<int>();
                foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
                {
                    int deviceId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    selectedDeviceIds.Add(deviceId);
                }

                DeleteNotesFromDevices(selectedDeviceIds);
            }
        }

        private void SetupAlertSystem()
        {
            // Timer để kiểm tra cảnh báo mỗi 3 giây
            Timer alertTimer = new Timer { Interval = 3000 };
            alertTimer.Tick += timer2_Tick;
            alertTimer.Start();

            //Mô phỏng thay đổi trạng thái
            statusChangeTimer = new Timer { Interval = 3000 }; // Every 3 seconds
            statusChangeTimer.Tick += (s, e) =>
            {
                Random rand = new Random();
                if (rand.Next(1, 11) <= 90) // 90% chance
                {
                    SimulateStatusChange();
                }
            };
            statusChangeTimer.Start();
        }

        private void SimulateStatusChange()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Random rand = new Random();
                    int deviceId = rand.Next(1, 101);
                    int newStatus = rand.Next(0, 2);

                    // Lấy trạng thái hiện tại trước
                    string getCurrentStatusQuery = "SELECT Status FROM DeviceStatus WHERE Id = @Id";
                    int currentStatus;
                    using (SqlCommand cmd = new SqlCommand(getCurrentStatusQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", deviceId);
                        currentStatus = (int)cmd.ExecuteScalar();
                    }


                    if (currentStatus != newStatus)
                    {
                        string updateQuery = @"
                            UPDATE DeviceStatus 
                            SET PreviousStatus = Status, Status = @Status, LastUpdated = GETDATE(),
                                AlertTime = CASE WHEN @Status = 0 THEN GETDATE() ELSE AlertTime END
                            WHERE Id = @Id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Status", newStatus);
                            cmd.Parameters.AddWithValue("@Id", deviceId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                LoadDeviceData();
            }
            catch (Exception)
            {
                // Silent error handling for simulation
            }
        }

        private void lblAlert_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CheckForAlerts();
            CheckForRecoveredDevices();
        }

        private void CheckForAlerts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Get devices that just stopped working (status changed from 1 to 0 in last 10 seconds)
                    string query = @"
                SELECT DeviceId, DeviceName, AlertTime
                FROM DeviceStatus 
                WHERE Status = 0 
                AND PreviousStatus = 1 
                AND AlertTime >= DATEADD(SECOND, -10, GETDATE())
                ORDER BY AlertTime DESC";

                    List<DeviceAlert> stoppedDevices = new List<DeviceAlert>();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string deviceId = reader["DeviceId"].ToString();
                                string deviceName = reader["DeviceName"].ToString();
                                DateTime alertTime = Convert.ToDateTime(reader["AlertTime"]);

                                stoppedDevices.Add(new DeviceAlert
                                {
                                    DeviceId = deviceId,
                                    DeviceName = deviceName,
                                    AlertTime = alertTime
                                });
                            }
                        }
                    }

                    // Tô đỏ TẤT CẢ thiết bị bị dừng cùng lúc
                    foreach (var device in stoppedDevices)
                    {
                        HighlightDeviceRow(device.DeviceId, true);
                    }

                    // Hiển thị cảnh báo cho từng thiết bị một cách tuần tự
                    if (stoppedDevices.Count > 0)
                    {
                        ShowSequentialAlerts(stoppedDevices);

                        // Reset PreviousStatus để ngăn chặn cảnh báo lặp lại 
                        string resetQuery = @"
                    UPDATE DeviceStatus 
                    SET PreviousStatus = Status 
                    WHERE Status = 0 AND PreviousStatus = 1 
                    AND AlertTime >= DATEADD(SECOND, -15, GETDATE())";

                        using (SqlCommand resetCmd = new SqlCommand(resetQuery, conn))
                        {
                            resetCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Silent error handling
            }
        }
        private void ShowSequentialAlerts(List<DeviceAlert> stoppedDevices)
        {
            if (stoppedDevices.Count == 1)
            {
                // Chỉ có 1 thiết bị - hiển thị ngay
                string alertMessage = $"CẢNH BÁO: Thiết bị {stoppedDevices[0].DeviceId} - {stoppedDevices[0].DeviceName} vừa dừng hoạt động!";
                ShowAlert(alertMessage);
            }
            else
            {
                // Nhiều thiết bị - hiển thị tổng quan trước
                string summaryMessage = $"CẢNH BÁO: {stoppedDevices.Count} thiết bị vừa dừng hoạt động cùng lúc!";
                ShowAlert(summaryMessage);

                // Sau đó hiển thị chi tiết từng thiết bị với delay
                ShowDetailedAlerts(stoppedDevices);
            }
        }

        // Timer để hiển thị cảnh báo chi tiết từng thiết bị
        private void ShowDetailedAlerts(List<DeviceAlert> devices)
        {
            Timer detailTimer = new Timer();
            int currentIndex = 0;

            detailTimer.Interval = 6000; // 6 giây giữa mỗi cảnh báo chi tiết
            detailTimer.Tick += (sender, e) =>
            {
                if (currentIndex < devices.Count)
                {
                    var device = devices[currentIndex];
                    string detailMessage = $"Chi tiết: {device.DeviceId} - {device.DeviceName} dừng lúc {device.AlertTime:HH:mm:ss}";
                    ShowAlert(detailMessage);
                    currentIndex++;
                }
                else
                {
                    // Đã hiển thị hết tất cả thiết bị
                    detailTimer.Stop();
                    detailTimer.Dispose();
                }
            };

            detailTimer.Start();
        }
        private void HighlightDeviceRow(string deviceId, bool isAlert)
        {
            // Sử dụng Invoke để đảm bảo thread-safe khi cập nhật UI
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HighlightDeviceRow(deviceId, isAlert)));
                return;
            }

            // Tô màu trong DataGridView
            if (dataGridView2 != null)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["Mã thiết bị"].Value != null)
                    {
                        string rowDeviceId = row.Cells["Mã thiết bị"].Value.ToString();
                        if (rowDeviceId == deviceId)
                        {
                            if (isAlert)
                            {
                                // TÔ ĐỎ dòng có cảnh báo
                                row.DefaultCellStyle.BackColor = Color.Red;
                                row.DefaultCellStyle.ForeColor = Color.White;
                            }
                            else
                            {
                                // Reset màu bình thường
                                row.DefaultCellStyle.BackColor = Color.White;
                                row.DefaultCellStyle.ForeColor = Color.Black;
                            }
                            break; // Tìm thấy rồi thì thoát luôn
                        }
                    }
                }
            }
        }
        private void UpdateDeviceRowColorAfterNote(string deviceId)
        {
            // Sử dụng Invoke để đảm bảo thread-safe khi cập nhật UI
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDeviceRowColorAfterNote(deviceId)));
                return;
            }

            // Tìm và cập nhật màu trong DataGridView
            if (dataGridView2 != null)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["Mã thiết bị"].Value != null)
                    {
                        string rowDeviceId = row.Cells["Mã thiết bị"].Value.ToString();
                        if (rowDeviceId == deviceId)
                        {
                            // Kiểm tra trạng thái thiết bị
                            string status = row.Cells["Trạng thái"].Value?.ToString();

                            if (status == "Dừng")
                            {
                                // Thiết bị vẫn dừng nhưng đã có ghi chú 
                                row.DefaultCellStyle.BackColor = Color.White;
                                row.DefaultCellStyle.ForeColor = Color.Black;
                            }
                            else
                            {
                                // Thiết bị đang hoạt động -> màu bình thường
                                row.DefaultCellStyle.BackColor = Color.White;
                                row.DefaultCellStyle.ForeColor = Color.Black;
                            }
                            break;
                        }
                    }
                }
            }
        }
        // CODE THÊM: Hàm để reset màu khi thiết bị hoạt động trở lại
        private void CheckForRecoveredDevices()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Tìm các thiết bị vừa hoạt động trở lại (status changed from 0 to 1)
                    string query = @"
                SELECT DeviceId
                FROM DeviceStatus 
                WHERE Status = 1 
                AND PreviousStatus = 0 
                AND LastUpdated >= DATEADD(SECOND, -15, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string deviceId = reader["DeviceId"].ToString();
                                // RESET MÀU ngay lập tức khi thiết bị hoạt động trở lại
                                HighlightDeviceRow(deviceId, false);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Silent error handling
            }
        }

        private void ShowAlert(string message, int displayDuration = 5000)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowAlert(message, displayDuration)));
                return;
            }

            lblAlert.Text = message;
            lblAlert.Visible = true;
            lblAlert.BackColor = Color.Red;
            lblAlert.ForeColor = Color.White;

            // Dừng timer cũ nếu có
            if (alertHideTimer != null)
            {
                alertHideTimer.Stop();
                alertHideTimer.Dispose();
            }

            // Tạo timer mới để ẩn cảnh báo
            alertHideTimer = new Timer { Interval = displayDuration };
            alertHideTimer.Tick += (s, ev) =>
            {
                lblAlert.Visible = false;
                alertHideTimer.Stop();
                alertHideTimer.Dispose();
                alertHideTimer = null;
            };
            alertHideTimer.Start();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ExportExcel(string path)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                // Initialize Excel application
                application = new Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Excel.Worksheet)workbook.ActiveSheet;

                // Xuất data cột header
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView2.Columns[i].HeaderText;
                }

                // Style headers 
                Excel.Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dataGridView2.Columns.Count]];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = Color.LightBlue;

                // Xuất data cột 
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        // Handle null values
                        worksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

                // Auto-fit columns
                worksheet.Columns.AutoFit();

                // Save the workbook
                workbook.SaveAs(path);
                workbook.Saved = true;

                MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xuất file không thành công!\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Clean up Excel objects
                if (worksheet != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                if (application != null)
                {
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Export Excel";
            savefile.Filter = "Excel Files|*.xlsx";
            savefile.FileName = $"DeviceStatus_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcel(savefile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xuất file không thành công!\n" + ex.Message);
                }
            }
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Dừng simulation khi lọc
                if (statusChangeTimer != null)
                {
                    statusChangeTimer.Stop();
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT Id, DeviceId as [Mã thiết bị], DeviceName as [Tên thiết bị], 
                       CASE WHEN Status = 1 THEN N'Hoạt động' ELSE N'Dừng' END as [Trạng thái],
                       Status, LastUpdated as [Cập nhật cuối], AlertTime as [Thời gian cảnh báo],
                       Note as [Ghi chú], NoteTime as [Thời gian ghi chú], NoteBy as [Người ghi chú]
                FROM DeviceStatus 
                WHERE Status = 0 AND AlertTime BETWEEN @FromDate AND @ToDate
                ORDER BY AlertTime DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@FromDate", dtpFromDate.Value.Date);
                    adapter.SelectCommand.Parameters.AddWithValue("@ToDate", dtpToDate.Value.Date.AddDays(1).AddSeconds(-1));

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView2.DataSource = dt;

                    // Giấu các cột không cần thiết
                    if (dataGridView2.Columns["Id"] != null)
                        dataGridView2.Columns["Id"].Visible = false;
                    if (dataGridView2.Columns["Status"] != null)
                        dataGridView2.Columns["Status"].Visible = false;

                    // Format datetime columns
                    if (dataGridView2.Columns["Cập nhật cuối"] != null)
                        dataGridView2.Columns["Cập nhật cuối"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    if (dataGridView2.Columns["Thời gian cảnh báo"] != null)
                        dataGridView2.Columns["Thời gian cảnh báo"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    if (dataGridView2.Columns["Thời gian ghi chú"] != null)
                        dataGridView2.Columns["Thời gian ghi chú"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                    // Highlight các thiết bị có cảnh báo
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells["Trạng thái"].Value?.ToString() == "Dừng")
                        {
                            // Kiểm tra xem thiết bị đã có ghi chú chưa
                            bool hasNote = !string.IsNullOrEmpty(row.Cells["Ghi chú"].Value?.ToString());

                            if (hasNote)
                            {
                                // Đã có ghi chú -> màu vàng nhạt
                                row.DefaultCellStyle.BackColor = Color.White;
                                row.DefaultCellStyle.ForeColor = Color.Black;
                            }
                            else
                            {
                                // Chưa có ghi chú -> màu đỏ (cảnh báo)
                                row.DefaultCellStyle.BackColor = Color.LightCoral;
                                row.DefaultCellStyle.ForeColor = Color.DarkRed;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lọc dữ liệu cảnh báo: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị loading cursor
                this.Cursor = Cursors.WaitCursor;

                // Reload dữ liệu
                LoadDeviceData();

                // Khởi động lại simulation
                if (statusChangeTimer != null)
                {
                    statusChangeTimer.Start();
                }

                // Trả về cursor bình thường
                this.Cursor = Cursors.Default;

               
            }
            catch (Exception ex)
            {
                // Trả về cursor bình thường nếu có lỗi
                this.Cursor = Cursors.Default;

                MessageBox.Show("Lỗi khi làm mới dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            if (statusChangeTimer != null)
            {
                statusChangeTimer.Stop();
            }
            if (dataGridView2.SelectedRows.Count == 0)
            {
                // Không có dòng nào được chọn - hỏi người dùng muốn ghi chú cho tất cả
                DialogResult result = MessageBox.Show(
                    "Không có thiết bị nào được chọn.\nBạn có muốn thêm ghi chú cho TẤT CẢ thiết bị?",
                    "Xác nhận",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowNoteFormForAllDevices();
                }
                return;
            }

            // Có dòng được chọn
            if (dataGridView2.SelectedRows.Count == 1)
            {
                // Chỉ một dòng được chọn
                ShowNoteFormForSelectedDevice();
            }
            else
            {
                // Nhiều dòng được chọn
                ShowNoteFormForMultipleDevices();
            }
        }
        private void ShowNoteFormForSelectedDevice()
        {
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
            string deviceName = selectedRow.Cells["Tên thiết bị"].Value?.ToString() ?? "";

            using (NoteForm noteForm = new NoteForm())
            {
                noteForm.Text = $"Ghi chú cho thiết bị: {deviceName}";

                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    int deviceId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    AddNoteToDevice(deviceId, noteForm.NoteText);
                }
            }
        }

        // Hiển thị form ghi chú cho nhiều thiết bị
        private void ShowNoteFormForMultipleDevices()
        {
            int selectedCount = dataGridView2.SelectedRows.Count;

            using (NoteForm noteForm = new NoteForm())
            {
                noteForm.Text = $"Ghi chú cho {selectedCount} thiết bị đã chọn";

                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    List<int> selectedDeviceIds = new List<int>();

                    foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
                    {
                        int deviceId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                        selectedDeviceIds.Add(deviceId);
                    }

                    AddNoteToMultipleDevices(selectedDeviceIds, noteForm.NoteText);
                }
            }
        }

        // Hiển thị form ghi chú cho tất cả thiết bị
        private void ShowNoteFormForAllDevices()
        {
            using (NoteForm noteForm = new NoteForm())
            {
                noteForm.Text = "Ghi chú cho TẤT CẢ thiết bị";

                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    DialogResult confirmResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn thêm ghi chú cho TẤT CẢ thiết bị?",
                        "Xác nhận ghi chú tất cả",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmResult == DialogResult.Yes)
                    {
                        AddNoteToAllDevices(noteForm.NoteText);
                    }
                }
            }
        }
        private void ShowDeviceStatusChart()
        {
            try
            {
                // Tạo form mới để hiển thị biểu đồ
                Form chartForm = new Form();
                chartForm.Text = "Biểu đồ trạng thái thiết bị";
                chartForm.Size = new Size(800, 600);
                chartForm.StartPosition = FormStartPosition.CenterParent;

                // Tạo Chart control
                Chart chart = new Chart();
                chart.Dock = DockStyle.Fill;

                // Tạo ChartArea
                ChartArea chartArea = new ChartArea("MainArea");
                chartArea.AxisX.Title = "Thiết bị";
                chartArea.AxisY.Title = "Số lượng";
                chartArea.AxisX.Interval = 1;
                chart.ChartAreas.Add(chartArea);

                // Lấy dữ liệu từ DataGridView hiện tại
                var deviceData = GetCurrentDeviceData();

                // Tạo series cho biểu đồ cột
                Series columnSeries = new Series("Trạng thái thiết bị");
                columnSeries.ChartType = SeriesChartType.Column;
                columnSeries.Color = Color.SteelBlue;

                // Thêm dữ liệu vào series
                columnSeries.Points.AddXY("Hoạt động", deviceData.ActiveCount);
                columnSeries.Points.AddXY("Dừng", deviceData.StoppedCount);

                // Màu sắc khác nhau cho các cột
                columnSeries.Points[0].Color = Color.Green;
                columnSeries.Points[1].Color = Color.Red;

                chart.Series.Add(columnSeries);

                // Tạo series cho biểu đồ tròn
                Series pieSeries = new Series("Tỷ lệ trạng thái");
                pieSeries.ChartType = SeriesChartType.Pie;
                pieSeries.Points.AddXY("Hoạt động", deviceData.ActiveCount);
                pieSeries.Points.AddXY("Dừng", deviceData.StoppedCount);
                pieSeries.Points[0].Color = Color.Green;
                pieSeries.Points[1].Color = Color.Red;

                // Hiển thị phần trăm trên biểu đồ tròn
                pieSeries.Label = "#PERCENT{P1}";
                pieSeries.LegendText = "#VALX (#VAL)";

                // Tạo TabControl để chứa cả 2 loại biểu đồ
                TabControl tabControl = new TabControl();
                tabControl.Dock = DockStyle.Fill;

                // Tab biểu đồ cột
                TabPage columnTab = new TabPage("Biểu đồ cột");
                Chart columnChart = new Chart();
                columnChart.Dock = DockStyle.Fill;
                columnChart.ChartAreas.Add(chartArea);
                columnChart.Series.Add(columnSeries);

                // Thêm tiêu đề
                Title columnTitle = new Title($"Trạng thái thiết bị - Tổng: {deviceData.TotalCount}");
                columnTitle.Font = new Font("Arial", 14, FontStyle.Bold);
                columnChart.Titles.Add(columnTitle);

                // Thêm legend
                Legend columnLegend = new Legend("Legend");
                columnChart.Legends.Add(columnLegend);

                columnTab.Controls.Add(columnChart);

                // Tab biểu đồ tròn
                TabPage pieTab = new TabPage("Biểu đồ tròn");
                Chart pieChart = new Chart();
                pieChart.Dock = DockStyle.Fill;

                ChartArea pieArea = new ChartArea("PieArea");
                pieChart.ChartAreas.Add(pieArea);
                pieChart.Series.Add(pieSeries);

                // Thêm tiêu đề cho biểu đồ tròn
                Title pieTitle = new Title($"Tỷ lệ trạng thái thiết bị - Tổng: {deviceData.TotalCount}");
                pieTitle.Font = new Font("Arial", 14, FontStyle.Bold);
                pieChart.Titles.Add(pieTitle);

                // Thêm legend cho biểu đồ tròn
                Legend pieLegend = new Legend("PieLegend");
                pieChart.Legends.Add(pieLegend);

                pieTab.Controls.Add(pieChart);

                // Tab biểu đồ theo vị trí
                TabPage positionTab = CreatePositionChart(deviceData);

                // Thêm các tab vào TabControl
                tabControl.TabPages.Add(columnTab);
                tabControl.TabPages.Add(pieTab);
                tabControl.TabPages.Add(positionTab);

                // Thêm panel thông tin thống kê
                Panel infoPanel = new Panel();
                infoPanel.Height = 80;
                infoPanel.Dock = DockStyle.Bottom;
                infoPanel.BackColor = Color.LightGray;

                Label infoLabel = new Label();
                infoLabel.AutoSize = false;
                infoLabel.Dock = DockStyle.Fill;
                infoLabel.TextAlign = ContentAlignment.MiddleCenter;
                infoLabel.Font = new Font("Arial", 10, FontStyle.Regular);
                infoLabel.Text = $"Tổng số thiết bị: {deviceData.TotalCount}\n" +
                                $"Đang hoạt động: {deviceData.ActiveCount} ({deviceData.ActivePercentage:F1}%)\n" +
                                $"Đã dừng: {deviceData.StoppedCount} ({deviceData.StoppedPercentage:F1}%)\n" +
                                $"Cập nhật lúc: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

                infoPanel.Controls.Add(infoLabel);

                chartForm.Controls.Add(tabControl);
                chartForm.Controls.Add(infoPanel);

                // Hiển thị form
                chartForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo biểu đồ: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TabPage CreatePositionChart(DeviceStatistics deviceData)
        {
            TabPage positionTab = new TabPage("Biểu đồ theo vị trí");

            Chart positionChart = new Chart();
            positionChart.Dock = DockStyle.Fill;

            ChartArea positionArea = new ChartArea("PositionArea");
            positionArea.AxisX.Title = "Vị trí";
            positionArea.AxisY.Title = "Số lượng thiết bị";
            positionArea.AxisX.Interval = 1;
            positionChart.ChartAreas.Add(positionArea);

            // Series cho thiết bị hoạt động theo vị trí
            Series activeSeries = new Series("Hoạt động");
            activeSeries.ChartType = SeriesChartType.Column;
            activeSeries.Color = Color.Green;

            // Series cho thiết bị dừng theo vị trí
            Series stoppedSeries = new Series("Dừng");
            stoppedSeries.ChartType = SeriesChartType.Column;
            stoppedSeries.Color = Color.Red;

            // Lấy dữ liệu theo vị trí
            var positionData = GetDeviceDataByPosition();

            foreach (var pos in positionData)
            {
                activeSeries.Points.AddXY($"Vị trí {pos.Position}", pos.ActiveCount);
                stoppedSeries.Points.AddXY($"Vị trí {pos.Position}", pos.StoppedCount);
            }

            positionChart.Series.Add(activeSeries);
            positionChart.Series.Add(stoppedSeries);

            // Thêm tiêu đề
            Title positionTitle = new Title("Phân bố thiết bị theo vị trí");
            positionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            positionChart.Titles.Add(positionTitle);

            // Thêm legend
            Legend positionLegend = new Legend("PositionLegend");
            positionChart.Legends.Add(positionLegend);

            positionTab.Controls.Add(positionChart);
            return positionTab;
        }

        private DeviceStatistics GetCurrentDeviceData()
        {
            int activeCount = 0;
            int stoppedCount = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Trạng thái"] != null && row.Cells["Trạng thái"].Value != null)
                {
                    string status = row.Cells["Trạng thái"].Value.ToString();
                    if (status == "Hoạt động")
                        activeCount++;
                    else if (status == "Dừng")
                        stoppedCount++;
                }
            }

            int totalCount = activeCount + stoppedCount;
            double activePercentage = totalCount > 0 ? (double)activeCount / totalCount * 100 : 0;
            double stoppedPercentage = totalCount > 0 ? (double)stoppedCount / totalCount * 100 : 0;

            return new DeviceStatistics
            {
                ActiveCount = activeCount,
                StoppedCount = stoppedCount,
                TotalCount = totalCount,
                ActivePercentage = activePercentage,
                StoppedPercentage = stoppedPercentage
            };
        }

        private List<PositionStatistics> GetDeviceDataByPosition()
        {
            var positionStats = new List<PositionStatistics>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT Position, 
                       SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) as ActiveCount,
                       SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) as StoppedCount
                FROM DeviceStatus 
                GROUP BY Position 
                ORDER BY Position";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                positionStats.Add(new PositionStatistics
                                {
                                    Position = Convert.ToInt32(reader["Position"]),
                                    ActiveCount = Convert.ToInt32(reader["ActiveCount"]),
                                    StoppedCount = Convert.ToInt32(reader["StoppedCount"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu theo vị trí: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return positionStats;
        }
        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                // Không có dòng nào được chọn - hỏi có muốn xóa ghi chú tất cả không
                DialogResult result = MessageBox.Show(
                    "Không có thiết bị nào được chọn.\nBạn có muốn xóa ghi chú của TẤT CẢ thiết bị?",
                    "Xác nhận",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteAllNotes();
                }
                return;
            }

            // Có dòng được chọn - xóa ghi chú của các dòng đã chọn
            List<int> selectedDeviceIds = new List<int>();
            foreach (DataGridViewRow selectedRow in dataGridView2.SelectedRows)
            {
                int deviceId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                selectedDeviceIds.Add(deviceId);
            }

            string confirmMessage = selectedDeviceIds.Count == 1 ?
                "Bạn có chắc chắn muốn xóa ghi chú của thiết bị đã chọn?" :
                $"Bạn có chắc chắn muốn xóa ghi chú của {selectedDeviceIds.Count} thiết bị đã chọn?";

            DialogResult confirmResult = MessageBox.Show(confirmMessage, "Xác nhận xóa ghi chú",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                DeleteNotesFromDevices(selectedDeviceIds);
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            ShowDeviceStatusChart();
        }
    }
}