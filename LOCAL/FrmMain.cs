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
       public partial class FrmMain : Form
    {
       
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
            apiTimer = new Timer();
            apiTimer.Interval = 60000; // mỗi 60 giây
            apiTimer.Tick += async (s, ev) => await ApiTimer_Tick(s, ev);
            apiTimer.Start();
            // Timer API CDD
            api_CDDTimer = new Timer();
            api_CDDTimer.Tick += async (s, ev) => await api_CDDTimer_Tick(s, ev); // Gán đúng hàm xử lý
            api_CDDTimer.Interval = 60000; // 60 giây
            api_CDDTimer.Start();
            // Timer để lấy dữ liệu Quan trắc mưa
            _refreshTimer = new Timer();
            _refreshTimer.Tick += async (s, e) => await _refreshTimer_Tick(s, e);
            _refreshTimer.Interval = 10 * 60 * 1000; // 10 phút
            _refreshTimer.Start();

            client.DefaultRequestHeaders.Add("x-api-key", API_KEY);


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
            string apiUrl = "http://dautiengphuochoa.com/api/getmn.aspx?key=dauhoaphuongtien%3b";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiData = await client.GetStringAsync(apiUrl);

                    // Danh sách các station cần parse
                    List<string> stations = new List<string> { "F01877", "F01203", "F01849" };

                    // Gọi hàm parse
                    var stationValues = ParseMultipleStationsFromAPI(apiData, stations);

                    // Hàm trợ giúp để chuyển đổi và xử lý giá trị decimal
                    Func<string, decimal> processFlowValue = (valueString) =>
                    {
                        decimal parsedValue;
                        if (decimal.TryParse(valueString, out parsedValue))
                        {
                            // Làm tròn đến 2 chữ số thập phân và sau đó chia cho 100
                            return Math.Round(parsedValue, 2) / 100m;
                        }
                        else
                        {
                            // Xử lý trường hợp không thể chuyển đổi, ví dụ: log lỗi và trả về 0
                            AppendLog($"Cảnh báo: Không thể chuyển đổi giá trị '{valueString}' từ API sang Decimal. Sử dụng giá trị 0.");
                            return 0m;
                        }
                    };

                    // Ghi từng trạm xuống PLC
                    foreach (var entry in stationValues)
                    {
                        string stationCode = entry.Key;
                        string rawValue = entry.Value; // Giá trị thô từ API

                        string tagName = "";
                        decimal processedDecimalValue = 0m; // Biến để lưu giá trị đã xử lý

                        switch (stationCode)
                        {
                            case "F01877": // Fllow_SonDai
                                tagName = "Fllow_SonDai";
                                processedDecimalValue = processFlowValue(rawValue);
                                break;
                            case "F01203": // Fllow_BenSuc
                                tagName = "Fllow_BenSuc";
                                processedDecimalValue = processFlowValue(rawValue);
                                break;
                            case "F01849": // Fllow_DauTieng
                                tagName = "Fllow_DauTieng";
                                processedDecimalValue = processFlowValue(rawValue);
                                break;
                            // Thêm các case khác nếu có các stationCode khác cần xử lý
                            default:
                                AppendLog($"Thông tin: Station Code '{stationCode}' không được mapping, bỏ qua xử lý.");
                                continue; // Bỏ qua entry này nếu không có mapping
                        }

                        if (!string.IsNullOrEmpty(tagName))
                        {
                            // Ghi xuống PLC. Convert decimal thành string lại vì WriteTagAsync có lẽ mong đợi string.
                            await ahdDriverConnector1.WriteTagAsync(
                                $"Local Station/DauTieng/S71500/API/{tagName}",
                                processedDecimalValue.ToString(), // Chuyển đổi decimal đã xử lý thành string
                                WritePiority.High
                            );

                            AppendLog($"✅ Ghi PLC: {tagName} = {processedDecimalValue} (Từ API: {rawValue})");
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Bắt lỗi cụ thể khi có vấn đề về HTTP (ví dụ: không kết nối được API)
                AppendLog($"❌ Lỗi đọc API: An error occurred while sending the request. {ex.Message}");
            }
            catch (Exception ex)
            {
                AppendLog($"❌ Lỗi Timer_Tick: {ex.Message}");
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

            await LoadRainfallStatsData();
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
                  //  // Replace this line:
                  //  ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").ValueChanged += Al_Door1_ValueChanged;
                  //  Al_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  //, new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  //, "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Remote").ValueChanged += Remote_ValueChanged;
                    Remote_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Remote")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Remote")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Remote").Value));



                }
            }

        }



        private void Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                var itemChange = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.Path == path);

                if (itemChange != null)
                {
                    var oldValue = itemChange.Remote;

                    //Debug.WriteLine($"{path}/Tempperature: {e.NewValue}");
                    itemChange.Remote = e.NewValue == "1" ? true : false;

                    //tinh toans
                    itemChange.Calculate = itemChange.Calculate + 1;
                    ahdDriverConnector1.GetTag($"{path}/Remote").WritAhdnc(itemChange.Calculate.ToString(), WritePiority.High);


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
        //private void S3_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    S3_DC3_OverChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevS3_DC3_Overload == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataAlarmModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 3",
        //            TagName = "Quá Tải Động Cơ 3", // ✅ Tên tag
        //            S3_DC3_Over = e.NewValue,
        //        };
        //        SQLLoginAlarm.InsertAlarm(model);
        //    }
        //    prevS3_DC3_Overload = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock1_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock1_1OpenChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock1_1Open == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 1_1 Mở", // ✅ Tên tag
        //            Doorlock1_1Open = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock1_1Open = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock1_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock1_1CloseChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock1_1Close == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 1_1 Đóng", // ✅ Tên tag
        //            Doorlock1_1Close = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock1_1Close = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock1_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock1_2OpenChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock1_2Open == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 1_2 Mở", // ✅ Tên tag
        //            Doorlock1_2Open = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock1_2Open = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock1_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock1_2CloseChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock1_2Close == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 1_2 Đóng", // ✅ Tên tag
        //            Doorlock1_2Close = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock1_2Close = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock2_1OpenChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock2_1Open == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 2_1 Mở", // ✅ Tên tag
        //            Doorlock2_1Open = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock2_1Open = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock2_1CloseChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock2_1Close == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 2_1 Đóng", // ✅ Tên tag
        //            Doorlock2_1Close = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock2_1Close = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock2_2OpenChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock2_2Open == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 2_2 Mở", // ✅ Tên tag
        //            Doorlock2_2Open = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock2_2Open = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock2_2CloseChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock2_2Close == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 1",
        //            TagName = "Chốt Cửa 2_2 Đóng", // ✅ Tên tag
        //            Doorlock2_2Close = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock2_2Close = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock3_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock3_1OpenChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock3_1Open == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 2",
        //            TagName = "Chốt Cửa 3_1 Mở", // ✅ Tên tag
        //            Doorlock3_1Open = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock3_1Open = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock3_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock3_1CloseChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock3_1Close == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 2",
        //            TagName = "Chốt Cửa 3_1 Đóng", // ✅ Tên tag
        //            Doorlock3_1Close = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock3_1Close = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock3_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock3_2OpenChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock3_2Open == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = " Trạm 2",
        //            TagName = "Chốt Cửa 3_2 Mở", // ✅ Tên tag
        //            Doorlock3_2Open = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock3_2Open = e.NewValue; // Cập nhật trạng thái trước
        //}
        //private void Doorlock3_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    Doorlock3_2CloseChanged?.Invoke(this, e);
        //    // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
        //    if (prevDoorlock3_2Close == "0" && e.NewValue == "1")
        //    {
        //        // Tạo object DataTranModel mới
        //        var model = new DataTranModel
        //        {
        //            CreateAt = DateTime.Now,
        //            Position = "Trạm 2",
        //            TagName = "Chốt Cửa 3_2 Đóng", // ✅ Tên tag
        //            Doorlock3_2Close = e.NewValue,
        //        };
        //        SQLLoginDataTran.InsertDataTran(model);
        //    }
        //    prevDoorlock3_2Close = e.NewValue; // Cập nhật trạng thái trước
        //}





























        #region GetCurrentValue
        public string GetAl_Door1Value()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Al_Door1").Value;
        }
        public string GetAl_Door2Value()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Al_Door2").Value;
        }
        public string GetAl_Door3Value()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Al_Door3").Value;
        }
        public string GetAl_Door4Value()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Al_Door4").Value;
        }
        public string GetAl_Door5Value()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Al_Door5").Value;
        }
        public string GetAl_Door6Value()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Al_Door6").Value;
        }
        //public string GetS1_Station_AlarmValue()
        //{
        //    return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Alarm").Value;
        //}
        //public string GetS2_Station_AlarmValue()
        //{
        //    return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Alarm").Value;
        //}
        //public string GetS3_Station_AlarmValue()
        //{
        //    return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Alarm").Value;
        //}
        public string GetDoor1_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureHigh").Value;
        }
        public string GetDoor1_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureLow").Value;
        }
        public string GetDoor2_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureHigh").Value;
        }
        public string GetDoor2_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureLow").Value;
        }
        public string GetDoor3_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureHigh").Value;
        }
        public string GetDoor3_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureLow").Value;
        }
        public string GetDoor4_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureHigh").Value;
        }
        public string GetDoor4_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureLow").Value;
        }
        public string GetDoor5_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureHigh").Value;
        }
        public string GetDoor5_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureLow").Value;
        }
        public string GetDoor6_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureHigh").Value;
        }
        public string GetDoor6_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureLow").Value;
        }
        public string GetS1_DC1_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Over").Value;
        }
        public string GetS1_DC2_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Over").Value;
        }
        public string GetS1_DC3_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Over").Value;
        }
        public string GetS2_DC1_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Over").Value;
        }
        public string GetS2_DC2_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Over").Value;
        }
        public string GetS2_DC3_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Over").Value;
        }
        public string GetS3_DC1_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Over").Value;
        }
        public string GetS3_DC2_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Over").Value;
        }
        public string GetS3_DC3_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Over").Value;
        }

        public string GetDoorlock1_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Open").Value;
        }
        public string GetDoorlock1_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Close").Value;
        }
        public string GetDoorlock1_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Open").Value;
        }
        public string GetDoorlock1_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Close").Value;
        }
        public string GetDoorlock2_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Open").Value;
        }
        public string GetDoorlock2_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Close").Value;
        }
        public string GetDoorlock2_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Open").Value;
        }
        public string GetDoorlock2_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Close").Value;
        }
        public string GetDoorlock3_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Open").Value;
        }
        public string GetDoorlock3_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Close").Value;
        }
        public string GetDoorlock3_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Open").Value;
        }
        public string GetDoorlock3_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Close").Value;
        }
        public string GetDoorlock4_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Open").Value;
        }
        public string GetDoorlock4_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Close").Value;
        }
        public string GetDoorlock4_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Open").Value;
        }
        public string GetDoorlock4_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Close").Value;
        }
        public string GetDoorlock5_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Open").Value;
        }
        public string GetDoorlock5_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Close").Value;
        }
        public string GetDoorlock5_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Open").Value;
        }
        public string GetDoorlock5_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Close").Value;
        }
        public string GetDoorlock6_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Open").Value;
        }
        public string GetDoorlock6_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Close").Value;
        }
        public string GetDoorlock6_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Open").Value;
        }
        public string GetDoorlock6_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Close").Value;
        }
        public string GetDoor1_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Open").Value;
        }
        public string GetDoor1_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Close").Value;
        }
        public string GetDoor2_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Open").Value;
        }
        public string GetDoor2_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Close").Value;
        }
        public string GetDoor3_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Open").Value;
        }
        public string GetDoor3_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Close").Value;
        }
        public string GetDoor4_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Open").Value;
        }

        private void tm_loginMN_Tick(object sender, EventArgs e)
        {
            try
            {
                var data = new DataMucNuocModel
                {
                    CreateAt = DateTime.Now,

                    Fllow_Ho = GetValue("Local Station/DauTieng/S71500/Group1/Fllow_Ho"),
                    Fllow_DauTieng = GetValue("Local Station/DauTieng/S71500/API/Fllow_DauTieng"),
                    Fllow_BenSuc = GetValue("Local Station/DauTieng/S71500/API/Fllow_BenSuc"),
                    Fllow_SonDai = GetValue("Local Station/DauTieng/S71500/API/Fllow_SonDai"),
                    Fllow_BinhNham = GetValue("Local Station/DauTieng/S71500/API/Fllow_BinhNham"),
                    Fllow_TL_CDD = GetValue("Local Station/DauTieng/S71500/API/Fllow_TL_CDD"),



                };

                SQLLoginMucNuoc.InsertDataMucNuoc(data);
                Console.WriteLine($"✅ Đã ghi DataMucNuoc lúc {data.CreateAt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi ghi DataMucNuoc: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHochua mn = new FrmHochua();
            OpenFormInPanel(mn, "Hồ chứa");
            mn.UrlToLoad = "https://vrain.vn/61/overview?public_map=windy";

            mn.Show();

            //FrmHochua mn = new FrmHochua();
            //OpenFormInPanel(mn, "Hồ chứa");
            ////   FrmHochua   frm = new FrmHochua();
            //  mn.UrlToLoad = "https://simc.id.vn/simc_esp/zdautieng.html";
            //mn.Show();




        }

        public string GetDoor4_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Close").Value;
        }



        public string GetDoor5_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Open").Value;
        }
        public string GetDoor5_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Close").Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadRainfallStatsData();
        }

        public string GetDoor6_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Open").Value;
        }
        public string GetDoor6_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Close").Value;
        }

        public string GetDoorlock1_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Opening").Value;
        }

        private void bntNhaplieu_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa đăng nhập
            if (string.IsNullOrEmpty(PermissionManager.CurrentUsername))
            {
                // Hiện form đăng nhập
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Bạn cần đăng nhập để sử dụng chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Sau khi đăng nhập xong, kiểm tra quyền
            if (!PermissionManager.CheckPermissionWithMessage("edit_data"))
                return;

            // Nếu đủ quyền, mở form nhập liệu
            FrmNhaplieu frm = new FrmNhaplieu();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmEditdata frm = new FrmEditdata();
            OpenFormInPanel(frm, "Chỉnh sửa dữ liệu");
        }

        public string GetDoorlock1_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Closing").Value;
        }
        public string GetDoorlock2_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Opening").Value;
        }
        public string GetDoorlock2_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Closing").Value;
        }
        public string GetDoorlock3_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Opening").Value;
        }
        public string GetDoorlock3_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Closing").Value;
        }

        private void tm_login_Tick(object sender, EventArgs e)
        {
            try
            {
                // Hàm trợ giúp để chuyển đổi và xử lý giá trị
                Func<string, decimal> processFlowValue = (valueString) =>
                {
                    decimal parsedValue;
                    if (decimal.TryParse(valueString, out parsedValue))
                    {
                        return Math.Round(parsedValue, 2) / 100m;
                    }
                    else
                    {
                        // Xử lý trường hợp không thể chuyển đổi, ví dụ: log lỗi và trả về 0
                        Console.Error.WriteLine($"Cảnh báo: Không thể chuyển đổi giá trị '{valueString}' sang Decimal. Sử dụng giá trị 0.");
                        return 0m;
                    }
                };

                var data = new DataVanHanhModel
                {
                    CreateAt = DateTime.Now,

                    HT_Cylinder1_1 = GetValue("Local Station/DauTieng/S71500/Group1/HT_Cylinder1_1"),
                    HT_Cylinder1_2 = GetValue("Local Station/DauTieng/S71500/Group1/HT_Cylinder1_2"),
                    HT_Cylinder2_1 = GetValue("Local Station/DauTieng/S71500/Group1/HT_Cylinder2_1"),
                    HT_Cylinder2_2 = GetValue("Local Station/DauTieng/S71500/Group1/HT_Cylinder2_2"),
                    HT_Cylinder3_1 = GetValue("Local Station/DauTieng/S71500/Group2/HT_Cylinder3_1"),
                    HT_Cylinder3_2 = GetValue("Local Station/DauTieng/S71500/Group2/HT_Cylinder3_2"),
                    HT_Cylinder4_1 = GetValue("Local Station/DauTieng/S71500/Group2/HT_Cylinder4_1"),
                    HT_Cylinder4_2 = GetValue("Local Station/DauTieng/S71500/Group2/HT_Cylinder4_2"),
                    HT_Cylinder5_1 = GetValue("Local Station/DauTieng/S71500/Group3/HT_Cylinder5_1"),
                    HT_Cylinder5_2 = GetValue("Local Station/DauTieng/S71500/Group3/HT_Cylinder5_2"),
                    HT_Cylinder6_1 = GetValue("Local Station/DauTieng/S71500/Group3/HT_Cylinder6_1"),
                    HT_Cylinder6_2 = GetValue("Local Station/DauTieng/S71500/Group3/HT_Cylinder6_2"),

                    Door1_Aperture = GetValue("Local Station/DauTieng/S71500/Group1/Door1_Aperture"),
                    Door2_Aperture = GetValue("Local Station/DauTieng/S71500/Group1/Door2_Aperture"),
                    Door3_Aperture = GetValue("Local Station/DauTieng/S71500/Group2/Door3_Aperture"),
                    Door4_Aperture = GetValue("Local Station/DauTieng/S71500/Group2/Door4_Aperture"),
                    Door5_Aperture = GetValue("Local Station/DauTieng/S71500/Group3/Door5_Aperture"),
                    Door6_Aperture = GetValue("Local Station/DauTieng/S71500/Group3/Door6_Aperture"),

                    Temp_Oil1 = GetValue("Local Station/DauTieng/S71500/Group1/Temp_Oil1"),
                    Temp_Oil2 = GetValue("Local Station/DauTieng/S71500/Group2/Temp_Oil2"),
                    Temp_Oil3 = GetValue("Local Station/DauTieng/S71500/Group3/Temp_Oil3"),

                    Fllow_Door1 = GetValue("Local Station/DauTieng/S71500/Group1/Fllow_Door1"),
                    Fllow_Door2 = GetValue("Local Station/DauTieng/S71500/Group1/Fllow_Door2"),
                    Fllow_Door3 = GetValue("Local Station/DauTieng/S71500/Group2/Fllow_Door3"),
                    Fllow_Door4 = GetValue("Local Station/DauTieng/S71500/Group2/Fllow_Door4"),
                    Fllow_Door5 = GetValue("Local Station/DauTieng/S71500/Group3/Fllow_Door5"),
                    Fllow_Door6 = GetValue("Local Station/DauTieng/S71500/Group3/Fllow_Door6"),

                    Total_Fllow = GetValue("Local Station/DauTieng/S71500/Group1/Total_Fllow"),
                    Fllow_Ho = GetValue("Local Station/DauTieng/S71500/Group4/Fllow_Ho"),
                    // Áp dụng hàm trợ giúp cho các trường cần xử lý
                    Fllow_DauTieng = processFlowValue(GetValue("Local Station/DauTieng/S71500/API/Fllow_DauTieng")),
                    Fllow_BenSuc = processFlowValue(GetValue("Local Station/DauTieng/S71500/API/Fllow_BenSuc")),
                    Fllow_SonDai = processFlowValue(GetValue("Local Station/DauTieng/S71500/API/Fllow_SonDai")),
                    Fllow_BinhNham = GetValue("Local Station/DauTieng/S71500/API/Fllow_BinhNham"),
                    Fllow_TL_CDD = GetValue("Local Station/DauTieng/S71500/API/Fllow_TL_CDD")
                };

                SQLLoginDataVanHanh.InsertDataVanHanh(data);
                Console.WriteLine($"✅ Đã ghi DataVanHanh lúc {data.CreateAt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi ghi DataVanHanh: {ex.Message}");
            }
        }



        public string GetDoorlock4_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Opening").Value;
        }
        public string GetDoorlock4_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Closing").Value;
        }
        public string GetDoorlock5_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Opening").Value;
        }
        public string GetDoorlock5_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Closing").Value;
        }
        public string GetDoorlock6_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Opening").Value;
        }
        public string GetDoorlock6_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Closing").Value;
        }
        public string GetDoor1_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Opening").Value;
        }
        public string GetDoor1_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Closing").Value;
        }
        public string GetDoor2_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Opening").Value;
        }
        public string GetDoor2_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Closing").Value;
        }
        public string GetDoor3_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Opening").Value;
        }
        public string GetDoor3_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Closing").Value;
        }
        public string GetDoor4_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Opening").Value;
        }
        public string GetDoor4_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Closing").Value;
        }
        public string GetDoor5_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Opening").Value;
        }
        public string GetDoor5_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Closing").Value;
        }
        public string GetDoor6_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Opening").Value;
        }
        public string GetDoor6_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Closing").Value;
        }

        public string GetS3_DC3_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Running").Value;
        }
        public string GetS3_DC2_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Running").Value;
        }
        public string GetS3_DC1_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Running").Value;
        }
        public string GetS2_DC3_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Running").Value;
        }
        public string GetS2_DC2_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Running").Value;
        }
        public string GetS2_DC1_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Running").Value;
        }
        public string GetS1_DC3_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Running").Value;
        }
        public string GetS1_DC2_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Running").Value;
        }
        public string GetS1_DC1_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Running").Value;
        }
        public string GetS1RemoteValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Remote").Value;
        }
        public string GetS1LocalValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local").Value;
        }
        public string GetS1AutoValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Auto").Value;
        }
        public string GetS1ManValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Man").Value;
        }
        public string GetS1LocalStopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local_Stop").Value;
        }
        public string GetS2RemoteValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Remote").Value;
        }
        public string GetS2LocalValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local").Value;
        }
        public string GetS2AutoValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Auto").Value;
        }
        public string GetS2ManValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Man").Value;
        }
        public string GetS2LocalStopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local_Stop").Value;
        }
        public string GetS3RemoteValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Remote").Value;
        }
        public string GetS3LocalValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local").Value;
        }
        public string GetS3AutoValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Auto").Value;
        }
        public string GetS3ManValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Man").Value;
        }
        public string GetS3LocalStopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local_Stop").Value;
        }
        #endregion

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmLogin.currentLoginLogId > 0)
            {
                FrmLogin.SaveLogoutTime(FrmLogin.currentLoginLogId);
            }
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
            FrmTran data = new FrmTran(this);
            OpenFormInPanel(data, "Hệ thống tràn");
        }

        private void bnt_TramMN_Click(object sender, EventArgs e)
        {
            FrmMucnuoc mn = new FrmMucnuoc();
            OpenFormInPanel(mn, "Mức Nước");
        }

        private void bnt_TrangChu_Click(object sender, EventArgs e)
        {
            FrmHome H = new FrmHome(this);
            OpenFormInPanel(H, " GIÁM SÁT CỦA TRÀN HỒ DẦU TIẾNG");
        }

        private void bnt_CanhBao_Click(object sender, EventArgs e)
        {
            FrmCanhBao canhBao = new FrmCanhBao(this);
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
        private void bnt_CaiDat_Click(object sender, EventArgs e)
        {
            FrmCaiDat caiDat = new FrmCaiDat();
            OpenFormInPanel(caiDat, " CÀI ĐẶT");
        }
        private void bnt_LogIn_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu đã đăng nhập thì không cần đăng nhập lại
            //if (!string.IsNullOrEmpty(PermissionManager.CurrentUsername))
            //{
            //    MessageBox.Show("Bạn đã đăng nhập rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            this.Hide();
            // Hiện form đăng nhập
            FrmLogin login = new FrmLogin();

            if (login.ShowDialog() == DialogResult.OK)
            {
                // Đăng nhập thành công, cập nhật lại thông tin người dùng
                lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
                //   btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
                bntEditdata.Enabled = PermissionManager.CurrentUserRole == "Admin";
            }

        }

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