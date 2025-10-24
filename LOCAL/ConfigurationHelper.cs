using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public static class ConfigurationHelper
    {
        // ⚙️ Tên chuỗi kết nối trong App.config
        private const string ConnectionName = "ApplicationDbContextConString";

        /// <summary>
        /// Lấy chuỗi kết nối hiện tại trong App.config
        /// </summary>
        public static string GetConnectionString()
        {
            // Đảm bảo ConnectionStrings được tải. Nếu không tồn tại sẽ trả về null
            return ConfigurationManager.ConnectionStrings[ConnectionName]?.ConnectionString;
        }

        /// <summary>
        /// Lưu chuỗi kết nối mới vào App.config (giữ nguyên providerName = System.Data.SqlClient)
        /// </summary>
        public static void SaveConnectionString(string connectionString)
        {
            try
            {
                // 1. Mở file cấu hình EXE (ví dụ: RegistrationForm1.exe.config)
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // 2. Lấy section connectionStrings
                var section = config.GetSection("connectionStrings") as ConnectionStringsSection;

                if (section == null)
                {
                    // Nếu section connectionStrings không tồn tại (rất hiếm, nhưng phòng hờ)
                    throw new ConfigurationErrorsException("Không tìm thấy section 'connectionStrings' trong file cấu hình.");
                }

                // 3. Kiểm tra và cập nhật/thêm chuỗi kết nối
                if (section.ConnectionStrings[ConnectionName] != null)
                {
                    // Cập nhật chuỗi kết nối hiện có
                    section.ConnectionStrings[ConnectionName].ConnectionString = connectionString;
                    section.ConnectionStrings[ConnectionName].ProviderName = "System.Data.SqlClient";
                    Console.WriteLine($"DEBUG: Cập nhật chuỗi kết nối '{ConnectionName}' thành công.");
                }
                else
                {
                    // Thêm chuỗi kết nối mới
                    section.ConnectionStrings.Add(new ConnectionStringSettings(
                        ConnectionName, connectionString, "System.Data.SqlClient"));
                    Console.WriteLine($"DEBUG: Thêm chuỗi kết nối '{ConnectionName}' mới thành công.");
                }

                // 4. Lưu cấu hình
                config.Save(ConfigurationSaveMode.Modified);

                // 5. Làm mới section connectionStrings để ứng dụng đọc lại dữ liệu mới
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("💾 Đã lưu cấu hình SQL thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Ghi chi tiết lỗi vào Console để tiện Debug
                Console.WriteLine($"FATAL CONFIG ERROR: {ex}");
                MessageBox.Show($"❌ Lỗi khi lưu cấu hình:\n{ex.Message}", "Lỗi Ghi Cấu hình",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tạo chuỗi kết nối SQL Server
        /// </summary>
        public static string BuildConnectionString(string server, string database, string username, string password, bool useWindowsAuth)
        {
            // Giữ đúng format bạn đang có trong App.config (EF + MultipleActiveResultSets)
            if (useWindowsAuth)
            {
                return $"data source={server};initial catalog={database};Integrated Security=True;" +
                       $"MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=True;Connection Timeout=300;App=EntityFramework";
            }
            else
            {
                return $"data source={server};initial catalog={database};Persist Security Info=True;" +
                       $"User ID={username};Password={password};MultipleActiveResultSets=True;Encrypt=false;" +
                       $"TrustServerCertificate=True;Connection Timeout=300;App=EntityFramework";
            }
        }

        /// <summary>
        /// Kiểm tra kết nối đến SQL Server
        /// </summary>
        public static bool TestConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("⚠️ Chuỗi kết nối trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Bỏ comment dòng dưới để hiển thị thông báo thành công nếu cần
                    // MessageBox.Show("✅ Kết nối thành công!", "Thành công",
                    //     MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Kết nối thất bại:\n{ex.Message}", "Lỗi Kết nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
