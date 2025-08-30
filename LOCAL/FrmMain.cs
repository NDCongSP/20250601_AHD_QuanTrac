using Ahd.Core;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain;
using Domain.Entities;
using Microsoft.Office.Interop.Excel;
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

        private int _logTime;
        private DateTime _startTime = DateTime.Now;

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
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_2")
                            {
                                _labALDoor1_Station2.Text = item.Al_Door1.ToString();
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_3")
                            {
                                _labALDoor1_Station3.Text = item.Al_Door1.ToString();
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
                            LogBaseInterval = true,
                            LocationId = item.LocationId,
                            LocationName = item.LocationName,

                            FlLow_DauTieng = item.CalculatorValue.Fllow_DauTieng,
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
        private async void FrmMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
            //      btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                // Change this line in FrmMain_Load:
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);

            _startTime = DateTime.Now;

            bnt_Tran.Click += (s, o) => {
                using (var nf =new FrmTran())
                {
                    nf.StartPosition = FormStartPosition.CenterScreen;
                    nf.ShowDialog();
                }
            };
        }

        private void Driver_Started(object sender, EventArgs e)
        {
            foreach (var item in Globalvariable.LocationsInfo)
            {
                foreach (var station in item.Stations.Where(x => x.Path.Contains("/Station_")))
                {
                    // Replace this line:
                    ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").ValueChanged += Al_Door1_ValueChanged;

                    Al_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").Value));
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
            }
        }

        //private string prevS1_Station_Stop = "0", prevS2_Station_Stop = "0", prevS3_Station_Stop = "0"; // biến lưu trạng thái trước đó
        //private string prevS3_Station_Alarm = "0", prevS2_Station_Alarm = "0", prevS1_Station_Alarm = "0"; // biến lưu trạng thái trước đó


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
                    var oldValue = station.Al_Door1;

                    station.Al_Door1 = e.NewValue == "1" ? true : false;


                    if (oldValue != station.Al_Door1)
                    {
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

                            //datalog
                            Globalvariable.DataLog.Id = Guid.NewGuid();
                            Globalvariable.DataLog.CreateAt = createAt;
                            Globalvariable.DataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.DataLog.LogBaseInterval = true;
                            Globalvariable.DataLog.LocationId = location.LocationId;
                            Globalvariable.DataLog.LocationName = location.LocationName;

                            Globalvariable.DataLog.FlLow_DauTieng = location.CalculatorValue.Fllow_DauTieng;
                            Globalvariable.DataLog.Fllow_BenSuc = location.CalculatorValue.Fllow_BenSuc;
                            Globalvariable.DataLog.Fllow_SonDai = location.CalculatorValue.Fllow_SonDai;
                            Globalvariable.DataLog.Fllow_BinhNham = location.CalculatorValue.Fllow_BinhNham;
                            Globalvariable.DataLog.Fllow_BinhNham2 = location.CalculatorValue.Fllow_BinhNham2;
                            Globalvariable.DataLog.Fllow_TL_CDD = location.CalculatorValue.Fllow_TL_CDD;
                            Globalvariable.DataLog.Fllow_HL_TXL = location.CalculatorValue.Fllow_HL_TXL;
                            Globalvariable.DataLog.Total_Fllow = location.CalculatorValue.Total_Fllow;
                            Globalvariable.DataLog.Q_Den = location.CalculatorValue.Q_Den;
                            Globalvariable.DataLog.Q_Di = location.CalculatorValue.Q_Di;
                            Globalvariable.DataLog.W_Ho = location.CalculatorValue.W_Ho;
                            Globalvariable.DataLog.LuuLuong = location.CalculatorValue.LuuLuong;
                            Globalvariable.DataLog.LuuLuongTong = location.CalculatorValue.LuuLuongTong;


                            Globalvariable.DataLog.StationId = station.StationId;
                            Globalvariable.DataLog.StationName = station.StationName;
                            Globalvariable.DataLog.Path = station.Path;

                            Globalvariable.DataLog.HT_Cylinder1_1 = station.HT_Cylinder1_1;
                            Globalvariable.DataLog.HT_Cylinder1_2 = station.HT_Cylinder1_2;
                            Globalvariable.DataLog.HT_Cylinder2_1 = station.HT_Cylinder2_1;
                            Globalvariable.DataLog.HT_Cylinder2_2 = station.HT_Cylinder2_2;
                            Globalvariable.DataLog.Door1_Aperture = station.Door1_Aperture;
                            Globalvariable.DataLog.Door2_Aperture = station.Door2_Aperture;
                            Globalvariable.DataLog.S1_Temp_Oil = station.S1_Temp_Oil;
                            Globalvariable.DataLog.Pressure_Oil_Door1 = station.Pressure_Oil_Door1;
                            Globalvariable.DataLog.Pressure_Oil_Door2 = station.Pressure_Oil_Door2;
                            Globalvariable.DataLog.Fllow_Door1 = station.Fllow_Door1;
                            Globalvariable.DataLog.Fllow_Door2 = station.Fllow_Door2;
                            Globalvariable.DataLog.Fllow_Ho = station.Fllow_Ho;

                            Globalvariable.DataLog.HT_Cylinder1_1_Offset = station.HT_Cylinder1_1_Offset;
                            Globalvariable.DataLog.HT_Cylinder1_2_Offset = station.HT_Cylinder1_2_Offset;
                            Globalvariable.DataLog.HT_Cylinder2_1_Offset = station.HT_Cylinder2_1_Offset;
                            Globalvariable.DataLog.HT_Cylinder2_2_Offset = station.HT_Cylinder2_2_Offset;
                            Globalvariable.DataLog.Door1_Aperture_Offset = station.Door1_Aperture_Offset;
                            Globalvariable.DataLog.Door2_Aperture_Offset = station.Door2_Aperture_Offset;
                            Globalvariable.DataLog.S1_Temp_Oil_Offset = station.S1_Temp_Oil_Offset;
                            Globalvariable.DataLog.Pressure_Oil_Door1_Offset = station.Pressure_Oil_Door1_Offset;
                            Globalvariable.DataLog.Pressure_Oil_Door2_Offset = station.Pressure_Oil_Door2_Offset;
                            Globalvariable.DataLog.Fllow_Door1_Offset = station.Fllow_Door1_Offset;
                            Globalvariable.DataLog.Fllow_Door2_Offset = station.Fllow_Door2_Offset;
                            Globalvariable.DataLog.Fllow_Ho_Offset = station.Fllow_Ho_Offset;

                            Globalvariable.DataLog.HT_Cylinder1_1_Final = station.HT_Cylinder1_1_Final;
                            Globalvariable.DataLog.HT_Cylinder1_2_Final = station.HT_Cylinder1_2_Final;
                            Globalvariable.DataLog.HT_Cylinder2_1_Final = station.HT_Cylinder2_1_Final;
                            Globalvariable.DataLog.HT_Cylinder2_2_Final = station.HT_Cylinder2_2_Final;
                            Globalvariable.DataLog.Door1_Aperture_Final = station.Door1_Aperture_Final;
                            Globalvariable.DataLog.Door2_Aperture_Final = station.Door2_Aperture_Final;
                            Globalvariable.DataLog.S1_Temp_Oil_Final = station.S1_Temp_Oil_Final;
                            Globalvariable.DataLog.Pressure_Oil_Door1_Final = station.Pressure_Oil_Door1_Final;
                            Globalvariable.DataLog.Pressure_Oil_Door2_Final = station.Pressure_Oil_Door2_Final;
                            Globalvariable.DataLog.Fllow_Door1_Final = station.Fllow_Door1_Final;
                            Globalvariable.DataLog.Fllow_Door2_Final = station.Fllow_Door2_Final;
                            Globalvariable.DataLog.Fllow_Ho_Final = station.Fllow_Ho_Final;

                            dbContext.FT03s.Add(Globalvariable.DataLog);

                            //alarms
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Al_Door1";
                            Globalvariable.AlarmDataLog.Value = station.Al_Door1 == true ? 1 : 0;
                            Globalvariable.AlarmDataLog.Description = station.Al_Door1 == true ? "Báo động cửa 1." : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
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
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}