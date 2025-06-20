using RegistrationForm1;
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
    public partial class LoginForm : Form
    {
        public User CurrentUser { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra 
                if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Đăng nhập
                User loggedInUser = UserService.LoginUser(txtFullName.Text.Trim(), txtPassword.Text);
                if (loggedInUser != null)
                {
                    // CẬP NHẬT THỜI GIAN ĐĂNG NHẬP
                    UserService.UpdateLoginTime(loggedInUser.UserID);

                    // TRUYỀN THÔNG TIN USER VÀO HOME FORM
                    FrmMain homeForm = new FrmMain(loggedInUser);
                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng {loggedInUser.FullName} ({loggedInUser.Position})",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    homeForm.ShowDialog();

                    // CẬP NHẬT THỜI GIAN ĐĂNG XUẤT KHI ĐÓNG HOME FORM
                    UserService.UpdateLogoutTime(loggedInUser.UserID);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Đảm bảo PermissionManager ở trạng thái LOGOUT để không can thiệp vào quá trình đăng ký
                PermissionManager.Logout();
                // Tạo form đăng ký với chế độ công khai
                // isRegistrationMode = true, isPublicRegistration = true
                FrmDangKyUser registrationForm = new FrmDangKyUser(isRegistrationMode: true, isPublicRegistration: true);
                DialogResult result = registrationForm.ShowDialog();
                if (result == DialogResult.OK)
                {                 
                    // Clear các field để user có thể đăng nhập
                    txtFullName.Clear();
                    txtPassword.Clear();
                    txtFullName.Focus();
                }

                // Đảm bảo PermissionManager vẫn ở trạng thái logout
                PermissionManager.Logout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình đăng ký: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
