using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmDangKyUser : Form
    {
         private string connectionString => ConfigurationHelper.GetConnectionString();
        private int? editingUserId = null;
        public FrmDangKyUser()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new string[] { "Quản Lý", "Vận Hành", "Bảo Trì" });
            cboRole.SelectedIndex = 0;

        }  
        public FrmDangKyUser(int userId) : this()
        {
            editingUserId = userId;
            LoadUserData();
        }
        private void LoadUserData()
        {
            if (!editingUserId.HasValue)
                return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", editingUserId.Value);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtFullName.Text = reader["FullName"].ToString();
                    txtUsername.Text = reader["Username"].ToString();
                    txtPosition.Text = reader["Position"].ToString();
                    cboRole.SelectedItem = reader["Role"].ToString();

                    if (reader["DateOfBirth"] != DBNull.Value)
                        dtpDOB.Value = Convert.ToDateTime(reader["DateOfBirth"]);
                    else
                        dtpDOB.Value = DateTime.Now;
                }
            }
        }
        // Tạo nút test sửa data
        
        //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (editingUserId.HasValue)
            {
                UpdateUser(); // cập nhật user
            }
            else
            {
                AddUser(); // thêm user mới
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
  
        private void AddUser()
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text.Trim());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (FullName, Username, Password, DateOfBirth, Position, Role) VALUES (@f, @u, @p, @dob, @pos, @r)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@f", txtFullName.Text);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", hashedPassword);
                cmd.Parameters.AddWithValue("@dob", dtpDOB.Value);
                cmd.Parameters.AddWithValue("@pos", txtPosition.Text);
                cmd.Parameters.AddWithValue("@r", cboRole.SelectedItem?.ToString() ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateUser()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET FullName=@f, DateOfBirth=@dob, Position=@p, Role=@r WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@f", txtFullName.Text);
                cmd.Parameters.AddWithValue("@dob", dtpDOB.Value);
                cmd.Parameters.AddWithValue("@p", txtPosition.Text);
                cmd.Parameters.AddWithValue("@r", cboRole.SelectedItem?.ToString() ?? "");
                cmd.Parameters.AddWithValue("@id", editingUserId.Value);
                cmd.ExecuteNonQuery();
            }
        }
    
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Sự kiện khi nhấn nút "Đăng ký"
        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            // Chỉ Admin mới được phép mở form này
            if (!PermissionManager.CheckPermissionWithMessage("add_user"))
                return;

            FrmDangKyUser frm = new FrmDangKyUser();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Tài khoản mới đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            DateTime dob = dtpDOB.Value.Date;
            string position = txtPosition.Text.Trim();
            string role = cboRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Băm mật khẩu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra username có bị trùng không
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @u";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@u", username);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại.");
                    return;
                }

                string query = @"INSERT INTO Users (FullName, Username, Password, DateOfBirth, Position, Role)
                                 VALUES (@FullName, @Username, @Password, @DateOfBirth, @Position, @Role)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@DateOfBirth", dob);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Role", role);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại!");
                }
            }
        }
    }
}
    
 
