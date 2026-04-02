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
                        .Where(u => !u.IsDeleted.HasValue || u.IsDeleted == false)
                        .OrderByDescending(u => u.CreateAt)
                        .AsEnumerable()
                        .Select((u, index) => new
                        {
                            STT = index + 1,
                            u.UserName,
                            u.FullName,
                            u.PermissionScada,
                            u.CreateOperatorId,
                            u.CreateAt,
                            u.UpdateOperatorId,
                            u.UpdateAt
                        })
                        .ToList();

                    dgvUsers.SuspendLayout();

                    dgvUsers.DataSource = users;
                    dgvUsers.Columns["CreateAt"].HeaderText = "Ngày tạo";
                    dgvUsers.Columns["UpdateAt"].HeaderText = "Ngày cập nhật";
                    dgvUsers.Columns["UserName"].HeaderText = "Tài khoản";
                    dgvUsers.Columns["FullName"].HeaderText = "Họ tên";
                    dgvUsers.Columns["PermissionScada"].HeaderText = "Quyền";
                    dgvUsers.Columns["CreateOperatorId"].HeaderText = "Người tạo";
                    dgvUsers.Columns["UpdateOperatorId"].HeaderText = "Người sửa";

                    dgvUsers.AutoResizeColumns();
                    dgvUsers.ScrollBars = ScrollBars.Both;
                    dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvUsers.ReadOnly = true;
                    dgvUsers.RowHeadersVisible = false;
                    dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Căn chỉnh cột
                    if (dgvUsers.Columns.Contains("STT"))
                        dgvUsers.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgvUsers.ResumeLayout();

                    if (users.Count == 0)
                        MessageBox.Show("Không có người dùng nào được tìm thấy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
