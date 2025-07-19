using Ahd.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
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
        public event EventHandler<TagValueChangedEventArgs> S1_Station_RunChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_Station_StopChanged;
        public event EventHandler<TagValueChangedEventArgs> S1_Station_AlarmChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_Station_RunChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_Station_StopChanged;
        public event EventHandler<TagValueChangedEventArgs> S2_Station_AlarmChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_Station_RunChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_Station_StopChanged;
        public event EventHandler<TagValueChangedEventArgs> S3_Station_AlarmChanged;

        public static DataTranModel CurrentDataTran = null;
        private Form currentChildForm = null;
     //   List<string> stations = new List<string> { "F01877", "F01203", "F01849" };
        private Timer _timer;
       

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
            _timer.Interval = 5000; // 5 giây test, thực tế đặt 5 * 60 * 1000 = 5 phút
            _timer.Tick += async (s, e) => await Timer_Tick();
            _timer.Start();
            
        }
        
        private async Task Timer_Tick()
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

                    // Ghi từng trạm xuống PLC
                    foreach (var entry in stationValues)
                    {
                        string stationCode = entry.Key;
                        string value = entry.Value;

                        // Mapping stationCode to actual PLC tag name
                        string tagName = "";
                        switch (stationCode)
                        {
                            case "F01877":
                                tagName = "Fllow_SonDai";
                                break;
                            case "F01203":
                                tagName = "Fllow_BenSuc";
                                break;
                            case "F01849":
                                tagName = "Fllow_DauTieng";
                                break;
                        }

                        if (!string.IsNullOrEmpty(tagName))
                        {
                            // Ghi xuống PLC
                            await ahdDriverConnector1.WriteTagAsync(
                                $"Local Station/DauTieng/S71500/API/{tagName}",
                                value,
                                WritePiority.High
                            );

                            AppendLog($"✅ Ghi PLC: {tagName} = {value}");
                        }
                        else
                        {
                            AppendLog($"❌ Không tìm thấy tag mapping cho station {stationCode}");
                        }
                    }
                }
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
            tm_loginMN.Interval = 10000;
            tm_loginMN.Enabled = true;
            tm_loginMN.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { tm_loginMN.Start(); });
                t.Enabled = true;
            };
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
            // Alarm các tag cần thiết
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Run").ValueChanged += S1_Station_Run_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Stop").ValueChanged += S1_Station_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Alarm").ValueChanged += S1_Station_Alarm_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Run").ValueChanged += S2_Station_Run_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Stop").ValueChanged += S2_Station_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Alarm").ValueChanged += S2_Station_Alarm_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Run").ValueChanged += S3_Station_Run_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Stop").ValueChanged += S3_Station_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Alarm").ValueChanged += S3_Station_Alarm_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Remote").ValueChanged += S1_Remote_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local").ValueChanged += S1_Local_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Auto").ValueChanged += S1_Auto_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Man").ValueChanged += S1_Man_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local_Stop").ValueChanged += S1_Local_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Remote").ValueChanged += S2_Remote_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local").ValueChanged += S2_Local_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Auto").ValueChanged += S2_Auto_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Man").ValueChanged += S2_Man_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local_Stop").ValueChanged += S2_Local_Stop_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Remote").ValueChanged += S3_Remote_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local").ValueChanged += S3_Local_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Auto").ValueChanged += S3_Auto_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Man").ValueChanged += S3_Man_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local_Stop").ValueChanged += S3_Local_Stop_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Running").ValueChanged += S1_DC1_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Running").ValueChanged += S1_DC2_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Running").ValueChanged += S1_DC3_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Running").ValueChanged += S2_DC1_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Running").ValueChanged += S2_DC2_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Running").ValueChanged += S2_DC3_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Running").ValueChanged += S3_DC1_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Running").ValueChanged += S3_DC2_Running_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Running").ValueChanged += S3_DC3_Running_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Opening").ValueChanged += Door3_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Closing").ValueChanged += Door3_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Opening").ValueChanged += Door4_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Closing").ValueChanged += Door4_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Opening").ValueChanged += Door5_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Closing").ValueChanged += Door5_Closing_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Opening").ValueChanged += Door6_Opening_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Closing").ValueChanged += Door6_Closing_ValueChanged;//Su kien FrmTran

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Over").ValueChanged += S1_DC1_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Over").ValueChanged += S1_DC2_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Over").ValueChanged += S1_DC3_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Over").ValueChanged += S2_DC1_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Over").ValueChanged += S2_DC2_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Over").ValueChanged += S2_DC3_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Over").ValueChanged += S3_DC1_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Over").ValueChanged += S3_DC2_Over_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Over").ValueChanged += S3_DC3_Over_ValueChanged;//Su kien Alarm FrmTran

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureHigh").ValueChanged += Door1_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureLow").ValueChanged += Door1_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureHigh").ValueChanged += Door2_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureLow").ValueChanged += Door2_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureHigh").ValueChanged += Door3_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureLow").ValueChanged += Door3_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureHigh").ValueChanged += Door4_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureLow").ValueChanged += Door4_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureHigh").ValueChanged += Door5_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureLow").ValueChanged += Door5_PressureLow_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureHigh").ValueChanged += Door6_PressureHigh_ValueChanged;//Su kien Alarm FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureLow").ValueChanged += Door6_PressureLow_ValueChanged;//Su kien Alarm FrmTran

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Opening").ValueChanged += Doorlock1_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Closing").ValueChanged += Doorlock1_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Opening").ValueChanged += Doorlock2_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Closing").ValueChanged += Doorlock2_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Opening").ValueChanged += Doorlock3_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Closing").ValueChanged += Doorlock3_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Opening").ValueChanged += Doorlock4_Opening_ValueChanged; //Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Closing").ValueChanged += Doorlock4_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Opening").ValueChanged += Doorlock5_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Closing").ValueChanged += Doorlock5_Closing_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Opening").ValueChanged += Doorlock6_Opening_ValueChanged;//Su kien FrmHome
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Closing").ValueChanged += Doorlock6_Closing_ValueChanged;//Su kien FrmHome

            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Open").ValueChanged += Door1_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Close").ValueChanged += Door1_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Open").ValueChanged += Door2_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Close").ValueChanged += Door2_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Open").ValueChanged += Door3_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Close").ValueChanged += Door3_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Open").ValueChanged += Door4_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Close").ValueChanged += Door4_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Open").ValueChanged += Door5_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Close").ValueChanged += Door5_Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Open").ValueChanged += Door6_Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Close").ValueChanged += Door6_Close_ValueChanged;//Su kien FrmTran
            // Tag dự phòng
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Open").ValueChanged += Doorlock1_1Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Close").ValueChanged += Doorlock1_1Close_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Open").ValueChanged += Doorlock1_2Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Close").ValueChanged += Doorlock1_2Close_ValueChanged;//Su kien áp cửa 1 cao
            // End Tag dự phòng
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Open").ValueChanged += Doorlock3_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Close").ValueChanged += Doorlock3_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Open").ValueChanged += Doorlock3_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Close").ValueChanged += Doorlock3_2Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Open").ValueChanged += Doorlock4_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Close").ValueChanged += Doorlock4_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Open").ValueChanged += Doorlock4_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Close").ValueChanged += Doorlock4_2Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Open").ValueChanged += Doorlock5_1Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Close").ValueChanged += Doorlock5_1Close_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Open").ValueChanged += Doorlock5_2Open_ValueChanged;//Su kien FrmTran
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Close").ValueChanged += Doorlock5_2Close_ValueChanged;//Su kien FrmTran
            // Tag dự phòng
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Open").ValueChanged += Doorlock6_1Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Close").ValueChanged += Doorlock6_1Close_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Open").ValueChanged += Doorlock6_2Open_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Close").ValueChanged += Doorlock6_2Close_ValueChanged;//Su kien áp cửa 1 cao
                                                                                                                                 
           // Gọi lần đầu khi load form để publish trạng thái hiện tại
            PublishInitialValues();
        }
        private void PublishInitialValues()
        {
            S1_Station_Run_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Run"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Run"), "", GetS1_Station_RunValue()));
            S1_Station_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Stop"), "", GetS1_Station_StopValue()));
            S1_Station_Alarm_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Alarm"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Alarm"), "", GetS1_Station_AlarmValue()));
            S2_Station_Run_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Run"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Run"), "", GetS2_Station_RunValue()));
            S2_Station_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Stop"), "", GetS2_Station_StopValue()));
            S2_Station_Alarm_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Alarm"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Alarm"), "", GetS2_Station_AlarmValue()));
            S3_Station_Run_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Run"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Run"), "", GetS3_Station_RunValue()));
            S3_Station_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Stop"), "", GetS3_Station_StopValue()));
            S3_Station_Alarm_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Alarm"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Alarm"), "", GetS3_Station_AlarmValue()));

            Door1_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureHigh"), "", GetDoor1_PressureHighValue()));
            Door1_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_PressureLow"), "", GetDoor1_PressureLowValue()));
            Door2_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureHigh"), "", GetDoor2_PressureHighValue()));
            Door2_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_PressureLow"), "", GetDoor2_PressureLowValue()));
            Door3_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureHigh"), "", GetDoor3_PressureHighValue()));
            Door3_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_PressureLow"), "", GetDoor3_PressureLowValue()));
            Door4_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureHigh"), "", GetDoor4_PressureHighValue()));
            Door4_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_PressureLow"), "", GetDoor4_PressureLowValue()));
            Door5_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureHigh"), "", GetDoor5_PressureHighValue()));
            Door5_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_PressureLow"), "", GetDoor5_PressureLowValue()));
            Door6_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureHigh"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureHigh"), "", GetDoor6_PressureHighValue()));
            Door6_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureLow"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_PressureLow"), "", GetDoor6_PressureLowValue()));

            S1_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Over"), "", GetS1_DC1_OverValue()));
            S1_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Over"), "", GetS1_DC2_OverValue()));
            S1_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Over"), "", GetS1_DC3_OverValue()));
            S2_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Over"), "", GetS2_DC1_OverValue()));
            S2_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Over"), "", GetS2_DC2_OverValue()));
            S2_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Over"), "", GetS2_DC3_OverValue()));
            S3_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Over"), "", GetS3_DC1_OverValue()));
            S3_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Over"), "", GetS3_DC2_OverValue()));
            S3_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Over"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Over"), "", GetS3_DC3_OverValue()));
           
            Doorlock1_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Open"), "", GetDoorlock1_1OpenValue()));
            Doorlock1_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_1Close"), "", GetDoorlock1_1CloseValue()));
            Doorlock1_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Open"), "", GetDoorlock1_2OpenValue()));
            Doorlock1_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_2Close"), "", GetDoorlock1_2CloseValue()));
            Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Open"), "", GetDoorlock2_1OpenValue()));
            Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_1Close"), "", GetDoorlock2_1CloseValue()));
            Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Open"), "", GetDoorlock2_2OpenValue()));
            Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_2Close"), "", GetDoorlock2_2CloseValue()));
            Doorlock3_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Open"), "", GetDoorlock3_1OpenValue()));
            Doorlock3_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_1Close"), "", GetDoorlock3_1CloseValue()));
            Doorlock3_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Open"), "", GetDoorlock3_2OpenValue()));
            Doorlock3_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_2Close"), "", GetDoorlock3_2CloseValue()));
            Doorlock4_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Open"), "", GetDoorlock4_1OpenValue()));
            Doorlock4_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_1Close"), "", GetDoorlock4_1CloseValue()));
            Doorlock4_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Open"), "", GetDoorlock4_2OpenValue()));
            Doorlock4_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_2Close"), "", GetDoorlock4_2CloseValue()));
            Doorlock5_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Open"), "", GetDoorlock5_1OpenValue()));
            Doorlock5_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_1Close"), "", GetDoorlock5_1CloseValue()));
            Doorlock5_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Open"), "", GetDoorlock5_2OpenValue()));
            Doorlock5_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_2Close"), "", GetDoorlock5_2CloseValue()));
            Doorlock6_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Open"), "", GetDoorlock6_1OpenValue()));
            Doorlock6_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_1Close"), "", GetDoorlock6_1CloseValue()));
            Doorlock6_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Open"), "", GetDoorlock6_2OpenValue()));
            Doorlock6_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_2Close"), "", GetDoorlock6_2CloseValue()));

            Door1_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Open"), "", GetDoor1_OpenValue()));
            Door1_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Close"), "", GetDoor1_CloseValue()));
            Door2_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Open"), "", GetDoor2_OpenValue()));
            Door2_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Close"), "", GetDoor2_CloseValue()));
            Door3_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Open"), "", GetDoor3_OpenValue()));
            Door3_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Close"), "", GetDoor3_CloseValue()));
            Door4_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Open"), "", GetDoor4_OpenValue()));
            Door4_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Close"), "", GetDoor4_CloseValue()));
            Door5_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Open"), "", GetDoor5_OpenValue()));
            Door5_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Close"), "", GetDoor5_CloseValue()));
            Door6_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Open"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Open"), "", GetDoor6_OpenValue()));
            Door6_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Close"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Close"), "", GetDoor6_CloseValue()));

            Doorlock1_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Opening"), "", GetDoorlock1_OpeningValue()));
            Doorlock1_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock1_Closing"), "", GetDoorlock1_ClosingValue()));
            Doorlock2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Opening"), "", GetDoorlock2_OpeningValue()));
            Doorlock2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Doorlock2_Closing"), "", GetDoorlock2_ClosingValue()));
            Doorlock3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Opening"), "", GetDoorlock3_OpeningValue()));
            Doorlock3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock3_Closing"), "", GetDoorlock3_ClosingValue()));
            Doorlock4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Opening"), "", GetDoorlock4_OpeningValue()));
            Doorlock4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Doorlock4_Closing"), "", GetDoorlock4_ClosingValue()));
            Doorlock5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Opening"), "", GetDoorlock5_OpeningValue()));
            Doorlock5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock5_Closing"), "", GetDoorlock5_ClosingValue()));
            Doorlock6_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Opening"), "", GetDoorlock6_OpeningValue()));
            Doorlock6_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Doorlock6_Closing"), "", GetDoorlock6_ClosingValue()));

            Door1_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Opening"), "", GetDoor1_OpeningValue()));
            Door1_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door1_Closing"), "", GetDoor1_ClosingValue()));
            Door2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Opening"), "", GetDoor2_OpeningValue()));
            Door2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Door2_Closing"), "", GetDoor2_ClosingValue()));           
            Door3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Opening"), "", GetDoor3_OpeningValue()));
            Door3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door3_Closing"), "", GetDoor3_ClosingValue()));
            Door4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Opening"), "", GetDoor4_OpeningValue()));
            Door4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Door4_Closing"), "", GetDoor4_ClosingValue()));         
            Door5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Opening"), "", GetDoor5_OpeningValue()));
            Door5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door5_Closing"), "", GetDoor5_ClosingValue()));
            Door6_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Opening"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Opening"), "", GetDoor6_OpeningValue()));
            Door6_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Closing"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Door6_Closing"), "", GetDoor6_ClosingValue()));

            S3_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC3_Running"), "", GetS3_DC3_RunningValue()));
            S3_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC2_Running"), "", GetS3_DC2_RunningValue()));
            S3_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/DC1_Running"), "", GetS3_DC1_RunningValue()));          
            S2_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC3_Running"), "", GetS2_DC3_RunningValue()));
            S2_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC2_Running"), "", GetS2_DC2_RunningValue()));
            S2_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/DC1_Running"), "", GetS2_DC1_RunningValue()));
            S1_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC3_Running"), "", GetS1_DC3_RunningValue()));
            S1_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC2_Running"), "", GetS1_DC2_RunningValue()));
            S1_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Running"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/DC1_Running"), "", GetS1_DC1_RunningValue()));

            // đang cấuu hình
            S3_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Remote"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Remote"), "", GetS3RemoteValue()));
            S3_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local"), "", GetS3LocalValue()));
            S3_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Auto"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Auto"), "", GetS3AutoValue()));
            S3_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Man"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Man"), "", GetS3ManValue()));
            S3_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Local_Stop"), "", GetS3LocalStopValue()));
            //
            S2_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Remote"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Remote"), "", GetS2RemoteValue()));
            S2_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local"), "", GetS2LocalValue()));
            S2_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Auto"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Auto"), "", GetS2AutoValue()));
            S2_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Man"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Man"), "", GetS2ManValue()));
            S2_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Local_Stop"), "", GetS2LocalStopValue()));
            S1_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Remote"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Remote") , "", GetS1RemoteValue()));                
            S1_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local"), "", GetS1LocalValue()));               
            S1_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Auto"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Auto"), "", GetS1AutoValue()));
            S1_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Man"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Man"), "", GetS1ManValue()));
            S1_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local_Stop"),
                new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Local_Stop"), "", GetS1LocalStopValue()));
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
        private string prevS1_Station_Run = "0", prevS2_Station_Run = "0", prevS3_Station_Run = "0"; // biến lưu trạng thái trước đó
        private string prevS1_Station_Stop = "0", prevS2_Station_Stop = "0", prevS3_Station_Stop = "0"; // biến lưu trạng thái trước đó
        private string prevS3_Station_Alarm = "0", prevS2_Station_Alarm = "0", prevS1_Station_Alarm = "0"; // biến lưu trạng thái trước đó

    
        private void S1_Station_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S1_Station_RunChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_Station_Run == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Trạm 1 Chạy", // ✅ Tên tag
                    S1_Station_Run = e.NewValue,
                   
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1_Station_Run = e.NewValue;
        }
        private void S2_Station_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S2_Station_RunChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_Station_Run == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 2",
                    TagName = "Trạm 2 Chạy", // ✅ Tên tag
                    S2_Station_Run = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS2_Station_Run = e.NewValue;
        }
        private void S3_Station_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S3_Station_RunChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_Station_Run == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 3",
                    TagName = "Trạm 3 Chạy", // ✅ Tên tag
                    S3_Station_Run = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS3_Station_Run = e.NewValue;
        }
        private void S1_Station_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S1_Station_StopChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_Station_Stop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Trạm 1 Dừng", // ✅ Tên tag
                    S1_Station_Stop = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1_Station_Stop = e.NewValue;
        }
        private void S2_Station_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S2_Station_StopChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_Station_Stop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 2",
                    TagName = "Trạm 2 Dừng", // ✅ Tên tag
                    S2_Station_Stop = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS2_Station_Stop = e.NewValue;
        }
        private void S3_Station_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S3_Station_StopChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_Station_Stop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 3",
                    TagName = "Trạm 3 Dừng", // ✅ Tên tag
                    S3_Station_Stop = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS3_Station_Stop = e.NewValue;
        }
        private void S1_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S1_Station_AlarmChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_Station_Alarm == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Trạm 1 Lỗi", // ✅ Tên tag
                    S1_Station_Alarm = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            // Cập nhật trạng thái trước
            prevS1_Station_Alarm = e.NewValue;
        }
        private void S2_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S2_Station_AlarmChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS2_Station_Alarm == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 2",
                    TagName = "Trạm 2 Lỗi", // ✅ Tên tag
                    S2_Station_Alarm = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            // Cập nhật trạng thái trước
            prevS2_Station_Alarm = e.NewValue;
        }
        private void S3_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S3_Station_AlarmChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS3_Station_Alarm == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataAlarmModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 3",
                    TagName = "Trạm 3 Lỗi", // ✅ Tên tag
                    S3_Station_Alarm = e.NewValue,
                };
                SQLLoginAlarm.InsertAlarm(model);
            }
            // Cập nhật trạng thái trước
            prevS3_Station_Alarm = e.NewValue;
        }

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
                    Position = "Trạm 1",
                    TagName = "Áp Suất Cửa 1 Cao", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Áp Suất Cửa 1 Thấp", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Áp Suất Cửa 2 Cao", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Áp Suất Cửa 2 Thấp", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Áp Suất Cửa 3 Cao", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Áp Suất Cửa 3 Thấp", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Áp Suất Cửa 4 Cao", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Áp Suất Cửa 4 Thấp", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName ="Áp Suất Cửa 5 Cao", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName = "Áp Suất Cửa 5 Thấp", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName = "Áp Suất Cửa 6 Cao", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName = "Áp Suất Cửa 6 Thấp", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Quá Tải Động Cơ 1", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Quá Tải Động Cơ 2", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Quá Tải Động Cơ 3", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Quá Tải Động Cơ 1", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Quá Tải Động Cơ 2", // ✅ Tên tag
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
                    Position = "Trạm 2",
                    TagName = "Quá Tải Động Cơ 3", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName = "Quá Tải Động Cơ 1", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName = "Quá Tải Động Cơ 2", // ✅ Tên tag
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
                    Position = "Trạm 3",
                    TagName = "Quá Tải Động Cơ 3", // ✅ Tên tag
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 1_1 Mở", // ✅ Tên tag
                    Doorlock1_1Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 1_1 Đóng", // ✅ Tên tag
                    Doorlock1_1Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 1_2 Mở", // ✅ Tên tag
                    Doorlock1_2Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 1_2 Đóng", // ✅ Tên tag
                    Doorlock1_2Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 2_1 Mở", // ✅ Tên tag
                    Doorlock2_1Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 2_1 Đóng", // ✅ Tên tag
                    Doorlock2_1Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 2_2 Mở", // ✅ Tên tag
                    Doorlock2_2Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 2_2 Đóng", // ✅ Tên tag
                    Doorlock2_2Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 3_1 Mở", // ✅ Tên tag
                    Doorlock3_1Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 3_1 Đóng", // ✅ Tên tag
                    Doorlock3_1Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position =" Trạm 2",
                    TagName = "Chốt Cửa 3_2 Mở", // ✅ Tên tag
                    Doorlock3_2Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 3_2 Đóng", // ✅ Tên tag
                    Doorlock3_2Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 4_1 Mở", // ✅ Tên tag
                    Doorlock4_1Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 4_1 Đóng", // ✅ Tên tag
                    Doorlock4_1Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position =" Trạm 2",
                    TagName = "Chốt Cửa 4_2 Mở", // ✅ Tên tag
                    Doorlock4_2Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 4_2 Đóng", // ✅ Tên tag
                    Doorlock4_2Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 5_1 Mở", // ✅ Tên tag
                    Doorlock5_1Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 5_1 Đóng", // ✅ Tên tag
                    Doorlock5_1Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 5_2 Mở", // ✅ Tên tag
                    Doorlock5_2Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 5_2 Đóng", // ✅ Tên tag
                    Doorlock5_2Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 6_1 Mở", // ✅ Tên tag
                    Doorlock6_1Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 6_1 Đóng", // ✅ Tên tag
                    Doorlock6_1Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 6_2 Mở", // ✅ Tên tag
                    Doorlock6_2Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 6_2 Đóng", // ✅ Tên tag
                    Doorlock6_2Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 1 Mở Hoàn Toàn", // ✅ Tên tag
                    Door1_Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 1 Đóng Hoàn Toàn", // ✅ Tên tag
                    Door1_Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 2 Mở Hoàn Toàn", // ✅ Tên tag
                    Door2_Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 2 Đóng Hoàn Toàn", // ✅ Tên tag
                    Door2_Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 3 Mở Hoàn Toàn", // ✅ Tên tag
                    Door3_Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 3 Đóng Hoàn Toàn", // ✅ Tên tag
                    Door3_Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 4 Mở Hoàn Toàn", // ✅ Tên tag
                    Door4_Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 4 Đóng Hoàn Toàn", // ✅ Tên tag
                    Door4_Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position =" Trạm 3",
                    TagName = "Cửa 5 Mở Hoàn Toàn", // ✅ Tên tag
                    Door5_Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 5 Đóng Hoàn Toàn", // ✅ Tên tag
                    Door5_Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 6 Mở Hoàn Toàn", // ✅ Tên tag
                    Door6_Open = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 6 Đóng Hoàn Toàn", // ✅ Tên tag
                    Door6_Close = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 1 Đang Mở", // ✅ Tên tag
                    Doorlock1_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 1 Đang Đóng", // ✅ Tên tag
                    Doorlock1_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 2 Đang Mở", // ✅ Tên tag
                    Doorlock2_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Chốt Cửa 2 Đang Đóng", // ✅ Tên tag
                    Doorlock2_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 3 Đang Mở", // ✅ Tên tag
                    Doorlock3_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 3 Đang Đóng", // ✅ Tên tag
                    Doorlock3_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 4 Đang Mở", // ✅ Tên tag
                    Doorlock4_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chốt Cửa 4 Đang Đóng", // ✅ Tên tag
                    Doorlock4_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 5 Đang Mở", // ✅ Tên tag
                    Doorlock5_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 5 Đang Đóng", // ✅ Tên tag
                    Doorlock5_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 6 Đang Mở", // ✅ Tên tag
                    Doorlock6_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chốt Cửa 6 Đang Đóng", // ✅ Tên tag
                    Doorlock6_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position =" Trạm 1",
                    TagName = "Cửa 1 Đang Mở", // ✅ Tên tag
                    Door1_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 1 Đang Đóng", // ✅ Tên tag
                    Door1_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 2 Đang Mở", // ✅ Tên tag
                    Door2_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 1",
                    TagName = "Cửa 2 Đang Đóng", // ✅ Tên tag
                    Door2_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 3 Đang Mở", // ✅ Tên tag
                    Door3_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 3 Đang Đóng", // ✅ Tên tag
                    Door3_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 4 Đang Mở", // ✅ Tên tag
                    Door4_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Cửa 4 Đang Đóng", // ✅ Tên tag
                    Door4_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 5 Đang Mở", // ✅ Tên tag
                    Door5_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 5 Đang Đóng", // ✅ Tên tag
                    Door5_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 6 Đang Mở", // ✅ Tên tag
                    Door6_Opening = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Cửa 6 Đang Đóng", // ✅ Tên tag
                    Door6_Closing = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            prevDoor6_Closing = e.NewValue; // Cập nhật trạng thái trước
        }


        
        private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S1_DC1_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_DC1_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Động Cơ 1 Đang Chạy", // ✅ Tên tag
                    S1_DC1_Running = e.NewValue,                 
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1_DC1_Running = e.NewValue;

        }

        private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S1_DC2_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_DC2_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Động Cơ 2 Đang Chạy", // ✅ Tên tag
                    S1_DC2_Running = e.NewValue,
     
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1_DC2_Running = e.NewValue;

        }
        private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            // 🔔 Raise event để form khác nhận
            S1_DC3_RunningChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1_DC3_Running == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Động Cơ 3 Đang Chạy", // ✅ Tên tag
                    S1_DC3_Running = e.NewValue,
                   
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
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
                    Position =" Trạm 2",
                    TagName = "Động Cơ 1 Đang Chạy", // ✅ Tên tag
                    S2_DC1_Running = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Động Cơ 2 Đang Chạy", // ✅ Tên tag
                    S2_DC2_Running = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Động Cơ 3 Đang Chạy", // ✅ Tên tag
                    S2_DC3_Running = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position =" Trạm 3",
                    TagName = "Động Cơ 1 Đang Chạy", // ✅ Tên tag
                    S3_DC1_Running = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Động Cơ 2 Đang Chạy", // ✅ Tên tag
                    S3_DC2_Running = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Động Cơ 3 Đang Chạy", // ✅ Tên tag
                    S3_DC3_Running = e.NewValue,
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            prevS3_DC3_Running = e.NewValue; // Cập nhật trạng thái trước
        }
        private void S1_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {  
  
            // 🔔 Raise event để form khác nhận
            S1RemoteChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Remote == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Chế Độ Từ Xa", // ✅ Tên tag
                    S1_Remote = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1Remote = e.NewValue;
        }
        private void S1_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            
            S1LocalChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Local == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Chế Độ Tại Chỗ", // ✅ Tên tag
                    S1_Local = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1Local = e.NewValue;
        }
        private void S1_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
           
            S1AutoChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Auto == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Chế Độ Tự Động", // ✅ Tên tag
                    S1_Auto = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1Auto = e.NewValue;
        }
        private void S1_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {           
            S1ManChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1Man == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Chế Độ Thủ Công", // ✅ Tên tag
                    S1_Man = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS1Man = e.NewValue;
        }
        private void S1_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {         
            S1LocalStopChanged?.Invoke(this, e);
            // ✅ Ghi xuống SQL Server chỉ khi từ "0" -> "1"
            if (prevS1LocalStop == "0" && e.NewValue == "1")
            {
                // Tạo object DataTranModel mới
                var model = new DataTranModel
                {
                    CreateAt = DateTime.Now,
                    Position = "Trạm 1",
                    TagName = "Chế Độ Dừng Tại Chỗ", // ✅ Tên tag
                    S1_Local_Stop = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chế Độ Từ Xa", // ✅ Tên tag
                    S2_Remote = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chế Độ Tại Chỗ", // ✅ Tên tag
                    S2_Local = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chế Độ Tự Động", // ✅ Tên tag
                    S2_Auto = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chế Độ Thủ Công", // ✅ Tên tag
                    S2_Man = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 2",
                    TagName = "Chế Độ Dừng Tại Chỗ", // ✅ Tên tag
                    S2_Local_Stop = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position =" Trạm 3",
                    TagName = "Chế Độ Từ Xa", // ✅ Tên tag
                    S3_Remote = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chế Độ Tại Chỗ", // ✅ Tên tag
                    S3_Local = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chế Độ Tự Động", // ✅ Tên tag
                    S3_Auto = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chế Độ Thủ Công", // ✅ Tên tag
                    S3_Man = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
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
                    Position = "Trạm 3",
                    TagName = "Chế Độ Dừng Tại Chỗ", // ✅ Tên tag
                    S3_Local_Stop = e.NewValue,
                    // TODO: set các property khác nếu cần
                };
                SQLLoginDataTran.InsertDataTran(model);
            }
            // Cập nhật trạng thái trước
            prevS3LocalStop = e.NewValue;
        }


        #region GetCurrentValue
        public string GetS1_Station_RunValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Run").Value;
        }
        public string GetS2_Station_RunValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Run").Value;
        }
        public string GetS3_Station_RunValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Run").Value;
        }
        public string GetS1_Station_StopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Stop").Value;
        }
        public string GetS2_Station_StopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Stop").Value;
        }
        public string GetS3_Station_StopValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Stop").Value;
        }
        public string GetS1_Station_AlarmValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group1/Station_Alarm").Value;
        }
        public string GetS2_Station_AlarmValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group2/Station_Alarm").Value;
        }
        public string GetS3_Station_AlarmValue()
        {
            return ahdDriverConnector1.GetTag("Local Station/DauTieng/S71500/Group3/Station_Alarm").Value;
        }
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


                };

                SQLLoginMucNuoc.InsertDataMucNuoc(data);
                Console.WriteLine($"✅ Đã ghi DataMucNuoc lúc {data.CreateAt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi ghi DataMucNuoc: {ex.Message}");
            }
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
            FrmTramMN mn = new FrmTramMN();
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