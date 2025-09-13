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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RegistrationForm1
{
    public partial class FrmUserManager : Form
    {
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public FrmUserManager()
        {
            InitializeComponent();
            
        }

        private void FrmUserManager_Load(object sender, EventArgs e)
        {
            LoadUsers();

            // Phân quyền button theo role
            btnAdd.Enabled = PermissionManager.HasPermission("add_user");
            btnEdit.Enabled = PermissionManager.HasPermission("edit_user");
            btnDelete.Enabled = PermissionManager.HasPermission("delete_user");
       //     edit_data.Enabled = PermissionManager.HasPermission("edit_data");

        }
        private void LoadUsers()
        {

           // using var dbContext = new ApplicationDbContext();
           // string username = null;
           // Globalvariable.UserInfo = dbContext.ScadaUsers.FirstOrDefault(u => u.UserName == username);

           //// using (SqlConnection conn = new SqlConnection(connectionString))
           // {
           //     //conn.Open();
           //     //SqlDataAdapter da = new SqlDataAdapter("SELECT Id, FullName, Username, Role FROM Users", conn);
           //     DataTable dt = new DataTable();
           // //    da.Fill(dt);
           //     dgvUsers.DataSource = dt;
           //     // ✅ Đặt tiêu đề tiếng Việt cho từng cột
           //     //dgvUsers.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           //     //dgvUsers.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           //     //dgvUsers.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           //     //dgvUsers.Columns["Role"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
               
           //     dgvUsers.Columns["Id"].HeaderText = "STT";
           //     dgvUsers.Columns["FullName"].HeaderText = "Họ Và Tên";
           //     dgvUsers.Columns["Username"].HeaderText = "Tên Đăng Nhập";
           //     dgvUsers.Columns["Role"].HeaderText = "Phân Quyền";
           //     // ✅ Tùy chỉnh màu tiêu đề
           //     dgvUsers.EnableHeadersVisualStyles = false; // Cho phép tùy chỉnh tiêu đề
           //     dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
           //     dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
           //     dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
           //     dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermissionWithMessage("add_user"))
                return;

            FrmDangKyUser frm = new FrmDangKyUser();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
                MessageBox.Show("Đã thêm user mới!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermissionWithMessage("delete_user"))
                return;

            if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Cells["Id"].Value != DBNull.Value)
            {
                int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["Id"].Value);
                string username = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

                var confirm = MessageBox.Show($"Xác nhận xóa user: {username}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.ExecuteNonQuery();
                    }
                    LoadUsers();
                    MessageBox.Show($"Đã xóa user: {username}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn user để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (!PermissionManager.CheckPermissionWithMessage("edit_user"))
                return;

            if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Cells["Id"].Value != DBNull.Value)
            {
                int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["Id"].Value);
                FrmDangKyUser frm = new FrmDangKyUser(userId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();
                    string username = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();
                    MessageBox.Show($"Đã cập nhật user: {username}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn user để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var frm = new FrmDoipassword();
            frm.ShowDialog();
        }
    
    }
}
