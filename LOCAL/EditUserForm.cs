using System;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class EditUserForm : Form
    {
        private User currentUser;
        private UserRole currentUserRole;

        // Default constructor
        public EditUserForm()
        {
            InitializeComponent();
        }

        // Constructor with User parameter
        public EditUserForm(User user) : this()
        {
            currentUser = user;
            currentUserRole = UserRole.Admin; // Default to Admin for backward compatibility
            LoadUserData();
            SetupPermissions();
        }

        // New constructor with User and UserRole parameters
        public EditUserForm(User user, UserRole userRole) : this()
        {
            currentUser = user;
            currentUserRole = userRole;
            LoadUserData();
            SetupPermissions();
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            // LoadUserData and SetupPermissions are called in the constructor
        }

        private void LoadUserData()
        {
            if (currentUser != null)
            {
                txtUserID.Text = currentUser.UserID.ToString();
                txtFullName.Text = currentUser.FullName;
                dtpBirth.Value = currentUser.DateOfBirth;
                cmbPosition.Text = currentUser.Position;
                chkIsActive.Checked = currentUser.IsActive;
                this.Text = $"Chỉnh sửa người dùng - {currentUser.FullName} ({GetRoleDisplayName(currentUserRole)})";
            }
        }

        private void SetupPermissions()
        {
            // Configure form controls based on user role
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    // Employees can edit FullName and DateOfBirth
                    txtUserID.Enabled = false;
                    txtFullName.Enabled = true;
                    dtpBirth.Enabled = true;
                    cmbPosition.Enabled = false; // Restrict Position
                    chkIsActive.Enabled = false; // Restrict IsActive
                    break;

                case UserRole.Maintenance:
                    // Maintenance can edit FullName and DateOfBirth
                    txtUserID.Enabled = false;
                    txtFullName.Enabled = true;
                    dtpBirth.Enabled = true;
                    cmbPosition.Enabled = false; // Restrict Position
                    chkIsActive.Enabled = false; // Restrict IsActive
                    break;

                case UserRole.Admin:
                    // Admins can edit all fields
                    txtUserID.Enabled = false; // UserID is typically read-only
                    txtFullName.Enabled = true;
                    dtpBirth.Enabled = true;
                    cmbPosition.Enabled = true;
                    chkIsActive.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Quyền không hợp lệ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    break;
            }
        }

        private string GetRoleDisplayName(UserRole role)
        {
            switch (role)
            {
                case UserRole.Employee:
                    return "Nhân viên";
                case UserRole.Maintenance:
                    return "Vận hành bảo trì";
                case UserRole.Admin:
                    return "Quản trị viên";
                default:
                    return "Không xác định";
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (cmbPosition.SelectedIndex == -1 && currentUserRole == UserRole.Admin)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPosition.Focus();
                return false;
            }

            DateTime today = DateTime.Today;
            int age = today.Year - dtpBirth.Value.Year;
            if (dtpBirth.Value.Date > today.AddYears(-age)) age--;

            if (age < 18 || age > 65)
            {
                MessageBox.Show("Tuổi phải từ 18 đến 65!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirth.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // KIỂM TRA QUYỀN TRƯỚC KHI LÀM GÌ
                if (!PermissionManager.HasPermission("edit"))
                {
                    MessageBox.Show("Bạn không có quyền chỉnh sửa!", "Không có quyền",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // KIỂM TRA LOGIC BỔ SUNG: Không phải Admin chỉ được sửa thông tin của mình
                if (currentUserRole != UserRole.Admin)
                {
                    if (currentUser.UserID != PermissionManager.CurrentUserID)
                    {
                        MessageBox.Show("Bạn chỉ có thể sửa thông tin của chính mình!",
                            "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Update user information
                currentUser.FullName = txtFullName.Text.Trim();
                currentUser.DateOfBirth = dtpBirth.Value;

                // CHỈ ADMIN MỚI ĐƯỢC THAY ĐỔI POSITION VÀ ISACTIVE
                if (currentUserRole == UserRole.Admin)
                {
                    currentUser.Position = cmbPosition.Text;
                    currentUser.IsActive = chkIsActive.Checked;
                }
                // Với non-Admin, Position và IsActive giữ nguyên

                // Call service to update
                if (UserService.UpdateUser(currentUser))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}