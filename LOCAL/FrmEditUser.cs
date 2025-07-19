using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class FrmEditUser : Form
    {
        private readonly int _userId;
        private string _username;
        //   private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        private string connectionString => ConfigurationHelper.GetConnectionString();
        public FrmEditUser(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void FrmEditUser_Load(object sender, EventArgs e)
        {
            cboRole.Items.AddRange(new string[] { "Admin", "User", "Viewer" });

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FullName, Username, DateOfBirth, Position, Role FROM Users WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _userId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtFullName.Text = reader["FullName"].ToString();
                    _username = reader["Username"].ToString();
                    txtUsername.Text = _username;

                    // ✅ kiểm tra NULL trước khi convert
                    if (reader["DateOfBirth"] != DBNull.Value)
                        dtpDOB.Value = Convert.ToDateTime(reader["DateOfBirth"]);
                    else
                        dtpDOB.Value = DateTime.Now; // hoặc một ngày mặc định

                    txtPosition.Text = reader["Position"].ToString();
                    cboRole.SelectedItem = reader["Role"].ToString();
                }
            }
        }
    }
}
