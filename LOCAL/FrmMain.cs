using Ahd.Core;
using Dapper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{    
    public partial class FrmMain : Form
    {
        // ✅ Event phải đặt bên trong class
        public event EventHandler<TagValueChangedEventArgs> S1RemoteChanged;
        public event EventHandler<TagValueChangedEventArgs> S1LocalChanged;
        public event EventHandler<TagValueChangedEventArgs> S1AutoChanged;
        public event EventHandler<TagValueChangedEventArgs> S1ManChanged;
        public event EventHandler<TagValueChangedEventArgs> S1LocalStopChanged;
        public event EventHandler<TagValueChangedEventArgs> S2RemoteChanged;
        public event EventHandler<TagValueChangedEventArgs> S2LocalChanged;
        public event EventHandler<TagValueChangedEventArgs> S2AutoChanged;
        public event EventHandler<TagValueChangedEventArgs> S2ManChanged;
        public event EventHandler<TagValueChangedEventArgs> S2LocalStopChanged;
        public event EventHandler<TagValueChangedEventArgs> S3RemoteChanged;
        public event EventHandler<TagValueChangedEventArgs> S3LocalChanged;
        public event EventHandler<TagValueChangedEventArgs> S3AutoChanged;
        public event EventHandler<TagValueChangedEventArgs> S3ManChanged;
        public event EventHandler<TagValueChangedEventArgs> S3LocalStopChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_DC1_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_DC2_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_DC3_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_DC1_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_DC2_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_DC3_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_DC1_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_DC2_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_DC3_RunningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door1_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door1_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Door2_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door2_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Door3_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door3_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Door4_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door4_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Door5_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door5_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Door6_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Door6_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock1_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock1_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock2_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock2_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock3_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock3_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock4_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock4_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock5_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock5_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock6_OpeningChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock6_ClosingChanged;
        public event EventHandler<TagValueChangedEventArgs> Door1_OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Door1_CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Door2_OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Door2_CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Door3_OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Door3_CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Door4_OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Door4_CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Door5_OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Door5_CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Door6_OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Door6_CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock1_1OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock1_1CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock1_2OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock1_2CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock2_1OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock2_1CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock2_2OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock2_2CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock3_1OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock3_1CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock3_2OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock3_2CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock4_1OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock4_1CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock4_2OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock4_2CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock5_1OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock5_1CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock5_2OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock5_2CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock6_1OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock6_1CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock6_2OpenChanged;
        public event EventHandler<TagValueChangedEventArgs> Doorlock6_2CloseChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_DC1_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_DC2_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_DC3_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_DC1_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_DC2_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_DC3_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_DC1_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_DC2_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_DC3_OverChanged;
        public event EventHandler<TagValueChangedEventArgs> Door1_PressureHighChanged;
        public event EventHandler<TagValueChangedEventArgs> Door1_PressureLowChanged;
        public event EventHandler<TagValueChangedEventArgs> Door2_PressureHighChanged;
        public event EventHandler<TagValueChangedEventArgs> Door2_PressureLowChanged;
        public event EventHandler<TagValueChangedEventArgs> Door3_PressureHighChanged;
        public event EventHandler<TagValueChangedEventArgs> Door3_PressureLowChanged;
        public event EventHandler<TagValueChangedEventArgs> Door4_PressureHighChanged;
        public event EventHandler<TagValueChangedEventArgs> Door4_PressureLowChanged;
        public event EventHandler<TagValueChangedEventArgs> Door5_PressureHighChanged;
        public event EventHandler<TagValueChangedEventArgs> Door5_PressureLowChanged;
        public event EventHandler<TagValueChangedEventArgs> Door6_PressureHighChanged;
        public event EventHandler<TagValueChangedEventArgs> Door6_PressureLowChanged;






        public static DataTranModel CurrentDataTran = null;
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
             Load += FrmMain_Load; // Đăng ký sự kiện Load của Form
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
        
        }
        IAhdDriverConnector driver;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            SQLLogin.InitCurrentDataTran();
            lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
            //      btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
      
            timer1.Enabled = true;
            tm_login.Interval = 10000;
            tm_login.Enabled = true;
            tm_login.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { tm_login.Start(); });
                t.Enabled = true;
            };
        }
        
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
            // Alarm các tag cần thiết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over").ValueChanged += S1_DC1_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over").ValueChanged += S1_DC2_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over").ValueChanged += S1_DC3_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over").ValueChanged += S2_DC1_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over").ValueChanged += S2_DC2_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over").ValueChanged += S2_DC3_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over").ValueChanged += S3_DC1_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over").ValueChanged += S3_DC2_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over").ValueChanged += S3_DC3_Over_ValueChanged;//Su kien Alarm FrmTran

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh").ValueChanged += Door1_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow").ValueChanged += Door1_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh").ValueChanged += Door2_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow").ValueChanged += Door2_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh").ValueChanged += Door3_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow").ValueChanged += Door3_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh").ValueChanged += Door4_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow").ValueChanged += Door4_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh").ValueChanged += Door5_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow").ValueChanged += Door5_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh").ValueChanged += Door6_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow").ValueChanged += Door6_PressureLow_ValueChanged;//Su kien Alarm FrmTran



            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote").ValueChanged += S1_Remote_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local").ValueChanged += S1_Local_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto").ValueChanged += S1_Auto_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man").ValueChanged += S1_Man_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop").ValueChanged += S1_Local_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote").ValueChanged += S2_Remote_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local").ValueChanged += S2_Local_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto").ValueChanged += S2_Auto_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man").ValueChanged += S2_Man_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop").ValueChanged += S2_Local_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote").ValueChanged += S3_Remote_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local").ValueChanged += S3_Local_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto").ValueChanged += S3_Auto_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man").ValueChanged += S3_Man_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop").ValueChanged += S3_Local_Stop_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running").ValueChanged += S1_DC1_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running").ValueChanged += S1_DC2_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running").ValueChanged += S1_DC3_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running").ValueChanged += S2_DC1_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running").ValueChanged += S2_DC2_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running").ValueChanged += S2_DC3_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running").ValueChanged += S3_DC1_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running").ValueChanged += S3_DC2_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running").ValueChanged += S3_DC3_Running_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening").ValueChanged += Door3_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing").ValueChanged += Door3_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening").ValueChanged += Door4_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing").ValueChanged += Door4_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening").ValueChanged += Door5_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing").ValueChanged += Door5_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening").ValueChanged += Door6_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing").ValueChanged += Door6_Closing_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Opening").ValueChanged += Doorlock1_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Closing").ValueChanged += Doorlock1_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening").ValueChanged += Doorlock2_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing").ValueChanged += Doorlock2_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening").ValueChanged += Doorlock3_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing").ValueChanged += Doorlock3_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening").ValueChanged += Doorlock4_Opening_ValueChanged; //Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing").ValueChanged += Doorlock4_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening").ValueChanged += Doorlock5_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing").ValueChanged += Doorlock5_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Opening").ValueChanged += Doorlock6_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Closing").ValueChanged += Doorlock6_Closing_ValueChanged;//Su kien FrmHome

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").ValueChanged += Door1_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").ValueChanged += Door1_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").ValueChanged += Door2_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").ValueChanged += Door2_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open").ValueChanged += Door3_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close").ValueChanged += Door3_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open").ValueChanged += Door4_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close").ValueChanged += Door4_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open").ValueChanged += Door5_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close").ValueChanged += Door5_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open").ValueChanged += Door6_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close").ValueChanged += Door6_Close_ValueChanged;//Su kien FrmTran
            // Tag dự phòng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Open").ValueChanged += Doorlock1_1Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close").ValueChanged += Doorlock1_1Close_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Open").ValueChanged += Doorlock1_2Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Close").ValueChanged += Doorlock1_2Close_ValueChanged;//Su kien áp cửa 1 cao
            // End Tag dự phòng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open").ValueChanged += Doorlock3_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close").ValueChanged += Doorlock3_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open").ValueChanged += Doorlock3_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close").ValueChanged += Doorlock3_2Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open").ValueChanged += Doorlock4_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close").ValueChanged += Doorlock4_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open").ValueChanged += Doorlock4_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close").ValueChanged += Doorlock4_2Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open").ValueChanged += Doorlock5_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close").ValueChanged += Doorlock5_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open").ValueChanged += Doorlock5_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close").ValueChanged += Doorlock5_2Close_ValueChanged;//Su kien FrmTran
            // Tag dự phòng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Open").ValueChanged += Doorlock6_1Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Close").ValueChanged += Doorlock6_1Close_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Open").ValueChanged += Doorlock6_2Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Close").ValueChanged += Doorlock6_2Close_ValueChanged;//Su kien áp cửa 1 cao
                                                                                                                                        // End Tag dự phòng
                                                                                                                                        // Gọi lần đầu khi load form để publish trạng thái hiện tại
            PublishInitialValues();
        }
        private void PublishInitialValues()
        {

            Door1_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh"), "", GetDoor1_PressureHighValue()));
            Door1_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow"), "", GetDoor1_PressureLowValue()));
            Door2_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh"), "", GetDoor2_PressureHighValue()));
            Door2_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow"), "", GetDoor2_PressureLowValue()));
            Door3_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh"), "", GetDoor3_PressureHighValue()));
            Door3_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow"), "", GetDoor3_PressureLowValue()));
            Door4_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh"), "", GetDoor4_PressureHighValue()));
            Door4_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow"), "", GetDoor4_PressureLowValue()));
            Door5_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh"), "", GetDoor5_PressureHighValue()));
            Door5_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow"), "", GetDoor5_PressureLowValue()));
            Door6_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh"), "", GetDoor6_PressureHighValue()));
            Door6_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow"), "", GetDoor6_PressureLowValue()));

            S1_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over"), "", GetS1_DC1_OverValue()));
            S1_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over"), "", GetS1_DC2_OverValue()));
            S1_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over"), "", GetS1_DC3_OverValue()));
            S2_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over"), "", GetS2_DC1_OverValue()));
            S2_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over"), "", GetS2_DC2_OverValue()));
            S2_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over"), "", GetS2_DC3_OverValue()));
            S3_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over"), "", GetS3_DC1_OverValue()));
            S3_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over"), "", GetS3_DC2_OverValue()));
            S3_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over"), "", GetS3_DC3_OverValue()));

            
            Doorlock1_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Open"), "", GetDoorlock1_1OpenValue()));
            Doorlock1_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close"), "", GetDoorlock1_1CloseValue()));
            Doorlock1_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Open"), "", GetDoorlock1_2OpenValue()));
            Doorlock1_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Close"), "", GetDoorlock1_2CloseValue()));
            Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open"), "", GetDoorlock2_1OpenValue()));
            Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close"), "", GetDoorlock2_1CloseValue()));
            Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open"), "", GetDoorlock2_2OpenValue()));
            Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close"), "", GetDoorlock2_2CloseValue()));
            Doorlock3_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open"), "", GetDoorlock3_1OpenValue()));
            Doorlock3_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close"), "", GetDoorlock3_1CloseValue()));
            Doorlock3_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open"), "", GetDoorlock3_2OpenValue()));
            Doorlock3_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close"), "", GetDoorlock3_2CloseValue()));
            Doorlock4_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open"), "", GetDoorlock4_1OpenValue()));
            Doorlock4_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close"), "", GetDoorlock4_1CloseValue()));
            Doorlock4_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open"), "", GetDoorlock4_2OpenValue()));
            Doorlock4_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close"), "", GetDoorlock4_2CloseValue()));
            Doorlock5_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open"), "", GetDoorlock5_1OpenValue()));
            Doorlock5_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close"), "", GetDoorlock5_1CloseValue()));
            Doorlock5_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open"), "", GetDoorlock5_2OpenValue()));
            Doorlock5_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close"), "", GetDoorlock5_2CloseValue()));
            Doorlock6_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Open"), "", GetDoorlock6_1OpenValue()));
            Doorlock6_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Close"), "", GetDoorlock6_1CloseValue()));
            Doorlock6_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Open"), "", GetDoorlock6_2OpenValue()));
            Doorlock6_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Close"), "", GetDoorlock6_2CloseValue()));

            Door1_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open"), "", GetDoor1_OpenValue()));
            Door1_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close"), "", GetDoor1_CloseValue()));
            Door2_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open"), "", GetDoor2_OpenValue()));
            Door2_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close"), "", GetDoor2_CloseValue()));
            Door3_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open"), "", GetDoor3_OpenValue()));
            Door3_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close"), "", GetDoor3_CloseValue()));
            Door4_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open"), "", GetDoor4_OpenValue()));
            Door4_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close"), "", GetDoor4_CloseValue()));
            Door5_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open"), "", GetDoor5_OpenValue()));
            Door5_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close"), "", GetDoor5_CloseValue()));
            Door6_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open"), "", GetDoor6_OpenValue()));
            Door6_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close"), "", GetDoor6_CloseValue()));

            Doorlock1_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Opening"), "", GetDoorlock1_OpeningValue()));
            Doorlock1_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Closing"), "", GetDoorlock1_ClosingValue()));
            Doorlock2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening"), "", GetDoorlock2_OpeningValue()));
            Doorlock2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing"), "", GetDoorlock2_ClosingValue()));
            Doorlock3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening"), "", GetDoorlock3_OpeningValue()));
            Doorlock3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing"), "", GetDoorlock3_ClosingValue()));
            Doorlock4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening"), "", GetDoorlock4_OpeningValue()));
            Doorlock4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing"), "", GetDoorlock4_ClosingValue()));
            Doorlock5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening"), "", GetDoorlock5_OpeningValue()));
            Doorlock5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing"), "", GetDoorlock5_ClosingValue()));
            Doorlock6_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Opening"), "", GetDoorlock6_OpeningValue()));
            Doorlock6_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Closing"), "", GetDoorlock6_ClosingValue()));


            Door1_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening"), "", GetDoor1_OpeningValue()));
            Door1_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing"), "", GetDoor1_ClosingValue()));
            Door2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening"), "", GetDoor2_OpeningValue()));
            Door2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing"), "", GetDoor2_ClosingValue()));
            Door3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening"), "", GetDoor3_OpeningValue()));
            Door3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing"), "", GetDoor3_ClosingValue()));
            Door4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening"), "", GetDoor4_OpeningValue()));
            Door4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing"), "", GetDoor4_ClosingValue()));
            Door5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening"), "", GetDoor5_OpeningValue()));
            Door5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing"), "", GetDoor5_ClosingValue()));
            Door6_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening"), "", GetDoor6_OpeningValue()));
            Door6_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing"), "", GetDoor6_ClosingValue()));

            S3_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running"), "", GetS3_DC3_RunningValue()));
            S3_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running"), "", GetS3_DC2_RunningValue()));
            S3_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running"), "", GetS3_DC1_RunningValue()));
           
            S2_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running"), "", GetS2_DC3_RunningValue()));
            S2_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running"), "", GetS2_DC2_RunningValue()));
            S2_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running"), "", GetS2_DC1_RunningValue()));
            S1_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running"), "", GetS1_DC3_RunningValue()));
            S1_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running"), "", GetS1_DC2_RunningValue()));
            S1_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running"), "", GetS1_DC1_RunningValue()));


            S3_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote"), "", GetS3RemoteValue()));
            S3_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local"), "", GetS3LocalValue()));
            S3_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto"), "", GetS3AutoValue()));
            S3_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man"), "", GetS3ManValue()));
            S3_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop"), "", GetS3LocalStopValue()));
            S2_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote"), "", GetS2RemoteValue()));
            S2_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local"), "", GetS2LocalValue()));
            S2_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto"), "", GetS2AutoValue()));
            S2_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man"), "", GetS2ManValue()));
            S2_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop"), "", GetS2LocalStopValue()));
            S1_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote") , "", GetS1RemoteValue()));                
            S1_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local"), "", GetS1LocalValue()));               
            S1_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto"), "", GetS1AutoValue()));
            S1_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man"), "", GetS1ManValue()));
            S1_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop"), "", GetS1LocalStopValue()));
        }
        
        private string prevS1Remote = "0", prevS1Local = "0", prevS1Auto = "0", prevS1Man = "0", prevS1LocalStop = "0"; // biến lưu trạng thái trước đó
        private string prevS2Remote = "0", prevS2Local = "0", prevS2Auto = "0", prevS2Man = "0", prevS2LocalStop = "0"; // biến lưu trạng thái trước đó
        private string prevS3Remote = "0", prevS3Local = "0", prevS3Auto = "0", prevS3Man = "0", prevS3LocalStop = "0"; // biến lưu trạng thái trước đó
        private string prevS1_DC1_Running = "0", prevS1_DC2_Running = "0", prevS1_DC3_Running = "0"; // biến lưu trạng thái trước đó
        private string prevS2_DC1_Running = "0", prevS2_DC2_Running = "0", prevS2_DC3_Running = "0"; // biến lưu trạng thái trước đó
        private string prevS3_DC1_Running = "0", prevS3_DC2_Running = "0", prevS3_DC3_Running = "0"; // biến lưu trạng thái trước đó
        private string prevDoor1_Opening = "0", prevDoor1_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoor2_Opening = "0", prevDoor2_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoor3_Opening = "0", prevDoor3_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoor4_Opening = "0", prevDoor4_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoor5_Opening = "0", prevDoor5_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoor6_Opening = "0", prevDoor6_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock1_Opening = "0", prevDoorlock1_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock2_Opening = "0", prevDoorlock2_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock3_Opening = "0", prevDoorlock3_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock4_Opening = "0", prevDoorlock4_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock5_Opening = "0", prevDoorlock5_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock6_Opening = "0", prevDoorlock6_Closing = "0"; // biến lưu trạng thái trước đó
        private string prevDoor1_Open = "0", prevDoor1_Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoor2_Open = "0", prevDoor2_Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoor3_Open = "0", prevDoor3_Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoor4_Open = "0", prevDoor4_Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoor5_Open = "0", prevDoor5_Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoor6_Open = "0", prevDoor6_Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock1_1Open = "0", prevDoorlock1_1Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock1_2Open = "0", prevDoorlock1_2Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock2_1Open = "0", prevDoorlock2_1Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock2_2Open = "0", prevDoorlock2_2Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock3_1Open = "0", prevDoorlock3_1Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock3_2Open = "0", prevDoorlock3_2Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock4_1Open = "0", prevDoorlock4_1Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock4_2Open = "0", prevDoorlock4_2Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock5_1Open = "0", prevDoorlock5_1Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock5_2Open = "0", prevDoorlock5_2Close = "0"; //
        private string prevDoorlock6_1Open = "0", prevDoorlock6_1Close = "0"; // biến lưu trạng thái trước đó
        private string prevDoorlock6_2Open = "0", prevDoorlock6_2Close = "0"; // biến lưu trạng thái trước đó
        private string prevS1_DC1_Overload = "0", prevS1_DC2_Overload = "0", prevS1_DC3_Overload = "0"; // biến lưu trạng thái trước đó 
        private string prevS2_DC1_Overload = "0", prevS2_DC2_Overload = "0", prevS2_DC3_Overload = "0"; // biến lưu trạng thái trước đó
        private string prevS3_DC1_Overload = "0", prevS3_DC2_Overload = "0", prevS3_DC3_Overload = "0"; // biến lưu trạng thái trước đó
        private string prevDoor1_PressureHigh = "0", prevDoor1_PressureLow = "0"; // biến lưu trạng thái trước đó
        private string prevDoor2_PressureHigh = "0", prevDoor2_PressureLow = "0"; // biến lưu trạng thái trước đó
        private string prevDoor3_PressureHigh = "0", prevDoor3_PressureLow = "0"; // biến lưu trạng thái trước đó
        private string prevDoor4_PressureHigh = "0", prevDoor4_PressureLow = "0"; // biến lưu trạng thái trước đó
        private string prevDoor5_PressureHigh = "0", prevDoor5_PressureLow = "0"; // biến lưu trạng thái trước đó
        private string prevDoor6_PressureHigh = "0", prevDoor6_PressureLow = "0"; // biến lưu trạng thái trước đó




       private void Door1_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door1_PressureHighChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor1_PressureHigh == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    Door1_PressureHigh = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor1_PressureHigh = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door1_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door1_PressureLowChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor1_PressureLow == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    Door1_PressureLow = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor1_PressureLow = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door2_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door2_PressureHighChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor2_PressureHigh == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    Door2_PressureHigh = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor2_PressureHigh = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door2_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door2_PressureLowChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor2_PressureLow == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    Door2_PressureLow = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor2_PressureLow = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door3_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door3_PressureHighChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor3_PressureHigh == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    Door3_PressureHigh = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor3_PressureHigh = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door3_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door3_PressureLowChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor3_PressureLow == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    Door3_PressureLow = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor3_PressureLow = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door4_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door4_PressureHighChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor4_PressureHigh == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    Door4_PressureHigh = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor4_PressureHigh = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door4_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door4_PressureLowChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor4_PressureLow == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    Door4_PressureLow = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor4_PressureLow = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door5_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door5_PressureHighChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor5_PressureHigh == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    Door5_PressureHigh = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor5_PressureHigh = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door5_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door5_PressureLowChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor5_PressureLow == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    Door5_PressureLow = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor5_PressureLow = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door6_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door6_PressureHighChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor6_PressureHigh == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    Door6_PressureHigh = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor6_PressureHigh = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door6_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door6_PressureLowChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor6_PressureLow == "0" && e.NewValue == "1")
            {
                // Tạo object DataAlarmModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    Door6_PressureLow = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevDoor6_PressureLow = e.NewValue; // Cập nhật trạng thái trước
        }


        private void S1_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S1_DC1_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_DC1_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    S1_DC1_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS1_DC1_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S1_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S1_DC2_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_DC2_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    S1_DC2_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS1_DC2_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S1_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S1_DC3_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_DC3_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 1",
                    S1_DC3_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS1_DC3_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S2_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S2_DC1_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_DC1_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    S2_DC1_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS2_DC1_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S2_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S2_DC2_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_DC2_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    S2_DC2_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS2_DC2_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S2_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S2_DC3_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_DC3_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 2",
                    S2_DC3_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS2_DC3_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S3_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S3_DC1_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_DC1_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    S3_DC1_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS3_DC1_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S3_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S3_DC2_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_DC2_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    S3_DC2_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS3_DC2_Overload = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S3_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S3_DC3_OverChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_DC3_Overload == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Area = "Trạm 3",
                    S3_DC3_Over = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            prevS3_DC3_Overload = e.NewValue; // Cập nhật trạng thái trước
        }


        private void Doorlock1_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock1_1OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock1_1Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock1_1Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock1_1Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock1_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock1_1CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock1_1Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock1_1Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock1_1Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock1_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock1_2OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock1_2Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock1_2Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock1_2Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock1_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock1_2CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock1_2Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock1_2Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock1_2Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock2_1OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock2_1Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock2_1Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock2_1Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock2_1CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock2_1Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock2_1Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock2_1Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock2_2OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock2_2Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock2_2Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock2_2Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock2_2CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock2_2Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock2_2Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock2_2Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock3_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock3_1OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock3_1Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock3_1Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock3_1Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock3_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock3_1CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock3_1Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock3_1Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock3_1Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock3_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock3_2OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock3_2Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock3_2Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock3_2Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock3_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock3_2CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock3_2Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock3_2Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock3_2Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock4_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock4_1OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock4_1Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock4_1Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock4_1Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock4_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock4_1CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock4_1Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock4_1Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock4_1Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock4_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock4_2OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock4_2Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock4_2Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock4_2Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock4_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock4_2CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock4_2Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock4_2Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock4_2Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock5_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock5_1OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock5_1Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock5_1Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock5_1Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock5_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock5_1CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock5_1Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock5_1Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock5_1Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock5_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock5_2OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock5_2Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock5_2Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock5_2Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock5_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock5_2CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock5_2Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock5_2Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock5_2Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock6_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock6_1OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock6_1Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock6_1Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock6_1Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock6_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock6_1CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock6_1Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock6_1Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock6_1Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock6_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock6_2OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock6_2Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock6_2Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock6_2Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock6_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock6_2CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock6_2Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock6_2Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock6_2Close = e.NewValue; // Cập nhật trạng thái trước
        }


        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door1_OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor1_Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door1_Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor1_Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door1_CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor1_Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door1_Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor1_Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door2_OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor2_Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door2_Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor2_Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door2_CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor2_Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door2_Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor2_Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door3_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door3_OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor3_Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door3_Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor3_Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door3_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door3_CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor3_Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door3_Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor3_Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door4_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door4_OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor4_Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door4_Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor4_Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door4_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door4_CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor4_Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door4_Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor4_Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door5_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door5_OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor5_Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door5_Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor5_Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door5_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door5_CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor5_Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door5_Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor5_Close = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door6_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door6_OpenChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor6_Open == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door6_Open = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor6_Open = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door6_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door6_CloseChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor6_Close == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door6_Close = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor6_Close = e.NewValue; // Cập nhật trạng thái trước
        }

        private void Doorlock1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock1_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock1_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock1_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock1_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock1_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock1_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock1_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock1_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock2_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock2_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock2_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock2_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock2_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock2_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock2_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock2_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock3_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock3_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock3_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock3_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock3_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock3_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock3_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock3_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock4_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock4_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock4_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock4_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock4_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock4_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock4_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock4_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock5_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock5_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock5_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock5_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock5_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock5_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock5_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock5_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock6_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock6_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock6_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock6_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Doorlock6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Doorlock6_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoorlock6_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Doorlock6_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoorlock6_Closing = e.NewValue; // Cập nhật trạng thái trước
        }


        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door1_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor1_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door1_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor1_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door1_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor1_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door1_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor1_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door2_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor2_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door2_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor2_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door2_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor2_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door2_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor2_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door3_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor3_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door3_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor3_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door3_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor3_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door3_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor3_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door4_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor4_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door4_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor4_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door4_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor4_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door4_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor4_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door5_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor5_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door5_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor5_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door5_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor5_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door5_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor5_Closing = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door6_OpeningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor6_Opening == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door6_Opening = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor6_Opening = e.NewValue; // Cập nhật trạng thái trước
        }
        private void Door6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Door6_ClosingChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevDoor6_Closing == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Door6_Closing = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevDoor6_Closing = e.NewValue; // Cập nhật trạng thái trước
        }


        
        private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 1. Cập nhật CurrentDataTran
            SQLLogin.UpdateTagValue("S1_DC1_Running", e.NewValue);

            // 2. Tạo event cho Form khác nếu cần
            S1_DC1_RunningChanged?.Invoke(this, e);

            // 3. Nếu từ 0 -> 1 thì insert
            if (prevS1_DC1_Running == "0" && e.NewValue == "1")
            {
                SQLLogin.CurrentDataTran.CreateAt = DateTime.Now;
                SQLLogin.InsertAllTagsToSQL(SQLLogin.CurrentDataTran);
            }
            prevS1_DC1_Running = e.NewValue;

            // 4. Update DataGridView
         //  UpdateDataGridViewRow("Bơm 1 Đang Chạy", e.NewValue);
        }

        private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 1. Cập nhật CurrentDataTran
            SQLLogin.UpdateTagValue("S1_DC2_Running", e.NewValue);

            // 2. Tạo event cho Form khác nếu cần
            S1_DC2_RunningChanged?.Invoke(this, e);

            // 3. Nếu từ 0 -> 1 thì insert
            if (prevS1_DC2_Running == "0" && e.NewValue == "1")
            {
                SQLLogin.CurrentDataTran.CreateAt = DateTime.Now;
                SQLLogin.InsertAllTagsToSQL(SQLLogin.CurrentDataTran);
            }
            prevS1_DC2_Running = e.NewValue;

        }
        private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 1. Luôn update CurrentDataTran
            SQLLogin.UpdateTagValue("S1_DC3_Running", e.NewValue);
            S1_DC3_RunningChanged?.Invoke(this, e);
            // 2. Chỉ insert khi rising edge (0 -> 1)
            if (prevS1_DC3_Running == "0" && e.NewValue == "1")
            {
                SQLLogin.CurrentDataTran.CreateAt = DateTime.Now;
                SQLLogin.InsertAllTagsToSQL(SQLLogin.CurrentDataTran);
            }
            prevS1_DC3_Running = e.NewValue;
        }
        private void S2_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S2_DC1_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_DC1_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_DC1_Running = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevS2_DC1_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S2_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S2_DC2_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_DC2_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_DC2_Running = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevS2_DC2_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S2_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S2_DC3_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_DC3_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_DC3_Running = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevS2_DC3_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S3_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S3_DC1_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_DC1_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_DC1_Running = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevS3_DC1_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S3_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S3_DC2_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_DC2_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_DC2_Running = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevS3_DC2_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S3_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            S3_DC3_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_DC3_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_DC3_Running = e.NewValue,
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            prevS3_DC3_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S1_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {  
  //          this.Invoke((MethodInvoker)(() =>
  //          {
  ////              button1.BackColor = e.NewValue == "1" ? Color.GreenYellow : DefaultBackColor; label1.Text = $"Đang Mở: {e.NewValue}";
  //          }));
            // 🔔 Raise event để form khác nhận
            S1RemoteChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Remote == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S1_Remote = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS1Remote = e.NewValue;
        }
        private void S1_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //this.Invoke((MethodInvoker)(() =>
            //{
            //    //              button1.BackColor = e.NewValue == "1" ? Color.GreenYellow : DefaultBackColor; label1.Text = $"Đang Mở: {e.NewValue}";
            //}));
            // 🔔 Raise event để form khác nhận
            S1LocalChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Local == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S1_Local = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS1Local = e.NewValue;
        }
        private void S1_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //this.Invoke((MethodInvoker)(() =>
            //{
            //    //              button1.BackColor = e.NewValue == "1" ? Color.GreenYellow : DefaultBackColor; label1.Text = $"Đang Mở: {e.NewValue}";
            //}));
            // 🔔 Raise event để form khác nhận
            S1AutoChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Auto == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S1_Auto = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS1Auto = e.NewValue;
        }
        private void S1_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //this.Invoke((MethodInvoker)(() =>
            //{
            ////    button4.BackColor = e.NewValue == "1" ? Color.GreenYellow : DefaultBackColor;
            //}));

            S1ManChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Man == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S1_Man = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS1Man = e.NewValue;
        }
        private void S1_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //this.Invoke((MethodInvoker)(() =>
            //{
            // //   button5.BackColor = e.NewValue == "1" ? Color.GreenYellow : DefaultBackColor;
            //}));

            S1LocalStopChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1LocalStop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S1_Local_Stop = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS1LocalStop = e.NewValue;
        }
        private void S2_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {        
            // 🔔 Raise event để form khác nhận
            S2RemoteChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2Remote == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_Remote = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS2Remote = e.NewValue;
        }
        private void S2_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {         
            // 🔔 Raise event để form khác nhận
            S2LocalChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2Local == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_Local = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS2Local = e.NewValue;
        }
        private void S2_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {         
            // 🔔 Raise event để form khác nhận
            S2AutoChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2Auto == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_Auto = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS2Auto = e.NewValue;
        }
        private void S2_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {         
            S2ManChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2Man == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_Man = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS2Man = e.NewValue;
        }
        private void S2_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {          
            S2LocalStopChanged?.Invoke(this, e);
            if (prevS2LocalStop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S2_Local_Stop = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS2LocalStop = e.NewValue;
        }
        private void S3_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {       
            S3RemoteChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3Remote == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_Remote = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS3Remote = e.NewValue;
        }
        private void S3_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {         
            S3LocalChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3Local == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_Local = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS3Local = e.NewValue;
        }
        private void S3_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {        
            S3AutoChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3Auto == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_Auto = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS3Auto = e.NewValue;
        }
        private void S3_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {           
            S3ManChanged?.Invoke(this, e);
            if (prevS3Man == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_Man = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS3Man = e.NewValue;
        }
        private void S3_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {         
            S3LocalStopChanged?.Invoke(this, e);
            if (prevS3LocalStop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    S3_Local_Stop = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLogin.InsertAllTagsToSQL(model);
            }
            // Cập nhật trạng thái trước
            prevS3LocalStop = e.NewValue;
        }


        #region GetCurrentValue
        public string GetDoor1_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh").Value;
        }
        public string GetDoor1_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow").Value;
        }
        public string GetDoor2_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh").Value;
        }
        public string GetDoor2_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow").Value;
        }
        public string GetDoor3_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh").Value;
        }
        public string GetDoor3_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow").Value;
        }
        public string GetDoor4_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh").Value;
        }
        public string GetDoor4_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow").Value;
        }
        public string GetDoor5_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh").Value;
        }
        public string GetDoor5_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow").Value;
        }
        public string GetDoor6_PressureHighValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh").Value;
        }
        public string GetDoor6_PressureLowValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow").Value;
        }
        public string GetS1_DC1_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over").Value;
        }
        public string GetS1_DC2_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over").Value;
        }
        public string GetS1_DC3_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over").Value;
        }
        public string GetS2_DC1_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over").Value;
        }
        public string GetS2_DC2_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over").Value;
        }
        public string GetS2_DC3_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over").Value;
        }
        public string GetS3_DC1_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over").Value;
        }
        public string GetS3_DC2_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over").Value;
        }
        public string GetS3_DC3_OverValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over").Value;
        }

        public string GetDoorlock1_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Open").Value;
        }
        public string GetDoorlock1_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close").Value;
        }
        public string GetDoorlock1_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Open").Value;
        }
        public string GetDoorlock1_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_2Close").Value;
        }
        public string GetDoorlock2_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").Value;
        }
        public string GetDoorlock2_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").Value;
        }
        public string GetDoorlock2_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").Value;
        }
        public string GetDoorlock2_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").Value;
        }
        public string GetDoorlock3_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open").Value;
        }
        public string GetDoorlock3_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close").Value;
        }
        public string GetDoorlock3_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open").Value;
        }
        public string GetDoorlock3_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close").Value;
        }
        public string GetDoorlock4_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open").Value;
        }
        public string GetDoorlock4_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close").Value;
        }
        public string GetDoorlock4_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open").Value;
        }
        public string GetDoorlock4_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close").Value;
        }
        public string GetDoorlock5_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open").Value;
        }
        public string GetDoorlock5_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close").Value;
        }
        public string GetDoorlock5_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open").Value;
        }
        public string GetDoorlock5_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close").Value;
        }
        public string GetDoorlock6_1OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Open").Value;
        }
        public string GetDoorlock6_1CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_1Close").Value;
        }
        public string GetDoorlock6_2OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Open").Value;
        }
        public string GetDoorlock6_2CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_2Close").Value;
        }
        public string GetDoor1_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").Value;
        }
        public string GetDoor1_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").Value;
        }
        public string GetDoor2_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").Value;
        }
        public string GetDoor2_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").Value;
        }
        public string GetDoor3_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open").Value;
        }
        public string GetDoor3_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close").Value;
        }
        public string GetDoor4_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open").Value;
        }
        public string GetDoor4_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close").Value;
        }
        public string GetDoor5_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open").Value;
        }
        public string GetDoor5_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close").Value;
        }
        public string GetDoor6_OpenValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open").Value;
        }
        public string GetDoor6_CloseValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close").Value;
        }

        public string GetDoorlock1_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Opening").Value;
        }
        public string GetDoorlock1_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_Closing").Value;
        }
        public string GetDoorlock2_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening").Value;
        }
        public string GetDoorlock2_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing").Value;
        }
        public string GetDoorlock3_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening").Value;
        }
        public string GetDoorlock3_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing").Value;
        }

        private void tm_login_Tick(object sender, EventArgs e)
        {
            try
            {
                var data = new DataVanHanhModel
                {
                    CreateAt = DateTime.Now,

                    HT_Cylinder1_1 = GetValue("Local Station/Channel1/Device1/HT_Cylinder1_1"),
                    HT_Cylinder1_2 = GetValue("Local Station/Channel1/Device1/HT_Cylinder1_2"),
                    HT_Cylinder2_1 = GetValue("Local Station/Channel1/Device1/HT_Cylinder2_1"),
                    HT_Cylinder2_2 = GetValue("Local Station/Channel1/Device1/HT_Cylinder2_2"),
                    HT_Cylinder3_1 = GetValue("Local Station/Channel1/Device1/HT_Cylinder3_1"),
                    HT_Cylinder3_2 = GetValue("Local Station/Channel1/Device1/HT_Cylinder3_2"),
                    HT_Cylinder4_1 = GetValue("Local Station/Channel1/Device1/HT_Cylinder4_1"),
                    HT_Cylinder4_2 = GetValue("Local Station/Channel1/Device1/HT_Cylinder4_2"),
                    HT_Cylinder5_1 = GetValue("Local Station/Channel1/Device1/HT_Cylinder5_1"),
                    HT_Cylinder5_2 = GetValue("Local Station/Channel1/Device1/HT_Cylinder5_2"),
                    HT_Cylinder6_1 = GetValue("Local Station/Channel1/Device1/HT_Cylinder6_1"),
                    HT_Cylinder6_2 = GetValue("Local Station/Channel1/Device1/HT_Cylinder6_2"),

                    Door1_Aperture = GetValue("Local Station/Channel1/Device1/Door1_Aperture"),
                    Door2_Aperture = GetValue("Local Station/Channel1/Device1/Door2_Aperture"),
                    Door3_Aperture = GetValue("Local Station/Channel1/Device1/Door3_Aperture"),
                    Door4_Aperture = GetValue("Local Station/Channel1/Device1/Door4_Aperture"),
                    Door5_Aperture = GetValue("Local Station/Channel1/Device1/Door5_Aperture"),
                    Door6_Aperture = GetValue("Local Station/Channel1/Device1/Door6_Aperture"),

                    Temp_Oil1 = GetValue("Local Station/Channel1/Device1/Temp_Oil1"),
                    Temp_Oil2 = GetValue("Local Station/Channel1/Device1/Temp_Oil2"),
                    Temp_Oil3 = GetValue("Local Station/Channel1/Device1/Temp_Oil3"),

                    Fllow_Door1 = GetValue("Local Station/Channel1/Device1/Fllow_Door1"),
                    Fllow_Door2 = GetValue("Local Station/Channel1/Device1/Fllow_Door2"),
                    Fllow_Door3 = GetValue("Local Station/Channel1/Device1/Fllow_Door3"),
                    Fllow_Door4 = GetValue("Local Station/Channel1/Device1/Fllow_Door4"),
                    Fllow_Door5 = GetValue("Local Station/Channel1/Device1/Fllow_Door5"),
                    Fllow_Door6 = GetValue("Local Station/Channel1/Device1/Fllow_Door6"),

                    Total_Fllow = GetValue("Local Station/Channel1/Device1/Total_Fllow"),
                    Fllow_Ho = GetValue("Local Station/Channel1/Device1/Fllow_Ho"),
                    Fllow_DauTieng = GetValue("Local Station/Channel1/Device1/Fllow_DauTieng"),
                    Fllow_BenSuc = GetValue("Local Station/Channel1/Device1/Fllow_BenSuc"),
                    Fllow_SonDai = GetValue("Local Station/Channel1/Device1/Fllow_SonDai")
                };

                SQLLoginDataTran.InsertDataTran(data);
                Console.WriteLine($"✅ Đã ghi DataVanHanh lúc {data.CreateAt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi ghi DataVanHanh: {ex.Message}");
            }
        }

        

        public string GetDoorlock4_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening").Value;
        }
        public string GetDoorlock4_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing").Value;
        }
        public string GetDoorlock5_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening").Value;
        }
        public string GetDoorlock5_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing").Value;
        }
        public string GetDoorlock6_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Opening").Value;
        }
        public string GetDoorlock6_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock6_Closing").Value;
        }
        public string GetDoor1_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").Value;
        }
        public string GetDoor1_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").Value;
        }
        public string GetDoor2_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").Value;
        }
        public string GetDoor2_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").Value;
        }
        public string GetDoor3_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening").Value;
        }
        public string GetDoor3_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing").Value;
        }
        public string GetDoor4_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening").Value;
        }
        public string GetDoor4_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing").Value;
        }
        public string GetDoor5_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening").Value;
        }
        public string GetDoor5_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing").Value;
        }
        public string GetDoor6_OpeningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening").Value;
        }
        public string GetDoor6_ClosingValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing").Value;
        }

        public string GetS3_DC3_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running").Value;
        }
        public string GetS3_DC2_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running").Value;
        }
        public string GetS3_DC1_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running").Value;
        }
        public string GetS2_DC3_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running").Value;
        }
        public string GetS2_DC2_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running").Value;
        }
        public string GetS2_DC1_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running").Value;
        }
        public string GetS1_DC3_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running").Value;
        }
        public string GetS1_DC2_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running").Value;
        }
        public string GetS1_DC1_RunningValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running").Value;
        }
        public string GetS1RemoteValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote").Value;
        }
        public string GetS1LocalValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local").Value;
        }
        public string GetS1AutoValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto").Value;
        }
        public string GetS1ManValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man").Value;
        }
        public string GetS1LocalStopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop").Value;
        }
        public string GetS2RemoteValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote").Value;
        }
        public string GetS2LocalValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local").Value;
        }
        public string GetS2AutoValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto").Value;
        }
        public string GetS2ManValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man").Value;
        }
        public string GetS2LocalStopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop").Value;
        }
        public string GetS3RemoteValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote").Value;
        }
        public string GetS3LocalValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local").Value;
        }
        public string GetS3AutoValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto").Value;
        }
        public string GetS3ManValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man").Value;
        }
        public string GetS3LocalStopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop").Value;
        }
        #endregion

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
            FrmTran data = new FrmTran(this);
            OpenFormInPanel(data, "Hệ thống tràn");
        }

        private void bnt_TramMN_Click(object sender, EventArgs e)
        {
            FrmMNGoogle mn = new FrmMNGoogle();
            OpenFormInPanel(mn, "Mức Nước");
        }

        private void bnt_TrangChu_Click(object sender, EventArgs e)
        {
          FrmHome H = new FrmHome(this);
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