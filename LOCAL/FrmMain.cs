using Ahd.Core;
using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace RegistrationForm1
{
    // private const string CONNECTION_STRING = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

    //  public static string ConnectionString => ConfigurationHelper.GetConnectionString();
    public partial class FrmMain : Form
    {
        // ✅ Event phải đặt bên trong class

        //public event EventHandler<TagValueChangedEventArgs> S3_Station_RunChanged;
        //public event EventHandler<TagValueChangedEventArgs> S3_Station_StopChanged;
        //public event EventHandler<TagValueChangedEventArgs> S3_Station_AlarmChanged;

        public static DataTranModel CurrentDataTran = null;
        private Form currentChildForm = null;

        private Timer _timer;
        private Timer apiTimer;
        private Timer api_CDDTimer;
        private Timer _refreshTimer;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;
        private static readonly HttpClient client = new HttpClient();
        private const string API_STATIONS_URL = "https://kttv-open.vrain.vn/v1/stations";
        private const string API_STATS_URL = "https://kttv-open.vrain.vn/v1/stations/stats";
        private const string API_KEY = "4c81eccdb524441ba52c390d5b96e233";
        public FrmMain()
        {
            InitializeComponent();
            Load += FrmMain_Load; // Đăng ký sự kiện Load của Form
            InitializeTimer();

        }
        IAhdDriverConnector driver;
        private void InitializeTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000; // 5 giây test, thực tế đặt 5 * 60 * 1000 = 5 phút
            _timer.Tick += async (s, e) => await Timer_Tick();
            _timer.Start();
            // Timer API Bình Nhâm
            //apiTimer = new Timer();
            //apiTimer.Interval = 60000; // mỗi 60 giây
            //apiTimer.Tick += async (s, ev) => await ApiTimer_Tick(s, ev);
            //apiTimer.Start();
            //// Timer API CDD
            //api_CDDTimer = new Timer();
            //api_CDDTimer.Tick += async (s, ev) => await api_CDDTimer_Tick(s, ev); // Gán đúng hàm xử lý
            //api_CDDTimer.Interval = 60000; // 60 giây
            //api_CDDTimer.Start();
            //// Timer để lấy dữ liệu Quan trắc mưa
            //_refreshTimer = new Timer();
            //_refreshTimer.Tick += async (s, e) => await _refreshTimer_Tick(s, e);
            //_refreshTimer.Interval = 10 * 60 * 1000; // 10 phút
            //_refreshTimer.Start();

            //client.DefaultRequestHeaders.Add("x-api-key", API_KEY);


        }

        private async Task _refreshTimer_Tick(object sender, EventArgs e)
        {
            await LoadRainfallStatsData();
        }
        private async Task LoadStationsData()
        {

            lblStationsTitle.Text = "Danh sách Trạm Quan Trắc (Đang tải...)";

            try
            {
                HttpResponseMessage response = await client.GetAsync(API_STATIONS_URL);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = $"API trả về lỗi: {(int)response.StatusCode} {response.ReasonPhrase}.";
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(errorContent))
                    {
                        errorMessage += $"\nChi tiết: {errorContent}";
                    }
                    MessageBox.Show(errorMessage, "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStationsTitle.Text = "Danh sách Trạm Quan Trắc (Lỗi tải)";
                    dgvStations.DataSource = null;
                    return;
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(responseBody);

                if (stations != null && stations.Count > 0)
                {
                    dgvStations.DataSource = stations;
                    lblStationsTitle.Text = $"Danh sách Trạm Quan Trắc ({stations.Count} trạm)";
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu trạm nào từ API.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvStations.DataSource = null;
                    lblStationsTitle.Text = "Danh sách Trạm Quan Trắc (Không có dữ liệu)";
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Lỗi HTTP khi tải danh sách trạm: {e.Message}\nVui lòng kiểm tra kết nối internet hoặc URL API.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStationsTitle.Text = "Danh sách Trạm Quan Trắc (Lỗi tải)";
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Lỗi khi phân tích dữ liệu JSON cho trạm: {e.Message}\nCấu trúc dữ liệu nhận được có thể không khớp.", "Lỗi JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStationsTitle.Text = "Danh sách Trạm Quan Trắc (Lỗi định dạng)";
            }
            catch (Exception e)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn khi tải trạm: {e.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStationsTitle.Text = "Danh sách Trạm Quan Trắc (Lỗi không xác định)";
            }
        }
        private async Task LoadRainfallStatsData()
        {
            // Thiết lập thời gian bắt đầu và kết thúc cho API
            // Bắt đầu từ 10 phút trước thời điểm hiện tại
            DateTime startTime = DateTime.Now.AddMinutes(-10);
            DateTime endTime = DateTime.Now;

            // --- Bắt đầu phần làm mới giao diện ---
            // Cập nhật trạng thái để người dùng biết dữ liệu đang được tải
            lblStatusMessage.Text = "Trạng thái tải dữ liệu: Đang xử lý...";
            lblStatusMessage.ForeColor = System.Drawing.Color.Orange;
            // --- Kết thúc phần làm mới giao diện ---

            string formattedStartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedEndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss");

            string statsUrl = $"{API_STATS_URL}?start_time={Uri.EscapeDataString(formattedStartTime)}&end_time={Uri.EscapeDataString(formattedEndTime)}&format=10m";

            try
            {
                // Gửi yêu cầu API
                HttpResponseMessage response = await client.GetAsync(statsUrl);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = $"API trả về lỗi: {(int)response.StatusCode} {response.ReasonPhrase}.";
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(errorContent))
                    {
                        errorMessage += $"\nChi tiết: {errorContent}";
                    }
                    MessageBox.Show(errorMessage, "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusMessage.Text = "Trạng thái tải dữ liệu: Lỗi tải dữ liệu từ API.";
                    lblStatusMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                RainfallStatsResponse statsResponse = JsonConvert.DeserializeObject<RainfallStatsResponse>(responseBody);

                if (statsResponse?.Data == null || !statsResponse.Data.Any())
                {
                    MessageBox.Show(
                        "Không tìm thấy dữ liệu thống kê lượng mưa nào trong khoảng thời gian đã chọn.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    lblStatusMessage.Text = "Trạng thái tải dữ liệu: Không có dữ liệu từ API.";
                    lblStatusMessage.ForeColor = System.Drawing.Color.Black;
                    return;
                }

                var displayData = new List<object>();
                var latestDataPointsByStationFetched = new Dictionary<string, RealtimeRainfallData>();

                foreach (var measurement in statsResponse.Data)
                {
                    if (measurement.Value != null && measurement.Value.Any())
                    {
                        string stationId = measurement.StationId;
                        foreach (var depthMeas in measurement.Value)
                        {
                            displayData.Add(new
                            {
                                StationId = stationId,
                                Timestamp = depthMeas.TimePoint,
                                Depth = depthMeas.Depth,
                                Unit = measurement.Unit
                            });

                            RealtimeRainfallData currentRealtimeData = new RealtimeRainfallData
                            {
                                StationId = stationId,
                                TimePoint = depthMeas.TimePoint,
                                Depth = depthMeas.Depth,
                                Unit = measurement.Unit,
                                RecordedAt = DateTime.Now
                            };

                            if (!latestDataPointsByStationFetched.ContainsKey(stationId) || currentRealtimeData.TimePoint > latestDataPointsByStationFetched[stationId].TimePoint)
                            {
                                latestDataPointsByStationFetched[stationId] = currentRealtimeData;
                            }
                        }
                    }
                }

                dgvStats.DataSource = displayData;
                lblStatusMessage.Text = $"Trạng thái tải dữ liệu: Đã tải thành công {displayData.Count} bản ghi.";
                lblStatusMessage.ForeColor = System.Drawing.Color.Green;

                List<RealtimeRainfallData> realTimeDataToSave = latestDataPointsByStationFetched.Values.ToList();
                string saveStatusMessage = "Không có dữ liệu tức thời mới nhất để lưu vào SQL.";
                bool realtimeSaveSuccess = false;

                if (realTimeDataToSave.Any())
                {
                    try
                    {
                        await WriteQTM(latestDataPointsByStationFetched);

                        saveStatusMessage = $"Đã lưu {realTimeDataToSave.Count} bản ghi tức thời mới nhất vào SQL.";
                        realtimeSaveSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        saveStatusMessage = $"Lỗi lưu tức thời vào SQL: {ex.Message}.";
                    }
                }

                if (realtimeSaveSuccess)
                {
                    lblStatusMessage.Text = $"Trạng thái: Đã tải dữ liệu. {saveStatusMessage}";
                    lblStatusMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatusMessage.Text = $"Trạng thái: Tải dữ liệu thành công nhưng có lỗi khi lưu SQL: {saveStatusMessage}";
                    lblStatusMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Lỗi HTTP: {e.Message}\nVui lòng kiểm tra kết nối internet hoặc URL API.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatusMessage.Text = $"Trạng thái: Lỗi HTTP khi tải dữ liệu: {e.Message}";
                lblStatusMessage.ForeColor = System.Drawing.Color.Red;
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Lỗi khi phân tích dữ liệu JSON: {e.Message}\nCấu trúc dữ liệu nhận được có thể không khớp.", "Lỗi JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatusMessage.Text = $"Trạng thái: Lỗi JSON khi phân tích dữ liệu: {e.Message}";
                lblStatusMessage.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn: {e.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatusMessage.Text = $"Trạng thái: Lỗi không xác định khi tải dữ liệu: {e.Message}";
                lblStatusMessage.ForeColor = System.Drawing.Color.Red;
            }
        }



        // Khu vực tạo Class Quan trắc mưa 
        // Định nghĩa lớp Station để ánh xạ dữ liệu JSON từ API /v1/station
        public class Station
        {
            [JsonProperty("uuid")]
            public string Uuid { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("number")]
            public string Number { get; set; }

            [JsonProperty("latitude")]
            public double Latitude { get; set; }

            [JsonProperty("longitude")]
            public double Longitude { get; set; }

            [JsonProperty("area")]
            public string Area { get; set; }

            [JsonProperty("district")]
            public string District { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("altitude")]
            public object Altitude { get; set; }

            [JsonProperty("waterStationType")]
            public object WaterStationType { get; set; }
        }
        // Lớp mới để ánh xạ mỗi đối tượng bên trong mảng "value"
        public class DepthMeasurement
        {
            [JsonProperty("depth")]
            public double Depth { get; set; }

            [JsonProperty("time_point")]
            public DateTime TimePoint { get; set; }
        }
        // Định nghĩa lớp RainfallMeasurement để ánh xạ dữ liệu JSON cho mỗi bản ghi thống kê
        public class RainfallMeasurement
        {
            [JsonProperty("station_id")]
            public string StationId { get; set; }

            [JsonProperty("timestamp")]
            public DateTime? Timestamp { get; set; }

            [JsonProperty("value")]
            public List<DepthMeasurement> Value { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }
        }
        // Lớp mới để ánh xạ toàn bộ phản hồi từ API /v1/stations/stats
        public class RainfallStatsResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("data")]
            public List<RainfallMeasurement> Data { get; set; }
        }

        // Lớp đại diện cho dữ liệu tức thời để lưu vào DB
        public class RealtimeRainfallData
        {
            public string StationId { get; set; }
            public DateTime TimePoint { get; set; } // Thời điểm đo
            public double Depth { get; set; }
            public string Unit { get; set; }
            public DateTime RecordedAt { get; set; } // Thời gian ứng dụng ghi nhận bản ghi này
        }
        // SingleOrArrayConverter không còn được sử dụng trực tiếp trong RainfallMeasurement.Value
        // nhưng được giữ lại nếu cần cho các trường hợp khác trong tương lai.
        public class SingleOrArrayConverter<T> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(List<T>));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<T>>();
                }
                else if (token.Type == JTokenType.Null)
                {
                    return null;
                }
                else
                {
                    return new List<T> { token.ToObject<T>() };
                }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }




        // Kết thúc khu vực tạo Class Quan trắc mưa
        public class SoLieuAPICDDModel
        {
            public string ThoiGian { get; set; }
            public int MaQuanTrac { get; set; }
            public double GiaTri { get; set; }
        }
        public class SoLieuAPIBinhNhamModel
        {
            public long ts { get; set; }
            public long c { get; set; }
            public double water_proof_1 { get; set; }
            public double water_proof_2 { get; set; }

            public DateTime Timestamp => DateTimeOffset.FromUnixTimeSeconds(ts).ToLocalTime().DateTime;
            public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(c).ToLocalTime().DateTime;
        }


        private async Task api_CDDTimer_Tick(object sender, EventArgs ev)
        {
            string url = "https://apiv2.thuyloivietnam.vn/Api/getSoLieuQuanTrac?Key=apiktdlqtDauTieng&MaQuanTrac=7001";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        string json = await response.Content.ReadAsStringAsync();
                        var dataList = JsonConvert.DeserializeObject<List<SoLieuAPICDDModel>>(json);
                        if (dataList != null && dataList.Count > 0)
                        {
                            var latest = dataList.OrderByDescending(x => x.ThoiGian).First();

                            // Ghi async xuống PLC
                            await WriteAPI_CDDsync(latest.GiaTri);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi đọc API: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gọi API:\n" + ex.Message);
            }
        }
        private async Task WriteAPI_CDDsync(double GT)
        {
            try
            {
                if (ahdDriverConnector1 == null)
                {
                    MessageBox.Show("Kết nối PLC chưa được khởi tạo.");
                    return;
                }

                await ahdDriverConnector1.WriteTagAsync(
                    $"Local Station/DauTieng/S71500/API/Fllow_TL_CDD",
                    GT.ToString("0.00"),
                    WritePiority.High);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi PLC async: " + ex.Message);
            }
        }

        private async Task ApiTimer_Tick(object sender, EventArgs e)
        {
            string url = "https://input.dulieuthuyloivietnam.vn/latest?device_id=CR300-21411";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    var dataList = JsonConvert.DeserializeObject<List<SoLieuAPIBinhNhamModel>>(json);

                    if (dataList != null && dataList.Count > 0)
                    {
                        var latest = dataList.OrderByDescending(x => x.ts).First();

                        // Ghi async xuống PLC
                        await WriteToPLCAsync(latest.water_proof_1, latest.water_proof_2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi đọc API: " + ex.Message);
                }
            }
        }
        private async Task WriteToPLCAsync(double wp1, double wp2)
        {
            try
            {
                await ahdDriverConnector1.WriteTagAsync(
                    $"Local Station/DauTieng/S71500/API/Fllow_BinhNham",
                    wp1.ToString("0.00"),
                    WritePiority.High);

                await ahdDriverConnector1.WriteTagAsync(
                    $"Local Station/DauTieng/S71500/API/Fllow_BinhNham2",
                    wp2.ToString("0.00"),
                    WritePiority.High);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ghi PLC async: " + ex.Message);
            }
        }
        private async Task WriteQTM(Dictionary<string, RealtimeRainfallData> latestApiData)
        {
            try
            {
                // Xác định các ID trạm (hoặc các phần cuối của tag ID) mà bạn muốn ghi

                string[] stationIdsToProcess = { "610001", "610002", "610003", "610004", "610005", "610006", "610007", "610008", "610009", "610010", "610011", "610012", "610013" };

                // Lặp qua từng StationId mong muốn
                foreach (string stationId in stationIdsToProcess)
                {
                    // Tạo đường dẫn tag hoàn chỉnh
                    string tagPath = $"Local Station/DauTieng/S71500/API/{stationId}";

                    // Kiểm tra xem có dữ liệu mới nhất cho StationId này không
                    if (latestApiData.TryGetValue(stationId, out RealtimeRainfallData data))
                    {
                        // Lấy giá trị 'Depth' (lượng mưa) từ dữ liệu tức thời và định dạng
                        string valueToWrite = data.Depth.ToString("0.00");

                        // Ghi giá trị vào tag tương ứng
                        await ahdDriverConnector1.WriteTagAsync(
                            tagPath,
                            valueToWrite,
                            WritePiority.High
                        );
                    }
                    else
                    {
                        // Xử lý trường hợp không tìm thấy dữ liệu cho một trạm cụ thể
                        // Bạn có thể ghi log, thông báo lỗi, hoặc bỏ qua nếu không cần thiết
                        Console.WriteLine($"Cảnh báo: Không tìm thấy dữ liệu tức thời cho trạm '{stationId}' để ghi.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi nếu có bất kỳ ngoại lệ nào xảy ra trong quá trình ghi PLC
                Console.WriteLine($"Lỗi khi ghi giá trị tức thời vào PLC: {ex.Message}");
            }
        }
        public async Task Timer_Tick()
        {
            try
            {
                _timer.Enabled = false;

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    foreach (var item in Globalvariable.RealtimeDisplays)
                    {
                        if (item.Path == "Local Station/DauTieng/S71500/Group1")
                        {
                            _labALDoor1_Station1.Text = item.Al_Door1.ToString();
                            _labCalcular1.Text = item.Calculate.ToString();
                        }
                        else if (item.Path == "Local Station/DauTieng/S71500/Group2")
                        {
                            _labALDoor1_Station2.Text = item.Al_Door1.ToString();
                        }
                        else// (item.Path == "Local Station/DauTieng/S71500/Group1")
                        {
                            _labALDoor1_Station3.Text = item.Al_Door1.ToString();
                        }
                    }
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _timer.Enabled = true;
            }
        }
        private Dictionary<string, string> ParseMultipleStationsFromAPI(string apiData, List<string> stationCodes)
        {
            var results = new Dictionary<string, string>();

            // ✅ Update regex để parse số âm
            var regex = new System.Text.RegularExpressions.Regex(@"(F\d{5});\d{2}/\d{2}/\d{4};\d{2}:\d{2};value=(-?\d+);");

            var matches = regex.Matches(apiData);
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (match.Success)
                {
                    string code = match.Groups[1].Value;
                    string value = match.Groups[2].Value;

                    if (stationCodes.Contains(code))
                    {
                        results[code] = value;
                    }
                }
            }

            return results;
        }

        // Hàm ghi log xuống TextBox
        private void AppendLog(string message)
        {
            //if (txtLog.InvokeRequired)
            //    txtLog.Invoke(new Action(() => txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}")));
            //else
            //    txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
        }
        private async void FrmMain_Load(object sender, EventArgs e)
        {
            PermissionManager.ApplyPermission(bntNhaplieu, "edit_data");// test nút nhấn nhập liệu
            SQLLogin.InitCurrentDataTran();
            lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
            //      btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                // Change this line in FrmMain_Load:
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);

            timer1.Enabled = true;
            tm_login.Interval = 60000;
            tm_login.Enabled = true;
            tm_login.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { tm_login.Start(); });
                t.Enabled = true;
            };
            tm_loginMN.Interval = 60000;
            tm_loginMN.Enabled = true;
            tm_loginMN.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { tm_loginMN.Start(); });
                t.Enabled = true;
            };

            //await LoadRainfallStatsData();
            await LoadStationsData();



        }
        // Hàm Lấy giá trị cho Timer ghi xuống SQL
        private string GetValue(string tagName)
        {
            try
            {
                var tag = ahdDriverConnector1.GetTag(tagName);
                if (tag != null)
                {
                    return tag.Value?.ToString() ?? "0";
                }
                return "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi đọc tag {tagName}: {ex.Message}");
                return "0";
            }
        }


        private void Driver_Started(object sender, EventArgs e)
        {
            foreach (var item in Globalvariable.LocationsInfo)
            {
                foreach (var station in item.Stations)
                {
                    // Replace this line:
                    ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").ValueChanged += Al_Door1_ValueChanged;

                    Al_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").Value));
                }
            }
        }

        //private string prevS1_Station_Stop = "0", prevS2_Station_Stop = "0", prevS3_Station_Stop = "0"; // biến lưu trạng thái trước đó
        //private string prevS3_Station_Alarm = "0", prevS2_Station_Alarm = "0", prevS1_Station_Alarm = "0"; // biến lưu trạng thái trước đó


        private void Al_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                var itemChange = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.Path == path);

                if (itemChange != null)
                {
                    var oldValue = itemChange.Al_Door1;

                    //Debug.WriteLine($"{path}/Tempperature: {e.NewValue}");
                    itemChange.Al_Door1 = e.NewValue == "1" ? true : false;

                    //tinh toans
                    itemChange.Calculate = itemChange.Calculate + 1;
                    ahdDriverConnector1.GetTag($"{path}/Fllow_Door1").WritAhdnc(itemChange.Calculate.ToString(), WritePiority.High);


                    if (oldValue != itemChange.Al_Door1)
                    {
                        using (var dbContext = new ApplicationDbContext())
                        {
                            //Real time
                            var check = dbContext.FT02s.FirstOrDefault();

                            if (check != null)
                            {
                                check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                                dbContext.SaveChanges();
                            }
                            else
                            {
                                var newLine = new FT02
                                {
                                    Id = Guid.NewGuid(),
                                    C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                    IsDeleted = false,
                                    CreateAt = DateTime.Now,
                                    CreateOperatorId = "System",
                                };

                                dbContext.FT02s.Add(newLine);
                                dbContext.SaveChanges();
                            }

                            //datalog
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}