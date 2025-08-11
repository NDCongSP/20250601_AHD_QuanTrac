using System;
using System.Data.SqlClient;

namespace RegistrationForm1
{
    public static class LoginService
    {
        public static bool ValidateUser(string username, string password, out string role)
        {
            role = null;

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=TenCSDL;Integrated Security=True"))
                {
                    conn.Open();
                    string query = "SELECT PasswordHash, Role FROM Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["PasswordHash"].ToString();
                                role = reader["Role"].ToString();

                                // So sánh mật khẩu bằng BCrypt
                                return BCrypt.Net.BCrypt.Verify(password, storedHash);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi khi kiểm tra tài khoản: " + ex.Message);
            }

            return false;
        }
    }
}