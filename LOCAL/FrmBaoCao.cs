using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using Dapper;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace RegistrationForm1
{
    
    
    public partial class FrmBaoCao : Form
    {
       
      //  private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";
        public FrmBaoCao()
        {
            InitializeComponent();
            Load += FrmBaoCao_Load;
        }

        private void FrmBaoCao_Load(object sender, EventArgs e)
        { // Thiết lập chế độ báo cáo mặc định
            cbTimeRange.Items.Add("5 Phút");
            cbTimeRange.Items.Add("10 Phút");
            cbTimeRange.Items.Add("30 Phút");
            cbTimeRange.Items.Add("60 Phút");
            cbTimeRange.Items.Add("Tất Cả");
            cbTimeRange.SelectedIndex = 0; // Chọn mặc định
            // throw new NotImplementedException();
            dtpFrom.Value = DateTime.Today.AddDays(-7); // mặc định 7 ngày trước
            dtpTo.Value = DateTime.Today;
            btnSearch.PerformClick(); // tự động tìm kiếm
        }

        private static string GetSqlType(Type type)
        {
            if (type == typeof(int) || type == typeof(int?))
                return "INT";
            if (type == typeof(string))
                return "NVARCHAR(MAX)";
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return "DATETIME";
            if (type == typeof(bool) || type == typeof(bool?))
                return "BIT";
            // thêm nếu cần kiểu khác
            return "NVARCHAR(MAX)";
        }

        private static bool IsNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null || !type.IsValueType;
        }
        // CREATE TABLE từ class:
        public string GenerateCreateTableSQL<T>(string tableName)
        {
            var props = typeof(T).GetProperties();
            var sql = new StringBuilder();
            sql.AppendLine($"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{tableName}' AND xtype='U')");
            sql.AppendLine($"CREATE TABLE {tableName} (");

            foreach (var prop in props)
            {
                string colName = prop.Name;
                string sqlType = "NVARCHAR(MAX)";

                if (prop.PropertyType == typeof(int))
                    sqlType = "INT";
                else if (prop.PropertyType == typeof(DateTime))
                    sqlType = "DATETIME";

                if (colName == "Id")
                    sql.AppendLine($"    {colName} INT IDENTITY(1,1) PRIMARY KEY,");
                else
                    sql.AppendLine($"    {colName} {sqlType},");
            }

            sql.Length--; // xóa dấu phẩy cuối
            sql.AppendLine(")");

            return sql.ToString();
        }
        // Tạo bảng 1 lần 
        private void EnsureTableCreated()
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                string sql = GenerateCreateTableSQL<DataMucNuocModel>("MucNuoc");
                db.Execute(sql);
            }
        }
        private void SavemnToSql(DataMucNuocModel data)
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                var props = typeof(DataMucNuocModel).GetProperties()
                    .Where(p => p.Name != "Id").ToList();

                string columns = string.Join(",", props.Select(p => p.Name));
                string values = string.Join(",", props.Select(p => "@" + p.Name));

                string sql = $"INSERT INTO MucNuoc ({columns}) VALUES ({values})";

                db.Execute(sql, data);
                MessageBox.Show("Lưu thành công!");
            }
        }
        private void SavevhToSql(Vanhanh datavh)
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                var props = typeof(Vanhanh).GetProperties()
                    .Where(p => p.Name != "Id").ToList();

                string columns = string.Join(",", props.Select(p => p.Name));
                string values = string.Join(",", props.Select(p => "@" + p.Name));

                string sql = $"INSERT INTO VanHanh ({columns}) VALUES ({values})";

                db.Execute(sql, datavh);
                MessageBox.Show("Lưu thành công!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var data = new DataMucNuocModel
            {
                CreateAt = DateTime.Now,
                Fllow_Ho = "100",
                Fllow_DauTieng = "150",
                Fllow_BenSuc = "160",
                Fllow_SonDai = "90",
                
                // ... các trường còn lại
            };
            SavemnToSql(data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var datavh = new Vanhanh
            {
                CreateAt = DateTime.Now,
                Fllow_Ho = "100",
                Door1_Aperture = "150",
                Door2_Aperture = "160",
                Door3_Aperture = "90",
                Door4_Aperture = "150",
                Door5_Aperture = "160",
                Door6_Aperture = "90",
                Total_Fllow = "200",

                // ... các trường còn lại
            };
            SavevhToSql(datavh);
        }
        private void FormatDataGridViewColumns()
        {
            foreach (var prop in typeof(Vanhanh).GetProperties())
            {
                var displayNameAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                          .FirstOrDefault() as DisplayNameAttribute;

                if (displayNameAttr != null && dataGridView1.Columns[prop.Name] != null)
                {
                    dataGridView1.Columns[prop.Name].HeaderText = displayNameAttr.DisplayName;
                }
            }
        }
        private void bntDataVanHanh_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            int minutes = 0;
            string selected = cbTimeRange.SelectedItem.ToString();

            if (selected.Contains("5")) minutes = 5;
            else if (selected.Contains("10")) minutes = 10;
            else if (selected.Contains("30")) minutes = 30;
            else if (selected.Contains("60")) minutes = 60;

            DateTime fromTime = DateTime.Now.AddMinutes(-minutes);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT * FROM VanHanh ";

                if (!selected.Contains("Tất Cả"))
                {
                    query += "WHERE CreateAt >= @FromTime ";
                }

                query += "ORDER BY CreateAt DESC";

                var data = conn.Query<Vanhanh>(query, new { FromTime = fromTime }).ToList();
                dataGridView1.DataSource = data;
            }
        }
        

        private void bntDataMucNuoc_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;TrustServerCertificate=True";

            int minutes = 0;
            string selected = cbTimeRange.SelectedItem.ToString();

            if (selected.Contains("5")) minutes = 5;
            else if (selected.Contains("10")) minutes = 10;
            else if (selected.Contains("30")) minutes = 30;
            else if (selected.Contains("60")) minutes = 60;

            DateTime fromTime = DateTime.Now.AddMinutes(-minutes);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT * FROM MucNuoc ";

                if (!selected.Contains("Tất Cả"))
                {
                    query += "WHERE CreateAt >= @FromTime ";
                }

                query += "ORDER BY CreateAt DESC";

                var data = conn.Query<DataMucNuocModel>(query, new { FromTime = fromTime }).ToList();
                dataGridView1.DataSource = data;
            }
        }
        
        private void ExportToExcel(DataGridView dgv)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var ws = workbook.Worksheets.Add("Báo cáo");

                        // 1. Thêm LOGO
                        string logoPath = @"D:\Logo\logo.png";
                        if (File.Exists(logoPath))
                        {
                            var img = ws.AddPicture(logoPath)
                                        .MoveTo(ws.Cell("A1"))
                                        .WithPlacement(XLPicturePlacement.FreeFloating)
                                        .Scale(0.3); // Tùy chỉnh tỉ lệ
                        }

                        // 2. Tên công ty & địa chỉ
                        ws.Cell("C1").Value = "CÔNG TY TNHH KỸ THUẬT ABC";
                        ws.Cell("C2").Value = "Địa chỉ: 123 Đường Nguyễn Văn A, Quận 1, TP.HCM";
                        ws.Range("C1:C2").Style.Font.SetBold().Font.FontSize = 12;

                        // 3. HEADER bắt đầu từ dòng 5
                        int rowStart = 5;
                        for (int col = 0; col < dgv.Columns.Count; col++)
                        {
                            var header = dgv.Columns[col].HeaderText;
                            var cell = ws.Cell(rowStart, col + 1);
                            cell.Value = header;
                            cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                            cell.Style.Font.Bold = true;
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }

                        // 4. Ghi dữ liệu
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                var value = dgv.Rows[i].Cells[j].Value?.ToString();
                                var cell = ws.Cell(rowStart + 1 + i, j + 1);
                                cell.Value = value;

                                // Căn giữa nếu tên cột liên quan đến tag điều khiển
                                var header = dgv.Columns[j].HeaderText;
                                if (header.Contains("Cửa") || header.Contains("Chốt") || header.Contains("Trạng thái") || header.Contains("Door") || header.Contains("Lock"))
                                {
                                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                            }
                        }

                        // 5. Auto-fit & căn giữa toàn cột nếu cần
                        ws.Columns().AdjustToContents();

                        // 6. Lưu file
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void bntExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Báo Cáo");

                        // Header
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dataGridView1.Columns[i].HeaderText;
                        }

                        // Dữ liệu
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1 );
        }
    }
}
