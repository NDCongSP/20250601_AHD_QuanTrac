using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmEditdata : Form
    {
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        private int selectedUserId = -1;
        public FrmEditdata()
        {
            InitializeComponent();
            LoadData();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DataMucNuoc";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Gán lại ID (bạn đang thiếu bước này!)
                selectedUserId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Hiển thị dữ liệu lên các TextBox
                txtHo.Text = selectedRow.Cells["Fllow_Ho"].Value?.ToString();
                txtDautieng.Text = selectedRow.Cells["Fllow_DauTieng"].Value?.ToString();
                txtBensuc.Text = selectedRow.Cells["Fllow_BenSuc"].Value?.ToString();
                txtSondai.Text = selectedRow.Cells["Fllow_SonDai"].Value?.ToString();
                txtBinhnham.Text = selectedRow.Cells["Fllow_BinhNham"].Value?.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE DataMucNuoc SET Fllow_Ho = @Fllow_Ho, Fllow_DauTieng = @Fllow_DauTieng, Fllow_BenSuc = @Fllow_BenSuc, Fllow_SonDai = @Fllow_SonDai,Fllow_BinhNham = @Fllow_BinhNham WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@Fllow_Ho", txtHo.Text.Trim());
                cmd.Parameters.AddWithValue("@Fllow_DauTieng", txtDautieng.Text.Trim());
                cmd.Parameters.AddWithValue("@Fllow_BenSuc", txtBensuc.Text.Trim());
                cmd.Parameters.AddWithValue("@Fllow_SonDai", txtSondai.Text.Trim());
                cmd.Parameters.AddWithValue("@Fllow_BinhNham", txtBinhnham.Text.Trim());
                cmd.Parameters.AddWithValue("@Id", selectedUserId);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật thành công.");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }
            }
        }
    }
}
