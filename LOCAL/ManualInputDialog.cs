using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class ManualInputDialog : Form
    {
        public event Action<string, int> ValueSubmitted; // Sự kiện để truyền giá trị
        private string stationId;
        private string stationName;

        public ManualInputDialog(string stationId, string stationName)
        {
            InitializeComponent();
            this.stationId = stationId;
            this.stationName = stationName;
            InitializeControls();
        }

        private void InitializeControls()
        {
            this.Text = $"Nhập mực nước - {stationName}";
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label
            var lblPrompt = new Label
            {
                Text = $"Nhập mực nước cho {stationName}:",
                Location = new Point(20, 20),
                Size = new Size(250, 20)
            };

            // TextBox
            var txtWaterLevel = new TextBox
            {
                Name = "txtWaterLevel",
                Location = new Point(20, 50),
                Size = new Size(100, 20)
            };

            // Submit Button
            var btnSubmit = new Button
            {
                Text = "Xác nhận",
                Location = new Point(20, 80),
                Size = new Size(100, 30)
            };
            btnSubmit.Click += (s, e) =>
            {
                if (int.TryParse(txtWaterLevel.Text, out int value))
                {
                    ValueSubmitted?.Invoke(stationId, value);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập giá trị số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Cancel Button
            var btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(130, 80),
                Size = new Size(100, 30)
            };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblPrompt, txtWaterLevel, btnSubmit, btnCancel });
        }
        private void ManualInputDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
