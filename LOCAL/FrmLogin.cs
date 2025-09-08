using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmLogin : Form
    {
        public static int currentLoginLogId = 0; // Share giữa 2 form (FrmLogin và FrmMain)
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public FrmLogin()
        {
            InitializeComponent();
            //    CreateTestUser(); // Tạo user test mặc định (admin / 123456)
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (CheckLogin(username, password))
            {
                FrmMain main = new FrmMain();
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool CheckLogin(string username, string password)
        {
            using var dbContext = new ApplicationDbContext();
            Globalvariable.UserInfo = dbContext.ScadaUsers.FirstOrDefault(u => u.UserName == username);

            return Globalvariable.UserInfo != null && BCrypt.Net.BCrypt.Verify(password, Globalvariable.UserInfo.Password);
        }

        //private void CreateTestUser()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
        //        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
        //        int count = (int)checkCmd.ExecuteScalar();

        //        if (count > 0) return;

        //        string hashed = BCrypt.Net.BCrypt.HashPassword("123456");
        //        string query = "INSERT INTO Users (Username, PasswordHash, Role) VALUES ('admin', @p, 'Admin')";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@p", hashed);
        //        cmd.ExecuteNonQuery();

        //        MessageBox.Show("Tài khoản test admin/123456 đã được tạo.");
        //    }
        //}
        // Khi User thoát đăng nhập, lưu log đăng nhập
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (FrmLogin.currentLoginLogId > 0)
            {
                SaveLogoutTime(FrmLogin.currentLoginLogId);
            }
        }
        // LƯu thời gian đăng xuất vào bảng LoginLogs
        public static void SaveLogoutTime(int logId)
        {
            string query = "UPDATE LoginLogs SET LogoutTime = @logout WHERE Id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@logout", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", logId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu LogoutTime: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int SaveLoginLog(string username, bool isSuccess)
        {
            string ip = GetLocalIPAddress();
            string query = "INSERT INTO LoginLogs (Username, LoginTime, IPAddress, IsSuccess) OUTPUT INSERTED.Id VALUES (@username, @time, @ip, @success)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.Parameters.AddWithValue("@ip", ip);
                cmd.Parameters.AddWithValue("@success", isSuccess);
                return (int)cmd.ExecuteScalar(); // trả về ID mới insert
            }
        }
        private string GetLocalIPAddress()
        {
            string localIP = "Unknown";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy IP Address: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return localIP;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void bntRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
