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
    public partial class FrmDoipassword : Form
    {
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public FrmDoipassword()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string username = PermissionManager.CurrentUsername;
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.");
                return;
            }

            // Lấy mật khẩu hash từ DB
            string storedHash = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            //     using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=TenCSDL;Integrated Security=True"))
            {
                conn.Open();
                string selectQuery = "SELECT PasswordHash FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        storedHash = reader["PasswordHash"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người dùng.");
                        return;
                    }
                }
            }

            // So sánh mật khẩu cũ
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, storedHash))
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
                return;
            }

            // Băm mật khẩu mới
            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Cập nhật vào DB
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string updateQuery = "UPDATE Users SET PasswordHash = @NewPassword WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newHashedPassword);
                    cmd.Parameters.AddWithValue("@Username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đổi mật khẩu thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản để cập nhật.");
                    }
                }
            }
        }
    }
}
