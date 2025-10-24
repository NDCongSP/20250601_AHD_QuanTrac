using System;

using System.Linq;
using System.Windows.Forms;


namespace RegistrationForm1
{
    public partial class FrmUserManager : Form
    {
     
        public FrmUserManager()
        {
            InitializeComponent();
            
        }

        private void FrmUserManager_Load(object sender, EventArgs e)
        {
            LoadUsers();

           

        }
        private void LoadUsers()
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    var users = dbContext.ScadaUsers
                        .Where(u => u.IsDeleted == false)
                        .OrderByDescending(u => u.CreateAt)
                        .Select(u => new
                        {
                            u.Id,
                            u.UserName,
                            u.FullName,
                            u.PermissionScada,
                            u.CreateOperatorId,
                            u.CreateAt,
                            u.UpdateOperatorId,
                            u.UpdateAt
                        })
                        .ToList();

                    dgvUsers.DataSource = users;
                    dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvUsers.ReadOnly = true;
                    dgvUsers.RowHeadersVisible = false;

                    // ✅ Đặt tiêu đề cột sang tiếng Việt
                    dgvUsers.Columns["Id"].HeaderText = "Mã người dùng";
                    dgvUsers.Columns["UserName"].HeaderText = "Tên đăng nhập";
                    dgvUsers.Columns["FullName"].HeaderText = "Họ và tên";
                    dgvUsers.Columns["PermissionScada"].HeaderText = "Quyền truy cập";
                    dgvUsers.Columns["CreateOperatorId"].HeaderText = "Người tạo";
                    dgvUsers.Columns["CreateAt"].HeaderText = "Ngày tạo";
                    dgvUsers.Columns["UpdateOperatorId"].HeaderText = "Người cập nhật";
                    dgvUsers.Columns["UpdateAt"].HeaderText = "Ngày cập nhật";
                    // Căn giữa tiêu đề cột
                    dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Căn giữa nội dung cho các cột số hoặc ngắn
                    dgvUsers.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvUsers.Columns["PermissionScada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Đặt định dạng ngày giờ đẹp
                    dgvUsers.Columns["CreateAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvUsers.Columns["UpdateAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
