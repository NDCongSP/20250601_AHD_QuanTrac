using System;
using System.Linq;
using System.Windows.Forms;
using BCrypt.Net;

namespace RegistrationForm1
{
    public partial class FrmDoipassword : Form
    {
        public FrmDoipassword()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            
                string username = Globalvariable.UserInfo?.UserName; // ✅ Lấy từ user đang đăng nhập

                string oldPassword = txtOldPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newPassword.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // (Tuỳ chọn) Kiểm tra độ mạnh của mật khẩu
                if (!System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$"))
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 1 chữ hoa, 1 số và 1 ký tự đặc biệt.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    using (var dbContext = new ApplicationDbContext())
                    {
                        if (string.IsNullOrEmpty(username))
                        {
                            MessageBox.Show("Không xác định được tài khoản hiện tại. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var user = dbContext.ScadaUsers
                            .FirstOrDefault(u => u.UserName == username && (u.IsDeleted == false || u.IsDeleted == null));

                        if (user == null)
                        {
                            MessageBox.Show($"Không tìm thấy người dùng: {username}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                        {
                            MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (BCrypt.Net.BCrypt.Verify(newPassword, user.Password))
                        {
                            MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                        user.UpdateOperatorId = username;
                        user.UpdateAt = DateTime.Now;

                        dbContext.SaveChanges();

                        MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOldPassword.Clear();
                        txtNewPassword.Clear();
                        txtConfirmPassword.Clear();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình đổi mật khẩu. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        // 👁️ Hiển thị / ẩn mật khẩu
        //private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool show = chkShowPassword.Checked;
        //    txtOldPassword.PasswordChar = show ? '\0' : '*';
        //    txtNewPassword.PasswordChar = show ? '\0' : '*';
        //    txtConfirmPassword.PasswordChar = show ? '\0' : '*';
        //}
    }
}
