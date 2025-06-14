using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace RegistrationForm1
{
    // Enum định nghĩa các loại quyền
    public enum UserRole
    {
        Employee = 1,      // Nhân viên - chỉ sửa
        Maintenance = 2,   // Vận hành bảo trì - chỉ sửa
        Admin = 3          // Quản trị viên - tất cả quyền
    }

    public partial class Home : Form
    {
        private UserRole currentUserRole;
        private string currentUserName;
        public string FormTitle
        {
            get
            {
                string roleText = GetRoleDisplayName(currentUserRole);
                return $"Quản lý người dùng - {currentUserName} ({roleText})";
            }
        }

        // THÊM: Event để thông báo khi title thay đổi
        public event EventHandler TitleChanged;
        // Constructor mặc định - giả sử Admin để test
        public Home(User loggedInUser)
        {
            InitializeComponent();
           
            if (loggedInUser == null)
            {
                MessageBox.Show("ERROR: loggedInUser is null! Home form should not be opened without a logged-in user.",
                    "Debug Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("ERROR: Home constructor called with null user!");

                // Đóng form ngay lập tức
                this.WindowState = FormWindowState.Minimized;
                this.Close();
                return;
            }
            System.Diagnostics.Debug.WriteLine($"=== CONSTRUCTOR START ===");
            System.Diagnostics.Debug.WriteLine($"User Position: '{loggedInUser.Position}'");

            // QUAN TRỌNG: Đảm bảo mapping chính xác và KHÔNG có default fallback
            switch (loggedInUser.Position.Trim()) // Thêm Trim() để loại bỏ khoảng trắng
            {
                case "Quản trị viên":
                    this.currentUserRole = UserRole.Admin;
                    System.Diagnostics.Debug.WriteLine("Set role to Admin");
                    break;
                case "Nhân viên":
                    this.currentUserRole = UserRole.Employee;
                    System.Diagnostics.Debug.WriteLine("Set role to Employee");
                    break;
                case "Vận hành bảo trì":
                    this.currentUserRole = UserRole.Maintenance;
                    System.Diagnostics.Debug.WriteLine("Set role to Maintenance");
                    break;
                default:

                    System.Diagnostics.Debug.WriteLine($"ERROR: Unknown position '{loggedInUser.Position}' - This should not happen!");


                    throw new ArgumentException($"Unknown user position: '{loggedInUser.Position}'. Valid positions are: 'Quản trị viên', 'Nhân viên', 'Vận hành bảo trì'");
                    
            }

            this.currentUserName = loggedInUser.FullName;

            // CẬP NHẬT PermissionManager NGAY LẬP TỨC
            PermissionManager.Login(this.currentUserRole, this.currentUserName, loggedInUser.UserID);

            System.Diagnostics.Debug.WriteLine($"After PermissionManager.Login - PermissionManager.CurrentUserRole: {PermissionManager.CurrentUserRole}");
            System.Diagnostics.Debug.WriteLine($"=== CONSTRUCTOR END ===");
        }

        // Constructor với tham số phân quyền
        public Home(UserRole userRole, string userName)
        {
            InitializeComponent();
            this.currentUserRole = userRole;
            this.currentUserName = userName;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Home_Load - Current Role: {currentUserRole}");
            System.Diagnostics.Debug.WriteLine($"Home_Load - PermissionManager Role: {PermissionManager.CurrentUserRole}");
            
            
            LoadUserData();
            SetupPermissions();
            UpdateFormTitle();
        }
        
        private void SetupPermissions()
        {
            System.Diagnostics.Debug.WriteLine($"=== SETUP PERMISSIONS START ===");
            System.Diagnostics.Debug.WriteLine($"currentUserRole: {currentUserRole}");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.CurrentUserRole (before): {PermissionManager.CurrentUserRole}");

            // ĐẢM BẢO PermissionManager được cập nhật TRƯỚC KHI kiểm tra quyền
            PermissionManager.Login(currentUserRole, currentUserName, PermissionManager.CurrentUserID);
            System.Diagnostics.Debug.WriteLine($"After Login - PermissionManager.CurrentUserRole: {PermissionManager.CurrentUserRole}");

            // THÊM DELAY NHỎ để đảm bảo state được update
            System.Threading.Thread.Sleep(10);

            // KIỂM TRA DEBUG để đảm bảo quyền được set đúng
            System.Diagnostics.Debug.WriteLine($"=== PERMISSION CHECKS ===");
            System.Diagnostics.Debug.WriteLine($"Add permission: {PermissionManager.HasPermission("add")}");
            System.Diagnostics.Debug.WriteLine($"Edit permission: {PermissionManager.HasPermission("edit")}");
            System.Diagnostics.Debug.WriteLine($"Delete permission: {PermissionManager.HasPermission("delete")}");
            System.Diagnostics.Debug.WriteLine($"View permission: {PermissionManager.HasPermission("view")}");

            // SỬ DỤNG PermissionManager ĐẢM BẢO NHẤT QUÁN
            btnAdd.SetPermission("add", "Thêm");
            btnEdit.SetPermission("edit", "Sửa");
            btnDelete.SetPermission("delete", "Xóa");
            btnResearch.SetPermission("view", "Tìm kiếm");
            btnClearResarch.SetPermission("view", "Xóa bộ lọc");

            // Enable/disable date pickers dựa trên quyền view
            bool canSearch = PermissionManager.HasPermission("view");
            dtpFromDate.Enabled = canSearch;
            dtpToDate.Enabled = canSearch;
            lblFromDate.Enabled = canSearch;
            lblToDate.Enabled = canSearch;
            // Các button luôn enabled
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;

            System.Diagnostics.Debug.WriteLine($"=== FINAL BUTTON STATES ===");
            System.Diagnostics.Debug.WriteLine($"Add enabled: {btnAdd.Enabled}");
            System.Diagnostics.Debug.WriteLine($"Edit enabled: {btnEdit.Enabled}");
            System.Diagnostics.Debug.WriteLine($"Delete enabled: {btnDelete.Enabled}");
            System.Diagnostics.Debug.WriteLine($"ReSearch enabled: {btnResearch.Enabled}");
            System.Diagnostics.Debug.WriteLine($"=== SETUP PERMISSIONS END ===");
        }
       
        private void UpdateFormTitle()
        {
            string roleText = GetRoleDisplayName(currentUserRole);
            this.Text = $"Quản lý người dùng - {currentUserName} ({roleText})";
            TitleChanged?.Invoke(this, EventArgs.Empty);
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

        private bool CheckPermission(string action)
        {
            switch (action.ToLower())
            {
                case "add":
                    return currentUserRole == UserRole.Admin;

                case "edit":
                    return currentUserRole == UserRole.Admin ||
                           currentUserRole == UserRole.Employee ||
                           currentUserRole == UserRole.Maintenance;

                case "delete":
                    return currentUserRole == UserRole.Admin; // Chỉ Admin được xóa

                default:
                    return false;
            }
        }



        private void LoadUserData()
        {
            try
            {
                // Hiển thị loading
                this.Cursor = Cursors.WaitCursor;

                // Clear DataGridView
                dataGridView1.DataSource = null;

                // Load data từ database
                DataTable userData = UserService.GetAllUsers();

                if (userData != null)
                {
                    if (userData.Rows.Count > 0)
                    {
                        // Bind data
                        dataGridView1.DataSource = userData;

                        // Customize appearance
                        CustomizeDataGridView();

                        // Update title với thông tin quyền
                        UpdateFormTitle();
                        this.Text += $" - Tổng số: {userData.Rows.Count} người dùng";
                        TitleChanged?.Invoke(this, EventArgs.Empty);
                        System.Diagnostics.Debug.WriteLine($"Loaded {userData.Rows.Count} users successfully");
                    }
                    else
                    {
                        // Không có dữ liệu
                        UpdateFormTitle();
                        this.Text += " - Không có dữ liệu";

                        if (CheckPermission("add"))
                        {
                            MessageBox.Show("Database không có dữ liệu!\n\nHãy thêm user mới bằng nút 'Thêm'.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Database không có dữ liệu!\n\nLiên hệ Quản trị viên để thêm người dùng mới.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể lấy dữ liệu từ database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadUserData Error: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void CustomizeDataGridView()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                // Đặt tiêu đề cột bằng tiếng Việt
                if (dataGridView1.Columns.Contains("UserID"))
                    dataGridView1.Columns["UserID"].HeaderText = "Mã người dùng";

                if (dataGridView1.Columns.Contains("FullName"))
                    dataGridView1.Columns["FullName"].HeaderText = "Họ và tên";

                if (dataGridView1.Columns.Contains("DateOfBirth"))
                {
                    dataGridView1.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
                    dataGridView1.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dataGridView1.Columns.Contains("Position"))
                    dataGridView1.Columns["Position"].HeaderText = "Chức vụ";

                if (dataGridView1.Columns.Contains("CreatedDate"))
                {
                    dataGridView1.Columns["CreatedDate"].HeaderText = "Ngày tạo";
                    dataGridView1.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                if (dataGridView1.Columns.Contains("LastLoginTime"))
                {
                    dataGridView1.Columns["LastLoginTime"].HeaderText = "Lần đăng nhập cuối";
                    dataGridView1.Columns["LastLoginTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    dataGridView1.Columns["LastLoginTime"].Width = 150;
                    dataGridView1.Columns["LastLoginTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dataGridView1.Columns.Contains("LastLogoutTime"))
                {
                    dataGridView1.Columns["LastLogoutTime"].HeaderText = "Lần đăng xuất cuối";
                    dataGridView1.Columns["LastLogoutTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    dataGridView1.Columns["LastLogoutTime"].Width = 150;
                    dataGridView1.Columns["LastLogoutTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dataGridView1.Columns.Contains("IsActive"))
                    dataGridView1.Columns["IsActive"].HeaderText = "Trạng thái";

                // Ẩn cột Password nếu có
                if (dataGridView1.Columns.Contains("Password"))
                    dataGridView1.Columns["Password"].Visible = false;

                // Đặt độ rộng cột
                dataGridView1.Columns["UserID"].Width = 80;
                dataGridView1.Columns["FullName"].Width = 200;
                dataGridView1.Columns["Position"].Width = 120;
                dataGridView1.Columns["DateOfBirth"].Width = 120;
                dataGridView1.Columns["CreatedDate"].Width = 150;
                dataGridView1.Columns["IsActive"].Width = 100;

                // Căn giữa một số cột
                dataGridView1.Columns["UserID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["DateOfBirth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["IsActive"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"=== ADD BUTTON CLICKED ===");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.CurrentUserRole: {PermissionManager.CurrentUserRole}");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.HasPermission('add'): {PermissionManager.HasPermission("add")}");

            // CHỈ KIỂM TRA QUYỀN - Button này CHỈ dành cho việc them user từ bên trong hệ thống
            if (!PermissionManager.CheckPermissionWithMessage("add"))
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine("Access granted - Opening add user form");

            this.Hide();
            FrmDangKyUser addUserForm = new FrmDangKyUser(false); // false = chế độ thêm user từ bên trong hệ thống

            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                LoadUserData();
            }

            this.Show(); // Hiển thị lại Home form

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermissionWithMessage("edit"))
            {
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                if (selectedRow.Cells["UserID"].Value == null ||
                    selectedRow.Cells["FullName"].Value == null ||
                    selectedRow.Cells["DateOfBirth"].Value == null ||
                    selectedRow.Cells["Position"].Value == null ||
                    selectedRow.Cells["IsActive"].Value == null)
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Vui lòng kiểm tra lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                User selectedUser = new User
                {
                    UserID = Convert.ToInt32(selectedRow.Cells["UserID"].Value),
                    FullName = selectedRow.Cells["FullName"].Value.ToString(),
                    DateOfBirth = Convert.ToDateTime(selectedRow.Cells["DateOfBirth"].Value),
                    Position = selectedRow.Cells["Position"].Value.ToString(),
                    IsActive = Convert.ToBoolean(selectedRow.Cells["IsActive"].Value)
                };

                EditUserForm editForm = new EditUserForm(selectedUser, currentUserRole);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUserData();
                    MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form chỉnh sửa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"=== DELETE BUTTON CLICKED ===");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.CurrentUserRole: {PermissionManager.CurrentUserRole}");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.HasPermission('delete'): {PermissionManager.HasPermission("delete")}");

            // CHỈ SỬ DỤNG PermissionManager
            if (!PermissionManager.CheckPermissionWithMessage("delete"))
            {
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int userID = Convert.ToInt32(selectedRow.Cells["UserID"].Value);
            string fullName = selectedRow.Cells["FullName"].Value.ToString();

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{fullName}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (UserService.DeleteUser(userID))
                    {
                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_EnabledChanged(object sender, EventArgs e)
        {

        }
       
        private void btnDelete_EnabledChanged(object sender, EventArgs e)
        {

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị loading cursor
                this.Cursor = Cursors.WaitCursor;

                // Reload dữ liệu
                LoadUserData();

                // Trả về cursor bình thường
                this.Cursor = Cursors.Default;

                // Thông báo refresh thành công (tùy chọn)
                MessageBox.Show("Dữ liệu đã được cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Trả về cursor bình thường nếu có lỗi
                this.Cursor = Cursors.Default;

                MessageBox.Show("Lỗi khi làm mới dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method để thay đổi quyền người dùng hiện tại (nếu cần)
        public void ChangeUserRole(UserRole newRole, string userName)
        {
            this.currentUserRole = newRole;
            this.currentUserName = userName;
            SetupPermissions();
            UpdateFormTitle();
        }


        private void btnResearch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"=== SEARCH BUTTON CLICKED ===");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.CurrentUserRole: {PermissionManager.CurrentUserRole}");
            System.Diagnostics.Debug.WriteLine($"PermissionManager.HasPermission('view'): {PermissionManager.HasPermission("view")}");

            // Kiểm tra quyền xem
            if (!PermissionManager.CheckPermissionWithMessage("view"))
            {
                return;
            }

            // Validate ngày
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                DateTime fromDate = dtpFromDate.Value.Date; // Lấy từ đầu ngày
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1); // Đến cuối ngày

                System.Diagnostics.Debug.WriteLine($"Searching from {fromDate} to {toDate}");

                // Gọi method tìm kiếm
                DataTable searchResult = UserService.SearchUsersByDateRange(fromDate, toDate);

                if (searchResult != null && searchResult.Rows.Count > 0)
                {
                    dataGridView1.DataSource = searchResult;
                    CustomizeDataGridView();

                    // Cập nhật title
                    UpdateFormTitle();
                    this.Text += $" - Tìm thấy: {searchResult.Rows.Count} kết quả";
                    TitleChanged?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show($"Tìm thấy {searchResult.Rows.Count} người dùng trong khoảng thời gian từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}",
                        "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Không có dữ liệu
                    dataGridView1.DataSource = null;
                    UpdateFormTitle();
                    this.Text += " - Không có kết quả";

                    MessageBox.Show($"Không tìm thấy người dùng nào trong khoảng thời gian từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}",
                        "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Search Error: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClearResarch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"=== CLEAR SEARCH BUTTON CLICKED ===");

       
            if (!PermissionManager.CheckPermissionWithMessage("view"))
            {
                return;
            }

            try
            {
               
                dtpFromDate.Value = DateTime.Today.AddDays(-30);
                dtpToDate.Value = DateTime.Today;

              
                LoadUserData();

                MessageBox.Show("Đã xóa bộ lọc và tải lại toàn bộ dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa bộ lọc: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportExcel(string path)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // Initialize Excel application
                application = new Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Excel.Worksheet)workbook.ActiveSheet;

                // Export column headers
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }

                // Export row data
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Handle null values
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

                // Auto-fit columns
                worksheet.Columns.AutoFit();

                // Save the workbook
                workbook.SaveAs(path);
                workbook.Saved = true;

                MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xuất file không thành công!\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Clean up Excel objects
                if (worksheet != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                if (application != null)
                {
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                }
            }
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Export Excel";
            savefile.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2021 (*.xls)|*.xls";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcel(savefile.FileName);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Xuất  file không  thành  công !\n"+ex.Message);
                }
            }
        }
    }  
}