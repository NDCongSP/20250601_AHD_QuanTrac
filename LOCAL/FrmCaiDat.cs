using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmCaiDat : Form
    {
        public FrmCaiDat()
        {
            InitializeComponent();


        }

        private void FrmCaiDat_Load(object sender, EventArgs e)
        {
            // Đọc cấu hình cũ nếu có
            txtServer.Text = ConfigurationManager.AppSettings["ServerName"] ?? @".\SQLEXPRESS";
            txtDatabase.Text = ConfigurationManager.AppSettings["DatabaseName"] ?? "MyDatabase";
            txtUser.Text = ConfigurationManager.AppSettings["UserId"] ?? "sa";
            txtPassword.Text = ConfigurationManager.AppSettings["Password"] ?? "";


        }
        
        private string BuildConnectionString()
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtServer.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Server!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServer.Focus();
                return null;
            }

            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Database!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabase.Focus();
                return null;
            }

            if (rbSqlAuth.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text))
                {
                    MessageBox.Show("Vui lòng nhập Username!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUser.Focus();
                    return null;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập Password!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return null;
                }
            }

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServer.Text.Trim(),
                InitialCatalog = txtDatabase.Text.Trim()
            };

            if (rbWindowsAuth.Checked)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = txtUser.Text.Trim();
                builder.Password = txtPassword.Text;
            }
            // Thêm các tham số cần thiết khác
            builder.Encrypt = true;
            builder.TrustServerCertificate = true;

            return builder.ConnectionString;
        }
        private void LoadCurrentConfig()
        {
            string currentConnStr = ConfigurationHelper.GetConnectionString();
            if (!string.IsNullOrEmpty(currentConnStr))
            {
                try
                {
                    var builder = new SqlConnectionStringBuilder(currentConnStr);
                    txtServer.Text = builder.DataSource;
                    txtDatabase.Text = builder.InitialCatalog;
                    if (builder.IntegratedSecurity)
                    {
                        rbWindowsAuth.Checked = true;
                    }
                    else
                    {
                        rbSqlAuth.Checked = true;
                        txtUser.Text = builder.UserID;
                        txtPassword.Text = builder.Password;
                    }
                }
                catch (Exception ex)
                {
                    // Bỏ qua nếu chuỗi kết nối cũ bị lỗi
                    MessageBox.Show($"Lỗi khi đọc cấu hình cũ: {ex.Message}", "Cảnh báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }
        
        
        private bool TestConnectionSilently(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
       
        private void btnRestore_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationHelper.GetConnectionString();
            if (string.IsNullOrEmpty(connStr) || !TestConnectionSilently(connStr))
            {
                MessageBox.Show("Vui lòng cấu hình và lưu kết nối hợp lệ trước khi khôi phục.", "Yêu cầu",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Database!", "Thiếu thông tin",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Backup File (*.bak)|*.bak|All Files (*.*)|*.*";
                ofd.Title = "Chọn file backup để khôi phục";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn khôi phục database '{txtDatabase.Text}' từ file backup?\n\nLưu ý: Dữ liệu hiện tại sẽ bị ghi đè!",
                        "Xác nhận khôi phục", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        try
                        {
                            using (var connection = new SqlConnection(connStr))
                            {
                                connection.Open();

                                // Đặt database về single user mode để có thể restore
                                string setSingleUserQuery = $"ALTER DATABASE [{txtDatabase.Text.Trim()}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                                using (var command = new SqlCommand(setSingleUserQuery, connection))
                                {
                                    command.ExecuteNonQuery();
                                }

                                // Thực hiện restore
                                string restoreQuery = $"RESTORE DATABASE [{txtDatabase.Text.Trim()}] FROM DISK = N'{ofd.FileName}' WITH REPLACE;";
                                using (var command = new SqlCommand(restoreQuery, connection))
                                {
                                    command.CommandTimeout = 300; // 5 phút timeout cho restore
                                    command.ExecuteNonQuery();
                                }

                                // Đặt lại database về multi user mode
                                string setMultiUserQuery = $"ALTER DATABASE [{txtDatabase.Text.Trim()}] SET MULTI_USER;";
                                using (var command = new SqlCommand(setMultiUserQuery, connection))
                                {
                                    command.ExecuteNonQuery();
                                }

                                MessageBox.Show($"Khôi phục cơ sở dữ liệu '{txtDatabase.Text}' thành công!",
                                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khôi phục: {ex.Message}", "Lỗi",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            Cursor = Cursors.Default;
                        }
                    }
                }
            }

        }
       
        private void FrmCaiDat_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu form đang được đóng mà không có DialogResult, set về Cancel
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }
        private void txtServer_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txtDatabase_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void FrmCaiDat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnTest_Click(sender, e);
            }

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string connStr = $"Server={txtServer.Text};Database={txtDatabase.Text};User Id={txtUser.Text};Password={txtPassword.Text};";
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("✅ Kết nối SQL Server thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Kết nối thất bại!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("ServerName");
                config.AppSettings.Settings.Remove("DatabaseName");
                config.AppSettings.Settings.Remove("UserId");
                config.AppSettings.Settings.Remove("Password");

                config.AppSettings.Settings.Add("ServerName", txtServer.Text);
                config.AppSettings.Settings.Add("DatabaseName", txtDatabase.Text);
                config.AppSettings.Settings.Add("UserId", txtUser.Text);
                config.AppSettings.Settings.Add("Password", txtPassword.Text);

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show("💾 Đã lưu cấu hình SQL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu cấu hình:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationHelper.GetConnectionString();
            if (string.IsNullOrEmpty(connStr) || !TestConnectionSilently(connStr))
            {
                MessageBox.Show("Vui lòng cấu hình và lưu kết nối hợp lệ trước khi sao lưu.", "Yêu cầu",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Database!", "Thiếu thông tin",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Backup File (*.bak)|*.bak|All Files (*.*)|*.*";
                sfd.Title = "Chọn vị trí lưu file sao lưu";
                sfd.FileName = $"{txtDatabase.Text.Trim()}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        using (var connection = new SqlConnection(connStr))
                        {
                            connection.Open();
                            // Dùng WITH FORMAT để ghi đè file backup nếu đã tồn tại
                            string backupQuery = $"BACKUP DATABASE [{txtDatabase.Text.Trim()}] TO DISK = N'{sfd.FileName}' WITH FORMAT, INIT;";
                            using (var command = new SqlCommand(backupQuery, connection))
                            {
                                command.CommandTimeout = 300; // 5 phút timeout cho backup
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show($"Sao lưu cơ sở dữ liệu '{txtDatabase.Text}' thành công!\nFile: {sfd.FileName}",
                                          "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi sao lưu: {ex.Message}", "Lỗi",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
