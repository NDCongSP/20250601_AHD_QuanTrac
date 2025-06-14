using Ahd.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    
    public partial class FrmMain : Form
    {
        private User currentUser;
        private Form currentChildForm;

        public FrmMain(User loggedInUser)
        {
            InitializeComponent();
            this.currentUser = loggedInUser;

        }
        IAhdDriverConnector driver;
        private void MainHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentChildForm != null && !currentChildForm.IsDisposed)
            {
                currentChildForm.Close();
            }
            // Cập nhật thời gian đăng xuất cho user hiện tại
            if (PermissionManager.CurrentUserID > 0)
            {
                UserService.UpdateLogoutTime(PermissionManager.CurrentUserID);
            }
        }
        private void OpenFormInPanel(Form childForm, string Title)
        {
            try
            {
                if (currentUser == null)
                {
                    MessageBox.Show("Không có thông tin user!", "Lỗi",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đóng form con hiện tại nếu có
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                    currentChildForm = null;
                }

                // Thiết lập form con
                currentChildForm = childForm;
                childForm.TopLevel = false; // Quan trọng: Không phải top-level window
                childForm.FormBorderStyle = FormBorderStyle.None; // Bỏ border
                childForm.Dock = DockStyle.Fill; // Fill toàn bộ panel
               
                panelDesktop.Controls.Add(childForm);
                childForm.Show();
                if (label1 != null)
                {
                    label1.Text = Title;
                }
                if (childForm is Home homeForm)
                {
                    homeForm.TitleChanged += HomeForm_TitleChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HomeForm_TitleChanged(object sender, EventArgs e)
        {
            if (sender is Home homeForm && label1 != null)
            {
                label1.Text = homeForm.Text; // Sử dụng Text property của Home form
            }
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (currentUser == null)
                {
                    MessageBox.Show("Không có thông tin user để mở Home form!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                Home homeForm = new Home(currentUser);
                OpenFormInPanel(homeForm, "Hệ thống quản lý tài khoản");


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Home form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDevices_Click(object sender, EventArgs e)
        {
            DeviceStatusManager d = new DeviceStatusManager(currentUser);
            OpenFormInPanel(d, "Hệ thống quản lý thiết bị");

        }
             
        private void btnDataMonitor_Click(object sender, EventArgs e)
        {
            DataCollection data = new DataCollection(currentUser);
            OpenFormInPanel(data, "Hệ thống thu thập dữ liệu");
        }

        private void bnt_Tran_Click(object sender, EventArgs e)
        {
            FrmTran data = new FrmTran();
            OpenFormInPanel(data, "Hệ thống tràn");
        }

        private void bnt_TramMN_Click(object sender, EventArgs e)
        {
            FrmTramMN mn = new FrmTramMN();
            OpenFormInPanel(mn, "Mức Nước");
        }

        private void bnt_TrangChu_Click(object sender, EventArgs e)
        {
          FrmHome H = new FrmHome();
            OpenFormInPanel(H, " GIÁM SÁT CỦA TRÀN HỒ DẦU TIẾNG");
        }

        private void bnt_CanhBao_Click(object sender, EventArgs e)
        {
            FrmCanhBao canhBao = new FrmCanhBao();
            OpenFormInPanel(canhBao, " Thông Tin Cảnh Báo");
        }

        private void bnt_BaoCao_Click(object sender, EventArgs e)
        {
            FrmBaoCao baocao = new FrmBaoCao();
            OpenFormInPanel(baocao, "Báo Cáo");
        }

       

        private void bnt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
        }

        private void bnt_CaiDat_Click(object sender, EventArgs e)
        {
            FrmCaiDat caiDat = new FrmCaiDat();
            OpenFormInPanel(caiDat, " CÀI ĐẶT");
        }

        private void bnt_User_Click(object sender, EventArgs e)
        {
            try
            {

                if (currentUser == null)
                {
                    MessageBox.Show("Không có thông tin user để mở Home form!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Home homeForm = new Home(currentUser);
                OpenFormInPanel(homeForm, "Hệ thống quản lý tài khoản");


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Home form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bnt_LogIn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            lblTime.Text = DateTime.Now.ToLongTimeString();
            //  lblDate.Text = DateTime.Now.ToLongDateString();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //  lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            if (ahdDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                labDriverStatus.BackColor = Color.Green;
                labDriverStatus.Text = "PLC Đang Kết Nối";
            }
            else
            {
                labDriverStatus.BackColor = Color.Red;
                labDriverStatus.Text = "PLC Mất Kết Nối";
            }
        }
    }
}