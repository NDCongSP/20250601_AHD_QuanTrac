using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmConfigSQL : Form
    {
        private const string ConnectionName = "ApplicationDbContextConString";

        public FrmConfigSQL()
        {
            InitializeComponent();
            LoadCurrentConfig();
            // Thiết lập sự kiện thay đổi radio button
            rdoWindowsAuth.CheckedChanged += Auth_CheckedChanged;
            rdoSqlAuth.CheckedChanged += Auth_CheckedChanged;
        }

        /// <summary>
        /// Đọc và hiển thị cấu hình hiện tại từ App.config.
        /// </summary>
        private void LoadCurrentConfig()
        {
            try
            {
                var connectionSetting = ConfigurationManager.ConnectionStrings[ConnectionName];

                if (connectionSetting == null)
                {
                    // Lỗi chính xác xảy ra ở đây!
                    throw new InvalidOperationException($"Không tìm thấy mục '{ConnectionName}' trong thẻ <connectionStrings>.");
                }

                string connectionString = connectionSetting.ConnectionString;

                // Phân tích chuỗi kết nối thành các thành phần
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

                // Hiển thị lên Form
                txtDataSource.Text = builder.DataSource;
                txtInitialCatalog.Text = builder.InitialCatalog;
                // ... (Logic xác thực và hiển thị)
                if (builder.IntegratedSecurity)
                {
                    rdoWindowsAuth.Checked = true;
                }
                else
                {
                    rdoSqlAuth.Checked = true;
                    txtUserID.Text = builder.UserID;
                    txtPassword.Text = builder.Password;
                }
                UpdateAuthFieldsState();

            }
            catch (Exception ex)
            {
                // Thông báo lỗi chi tiết sẽ hiện ra nếu không tìm thấy chuỗi
                MessageBox.Show($"Lỗi không thể đọc cấu hình: {ex.Message}\n\nHãy kiểm tra file [Tên_App].exe.config có tồn tại trong thư mục chạy hay không.",
                    "Lỗi Cấu hình Khởi tạo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Thiết lập mặc định để người dùng có thể nhập cấu hình mới
                rdoSqlAuth.Checked = true;
                UpdateAuthFieldsState();
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi thay đổi loại xác thực (Windows/SQL).
        /// </summary>
        private void Auth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthFieldsState();
        }

        /// <summary>
        /// Cập nhật trạng thái Enabled của các TextBox Username và Password.
        /// </summary>
        private void UpdateAuthFieldsState()
        {
            bool isSqlAuth = rdoSqlAuth.Checked;
            txtUserID.Enabled = isSqlAuth;
            txtPassword.Enabled = isSqlAuth;
        }


        /// <summary>
        /// Xây dựng chuỗi kết nối mới từ dữ liệu trên Form, giữ lại các thuộc tính nâng cao cố định.
        /// </summary>
        private string BuildNewConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            // 1. Thông tin cơ bản
            builder.DataSource = txtDataSource.Text.Trim();
            builder.InitialCatalog = txtInitialCatalog.Text.Trim();

            // 2. Kiểu xác thực
            if (rdoWindowsAuth.Checked)
            {
                builder.IntegratedSecurity = true;
                // Xóa UserID/Password để đảm bảo chuỗi kết nối sạch sẽ
                builder.Remove("User ID");
                builder.Remove("Password");
            }
            else // SQL Authentication
            {
                builder.IntegratedSecurity = false;
                builder.UserID = txtUserID.Text.Trim();
                builder.Password = txtPassword.Text; // KHÔNG nên trim password
            }

            // 3. Giữ lại các thuộc tính nâng cao cố định từ cấu hình XML gốc
            // Đảm bảo các tham số này khớp với cấu hình Entity Framework của bạn
            builder.MultipleActiveResultSets = true;
            builder.Encrypt = false;
            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = 300;
            // Thuộc tính Persist Security Info nên được đặt nếu sử dụng User ID/Password
            builder.PersistSecurityInfo = !builder.IntegratedSecurity;

            // Thêm thuộc tính App (nếu bạn cần)
            builder.Add("App", "EntityFramework");

            return builder.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Lưu chuỗi kết nối mới vào App.config
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string newConnectionString = BuildNewConnectionString();
            try
            {
                // Mở file cấu hình thật (ví dụ: MyApp.exe.config)
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                ConnectionStringsSection connectionSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

                if (connectionSection == null)
                {
                    MessageBox.Show("Không tìm thấy thẻ <connectionStrings> trong file cấu hình.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Nếu chưa có connection thì thêm mới
                if (connectionSection.ConnectionStrings[ConnectionName] == null)
                {
                    ConnectionStringSettings newSetting = new ConnectionStringSettings(
                        ConnectionName,
                        newConnectionString,
                        "System.Data.SqlClient");

                    connectionSection.ConnectionStrings.Add(newSetting);
                    MessageBox.Show($"Đã tạo mới chuỗi kết nối '{ConnectionName}' và lưu thành công.",
                        "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Nếu có thì cập nhật
                    connectionSection.ConnectionStrings[ConnectionName].ConnectionString = newConnectionString;
                    MessageBox.Show("Cập nhật và lưu cấu hình thành công!",
                        "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                // ✅ Thông báo lưu thành công
                MessageBox.Show("Lưu cấu hình thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Mở lại FormLogin
                this.Hide(); // Ẩn form cấu hình
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();

                // Sau khi đóng login, thoát form cấu hình
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu cấu hình: {ex.Message}",
                    "Lỗi Ghi Cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Kiểm tra kết nối SQL Server bằng chuỗi kết nối mới tạo.
        /// </summary>
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = BuildNewConnectionString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Kết nối thành công tới SQL Server! 🎉", "Kiểm tra Kết nối", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           

            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Kết nối thất bại (Lỗi SQL Server):\n{sqlEx.Message}", "Lỗi Kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định:\n{ex.Message}", "Lỗi Kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}