using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public class ConfigurationHelper
    {
        // Lấy chuỗi kết nối từ App.config
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MyAppContext"]?.ConnectionString;
        }

        // Lưu chuỗi kết nối vào App.config
        public static void SaveConnectionString(string connectionString)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

                if (connectionStringsSection.ConnectionStrings["MyAppContext"] != null)
                {
                    connectionStringsSection.ConnectionStrings["MyAppContext"].ConnectionString = connectionString;
                }
                else
                {
                    connectionStringsSection.ConnectionStrings.Add(new ConnectionStringSettings("MyAppContext", connectionString));
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("Lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kiểm tra kết nối đến CSDL
        public static bool TestConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kết nối thất bại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
