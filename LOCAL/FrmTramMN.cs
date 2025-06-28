using Ahd.Core;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace RegistrationForm1
{
   
    public partial class FrmTramMN : Form
    {
        private Panel canvasPanel;
        private List<WaterStationLocation> stations;
        private Image markerImage;
        // Dictionary để định nghĩa vị trí tùy chỉnh (x, y) cho các trạm
        private Dictionary<string, PointF> customMarkerPositions;

        public FrmTramMN()
        {
            InitializeComponent();
            InitializeControls();
            stations = new List<WaterStationLocation>();
            InitializeCustomPositions();
            LoadMarkerImage();
        }

        private void InitializeControls()
        {
            this.Size = new Size(800, 600);
            this.Text = "Bản đồ tự tạo - Trạm quan trắc";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            // Canvas Panel để vẽ marker
            canvasPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            canvasPanel.Paint += CanvasPanel_Paint;
            canvasPanel.MouseClick += CanvasPanel_MouseClick;

            // Nút lưu ảnh
            var btnSaveMap = new Button
            {
                Text = "Lưu Bản Đồ",
                Location = new Point(10, 210),
                Size = new Size(100, 30)
            };
            btnSaveMap.Click += (s, e) => SaveMapAsImage("custom_map.png");

            // Thêm controls
            this.Controls.Add(canvasPanel);
            this.Controls.Add(btnSaveMap);
        }

        // Khởi tạo vị trí tùy chỉnh cho các marker
        private void InitializeCustomPositions()
        {
            customMarkerPositions = new Dictionary<string, PointF>
            {
                { "F01877", new PointF(100, 100) }, // Son Dai
                { "F01203", new PointF(300, 150) }, // Ben Suc
                { "F01849", new PointF(800, 250) }, // TVan_DT
                { "F01850", new PointF(250, 500) }, // Trạm nước 1
                { "F01851", new PointF(1000, 500) }  // Trạm nước 2
            };
        }

        private void LoadMarkerImage()
        {
            try
            {
                // Sửa đường dẫn, dùng @ để tránh lỗi escape character
                markerImage = Image.FromFile(@"C:\Users\Toly\OneDrive\Documents\video and picture\placeholder.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hình ảnh marker: {ex.Message}. Sử dụng hình mặc định.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Fallback: Tạo hình mặc định nếu không tải được
                markerImage = new Bitmap(32, 32);
                using (Graphics g = Graphics.FromImage(markerImage))
                {
                    g.Clear(Color.Red);
                    g.DrawEllipse(Pens.Black, 0, 0, 31, 31);
                }
            }
        }

        // Cập nhật dữ liệu trạm từ MapForm
        public void UpdateStationData(List<WaterStationLocation> stationLocations)
        {
            stations = stationLocations ?? new List<WaterStationLocation>();
            canvasPanel.Invalidate(); // Làm mới canvas
        }

        // Vẽ marker trên canvas
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Vẽ nền
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.FillRectangle(brush, canvasPanel.ClientRectangle);
            }

            foreach (var station in stations)
            {
                if (customMarkerPositions.ContainsKey(station.StationId) && markerImage != null)
                {
                    PointF position = customMarkerPositions[station.StationId];
                    // Thu nhỏ marker về kích thước 24x24 pixel
                    float x = position.X - (24 / 2); // Căn giữa
                    float y = position.Y - (24 / 2);

                    // Vẽ hình ảnh marker với kích thước cố định 24x24
                    g.DrawImage(markerImage, x, y, 24, 24);

                    // Vẽ nhãn (tên trạm + mực nước) ngay bên dưới marker
                    string label = $"{station.StationName}\n{station.WaterLevel:F2}m";
                    SizeF labelSize = g.MeasureString(label, new Font("Arial", 9, FontStyle.Bold));
                    using (SolidBrush textBrush = new SolidBrush(Color.Black))
                    {
                        g.DrawString(label, new Font("Arial", 9, FontStyle.Bold), textBrush, x, y + 24 + 5); // 5 pixel khoảng cách
                    }
                }
            }
        }

        // Xử lý tương tác khi click vào marker
        private void CanvasPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var station in stations)
            {
                if (customMarkerPositions.ContainsKey(station.StationId) && markerImage != null)
                {
                    PointF position = customMarkerPositions[station.StationId];
                    float x = position.X - (24 / 2); // Căn giữa hình ảnh 24x24
                    float y = position.Y - (24 / 2);
                    Rectangle markerRect = new Rectangle((int)x, (int)y, 24, 24);

                    // Kiểm tra nếu click vào vùng hình ảnh marker
                    if (markerRect.Contains(e.Location))
                    {
                        string info = $"=== {station.StationName.ToUpper()} ===\n\n" +
                                      $"🆔 Mã trạm: {station.StationId}\n" +
                                      $"🌊 Mực nước: {station.WaterLevel:F2}m\n" +
                                      $"🕐 Cập nhật: {station.LastUpdate:dd/MM/yyyy HH:mm:ss}\n" +
                                      $"📊 Trạng thái: {station.Status}\n" +
                                      $"📍 Vị trí tùy chỉnh: ({position.X}, {position.Y})";
                        MessageBox.Show(info, "Thông tin trạm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }
        }

        public void SaveMapAsImage(string filePath)
        {
            try
            {
                using (Bitmap bitmap = new Bitmap(canvasPanel.Width, canvasPanel.Height))
                {
                    canvasPanel.DrawToBitmap(bitmap, new Rectangle(0, 0, canvasPanel.Width, canvasPanel.Height));
                    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show($"Đã lưu bản đồ tại: {filePath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu ảnh bản đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
   
}
