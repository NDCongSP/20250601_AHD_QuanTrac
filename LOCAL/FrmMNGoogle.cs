using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
namespace RegistrationForm1
{
    public partial class FrmMNGoogle : Form
    {
        public event Action<string, int> ManualValueUpdated;
        private GMapControl gMapControl1;
        private GMapOverlay markersOverlay;
        private List<WaterStationLocation> stationLocations;
        private Panel controlPanel;
        private Panel customMapPanel;
        private ComboBox cmbMapProvider;
        private Label lblInfo;
        private Button btnRefresh;
        private Timer updateTimer;
        private DateTime lastUpdateTime;
        private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        private bool isFormLoaded = false;
        private bool isGoogleMap = true; // Biến để track loại bản đồ hiện tại
        private Form customMapForm;
        public FrmMNGoogle()
        {
            InitializeComponent();
            InitializeCustomComponents();
            InitializeStationLocations();
            InitializeMap();
            InitializeDatabase(); // Load dữ liệu từ DB trước
            AddStationMarkers(); // Sau đó mới thêm markers
            StartAutoUpdate();
        }

        private void InitializeCustomComponents()
        {
            this.SuspendLayout();

            // Form settings
            this.Size = new Size(1000, 700);
            this.Text = "Bản đồ trạm quan trắc mực nước - GMap.NET";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Control Panel
            this.controlPanel = new Panel();
            this.controlPanel.Dock = DockStyle.Top;
            this.controlPanel.Height = 60;
            this.controlPanel.BackColor = Color.FromArgb(52, 73, 94);

            // Map Provider ComboBox
            this.cmbMapProvider = new ComboBox();
            this.cmbMapProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMapProvider.Location = new Point(10, 15);
            this.cmbMapProvider.Size = new Size(150, 30);
            this.cmbMapProvider.Font = new Font("Segoe UI", 9F);
            this.cmbMapProvider.Items.AddRange(new string[] {
                "Google Maps",
                "Bản đồ tự tạo"
            });
            this.cmbMapProvider.SelectedIndex = 0;
            this.cmbMapProvider.SelectedIndexChanged += CmbMapProvider_SelectedIndexChanged;

            // Refresh Button
            this.btnRefresh = new Button();
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.Location = new Point(170, 12);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.BackColor = Color.FromArgb(41, 128, 185);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new Font("Segoe UI", 9F);
            this.btnRefresh.Click += BtnRefresh_Click;

            // Info Label
            this.lblInfo = new Label();
            this.lblInfo.Location = new Point(280, 20);
            this.lblInfo.Size = new Size(400, 20);
            this.lblInfo.ForeColor = Color.White;
            this.lblInfo.Font = new Font("Segoe UI", 9F);
            this.lblInfo.Text = "Đang tải dữ liệu...";

            // GMap Control
            this.gMapControl1 = new GMapControl();
            this.gMapControl1.Dock = DockStyle.Fill;
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 18;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Zoom = 12D;
            //customMapPanel
            this.customMapPanel = new Panel();
            this.customMapPanel.Dock = DockStyle.Fill;
            this.customMapPanel.BackColor = Color.LightGray;
            this.customMapPanel.Visible = false; // Ẩn ban đầu
            // Add controls to panels
            this.controlPanel.Controls.Add(this.cmbMapProvider);
            this.controlPanel.Controls.Add(this.btnRefresh);
            this.controlPanel.Controls.Add(this.lblInfo);

            // Add panels to form
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.customMapPanel);
            this.ResumeLayout(false);
            isFormLoaded = true; // Đánh dấu form đã load xong
        }

        private void InitializeStationLocations()
        {
            // Khởi tạo danh sách trống, sẽ được load từ database
            stationLocations = new List<WaterStationLocation>();
        }

        private void InitializeMap()
        {
            try
            {
                // Cấu hình GMap
                GMaps.Instance.Mode = AccessMode.ServerAndCache;
                gMapControl1.MapProvider = GMapProviders.GoogleMap;

                // Đặt vị trí trung tâm (giữa 3 trạm)
                gMapControl1.Position = new PointLatLng(11.409188, 106.329538);
                gMapControl1.Zoom = 12;

                // Tạo overlay cho markers
                markersOverlay = new GMapOverlay("markers");
                gMapControl1.Overlays.Add(markersOverlay);

                // Event handlers
                gMapControl1.OnMarkerClick += GMapControl1_OnMarkerClick;
                gMapControl1.OnPositionChanged += GMapControl1_OnPositionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo bản đồ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddStationMarkers()
        {
            if (markersOverlay == null) return;

            markersOverlay.Markers.Clear();

            foreach (var station in stationLocations)
            {
                // Tạo marker với icon tùy theo trạng thái
                Bitmap markerIcon = CreateWaterStationIcon(station.Status);
                GMapMarker marker = new GMarkerGoogle(
                    new PointLatLng(station.Latitude, station.Longitude),
                    markerIcon
                );

                // Đặt tooltip với dữ liệu thực từ database
                marker.ToolTipText = $"{station.StationName.ToUpper()}\n" +
                                   $"📍 {station.Description}\n" +
                                   $"🆔 Mã trạm: {station.StationId}\n" +
                                   $"🌊 Mực nước: {station.WaterLevel:F2}m\n" +
                                   $"🕐 Cập nhật: {station.LastUpdate:HH:mm:ss dd/MM/yyyy}";
                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;

                // Lưu thông tin station vào tag
                marker.Tag = station;

                markersOverlay.Markers.Add(marker);
            }

            // Cập nhật thông tin số lượng trạm
            SafeUpdateUI(() => {

                int totalStations = stationLocations.Count;


            });
        }

        private Bitmap CreateWaterStationIcon(string status)
        {
            // Tạo icon màu sắc theo trạng thái
            Color iconColor = Color.Blue;


            Bitmap bitmap = new Bitmap(28, 28);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Vẽ hình tròn chính
                using (SolidBrush brush = new SolidBrush(iconColor))
                {
                    g.FillEllipse(brush, 2, 2, 24, 24);
                }

                // Vẽ border
                using (Pen pen = new Pen(Color.White, 2))
                {
                    g.DrawEllipse(pen, 2, 2, 24, 24);
                }

                // Vẽ biểu tượng sóng nước
                using (Pen wavePen = new Pen(Color.White, 2))
                {
                    // Vẽ 3 đường sóng nhỏ
                    g.DrawArc(wavePen, 8, 10, 4, 3, 0, 180);
                    g.DrawArc(wavePen, 12, 10, 4, 3, 0, 180);
                    g.DrawArc(wavePen, 16, 10, 4, 3, 0, 180);

                    g.DrawArc(wavePen, 10, 14, 4, 3, 0, 180);
                    g.DrawArc(wavePen, 14, 14, 4, 3, 0, 180);
                }
            }

            return bitmap;
        }

        private void CmbMapProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cmbMapProvider.SelectedIndex)
                {
                    case 0: // Google Maps
                        ShowGoogleMap();
                        break;
                    case 1: // Bản đồ tự tạo
                        ShowCustomMap();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chuyển đổi map provider: {ex.Message}");
            }
        }
        private void ShowGoogleMap()
        {
            isGoogleMap = true;

            // Ẩn custom panel và đóng form con nếu có
            if (customMapForm != null && !customMapForm.IsDisposed)
            {
                customMapForm.Close();
                customMapForm = null;
            }

            customMapPanel.Visible = false;
            customMapPanel.Controls.Clear(); // Xóa form con khỏi panel

            // Hiển thị lại GMap control
            gMapControl1.Visible = true;
            gMapControl1.BringToFront();

            SafeUpdateUI(() => {
                lblInfo.Text = $"Google Maps - {stationLocations.Count} trạm";
            });
        }

        private void ShowCustomMap()
        {
            isGoogleMap = false;

            try
            {
                // Ẩn GMap control
                gMapControl1.Visible = false;

                // Hiển thị custom panel
                customMapPanel.Visible = true;
                customMapPanel.BringToFront();

                // Mở form tự tạo trong panel
                OpenCustomMapInPanel();

                SafeUpdateUI(() => {
                    lblInfo.Text = $"Bản đồ tự tạo - {stationLocations.Count} trạm";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở bản đồ tự tạo: {ex.Message}");
                // Quay lại Google Maps nếu lỗi
                cmbMapProvider.SelectedIndex = 0;
            }
        }

        private void OpenCustomMapInPanel()
        {
            try
            {
                // Xóa form cũ nếu có
                if (customMapForm != null && !customMapForm.IsDisposed)
                {
                    customMapForm.Close();
                }

                // Tạo form mới
                customMapForm = new FrmTramMN();

                // Cấu hình form để hiển thị trong panel
                customMapForm.TopLevel = false;
                customMapForm.FormBorderStyle = FormBorderStyle.None; // Bỏ border
                customMapForm.Dock = DockStyle.Fill; // Fill toàn bộ panel

                // Thêm form vào panel
                customMapPanel.Controls.Clear(); // Xóa controls cũ
                customMapPanel.Controls.Add(customMapForm);

                // Hiển thị form
                customMapForm.Show();

                // Truyền dữ liệu trạm nếu cần
                 //if (customMapForm is FrmTramMN cmf)
                 //{
                 //   cmf.UpdateStationData(stationLocations);
                 //}
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khởi tạo form bản đồ tự tạo: {ex.Message}");
            }
        }

        // Method để cập nhật kích thước panel khi form resize
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Đảm bảo custom panel luôn có kích thước giống GMapControl
            if (customMapPanel != null && gMapControl1 != null)
            {
                customMapPanel.Size = gMapControl1.Size;
                customMapPanel.Location = gMapControl1.Location;
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Đóng custom form nếu có
            if (customMapForm != null && !customMapForm.IsDisposed)
            {
                customMapForm.Close();
                customMapForm = null;
            }

            updateTimer?.Stop();
            updateTimer?.Dispose();
            base.OnFormClosed(e);
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataFromDatabase();
                AddStationMarkers();
                gMapControl1.ReloadMap();

                SafeUpdateUI(() => {
                    lblInfo.Text = $"{stationLocations.Count} trạm - Cập nhật: {DateTime.Now:HH:mm:ss}";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi làm mới: {ex.Message}");
            }
        }

        private void GMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag is WaterStationLocation station)
            {
                // Kiểm tra nếu là trạm thủ công (F01850 hoặc F01851)
                if (station.StationId == "F01850" || station.StationId == "F01851")
                {
                    // Mở form nhập liệu
                    var inputDialog = new ManualInputDialog(station.StationId, station.StationName);
                    inputDialog.ValueSubmitted += (stationId, value) =>
                    {
                        // Cập nhật giá trị trong stationLocations
                        var targetStation = stationLocations.FirstOrDefault(s => s.StationId == stationId);
                        if (targetStation != null)
                        {
                            targetStation.WaterLevel = value;
                            targetStation.LastUpdate = DateTime.Now;
                            targetStation.Status = DetermineStationStatus(value, DateTime.Now);
                        }

                        // Cập nhật markers
                        AddStationMarkers();

                        // Thông báo giá trị mới về MainHome
                        ManualValueUpdated?.Invoke(stationId, value);

                        SafeUpdateUI(() =>
                        {
                            lblInfo.Text = $"✓ Đã cập nhật {station.StationName}: {value}m - {DateTime.Now:HH:mm:ss}";
                        });
                    };
                    inputDialog.ShowDialog();
                }
                else
                {
                    // Hiển thị thông tin chi tiết cho các trạm khác
                    string info = $"=== {station.StationName.ToUpper()} ===\n\n" +
                                 $"🆔 Mã trạm: {station.StationId}\n" +
                                 $"📍 Vị trí: {station.Description}\n" +
                                 $"🌊 Mực nước hiện tại: {station.WaterLevel:F2}m\n" +
                                 $"🕐 Lần cập nhật cuối: {station.LastUpdate:dd/MM/yyyy HH:mm:ss}\n\n" +
                                 $"📍 Tọa độ: {station.Latitude:F6}, {station.Longitude:F6}";

                    MessageBox.Show(info, "Thông tin trạm quan trắc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GMapControl1_OnPositionChanged(PointLatLng point)
        {
            // Cập nhật thông tin vị trí hiện tại
            SafeUpdateUI(() => {
                lblInfo.Text = $"Vị trí: {point.Lat:F6}, {point.Lng:F6} - Zoom: {gMapControl1.Zoom:F0}";
            });
        }

        private void StartAutoUpdate()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 500000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                lastUpdateTime = DateTime.Now.AddDays(-1); // Load tất cả dữ liệu từ 1 ngày trước
                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database: {ex.Message}", "Lỗi Database",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query để lấy dữ liệu mới nhất của mỗi trạm
                    string query = @"
                        WITH LatestReadings AS (
                            SELECT 
                                [Mã Trạm] as StationId,
                                [Tên Trạm Nước] as StationName,
                                [Ngày] as ReadingDate,
                                [Giờ] as ReadingTime,
                                [Giá Trị] as WaterLevel,
                                ROW_NUMBER() OVER (PARTITION BY [Mã Trạm] ORDER BY 
                                    CAST([Ngày] AS DATETIME) + CAST([Giờ] AS DATETIME) DESC) as rn
                            FROM water_station
                            WHERE [Giá Trị] IS NOT NULL
                        )
                        SELECT StationId, StationName, ReadingDate, ReadingTime, WaterLevel
                        FROM LatestReadings 
                        WHERE rn = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var newStationData = new Dictionary<string, WaterStationLocation>();

                            while (reader.Read())
                            {
                                string stationId = reader["StationId"].ToString();
                                string stationName = reader["StationName"].ToString();
                                DateTime readingDate = Convert.ToDateTime(reader["ReadingDate"]);

                                // Xử lý trường Giờ
                                DateTime fullTimestamp;
                                var timeValue = reader["ReadingTime"];
                                if (timeValue is TimeSpan timeSpan)
                                {
                                    fullTimestamp = readingDate.Add(timeSpan);
                                }
                                else
                                {
                                    TimeSpan parsedTime = TimeSpan.Parse(timeValue.ToString());
                                    fullTimestamp = readingDate.Add(parsedTime);
                                }

                                decimal waterLevel = Convert.ToDecimal(reader["WaterLevel"]);

                                // Tìm trạm hiện có hoặc tạo mới
                                var existingStation = stationLocations.FirstOrDefault(s =>
                                    s.StationId == stationId);

                                if (existingStation != null)
                                {
                                    // Cập nhật dữ liệu cho trạm có sẵn
                                    existingStation.WaterLevel = waterLevel;
                                    existingStation.LastUpdate = fullTimestamp;
                                    existingStation.Status = DetermineStationStatus(waterLevel, fullTimestamp);
                                }
                                else
                                {
                                    // Tạo trạm mới với tọa độ mặc định (cần cập nhật theo thực tế)
                                    var coordinates = GetStationCoordinates(stationId, stationName);
                                    var newStation = new WaterStationLocation
                                    {
                                        StationId = stationId,
                                        StationName = stationName,
                                        Latitude = coordinates.Latitude,
                                        Longitude = coordinates.Longitude,
                                        Description = $"Trạm quan trắc mực nước {stationName}",
                                        Status = DetermineStationStatus(waterLevel, fullTimestamp),
                                        WaterLevel = waterLevel,
                                        LastUpdate = fullTimestamp
                                    };
                                    stationLocations.Add(newStation);
                                }
                            }

                            // Cập nhật markers sau khi load dữ liệu
                            if (isFormLoaded)
                            {
                                SafeUpdateUI(() => {
                                    AddStationMarkers();
                                    lblInfo.ForeColor = Color.LightGreen;
                                    lblInfo.Text = $"✓ Cập nhật thành công - {stationLocations.Count} trạm - {DateTime.Now:HH:mm:ss}";
                                });

                                // Reset màu sau 3 giây
                                Timer resetColorTimer = new Timer();
                                resetColorTimer.Interval = 3000;
                                resetColorTimer.Tick += (s, args) =>
                                {
                                    SafeUpdateUI(() => lblInfo.ForeColor = Color.White);
                                    resetColorTimer.Stop();
                                    resetColorTimer.Dispose();
                                };
                                resetColorTimer.Start();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load dữ liệu từ database: {ex.Message}");

                SafeUpdateUI(() => {
                    lblInfo.ForeColor = Color.Orange;
                    lblInfo.Text = $"⚠ Lỗi kết nối DB - {DateTime.Now:HH:mm:ss}";
                });
            }
        }

        private (double Latitude, double Longitude) GetStationCoordinates(string stationId, string stationName)
        {
            // Map các trạm với tọa độ thực tế
            // Cần cập nhật theo dữ liệu thực của bạn
            var coordinates = new Dictionary<string, (double, double)>
            {
                {"F01877", (11.371465662905464, 106.36254893186779)},  // Son Dai
                {"F01203", (11.35110814011132, 106.29695707551923)},  // Ben Suc 
                {"F01849", (11.362519081313378, 106.32905328735825)},  // TVan_DT
            };

            // Kiểm tra theo mã trạm trước
            if (coordinates.ContainsKey(stationId))
                return coordinates[stationId];

            // Kiểm tra theo tên trạm
            if (stationName.ToLower().Contains("Son Dai") || stationName.ToLower().Contains("sơn đài"))
                return (11.371465662905464, 106.36254893186779);
            else if (stationName.ToLower().Contains("Ben Suc") || stationName.ToLower().Contains("bến sức"))
                return (11.35110814011132, 106.29695707551923);
            else if (stationName.ToLower().Contains("TVan_DT") || stationName.ToLower().Contains("TVan_DT"))
                return (11.362519081313378, 106.32905328735825);

            return (11.332147, 106.328127);
        }

        private string DetermineStationStatus(decimal waterLevel, DateTime lastUpdate)
        {
            // Kiểm tra thời gian cập nhật
            if (DateTime.Now.Subtract(lastUpdate).TotalHours > 2)
                return "Mất kết nối";

            // Kiểm tra mực nước (điều chỉnh ngưỡng theo thực tế)
            if (waterLevel > 150) // Ngưỡng cảnh báo cao
                return "Cảnh báo";
            else if (waterLevel < 20) // Ngưỡng thấp
                return "Thấp";
            else
                return "Hoạt động";
        }

        private void SafeUpdateUI(Action action)
        {
            if (!isFormLoaded) return;

            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật UI: {ex.Message}");
            }
        }

        // Method để cập nhật dữ liệu trạm từ bên ngoài
        public void UpdateStationData(List<WaterStationReading> readings)
        {
            try
            {
                foreach (var reading in readings.GroupBy(r => r.StationId).Select(g => g.OrderByDescending(r => r.Timestamp).First()))
                {
                    var station = stationLocations.FirstOrDefault(s => s.StationId == reading.StationId);
                    if (station != null)
                    {
                        station.WaterLevel = reading.Value;
                        station.LastUpdate = reading.Timestamp;
                        station.Status = DetermineStationStatus(reading.Value, reading.Timestamp);
                    }
                }

                AddStationMarkers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật dữ liệu trạm: {ex.Message}");
            }
        }

        // Method để thêm trạm mới
        public void AddWaterStation(string stationId, string stationName, double latitude, double longitude, string description)
        {
            var newStation = new WaterStationLocation
            {
                StationId = stationId,
                StationName = stationName,
                Latitude = latitude,
                Longitude = longitude,
                Description = description,
                Status = "Hoạt động",
                WaterLevel = 0.0m,
                LastUpdate = DateTime.Now
            };

            stationLocations.Add(newStation);
            AddStationMarkers();
        }
    }

    // Class để lưu thông tin vị trí trạm mực nước
    public class WaterStationLocation
    {
        public string StationId { get; set; }
        public string StationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public decimal WaterLevel { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    public class WaterStationReading
    {
        public string StationId { get; set; }
        public string StationName { get; set; }
        public DateTime Timestamp { get; set; } // Combines Ngày and Giờ
        public decimal Value { get; set; }
    }
}