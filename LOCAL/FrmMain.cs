using Ahd.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{    
    public partial class FrmMain : Form
    {
       
        private Form currentChildForm = null;
    
        private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        private const string ApiUrl = "http://dautiengphuochoa.com/api/getmn.aspx?key=dauhoaphuongtien%3b";
        private Dictionary<string, string> TargetStations = new Dictionary<string, string>
            {

      {"F01877", "Son Đai"},

      {"F01203", "Ben Suc"},

      {"F01849", "TVan_DT"}
    };
        private Dictionary<string, string> ManualStations = new Dictionary<string, string>
    {
        {"F0000", "Hồ Dầu Tiếng"},
        {"F0001", "Dự Phòng"}
    };
        private HttpClient _httpClient;
        private Timer _updateTimer;
        private Timer _manualUpdateTimer;
        private bool _isUpdating = false;
        private bool _isManualUpdating = false;
        private Dictionary<string, int> manualValues = new Dictionary<string, int>();// Lưu giá trị thủ công từ MapForm

        public FrmMain()
        {
            InitializeComponent();
        
            _httpClient = new HttpClient(); // Khởi tạo HttpClient để gọi API
            InitializeTimer(); // Cấu hình Timer
            // --- Bắt đầu cập nhật tự động ngay khi Form khởi chạy ---
            _isUpdating = true; // Đặt trạng thái là đang cập nhật
            _isManualUpdating = true;
            _updateTimer.Start(); // Bắt đầu Timer để kích hoạt sau mỗi khoảng thời gian
            _manualUpdateTimer.Start();
            // Chạy lần kiểm tra và cập nhật đầu tiên ngay lập tức
            // Sử dụng Task.Run để tránh block UI thread và cho phép await
            Task.Run(async () => await OnTimerTick());
            Task.Run(async () => await OnManualTimerTick());
         //   this.currentUser = loggedInUser;
        }
        
        IAhdDriverConnector driver;
        private void InitializeTimer()
        {
            _updateTimer = new Timer();
            _updateTimer.Interval = 5 * 60 * 1000; // Đặt khoảng thời gian là 5 phút (tính bằng mili giây)
            // Đăng ký sự kiện Tick của Timer, phương thức OnTimerTick sẽ được gọi khi timer đếm hết
            _updateTimer.Tick += async (sender, e) => await OnTimerTick();
            // Timer cho Manual stations (3 phút)
            _manualUpdateTimer = new Timer();
            _manualUpdateTimer.Interval = 3 * 60 * 1000; // 3 phút
            _manualUpdateTimer.Tick += async (sender, e) => await OnManualTimerTick();
        }
       
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmLogin.currentLoginLogId > 0)
            {
                FrmLogin.SaveLogoutTime(FrmLogin.currentLoginLogId);
            }
        }
        private async Task OnTimerTick()
        {
            // Đảm bảo logic chỉ chạy khi trạng thái là đang cập nhật

            if (_isUpdating)
            {
                await FetchAndUpdateData(); // Gọi phương thức chính để lấy và cập nhật dữ liệu
            }
        }
        private async Task OnManualTimerTick()
        {
            if (_isManualUpdating)
            {
                await FetchAndUpdateManualData();
            }
        }
        private async Task FetchAndUpdateManualData()
        {
            try
            {
                Dictionary<string, int> valuesToProcess;
                lock (manualValues)
                {
                    if (!manualValues.Any())
                    {
                        AppendLog("Không có giá trị thủ công mới để xử lý.");
                        return;
                    }
                    valuesToProcess = new Dictionary<string, int>(manualValues);
                    manualValues.Clear();
                    AppendLog($"DEBUG: Số giá trị thủ công để xử lý: {valuesToProcess.Count}");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    AppendLog("DEBUG: Đang mở kết nối database...");
                    await connection.OpenAsync();
                    if (connection.State != ConnectionState.Open)
                    {
                        AppendLog("Lỗi: Không thể mở kết nối database.");
                        return;
                    }

                    DateTime currentDateTime = DateTime.Now;
                    DateTime fiveMinutesAgo = currentDateTime.AddMinutes(-5);
                    string stationCodesParam = string.Join(",", valuesToProcess.Keys.Select(k => $"'{k}'"));
                    AppendLog($"DEBUG: Truy vấn trạm: {stationCodesParam}");

                    // Lấy bản ghi gần nhất trong 5 phút
                    Dictionary<string, (int value, DateTime lastUpdate)> recentDbValues = new Dictionary<string, (int, DateTime)>();
                    string selectRecentSql = $@"
                SELECT [Mã Trạm], [Giá Trị], [Ngày], [Giờ]
                FROM water_station 
                WHERE [Mã Trạm] IN ({stationCodesParam})
                AND (
                    ([Ngày] = @FiveMinutesAgoDate AND [Giờ] >= @FiveMinutesAgoTime) OR
                    ([Ngày] > @FiveMinutesAgoDate)
                )
                ORDER BY [Mã Trạm], [Ngày] DESC, [Giờ] DESC";

                    using (SqlCommand selectRecentCommand = new SqlCommand(selectRecentSql, connection))
                    {
                        selectRecentCommand.Parameters.AddWithValue("@FiveMinutesAgoDate", fiveMinutesAgo.Date);
                        selectRecentCommand.Parameters.AddWithValue("@FiveMinutesAgoTime", fiveMinutesAgo.TimeOfDay);

                        AppendLog("DEBUG: Đang thực thi truy vấn lấy bản ghi gần nhất...");
                        using (SqlDataReader reader = await selectRecentCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string stationCode = reader["Mã Trạm"].ToString();
                                int value = Convert.ToInt32(reader["Giá Trị"]);
                                DateTime recordDate = Convert.ToDateTime(reader["Ngày"]);
                                TimeSpan recordTime = (TimeSpan)reader["Giờ"];
                                DateTime recordDateTime = recordDate.Add(recordTime);

                                if (!recentDbValues.ContainsKey(stationCode) ||
                                    recordDateTime > recentDbValues[stationCode].lastUpdate)
                                {
                                    recentDbValues[stationCode] = (value, recordDateTime);
                                    AppendLog($"DEBUG: Tìm thấy bản ghi cho {stationCode}: Giá trị = {value}, Thời gian = {recordDateTime:yyyy-MM-dd HH:mm:ss}");
                                }
                            }
                        }
                    }

                    int newRecordsCount = 0;
                    int skippedRecordsCount = 0;

                    foreach (var entry in valuesToProcess)
                    {
                        string stationCode = entry.Key;
                        int manualValue = entry.Value;
                        string stationName = ManualStations.ContainsKey(stationCode) ? ManualStations[stationCode] : "Unknown";
                        AppendLog($"DEBUG: Xử lý trạm {stationCode} ({stationName}) với giá trị {manualValue}");

                        bool shouldInsert = true;
                        string reason = "";

                        if (recentDbValues.ContainsKey(stationCode))
                        {
                            var (recentValue, lastUpdate) = recentDbValues[stationCode];
                            TimeSpan timeDifference = currentDateTime - lastUpdate;

                            if (recentValue == manualValue && timeDifference.TotalMinutes < 5)
                            {
                                reason = $"Giá trị thủ công trùng lặp ({manualValue}) trong vòng 5 phút.";
                                shouldInsert = false;
                                skippedRecordsCount++;
                            }
                            else
                            {
                                reason = timeDifference.TotalMinutes >= 5
                                    ? $"Giá trị thủ công trùng ({manualValue}) nhưng đã cách {timeDifference.TotalMinutes:F2} phút."
                                    : $"Giá trị thủ công thay đổi từ {recentValue} -> {manualValue}.";
                            }
                        }
                        else
                        {
                            reason = $"Không có dữ liệu trong 5 phút, giá trị thủ công mới: {manualValue}";
                        }

                        AppendLog($"Trạm {stationCode} ({stationName}): {reason}");

                        if (shouldInsert)
                        {
                            // Chèn bản ghi mới
                            string insertSql = @"
                        INSERT INTO water_station ([Mã Trạm], [Tên Trạm Nước], [Ngày], [Giờ], [Giá Trị])
                        VALUES (@StationCode, @StationName, @CurrentDate, @CurrentTime, @NewValue);";

                            using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@StationCode", stationCode);
                                insertCommand.Parameters.AddWithValue("@StationName", stationName);
                                insertCommand.Parameters.AddWithValue("@CurrentDate", currentDateTime.Date);
                                insertCommand.Parameters.AddWithValue("@CurrentTime", currentDateTime.TimeOfDay);
                                insertCommand.Parameters.AddWithValue("@NewValue", manualValue);

                                AppendLog("DEBUG: Đang chèn bản ghi mới...");
                                int rowsAffected = await insertCommand.ExecuteNonQueryAsync();

                                if (rowsAffected > 0)
                                {
                                    newRecordsCount++;
                                    AppendLog($"✓ Đã thêm bản ghi thủ công cho {stationCode} với giá trị {manualValue}.");
                                }
                                else
                                {
                                    AppendLog($"⚠ Không thể thêm bản ghi thủ công cho {stationCode}.");
                                }
                            }
                        }
                    }

                    AppendLog($"Kết quả xử lý thủ công: {newRecordsCount} bản ghi mới, {skippedRecordsCount} bản ghi bị bỏ qua.");
                }
            }
            catch (SqlException sqlEx)
            {
                AppendLog($"Lỗi SQL trong FetchAndUpdateManualData: {sqlEx.Message}");
                AppendLog($"Chi tiết: {sqlEx.StackTrace}");
            }
            catch (Exception ex)
            {
                AppendLog($"Lỗi không xác định trong FetchAndUpdateManualData: {ex.Message}");
                AppendLog($"Chi tiết: {ex.StackTrace}");
            }
        }
        private async Task FetchAndUpdateData()
        {
            try
            {
                // 1. Cấu hình HttpClient với headers phù hợp
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
                _httpClient.DefaultRequestHeaders.Add("Accept", "text/plain, text/html, */*");

                // Gửi yêu cầu HTTP GET đến API và lấy dữ liệu thô
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();
                string apiDataRaw = await response.Content.ReadAsStringAsync();

                // 2. Phân tích dữ liệu từ chuỗi text nhận được từ API
                Dictionary<string, int> parsedData = new Dictionary<string, int>();

                // Pattern 1: Format gốc với <br>
                Regex regex1_refined = new Regex(@"(F\d{5});\d{2}/\d{2}/\d{4};\d{2}:\d{2};value=(\d+);", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                // Pattern 2: Format không có <br>
                Regex regex2 = new Regex(@"(F\d{5});\d{2}/\d{2}/\d{4};\d{2}:\d{2};value=(\d+)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                // Pattern 3: Format có thể có space hoặc ký tự khác
                Regex regex3 = new Regex(@"(F\d{5})[^\d]*(\d{2}/\d{2}/\d{4})[^\d]*(\d{2}:\d{2})[^\d]*value\s*=\s*(\d+)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                // Thử từng pattern, đặt regex1_refined lên đầu để ưu tiên khớp chính xác nhất.
                var patterns = new[] { regex1_refined, regex2, regex3 };
                string[] patternNames = { "Pattern 1 (đã tinh chỉnh)", "Pattern 2 (không <br>)", "Pattern 3 (flexible)" };

                for (int i = 0; i < patterns.Length; i++)
                {
                    var matches = patterns[i].Matches(apiDataRaw);
                    AppendLog($"{patternNames[i]}: Tìm thấy {matches.Count} matches");

                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            string stationCode = match.Groups[1].Value; // Mã Trạm luôn ở Group 1
                            int value;

                            // Với regex1_refined và regex2, giá trị ở group 2.
                            // Với regex3, giá trị ở group 4.
                            // Điều chỉnh logic này cho phù hợp với các pattern mới.
                            int valueGroupIndex = 2; // Mặc định là group 2 cho regex1_refined và regex2
                            if (i == 2) // Nếu là Pattern 3 (index 2)
                            {
                                valueGroupIndex = 4; // Giá trị ở group 4
                            }

                            if (int.TryParse(match.Groups[valueGroupIndex].Value, out value))
                            {
                                // Chỉ lưu trữ dữ liệu của các trạm mà chúng ta quan tâm
                                if (TargetStations.ContainsKey(stationCode))
                                {
                                    parsedData[stationCode] = value;
                                    AppendLog($"Tìm thấy dữ liệu trạm {stationCode}: {value} (sử dụng {patternNames[i]})");
                                }
                            }
                        }

                        // Nếu tìm thấy dữ liệu với pattern này và parsedData có dữ liệu thì break
                        if (parsedData.Count > 0) break;
                    }
                }
                // 3. Kết nối đến Database và thực hiện so sánh/cập nhật
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // *** CẢI THIỆN: Lấy bản ghi trong 10 phút gần nhất của mỗi trạm ***
                    Dictionary<string, (int value, DateTime lastUpdate)> recentDbValues = new Dictionary<string, (int, DateTime)>();

                    // Tạo danh sách các station codes để query
                    string stationCodesParam = string.Join(",", parsedData.Keys.Select(k => $"'{k}'"));

                    DateTime currentDateTime = DateTime.Now;
                    DateTime tenMinutesAgo = currentDateTime.AddMinutes(-10);

                    // Query để lấy tất cả bản ghi trong 10 phút gần nhất của mỗi trạm
                    string selectRecentSql = $@"
                SELECT [Mã Trạm], [Giá Trị], [Ngày], [Giờ]
                FROM water_station 
                WHERE [Mã Trạm] IN ({stationCodesParam})
                AND (
                    ([Ngày] = @TenMinutesAgoDate AND [Giờ] >= @TenMinutesAgoTime) OR
                    ([Ngày] > @TenMinutesAgoDate)
                )
                ORDER BY [Mã Trạm], [Ngày] DESC, [Giờ] DESC";

                    SqlCommand selectRecentCommand = new SqlCommand(selectRecentSql, connection);
                    selectRecentCommand.Parameters.AddWithValue("@TenMinutesAgoDate", tenMinutesAgo.Date);
                    selectRecentCommand.Parameters.AddWithValue("@TenMinutesAgoTime", tenMinutesAgo.TimeOfDay);

                    using (SqlDataReader reader = await selectRecentCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string stationCode = reader["Mã Trạm"].ToString();
                            int value = Convert.ToInt32(reader["Giá Trị"]);
                            DateTime recordDate = Convert.ToDateTime(reader["Ngày"]);
                            TimeSpan recordTime = (TimeSpan)reader["Giờ"];
                            DateTime recordDateTime = recordDate.Add(recordTime);

                            // Lưu bản ghi gần nhất trong 10 phút (nếu chưa có hoặc gần hơn)
                            if (!recentDbValues.ContainsKey(stationCode) ||
                                recordDateTime > recentDbValues[stationCode].lastUpdate)
                            {
                                recentDbValues[stationCode] = (value, recordDateTime);
                            }

                            AppendLog($"DB trong 10 phút - Trạm {stationCode}: Giá trị = {value}, Thời gian = {recordDateTime:yyyy-MM-dd HH:mm:ss}");
                        }
                    }

                    // *** KIỂM TRA TRÙNG LẶP TRONG 10 PHÚT ***
                    int newRecordsCount = 0;
                    int skippedRecordsCount = 0;

                    foreach (var entry in parsedData)
                    {
                        string stationCode = entry.Key;
                        int apiValue = entry.Value;
                        string stationName = TargetStations[stationCode];

                        bool shouldInsert = true;
                        string reason = "";

                        if (recentDbValues.ContainsKey(stationCode))
                        {
                            var (recentValue, lastUpdate) = recentDbValues[stationCode];
                            TimeSpan timeDifference = currentDateTime - lastUpdate;

                            // Kiểm tra xem có giá trị trùng lặp trong 10 phút không
                            if (recentValue == apiValue)
                            {
                                reason = $"Giá trị trùng lặp ({apiValue}) trong 10 phút gần đây (lần cuối: {lastUpdate:HH:mm:ss}, cách đây {timeDifference.TotalMinutes:F1} phút)";
                                shouldInsert = false;
                                skippedRecordsCount++;
                            }
                            else
                            {
                                reason = $"Giá trị thay đổi từ {recentValue} -> {apiValue} (cách đây {timeDifference.TotalMinutes:F1} phút)";
                            }
                        }
                        else
                        {
                            reason = $"Không có dữ liệu trong 10 phút gần đây, giá trị mới: {apiValue}";
                        }

                        AppendLog($"Trạm {stationCode} ({stationName}): {reason}");

                        if (shouldInsert)
                        {
                            // Kiểm tra thêm để đảm bảo không có bản ghi trùng lặp chính xác
                            string checkExactDuplicateSql = @"
                        SELECT COUNT(*) 
                        FROM water_station 
                        WHERE [Mã Trạm] = @StationCode 
                        AND [Giá Trị] = @Value
                        AND [Ngày] = @CurrentDate
                        AND [Giờ] = @CurrentTime";

                            SqlCommand checkCmd = new SqlCommand(checkExactDuplicateSql, connection);
                            checkCmd.Parameters.AddWithValue("@StationCode", stationCode);
                            checkCmd.Parameters.AddWithValue("@Value", apiValue);
                            checkCmd.Parameters.AddWithValue("@CurrentDate", currentDateTime.Date);
                            checkCmd.Parameters.AddWithValue("@CurrentTime", currentDateTime.TimeOfDay);

                            int exactDuplicateCount = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                            if (exactDuplicateCount > 0)
                            {
                                AppendLog($"⚠ Trạm {stationCode}: Phát hiện bản ghi trùng lặp chính xác (cùng thời gian). Bỏ qua.");
                                skippedRecordsCount++;
                                continue;
                            }

                            // Thêm bản ghi mới
                            string insertSql = @"
                        INSERT INTO water_station ([Mã Trạm], [Tên Trạm Nước], [Ngày], [Giờ], [Giá Trị])
                        VALUES (@StationCode, @StationName, @CurrentDate, @CurrentTime, @NewValue);";

                            SqlCommand insertCommand = new SqlCommand(insertSql, connection);
                            insertCommand.Parameters.AddWithValue("@StationCode", stationCode);
                            insertCommand.Parameters.AddWithValue("@StationName", stationName);
                            insertCommand.Parameters.AddWithValue("@CurrentDate", currentDateTime.Date);
                            insertCommand.Parameters.AddWithValue("@CurrentTime", currentDateTime.TimeOfDay);
                            insertCommand.Parameters.AddWithValue("@NewValue", apiValue);

                            int rowsAffected = await insertCommand.ExecuteNonQueryAsync();

                            if (rowsAffected > 0)
                            {
                                newRecordsCount++;
                                AppendLog($"✓ Đã thêm mới bản ghi cho {stationCode} với giá trị {apiValue} lúc {currentDateTime:HH:mm:ss}");
                            }
                            else
                            {
                                AppendLog($"⚠ Không thể thêm bản ghi cho {stationCode}. Kiểm tra lại database.");
                            }
                        }
                    }


                }
            }
            catch (HttpRequestException httpEx)
            {
                AppendLog($"Lỗi HTTP khi gọi API: {httpEx.Message}");
                AppendLog($"Chi tiết: {httpEx.StackTrace}");
            }
            catch (SqlException sqlEx)
            {
                AppendLog($"Lỗi Database SQL Server: {sqlEx.Message}");
                AppendLog($"Chi tiết: {sqlEx.StackTrace}");
            }
            catch (Exception ex)
            {
                AppendLog($"Lỗi không xác định: {ex.Message}");
                AppendLog($"Chi tiết: {ex.StackTrace}");
            }
        }
        private void AppendLog(string message)
        {
            //// Hiển thị log trong TextBox hoặc Console
            //if (txtLog != null)
            //{
            //    txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}\r\n");
            //    txtLog.ScrollToCaret(); // Cuộn xuống cuối log
            //}
            //else
            //{
            //    Console.WriteLine($"{DateTime.Now:HH:mm:ss} - {message}");
            //}
        }

        private void OpenFormInPanel(Form childForm, string title)
        {
            try
            {
                // Nếu đang mở chính form đó thì không cần mở lại
                if (currentChildForm == childForm)
                    return;

                // Ẩn form cũ nếu đang có
                if (currentChildForm != null)
                {
                    currentChildForm.Hide(); // hoặc: panelDesktop.Controls.Remove(currentChildForm);
                }

                // Nếu form chưa được add thì cấu hình
                if (!panelDesktop.Controls.Contains(childForm))
                {
                    childForm.TopLevel = false;
                    childForm.FormBorderStyle = FormBorderStyle.None;
                    childForm.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(childForm);
                }

                currentChildForm = childForm;
                childForm.BringToFront();
                childForm.Show();

                // Cập nhật tiêu đề
                if (label1 != null)
                    label1.Text = title;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        
        private void bnt_Tran_Click(object sender, EventArgs e)
        {
            FrmTran data = new FrmTran();
            OpenFormInPanel(data, "Hệ thống tràn");
        }

        private void bnt_TramMN_Click(object sender, EventArgs e)
        {
            FrmMNGoogle mn = new FrmMNGoogle();
            OpenFormInPanel(mn, "Mức Nước");
        }

        private void bnt_TrangChu_Click(object sender, EventArgs e)
        {
          FrmHome H = new FrmHome();
            OpenFormInPanel(H, " GIÁM SÁT CỦA TRÀN HỒ DẦU TIẾNG");
        }

        private void bnt_CanhBao_Click(object sender, EventArgs e)
        {
            FrmCanhBao canhBao = new FrmCanhBao();
            OpenFormInPanel(canhBao, " Thông Tin Cảnh Báo");
        }

        private void bnt_BaoCao_Click(object sender, EventArgs e)
        {
            FrmBaoCao baocao = new FrmBaoCao();
            OpenFormInPanel(baocao, "Báo Cáo");
        }  

        private void bnt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
          
            lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
            //      btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
            timer1.Enabled = true;
        }
        private void Driver_Started(object sender, EventArgs e)
        {// khai báo sự kiện
            // Trạng thái động cơ Trạm 1,2,3
          //  ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Run").ValueChanged += Station1_Run_ValueChanged;//Su kien trạm 1 đang chạy     
        }
        private void bnt_CaiDat_Click(object sender, EventArgs e)
        {
            FrmCaiDat caiDat = new FrmCaiDat();
            OpenFormInPanel(caiDat, " CÀI ĐẶT");
        }

        

        private void bnt_LogIn_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
           
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Đăng nhập thành công, cập nhật lại thông tin người dùng
                lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
            //    btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
            }
        }

        //private void btnOpenRegister_Click(object sender, EventArgs e)
        //{
        //    // Chỉ Admin mới được phép mở form này
        //    if (!PermissionManager.CheckPermissionWithMessage("add_user"))
        //        return;

        //    FrmDangKyUser frm = new FrmDangKyUser();
        //    if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        MessageBox.Show("Tài khoản mới đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void bnt_User_Click(object sender, EventArgs e)
        {
            FrmUserManager U = new FrmUserManager();
                   OpenFormInPanel(U, "Hệ thống quản lý tài khoản");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            //  lblDate.Text = DateTime.Now.ToLongDateString();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //  lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            if (ahdDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                labDriverStatus.BackColor = Color.Green;
                labDriverStatus.Text = "PLC Đang Kết Nối";
            }
            else
            {
                labDriverStatus.BackColor = Color.Red;
                labDriverStatus.Text = "PLC Mất Kết Nối";
            }
        }
    }
}