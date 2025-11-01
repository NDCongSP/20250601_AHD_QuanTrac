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
                        .Select(u => new
                        {
                          //  u.Id,
                            u.UserName,
                            u.FullName,
                            u.PermissionScada,
                            u.CreateOperatorId,
                            u.CreateAt,
                            u.UpdateOperatorId,
                            u.UpdateAt
                        })
                        .ToList();

             //       MessageBox.Show($"Tổng số user lấy được: {users.Count}");

                    dgvUsers.DataSource = users;
                    dgvUsers.ScrollBars = ScrollBars.Both;
                    dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvUsers.ReadOnly = true;
                    dgvUsers.RowHeadersVisible = false;

                    dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
