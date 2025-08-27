using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
//using System.Globalization;
//using System.Threading;

namespace RegistrationForm1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Kiểm tra cấu hình hiện tại một cách im lặng (không hiển thị MessageBox)
            string connectionString = ConfigurationHelper.GetConnectionString();
            bool isConfigValid = !string.IsNullOrEmpty(connectionString) &&
                                TestConnectionSilently(connectionString);

            Globalvariable.ConfigSystem = Globalvariable.ConfigSystem ?? new ConfigSystemModel("DefaultConfig", "DefaultValue", "DefaultDescription"); // Khởi tạo ConfigSystem nếu chưa có
            Globalvariable.TagsValues.Add(new TagsValueModel()
                                            {
                                                TagName = "A",
                                                Value = "0"
                                            }
            ); // Khởi tạo TagsValues nếu chưa có

            isConfigValid = true; // Bỏ qua kiểm tra kết nối, giả sử cấu hình luôn hợp lệ

            if (isConfigValid)
            {
                // Nếu cấu hình đã hợp lệ, chạy ứng dụng chính
                // Thay YourMainForm bằng form chính của bạn
              //         Application.Run(new Form2());
                Application.Run(new FrmLogin());
            }
            else
            {
                // Nếu cấu hình chưa hợp lệ, hiển thị form cấu hình
                bool configurationCompleted = false;

                while (!configurationCompleted)
                {
                    MessageBox.Show("Cấu hình cơ sở dữ liệu không hợp lệ hoặc chưa được thiết lập. Vui lòng cấu hình.",
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    using (var configForm = new FrmCaiDat())
                    {
                        DialogResult result = configForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            // Kiểm tra lại cấu hình sau khi người dùng lưu
                            connectionString = ConfigurationHelper.GetConnectionString();
                            if (!string.IsNullOrEmpty(connectionString) &&
                                TestConnectionSilently(connectionString))
                            {
                                configurationCompleted = true;
                                // Chạy ứng dụng chính sau khi cấu hình thành công
                                Application.Run(new FrmLogin());
                            }
                            else
                            {
                                MessageBox.Show("Cấu hình vẫn chưa hợp lệ. Vui lòng kiểm tra lại.",
                                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Nếu người dùng đóng form mà không lưu, thoát ứng dụng
                            configurationCompleted = true; // Thoát khỏi vòng lặp
                            return;
                        }
                    }
                }


                FrmLogin login = new FrmLogin();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FrmMain());

                }
            }



        }
        private static bool TestConnectionSilently(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return false;
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
