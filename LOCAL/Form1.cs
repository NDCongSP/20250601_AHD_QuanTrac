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
    public partial class Form1 : Form
    {
        public bool IsRegistrationMode { get; set; } = true;
        public bool IsPublicRegistration { get; set; } = false;
        public Form1()
        {
            InitializeComponent();
            IsRegistrationMode = true; // Đảm bảo mặc định là true
            IsPublicRegistration = false;
        }
        public Form1(bool isRegistrationMode = true, bool isPublicRegistration = false)
        {
            InitializeComponent();
            IsRegistrationMode = isRegistrationMode;
            IsPublicRegistration = isPublicRegistration;

            // Cập nhật giao diện dựa trên chức năng
            if (!IsRegistrationMode)
            {
                this.Text = "Thêm người dùng mới";
                btnSignup.Text = "Thêm người dùng";
            }
            else
            {
                if (isPublicRegistration)
                {
                    this.Text = "Đăng ký tài khoản mới";
                    btnSignup.Text = "Đăng ký";
                }
                else
                {
                    this.Text = "Đăng ký tài khoản";
                    btnSignup.Text = "Đăng ký";
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupFormByMode();
        }
        private void SetupFormByMode()
        {
            if (IsRegistrationMode)
            {
                // Chế độ đăng ký - cho phép tất cả chức vụ
                this.Text = "Đăng ký tài khoản";
                btnSignup.Text = "Đăng ký";
            }
            else
            {
                // Chế độ thêm user từ admin - có thể hạn chế một số chức vụ
                this.Text = "Thêm người dùng mới";
                btnSignup.Text = "Thêm người dùng";
            }
        }


        private void btnSignup_Click_1(object sender, EventArgs e)
        {
            try
            {

                // Validation
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }


                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Text != txtconPassword.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtconPassword.Focus();
                    return;
                }

                if (cmbPosition.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPosition.Focus();
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhoneNumber.Focus();
                    return;
                    // Kiểm tra format số điện thoại (chỉ số và ít nhất 10 ký tự)
                    
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNumber.Text, @"^[0-9]{10,15}$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập 10-15 số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhoneNumber.Focus();
                    return;
                }

                // Kiểm tra số điện thoại đã tồn tại
                if (UserService.CheckPhoneExists(txtPhoneNumber.Text))
                {
                    MessageBox.Show("Số điện thoại đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhoneNumber.Focus();
                    return;
                }
                string selectedPosition = cmbPosition.SelectedItem.ToString();
                if (!IsRegistrationMode && UserService.IsAdminPosition(selectedPosition))
                {
                    // Chỉ kiểm tra giới hạn admin khi thêm user từ bên trong hệ thống
                    if (!UserService.CanCreateAdmin())
                    {
                        MessageBox.Show("Hệ thống chỉ cho phép có 1 quản trị viên!\nVui lòng chọn chức vụ khác.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbPosition.Focus();
                        return;
                    }
                }
                else if (IsRegistrationMode && UserService.IsAdminPosition(selectedPosition))
                {
                    // THÊM: Trong chế độ đăng ký, vẫn kiểm tra admin nhưng với thông báo khác
                    if (!UserService.CanCreateAdmin())
                    {
                        MessageBox.Show("Hệ thống đã có quản trị viên!\nVui lòng chọn chức vụ khác để đăng ký.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbPosition.Focus();
                        return;
                    }
                }

                // Check if username exists
                if (UserService.CheckUsernameExists(txtFullName.Text))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                // Create user object
                User newUser = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    DateOfBirth = dtpBirth.Value,
                    Password = txtPassword.Text,
                    Position = cmbPosition.SelectedItem.ToString(),
                    PhoneNumber = txtPhoneNumber.Text.Trim()
                };

                // Register user
                bool success = false;
                if (IsPublicRegistration)
                {
                    // Đăng ký công khai - không kiểm tra quyền
                    success = UserService.RegisterUserPublic(newUser);
                }
                else
                {
                    // Đăng ký từ admin - có kiểm tra quyền
                    success = UserService.RegisterUser(newUser);
                }

                if (success)
                {
                    if (IsRegistrationMode)
                    {
                        string message = IsPublicRegistration ?
                            "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ." :
                            "Đăng ký thành công!";

                        MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
 }
