using Ahd.Core;
using DocumentFormat.OpenXml.Drawing;
using Domain;
using Domain.Entities;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RegistrationForm1
{
    public partial class FrmMain : Form
    {
        private Form currentChildForm = null;
        private Timer _timer; //. Timer để lấy dữ liệu từ Scada
        private Timer api_DTtimer; //. Timer lấy dữ liễu API dầu tiếng
        private Timer apiTimer;
        private Timer api_CDDTimer;
        private Timer _refreshTimer;

        private List<Station> _cachedStations; // Lưu trữ danh sách trạm đã tải
        private Dictionary<string, string> _stationIdToNameMap; // Ánh xạ StationId (uuid HOẶC code) sang Name

        private static readonly HttpClient client = new HttpClient();
        private const string API_STATIONS_URL = "https://kttv-open.vrain.vn/v1/stations";
        private const string API_STATS_URL = "https://kttv-open.vrain.vn/v1/stations/stats";
        private const string API_KEY = "4c81eccdb524441ba52c390d5b96e233";
        private int _logTime;
        private DateTime _startTime = DateTime.Now;
        private const string CONNECTION_STRING = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public FrmMain()
        {
            InitializeComponent();
            Load += FrmMain_Load; // Đăng ký sự kiện Load của Form
            InitializeTimer();
            _stationIdToNameMap = new Dictionary<string, string>(); // Khởi tạo map

            dtpStartTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm:ss",
                Value = DateTime.Now.Date.AddHours(-1),
                Width = 180
            };
            dtpEndTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm:ss",
                Value = DateTime.Now,
                Width = 180
            };

        }
        IAhdDriverConnector driver;
        public interface ICalculatableForm
        {
            void PerformCalculations();
        }
        private async Task _refreshTimer_Tick(object sender, EventArgs e)
        {
            dtpEndTime.Value = DateTime.Now;
            // Thời gian bắt đầu là 10 phút trước thời gian hiện tại
            dtpStartTime.Value = DateTime.Now.AddMinutes(-10);
            // Tải lại dữ liệu thống kê mưa
            await LoadRainfallStatsData();
        }
        private async void FrmMain_Load(object sender, EventArgs e)
        {

            lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();

            if (!driver.IsStarted)
                // Change this line in FrmMain_Load:
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
            _startTime = DateTime.Now;

            timer1.Enabled = true;
            _refreshTimer.Start();
            await LoadRainfallStatsData();
            await LoadStationsData();



        }
        private void InitializeTimer()
        { // Timer API dầu tiếng
            api_DTtimer = new Timer();
            api_DTtimer.Interval = 60000; // mỗi 60 giây
            api_DTtimer.Tick += async (s, ev) => await Api_DTtimer_Tick();
            api_DTtimer.Start();


            // Timer Login dữ liệu Scada
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
        public class SoLieuAPICDDModel
        {
            public string ThoiGian { get; set; }
            public int MaQuanTrac { get; set; }
            public double GiaTri { get; set; }
        }


        private void Driver_Started(object sender, EventArgs e)
        {
            //if (ahdDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            //{
            //    labDriverStatus.BackColor = Color.Green;
            //}
            //else
            //{
            //    labDriverStatus.BackColor = Color.Red;
            //}

            foreach (var item in Globalvariable.LocationsInfo)
            {
                foreach (var station in item.Stations.Where(x => x.Path.Contains("/Station_")))
                {
                 
                    ahdDriverConnector1.GetTag($"{station.Path}/Remote").ValueChanged += Remote_ValueChanged;
                    Remote_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Remote")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Remote")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Remote").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Local").ValueChanged += Local_ValueChanged;
                    Local_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Local")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Local")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Local").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Auto").ValueChanged += Auto_ValueChanged;
                    Auto_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Auto")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Auto")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Auto").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Man").ValueChanged += Man_ValueChanged;
                    Man_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Man")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Man")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Man").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop").ValueChanged += Local_Stop_ValueChanged;
                    Local_Stop_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running").ValueChanged += DC1_Running_ValueChanged;
                    DC1_Running_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running").ValueChanged += DC2_Running_ValueChanged;
                    DC2_Running_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC3_Running").ValueChanged += DC3_Running_ValueChanged;
                    DC3_Running_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC3_Running")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC3_Running").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;
                    Door1_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;
                    Door1_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;
                    Door2_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;
                    Door2_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open").ValueChanged += Door1_Open_ValueChanged;
                    Door1_Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close").ValueChanged += Door1_Close_ValueChanged;
                    Door1_Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open").ValueChanged += Door2_Open_ValueChanged;
                    Door2_Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close").ValueChanged += Door2_Close_ValueChanged;
                    Door2_Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening").ValueChanged += Doorlock1_Opening_ValueChanged;
                    Doorlock1_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing").ValueChanged += Doorlock1_Closing_ValueChanged;
                    Doorlock1_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening").ValueChanged += Doorlock2_Opening_ValueChanged;
                    Doorlock2_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing").ValueChanged += Doorlock2_Closing_ValueChanged;
                    Doorlock2_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open").ValueChanged += Doorlock1_1Open_ValueChanged;
                    Doorlock1_1Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close").ValueChanged += Doorlock1_1Close_ValueChanged;
                    Doorlock1_1Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Open").ValueChanged += Doorlock1_2Open_ValueChanged;
                    Doorlock1_2Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close").ValueChanged += Doorlock1_2Close_ValueChanged;
                    Doorlock1_2Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;
                    Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;
                    Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;
                    Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;
                    Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close").Value));
                    // Alarm
                    ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over").ValueChanged += DC1_Over_ValueChanged;
                    DC1_Over_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over").ValueChanged += DC2_Over_ValueChanged;
                    DC2_Over_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over").ValueChanged += DC3_Over_ValueChanged;
                    DC3_Over_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh").ValueChanged += Door1_PressureHigh_ValueChanged;
                    Door1_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow").ValueChanged += Door1_PressureLow_ValueChanged;
                    Door1_PressureLow_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh").ValueChanged += Door2_PressureHigh_ValueChanged;
                    Door2_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow").ValueChanged += Door2_PressureLow_ValueChanged;
                    Door2_PressureLow_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").ValueChanged += Al_Door1_ValueChanged;
                    Al_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2").ValueChanged += Al_Door2_ValueChanged;
                    Al_Door2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1").ValueChanged += HT_Cylinder1_1_ValueChanged;
                    HT_Cylinder1_1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2").ValueChanged += HT_Cylinder1_2_ValueChanged;
                    HT_Cylinder1_2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1").ValueChanged += HT_Cylinder2_1_ValueChanged;
                    HT_Cylinder2_1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2").ValueChanged += HT_Cylinder2_2_ValueChanged;
                    HT_Cylinder2_2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1").ValueChanged += Pressure_Oil_Door1_ValueChanged;
                    Pressure_Oil_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2").ValueChanged += Pressure_Oil_Door2_ValueChanged;
                    Pressure_Oil_Door2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil").ValueChanged += S1_Temp_Oil_ValueChanged;
                    S1_Temp_Oil_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture").ValueChanged += Door1_Aperture_ValueChanged;
                    Door1_Aperture_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture").ValueChanged += Door2_Aperture_ValueChanged;
                    Door2_Aperture_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture").Value));











                }


                var stationLocation = item.Stations.FirstOrDefault(loc => loc.Path.Contains("/Location_Info"));
                if (stationLocation != null)
                {
                    // Replace this line:
                    ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho").ValueChanged += Fllow_Ho_ValueChanged;
                    Fllow_Ho_ValueChanged(ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho")
                  , "", ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho").Value));
                }

                //  }
            }

        }

        private async Task LoadStationsData()
        {
            _cachedStations = new List<Station>(); // Đảm bảo _cachedStations được khởi tạo
            _stationIdToNameMap.Clear(); // Xóa map cũ trước khi điền dữ liệu mới
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
                    //           MessageBox.Show(errorMessage, "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    dgvStations.DataSource = null;
                    return;
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                _cachedStations = JsonConvert.DeserializeObject<List<Station>>(responseBody); // Lưu vào _cachedStations
                if (_cachedStations != null && _cachedStations.Count > 0)
                {
                    dgvStations.DataSource = _cachedStations; // Gán cho DataGridView          
                    // Điền dữ liệu vào _stationIdToNameMap, sử dụng station.Code làm khóa
                    foreach (var station in _cachedStations)
                    {
                        // Chúng ta cần đảm bảo rằng Code không null hoặc rỗng
                        if (!string.IsNullOrEmpty(station.Code) && !_stationIdToNameMap.ContainsKey(station.Code))
                        {
                            _stationIdToNameMap.Add(station.Code, station.Name);
                            // Debug: In ra các trạm được thêm vào map
                            Console.WriteLine($"Debug (LoadStationsData): Added station to map: Code={station.Code}, Name={station.Name}");
                        }
                        else if (string.IsNullOrEmpty(station.Code))
                        {
                            Console.WriteLine($"Debug (LoadStationsData): Station with Uuid={station.Uuid} has empty/null Code. Skipping for name map.");
                        }
                        else if (_stationIdToNameMap.ContainsKey(station.Code))
                        {
                            Console.WriteLine($"Debug (LoadStationsData): Duplicate Code '{station.Code}' found for station Uuid={station.Uuid}. Skipping this entry for name map.");
                        }
                    }
                    Console.WriteLine($"Debug (LoadStationsData): _stationIdToNameMap populated with {_stationIdToNameMap.Count} entries.");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu trạm nào từ API.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvStations.DataSource = null;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Lỗi HTTP khi tải danh sách trạm: {e.Message}\nVui lòng kiểm tra kết nối internet hoặc URL API.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (JsonException e)
            {
                MessageBox.Show($"Lỗi khi phân tích dữ liệu JSON cho trạm: {e.Message}\nCấu trúc dữ liệu nhận được có thể không khớp.", "Lỗi JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn khi tải trạm: {e.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private async Task<double> GetLastAccumulatedDepthForStation(string stationId, DateTime sevenAmCycleStart)
        {
            double lastAccumulatedDepth = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    await connection.OpenAsync();
                    string sql = @"SELECT TOP 1 AccumulatedDepth
                                   FROM RealtimeQTM
                                   WHERE StationId = @StationId AND TimePoint >= @SevenAmCycleStart
                                   ORDER BY TimePoint DESC;"; // Lấy bản ghi mới nhất trong chu kỳ
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StationId", stationId);
                        command.Parameters.AddWithValue("@SevenAmCycleStart", sevenAmCycleStart);
                        object result = await command.ExecuteScalarAsync();
                        if (result != null && result != DBNull.Value)
                        {
                            lastAccumulatedDepth = Convert.ToDouble(result);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Debug (GetLastAccumulatedDepth): Lỗi SQL khi lấy AccumulatedDepth cho trạm {stationId}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug (GetLastAccumulatedDepth): Lỗi không mong muốn khi lấy AccumulatedDepth cho trạm {stationId}: {ex.Message}");
            }
            return lastAccumulatedDepth;
        }
        private async Task LoadRainfallStatsData()
        {
            DateTime now = DateTime.Now;
            // Xác định thời điểm 7h sáng của chu kỳ tích lũy hiện tại
            DateTime sevenAmCycleStart;
            if (now.Hour < 7) // Nếu hiện tại trước 7h sáng, chu kỳ bắt đầu từ 7h sáng ngày hôm trước
            {
                sevenAmCycleStart = now.Date.AddDays(-1).AddHours(7);
            }
            else // Nếu hiện tại từ 7h sáng trở đi, chu kỳ bắt đầu từ 7h sáng ngày hiện tại
            {
                sevenAmCycleStart = now.Date.AddHours(7);
            }

            // dtpStartTime và dtpEndTime được dùng để gọi API (lấy 10 phút gần nhất)
            // nhưng logic tích lũy sẽ dùng sevenAmCycleStart
            DateTime apiQueryStartTime = dtpStartTime.Value;
            DateTime apiQueryEndTime = dtpEndTime.Value;

            if (apiQueryStartTime >= apiQueryEndTime)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.", "Lỗi tham số", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string formattedApiQueryStartTime = apiQueryStartTime.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedApiQueryEndTime = apiQueryEndTime.ToString("yyyy-MM-dd HH:mm:ss");

            string statsUrl = $"{API_STATS_URL}?start_time={Uri.EscapeDataString(formattedApiQueryStartTime)}&end_time={Uri.EscapeDataString(formattedApiQueryEndTime)}&format=10m";
            dgvStats.DataSource = null;
            try
            {
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
                    dgvStats.DataSource = null;

                    return;
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                RainfallStatsResponse statsResponse = JsonConvert.DeserializeObject<RainfallStatsResponse>(responseBody);

                var displayData = new List<object>();
                var latestDataPointsByStationFetched = new Dictionary<string, RealtimeRainfallData>();
                var accumulatedRainfallForDisplay = new Dictionary<string, double>(); // Dùng để hiển thị lên lblTotalRainfallSummary

                // Kiểm tra xem _stationIdToNameMap đã được điền chưa
                if (_stationIdToNameMap == null || _stationIdToNameMap.Count == 0)
                {
                    Console.WriteLine("Debug (LoadRainfallStatsData): WARNING! _stationIdToNameMap is empty or null. Attempting to re-load station data.");
                    await LoadStationsData(); // Thử tải lại dữ liệu trạm nếu map trống
                }

                if (statsResponse?.Data != null && statsResponse.Data.Count > 0)
                {
                    // Lấy AccumulatedDepth cuối cùng từ DB cho mỗi trạm trong chu kỳ 7h sáng hiện tại
                    // Chỉ lấy một lần trước khi xử lý dữ liệu mới để có cơ sở tích lũy
                    var initialAccumulatedDepths = new Dictionary<string, double>();
                    foreach (var measurement in statsResponse.Data)
                    {
                        if (!initialAccumulatedDepths.ContainsKey(measurement.StationId))
                        {
                            initialAccumulatedDepths.Add(measurement.StationId, await GetLastAccumulatedDepthForStation(measurement.StationId, sevenAmCycleStart));
                        }
                    }

                    foreach (var measurement in statsResponse.Data)
                    {
                        if (measurement.Value != null && measurement.Value.Any())
                        {
                            string stationId = measurement.StationId;
                            // Debug: In ra StationId từ dữ liệu stats
                            //          Console.WriteLine($"Debug (LoadRainfallStatsData): Processing rainfall data for StationId: {stationId}");

                            // Sử dụng _stationIdToNameMap để tra cứu tên
                            string stationName = "Không xác định";
                            if (_stationIdToNameMap.ContainsKey(stationId))
                            {
                                stationName = _stationIdToNameMap[stationId];
                                //              Console.WriteLine($"Debug (LoadRainfallStatsData): Found name for StationId '{stationId}': '{stationName}'");
                            }
                            else
                            {
                                //              Console.WriteLine($"Debug (LoadRainfallStatsData): Name NOT found in map for StationId '{stationId}'.");
                            }

                            // Lấy giá trị tích lũy ban đầu cho trạm này trong chu kỳ hiện tại
                            double currentAccumulatedDepth = initialAccumulatedDepths.ContainsKey(stationId) ? initialAccumulatedDepths[stationId] : 0;

                            foreach (var depthMeas in measurement.Value)
                            {
                                // Cộng dồn lượng mưa của điểm đo hiện tại vào tổng tích lũy
                                currentAccumulatedDepth += depthMeas.Depth;

                                // Dữ liệu cho DataGridView (có thể hiển thị depth tức thời hoặc accumulated)
                                displayData.Add(new
                                {
                                    StationId = stationId,
                                    Name = stationName, // Hiển thị tên trạm
                                    Timestamp = depthMeas.TimePoint,
                                    Depth = depthMeas.Depth,
                                    AccumulatedDepth = currentAccumulatedDepth, // Hiển thị giá trị tích lũy
                                    Unit = measurement.Unit
                                });

                                // Tạo bản ghi RealtimeRainfallData để lưu vào DB
                                RealtimeRainfallData currentRealtimeData = new RealtimeRainfallData
                                {
                                    StationId = stationId,
                                    Name = stationName, // Gán tên trạm
                                    TimePoint = depthMeas.TimePoint,
                                    Depth = depthMeas.Depth,
                                    Unit = measurement.Unit,
                                    AccumulatedDepth = currentAccumulatedDepth, // Giá trị tích lũy sẽ được lưu
                                    RecordedAt = DateTime.Now
                                };

                                // Chỉ giữ lại bản ghi mới nhất cho mỗi trạm trong dữ liệu vừa fetch
                                if (latestDataPointsByStationFetched.ContainsKey(stationId))
                                {
                                    // Cập nhật nếu bản ghi mới hơn HOẶC nếu bản ghi có AccumulatedDepth lớn hơn (để đảm bảo tính tích lũy)
                                    if (currentRealtimeData.TimePoint > latestDataPointsByStationFetched[stationId].TimePoint ||
                                        (currentRealtimeData.TimePoint == latestDataPointsByStationFetched[stationId].TimePoint &&
                                         currentRealtimeData.AccumulatedDepth > latestDataPointsByStationFetched[stationId].AccumulatedDepth))
                                    {
                                        latestDataPointsByStationFetched[stationId] = currentRealtimeData;
                                    }
                                }
                                else
                                {
                                    latestDataPointsByStationFetched.Add(stationId, currentRealtimeData);
                                }
                            }

                            // Cập nhật giá trị tích lũy cuối cùng cho mục đích hiển thị tổng
                            accumulatedRainfallForDisplay[stationId] = currentAccumulatedDepth;
                        }
                    }

                    if (displayData.Any())
                    {
                        dgvStats.DataSource = displayData;
                    }
                    else
                    {
                        dgvStats.DataSource = null;

                    }

                    // Hiển thị tổng lượng mưa từng trạm trong lblTotalRainfallSummary
                    if (accumulatedRainfallForDisplay.Any())
                    {
                        StringBuilder summaryBuilder = new StringBuilder();
                        summaryBuilder.AppendLine("Tổng lượng mưa tích lũy theo trạm (Chu kỳ 7h sáng):");
                        foreach (var entry in accumulatedRainfallForDisplay)
                        {
                            string unit = statsResponse.Data.FirstOrDefault(m => m.StationId == entry.Key)?.Unit ?? "mm";
                            // Lấy tên trạm từ _stationIdToNameMap
                            string name = _stationIdToNameMap.ContainsKey(entry.Key) ? _stationIdToNameMap[entry.Key] : entry.Key;
                            summaryBuilder.AppendLine($"- Trạm {name} ({entry.Key}): {entry.Value:F2} {unit}");
                        }

                    }
                    else
                    {

                    }
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy dữ liệu thống kê lượng mưa nào trong khoảng thời gian đã chọn " +
                        "hoặc API trả về dữ liệu rỗng/null cho khoảng thời gian này.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    dgvStats.DataSource = null;

                }

                // Lấy danh sách các bản ghi mới nhất từ Dictionary để lưu dữ liệu tức thời vào SQL
                List<RealtimeRainfallData> realTimeDataToSave = latestDataPointsByStationFetched.Values.ToList();
                Console.WriteLine($"Debug: Số lượng bản ghi tức thời mới nhất cần lưu vào SQL: {realTimeDataToSave.Count}");

                string saveStatusMessage = "";
                bool realtimeSaveSuccess = false;

                if (realTimeDataToSave.Any())
                {
                    try
                    {
                        await WriteQTM(latestDataPointsByStationFetched);
                        await SaveRealtimeMeasurementsToSql(realTimeDataToSave);
                        saveStatusMessage += $"Đã lưu {realTimeDataToSave.Count} bản ghi tức thời mới nhất vào SQL (bao gồm tổng lượng mưa tích lũy từ 7h sáng và Tên trạm).";
                        realtimeSaveSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        saveStatusMessage += $"Lỗi lưu tức thời vào SQL: {ex.Message}.";
                    }
                }
                else
                {
                    saveStatusMessage += "Không có dữ liệu tức thời mới nhất để lưu vào SQL.";
                }

                if (realtimeSaveSuccess)
                {

                }
                else
                {

                }


            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Lỗi HTTP khi tải thống kê mưa: {e.Message}\nVui lòng kiểm tra kết nối internet hoặc URL API stats.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (JsonException e)
            {
                MessageBox.Show($"Lỗi khi phân tích dữ liệu JSON cho thống kê mưa: {e.Message}\nCấu trúc dữ liệu nhận được có thể không khớp.", "Lỗi JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn khi tải thống kê mưa: {e.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private async Task SaveRealtimeMeasurementsToSql(List<RealtimeRainfallData> realtimeData)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    await connection.OpenAsync();

                    foreach (var data in realtimeData)
                    {
                        // Ghi log giá trị Unit và Name trước khi thêm vào tham số
                        Console.WriteLine($"Debug (SaveRealtimeMeasurementsToSql): Trying to save Realtime data for StationId={data.StationId}, Name='{data.Name}', TimePoint={data.TimePoint}, Depth={data.Depth}, Unit='{data.Unit}', AccumulatedDepth={data.AccumulatedDepth}, RecordedAt={data.RecordedAt}");

                        // Kiểm tra xem bản ghi cho StationId và TimePoint đã tồn tại chưa
                        string checkSql = @"SELECT COUNT(1) FROM RealtimeQTM
                                            WHERE StationId = @StationId AND TimePoint = @TimePoint;";

                        using (SqlCommand checkCommand = new SqlCommand(checkSql, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@StationId", data.StationId);
                            checkCommand.Parameters.AddWithValue("@TimePoint", data.TimePoint);
                            int existingCount = (int)await checkCommand.ExecuteScalarAsync();

                            if (existingCount > 0)
                            {
                                string updateSql = @"UPDATE RealtimeQTM
                                                     SET Depth = @Depth,
                                                         Unit = @Unit,
                                                         AccumulatedDepth = @AccumulatedDepth,
                                                         Name = @Name,
                                                         RecordedAt = @RecordedAt
                                                     WHERE StationId = @StationId AND TimePoint = @TimePoint;";
                                using (SqlCommand updateCommand = new SqlCommand(updateSql, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Depth", data.Depth);
                                    updateCommand.Parameters.AddWithValue("@Unit", (object)data.Unit ?? DBNull.Value);
                                    updateCommand.Parameters.AddWithValue("@AccumulatedDepth", (object)data.AccumulatedDepth ?? DBNull.Value);
                                    updateCommand.Parameters.AddWithValue("@Name", (object)data.Name ?? DBNull.Value);
                                    updateCommand.Parameters.AddWithValue("@RecordedAt", data.RecordedAt);
                                    updateCommand.Parameters.AddWithValue("@StationId", data.StationId);
                                    updateCommand.Parameters.AddWithValue("@TimePoint", data.TimePoint);
                                    int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                                    Console.WriteLine($"Debug (SaveRealtimeMeasurementsToSql): UPDATE affected {rowsAffected} rows for Realtime data StationId={data.StationId}, TimePoint={data.TimePoint}.");
                                }
                            }
                            else
                            {
                                string insertSql = @"INSERT INTO RealtimeQTM (StationId, TimePoint, Depth, Unit, AccumulatedDepth, Name, RecordedAt)
                                                     VALUES (@StationId, @TimePoint, @Depth, @Unit, @AccumulatedDepth, @Name, @RecordedAt);";
                                using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@StationId", data.StationId);
                                    insertCommand.Parameters.AddWithValue("@TimePoint", data.TimePoint);
                                    insertCommand.Parameters.AddWithValue("@Depth", data.Depth);
                                    insertCommand.Parameters.AddWithValue("@Unit", (object)data.Unit ?? DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@AccumulatedDepth", (object)data.AccumulatedDepth ?? DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@Name", (object)data.Name ?? DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@RecordedAt", data.RecordedAt);
                                    int rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                                    Console.WriteLine($"Debug (SaveRealtimeMeasurementsToSql): INSERT affected {rowsAffected} rows for Realtime data StationId={data.StationId}, TimePoint={data.TimePoint}.");
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL khi lưu dữ liệu tức thời: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không mong muốn khi lưu dữ liệu tức thời vào SQL: {ex.Message}");
                throw;
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
            public string Name { get; set; } // Tên trạm
            public DateTime TimePoint { get; set; } // Thời điểm đo
            public double Depth { get; set; }
            public string Unit { get; set; }
            public double? AccumulatedDepth { get; set; } // Tổng lượng mưa tích lũy từ 7h sáng trong ngày
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
        public class SoLieuAPIBinhNhamModel
        {
            public long ts { get; set; }
            public long c { get; set; }
            public double water_proof_1 { get; set; }
            public double water_proof_2 { get; set; }

            public DateTime Timestamp => DateTimeOffset.FromUnixTimeSeconds(ts).ToLocalTime().DateTime;
            public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(c).ToLocalTime().DateTime;
        }
        public async Task Api_DTtimer_Tick()
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

                if (Globalvariable.RealtimeDisplays == null || Globalvariable.RealtimeDisplays.Count == 0)
                    return;

                #region hien thi UI

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    var location = Globalvariable.RealtimeDisplays?.FirstOrDefault(loc => loc.LocationId == 1);
                    if (Location != null)
                    {
                        foreach (var item in location.Stations)
                        {
                            if (item.Path == "Local Station/DauTieng/S71500/Station_1")
                            {
                                _labALDoor1_Station1.Text = item.Al_Door1.ToString();
                                _labALDoor2_Station1.Text = item.Al_Door2.ToString();
                                _labHT_Cylinder1_1.Text = item.HT_Cylinder1_1.ToString();
                                _labHT_Cylinder1_2.Text = item.HT_Cylinder1_2.ToString();



                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_2")
                            {
                                _labALDoor1_Station2.Text = item.Al_Door1.ToString();
                                _labALDoor2_Station2.Text = item.Al_Door2.ToString();
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_3")
                            {
                                _labALDoor1_Station3.Text = item.Al_Door1.ToString();
                                _labALDoor2_Station3.Text = item.Al_Door2.ToString();
                            }
                        }

                        _labFllowHo.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho.ToString();
                        _labFlowHoFinal.Text = location.CalculatorValue.LuuLuongTong.ToString();

                    }
                });
                #endregion

                #region Data log                
                _logTime = (int)(DateTime.Now - _startTime).TotalSeconds;

                if (_logTime >= Globalvariable.ConfigSystem.DataLogInterval)
                {
                    var dataLogs = new List<FT03>();
                    var createAt = DateTime.Now;
                    var createOperatorId = "System";

                    //datalog
                    foreach (var item in Globalvariable.RealtimeDisplays)
                    {
                        var line = new FT03
                        {
                            Id = Guid.NewGuid(),
                            CreateAt = createAt,
                            CreateOperatorId = createOperatorId,
                            IsDeleted = false,
                            LogBaseInterval = false,
                            LocationId = item.LocationId,
                            LocationName = item.LocationName,

                            Fllow_DauTieng = item.CalculatorValue.Fllow_DauTieng,
                            Fllow_BenSuc = item.CalculatorValue.Fllow_BenSuc,
                            Fllow_SonDai = item.CalculatorValue.Fllow_SonDai,
                            Fllow_BinhNham = item.CalculatorValue.Fllow_BinhNham,
                            Fllow_BinhNham2 = item.CalculatorValue.Fllow_BinhNham2,
                            Fllow_TL_CDD = item.CalculatorValue.Fllow_TL_CDD,
                            Fllow_HL_TXL = item.CalculatorValue.Fllow_HL_TXL,
                            Total_Fllow = item.CalculatorValue.Total_Fllow,
                            Q_Den = item.CalculatorValue.Q_Den,
                            Q_Di = item.CalculatorValue.Q_Di,
                            W_Ho = item.CalculatorValue.W_Ho,
                            LuuLuong = item.CalculatorValue.LuuLuong,
                            LuuLuongTong = item.CalculatorValue.LuuLuongTong,
                        };

                        foreach (var itemStation in item.Stations)
                        {
                            line.StationId = itemStation.StationId;
                            line.StationName = itemStation.StationName;
                            line.Path = itemStation.Path;

                            line.HT_Cylinder1_1 = itemStation.HT_Cylinder1_1;
                            line.HT_Cylinder1_2 = itemStation.HT_Cylinder1_2;
                            line.HT_Cylinder2_1 = itemStation.HT_Cylinder2_1;
                            line.HT_Cylinder2_2 = itemStation.HT_Cylinder2_2;
                            line.Door1_Aperture = itemStation.Door1_Aperture;
                            line.Door2_Aperture = itemStation.Door2_Aperture;
                            line.S1_Temp_Oil = itemStation.S1_Temp_Oil;
                            line.Pressure_Oil_Door1 = itemStation.Pressure_Oil_Door1;
                            line.Pressure_Oil_Door2 = itemStation.Pressure_Oil_Door2;
                            line.Fllow_Door1 = itemStation.Fllow_Door1;
                            line.Fllow_Door2 = itemStation.Fllow_Door2;
                            line.Fllow_Ho = itemStation.Fllow_Ho;

                            line.HT_Cylinder1_1_Offset = itemStation.HT_Cylinder1_1_Offset;
                            line.HT_Cylinder1_2_Offset = itemStation.HT_Cylinder1_2_Offset;
                            line.HT_Cylinder2_1_Offset = itemStation.HT_Cylinder2_1_Offset;
                            line.HT_Cylinder2_2_Offset = itemStation.HT_Cylinder2_2_Offset;
                            line.Door1_Aperture_Offset = itemStation.Door1_Aperture_Offset;
                            line.Door2_Aperture_Offset = itemStation.Door2_Aperture_Offset;
                            line.S1_Temp_Oil_Offset = itemStation.S1_Temp_Oil_Offset;
                            line.Pressure_Oil_Door1_Offset = itemStation.Pressure_Oil_Door1_Offset;
                            line.Pressure_Oil_Door2_Offset = itemStation.Pressure_Oil_Door2_Offset;
                            line.Fllow_Door1_Offset = itemStation.Fllow_Door1_Offset;
                            line.Fllow_Door2_Offset = itemStation.Fllow_Door2_Offset;
                            line.Fllow_Ho_Offset = itemStation.Fllow_Ho_Offset;

                            line.HT_Cylinder1_1_Final = itemStation.HT_Cylinder1_1_Final;
                            line.HT_Cylinder1_2_Final = itemStation.HT_Cylinder1_2_Final;
                            line.HT_Cylinder2_1_Final = itemStation.HT_Cylinder2_1_Final;
                            line.HT_Cylinder2_2_Final = itemStation.HT_Cylinder2_2_Final;
                            line.Door1_Aperture_Final = itemStation.Door1_Aperture_Final;
                            line.Door2_Aperture_Final = itemStation.Door2_Aperture_Final;
                            line.S1_Temp_Oil_Final = itemStation.S1_Temp_Oil_Final;
                            line.Pressure_Oil_Door1_Final = itemStation.Pressure_Oil_Door1_Final;
                            line.Pressure_Oil_Door2_Final = itemStation.Pressure_Oil_Door2_Final;
                            line.Fllow_Door1_Final = itemStation.Fllow_Door1_Final;
                            line.Fllow_Door2_Final = itemStation.Fllow_Door2_Final;
                            line.Fllow_Ho_Final = itemStation.Fllow_Ho_Final;

                            dataLogs.Add(line);
                        }
                    }

                    if (dataLogs.Count == 0)
                        return;

                    using var dbContext = new ApplicationDbContext();
                    dbContext.FT03s.AddRange(dataLogs);
                    dbContext.SaveChanges();//Luu thay doi vao db

                    _startTime = DateTime.Now;
                }
                #endregion
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
        // Hàm ghi log xuống TextBox
        private void AppendLog(string message)
        {
            //if (txtLog.InvokeRequired)
            //    txtLog.Invoke(new Action(() => txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}")));
            //else
            //    txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
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

       
        private void Door2_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_PressureLow = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                          .Where(x => x.Path == station.Path && x.TagName == "Door2_PressureLow" && x.IsDeleted != true)
                          .OrderByDescending(x => x.CreateAt)
                          .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_PressureLow)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_PressureLow";
                            Globalvariable.AlarmDataLog.Value = station.Door2_PressureLow;
                            Globalvariable.AlarmDataLog.Description = station.Door2_PressureLow == true ? "AS dầu cửa 2 thấp" : "AS dầu cửa 2 bình thường";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_PressureHigh = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                          .Where(x => x.Path == station.Path && x.TagName == "Door2_PressureHigh" && x.IsDeleted != true)
                          .OrderByDescending(x => x.CreateAt)
                          .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_PressureHigh)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_PressureHigh";
                            Globalvariable.AlarmDataLog.Value = station.Door2_PressureHigh;
                            Globalvariable.AlarmDataLog.Description = station.Door2_PressureHigh == true ? "AS dầu cửa 2 cao" : "AS dầu cửa 2 bình thường";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }

        private void Door1_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_PressureLow = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_PressureLow" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_PressureLow)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_PressureLow";
                            Globalvariable.AlarmDataLog.Value = station.Door1_PressureLow;
                            Globalvariable.AlarmDataLog.Description = station.Door1_PressureLow == true ? "AS dầu cửa 1 thấp" : "AS dầu cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_PressureHigh = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_PressureHigh" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_PressureHigh)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_PressureHigh";
                            Globalvariable.AlarmDataLog.Value = station.Door1_PressureHigh;
                            Globalvariable.AlarmDataLog.Description = station.Door1_PressureHigh == true ? "AS dầu cửa 1 cao" : "AS dầu cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }


        }

        private void DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC3_Over = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC3_Over" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC3_Over)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC3_Over";
                            Globalvariable.AlarmDataLog.Value = station.DC3_Over;
                            Globalvariable.AlarmDataLog.Description = station.DC3_Over == true ? "Quá tải bơm chốt" : "Bơm chốt bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC2_Over = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC2_Over" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC2_Over)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC2_Over";
                            Globalvariable.AlarmDataLog.Value = station.DC2_Over;
                            Globalvariable.AlarmDataLog.Description = station.DC2_Over == true ? "Quá tải bơm 2" : "Bơm 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC1_Over = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC1_Over" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC1_Over)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC1_Over";
                            Globalvariable.AlarmDataLog.Value = station.DC1_Over;
                            Globalvariable.AlarmDataLog.Description = station.DC1_Over == true ? "Bơm 1 quá tải" : "Bơm 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {                
                try
                {
                    var createAt = DateTime.Now;
                    var createOperatorId = "System";

                    var path = e.Tag.Parent.Path;

                    var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                    var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                    if (station != null)
                    {
                        station.Doorlock2_2Close = e.NewValue == "1" ? true : false;

                        using (var dbContext = new ApplicationDbContext())
                        {
                            //Real time
                            var check = dbContext.FT02s.FirstOrDefault();

                            if (check != null)
                            {
                                check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                                check.UpdateAt = createAt;
                                check.UpdateOperatorId = createOperatorId;
                            }
                            else
                            {
                                var newLine = new FT02
                                {
                                    Id = Guid.NewGuid(),
                                    C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                    IsDeleted = false,
                                    CreateAt = createAt,
                                    CreateOperatorId = createOperatorId,
                                };

                                dbContext.FT02s.Add(newLine);
                            }

                            //alarms
                            var checkExist = dbContext.FT04s
                                .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_2Close" && x.IsDeleted != true)
                                .OrderByDescending(x => x.CreateAt)
                                .FirstOrDefault();

                            if (checkExist == null || checkExist.Value != station.Doorlock2_2Close)
                            {
                                Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                                Globalvariable.AlarmDataLog.CreateAt = createAt;
                                Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                                Globalvariable.AlarmDataLog.IsDeleted = false;
                                Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                                Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                                Globalvariable.AlarmDataLog.StationId = station.StationId;
                                Globalvariable.AlarmDataLog.StationName = station.StationName;
                                Globalvariable.AlarmDataLog.Path = station.Path;
                                Globalvariable.AlarmDataLog.TagName = "Doorlock2_2Close";
                                Globalvariable.AlarmDataLog.Value = station.Doorlock2_2Close;
                                Globalvariable.AlarmDataLog.Description = station.Doorlock2_2Close == true ? "Chốt 2_2 đóng hết" : "Chốt 2_2 bình thường.";

                                dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                                dbContext.SaveChanges();//Luu thay doi vao db
                            }
                        }
                    }
                }
                catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

            }
        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_2Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_2Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_2Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_2Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_2Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_2Open == true ? "Chốt 2_2 mở hết" : "Chốt 2_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_1Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_1Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_1Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_1Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_1Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_1Close == true ? "Chốt 2_1 đóng hết" : "Chốt 2_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_1Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_1Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_1Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_1Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_1Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_1Open == true ? "Chốt 2_1 mở hết" : "Chốt 2_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_2Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_2Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_2Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_2Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_2Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_2Close == true ? "Chốt 1_2 đóng hết" : "Chốt 1_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_2Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_2Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_2Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_2Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_2Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_2Open == true ? "Chốt 1_2 mở hết" : "Chốt 1_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_1Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_1Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_1Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_1Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_1Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_1Close == true ? "Chốt 1_1 đóng hết" : "Chốt 1_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_1Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_1Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_1Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_1Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_1Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_1Open == true ? "Chốt 1_1 mở hết" : "Chốt 1_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_Closing == true ? "Chốt 2 đang đóng" : "Chốt 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_Opening == true ? "Chốt 2 đang mở" : "Chốt 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_Closing == true ? "Chốt 1 đang đóng" : "Chốt 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_Opening == true ? "Chốt 1 đang mở" : "Chốt 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Close";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Close;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Close == true ? "Cửa 2 đóng hết" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Open";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Open;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Open == true ? "Cửa 2 mở hết" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Close";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Close;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Close == true ? "Cửa 1 đóng hết" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Open";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Open;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Open == true ? "Cửa 1 mở hết" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Closing == true ? "Cửa 2 đang đóng" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Opening == true ? "Cửa 2 đang mở" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Closing == true ? "Cửa 1 đang đóng" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Opening == true ? "Cửa 1 đang mở" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC3_Running = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC3_Running" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC3_Running)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC3_Running";
                            Globalvariable.AlarmDataLog.Value = station.DC3_Running;
                            Globalvariable.AlarmDataLog.Description = station.DC3_Running == true ? "Bơm 3 đang chạy" : "Bơm 3 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC2_Running = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC2_Running" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC2_Running)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC2_Running";
                            Globalvariable.AlarmDataLog.Value = station.DC2_Running;
                            Globalvariable.AlarmDataLog.Description = station.DC2_Running == true ? "Bơm 2 đang chạy" : "Bơm 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC1_Running = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC1_Running" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC1_Running)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC1_Running";
                            Globalvariable.AlarmDataLog.Value = station.DC1_Running;
                            Globalvariable.AlarmDataLog.Description = station.DC1_Running == true ? "Bơm 1 đang chạy" : "Bơm 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Local_Stop = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Local_Stop" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Local_Stop)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Local_Stop";
                            Globalvariable.AlarmDataLog.Value = station.Local_Stop;
                            Globalvariable.AlarmDataLog.Description = station.Local_Stop == true ? "Đang dừng khẩn" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Man = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Man" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Man)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Man";
                            Globalvariable.AlarmDataLog.Value = station.Man;
                            Globalvariable.AlarmDataLog.Description = station.Man == true ? "Đang chạy tay" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Auto = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Auto" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Auto)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Auto";
                            Globalvariable.AlarmDataLog.Value = station.Auto;
                            Globalvariable.AlarmDataLog.Description = station.Auto == true ? "Đang chạy tự động" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Local = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Local" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Local)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Local";
                            Globalvariable.AlarmDataLog.Value = station.Local;
                            Globalvariable.AlarmDataLog.Description = station.Local == true ? "Đang chạy tại chổ" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Remote = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Remote" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Remote)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Remote";
                            Globalvariable.AlarmDataLog.Value = station.Remote;
                            Globalvariable.AlarmDataLog.Description = station.Remote == true ? "Đang chạy từ xa" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }

        private void Al_Door2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Al_Door2 = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Al_Door2" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Al_Door2)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Al_Door2";
                            Globalvariable.AlarmDataLog.Value = station.Al_Door2;
                            Globalvariable.AlarmDataLog.Description = station.Al_Door2 == true ? "Đang lệch cửa 2" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Al_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Al_Door1 = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Al_Door1" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Al_Door1)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Al_Door1";
                            Globalvariable.AlarmDataLog.Value = station.Al_Door1;
                            Globalvariable.AlarmDataLog.Description = station.Al_Door1 == true ? "Đang lệch cửa 1" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }

      private void HT_Cylinder1_1_ValueChanged(object sender, TagValueChangedEventArgs e)
            {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder1_1 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder1_1_Final = Math.Round(station.HT_Cylinder1_1 + station.HT_Cylinder1_1_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void HT_Cylinder1_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder1_2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder1_2_Final = Math.Round(station.HT_Cylinder1_2 + station.HT_Cylinder1_2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void HT_Cylinder2_1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder2_1 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder2_1_Final = Math.Round(station.HT_Cylinder2_1 + station.HT_Cylinder2_1_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void HT_Cylinder2_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder2_2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder2_2_Final = Math.Round(station.HT_Cylinder2_2 + station.HT_Cylinder2_2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Pressure_Oil_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Pressure_Oil_Door1 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Pressure_Oil_Door1_Final = Math.Round(station.Pressure_Oil_Door1 + station.Pressure_Oil_Door1_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Pressure_Oil_Door2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Pressure_Oil_Door2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Pressure_Oil_Door2_Final = Math.Round(station.Pressure_Oil_Door2 + station.Pressure_Oil_Door2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void S1_Temp_Oil_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.S1_Temp_Oil = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.S1_Temp_Oil_Final = Math.Round(station.S1_Temp_Oil + station.S1_Temp_Oil_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        // Door1_Aperture
        private void Door1_Aperture_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Door1_Aperture = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Door1_Aperture_Final = Math.Round(station.Door1_Aperture + station.Door1_Aperture_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Door2_Aperture_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Door2_Aperture = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Door2_Aperture_Final = Math.Round(station.Door2_Aperture + station.Door2_Aperture_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Fllow_Ho_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Fllow_Ho = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;

                    //tinh toans
                    station.Fllow_Ho_Final = Math.Round(station.Fllow_Ho + station.Fllow_Ho_Offset ?? 0, 2);

                    //location.CalculatorValue.LuuLuongTong = Math.Round((double)station.Fllow_Ho_Final * Globalvariable.ConfigSystem.ParametterConfig.HeSoLuuToc_Phi, 2);
                    location.CalculatorValue.LuuLuongTong = TinhToan((double)station.Fllow_Ho_Final);

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private double TinhToan(double tagValue)
        {
            return Math.Round(tagValue * Globalvariable.ConfigSystem.ParametterConfig.HeSoLuuToc_Phi, 2);
        }











        private void button1_Click(object sender, EventArgs e)
        {
            FrmHochua mn = new FrmHochua();
            OpenFormInPanel(mn, "Hồ chứa");
            mn.UrlToLoad = "https://vrain.vn/61/overview?public_map=windy";

            mn.Show();

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadRainfallStatsData();
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
            FrmTran data = new FrmTran();
            OpenFormInPanel(data, "Hệ thống tràn");
        }

        private void bnt_TramMN_Click(object sender, EventArgs e)
        {
            FrmMucnuoc mn = new FrmMucnuoc();
            OpenFormInPanel(mn, "Mức Nước");
        }

        private void bnt_TrangChu_Click(object sender, EventArgs e)
        {
            FrmHome H = new FrmHome();
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

        private void bntThongtin_Click(object sender, EventArgs e)
        {
            FrmThongtin tt = new FrmThongtin();
            OpenFormInPanel(tt, " CÁC THÔNG TIN CẬP NHẬT");
        }
    }
}