using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
   
    public class SQLLoginDataVanHanh
    {
        //  private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public static void InsertDataVanHanh(DataVanHanhModel data)
        {
            // Lệnh 'using' đảm bảo rằng đối tượng SqlConnection được
            // đóng và giải phóng đúng cách, ngay cả khi có lỗi xảy ra.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Định nghĩa truy vấn SQL INSERT. Sử dụng chuỗi hằng để cải thiện
                // khả năng đọc và ngăn ngừa sửa đổi ngẫu nhiên.
                const string query = @"
                    INSERT INTO DataVanHanh
                    (
                        CreateAt,
                        HT_Cylinder1_1, HT_Cylinder1_2, HT_Cylinder2_1, HT_Cylinder2_2,
                        HT_Cylinder3_1, HT_Cylinder3_2, HT_Cylinder4_1, HT_Cylinder4_2,
                        HT_Cylinder5_1, HT_Cylinder5_2, HT_Cylinder6_1, HT_Cylinder6_2,
                        Door1_Aperture, Door2_Aperture, Door3_Aperture, Door4_Aperture, Door5_Aperture, Door6_Aperture,
                        Temp_Oil1, Temp_Oil2, Temp_Oil3,
                        Fllow_Door1, Fllow_Door2, Fllow_Door3, Fllow_Door4, Fllow_Door5, Fllow_Door6,
                        Total_Fllow, Fllow_Ho,
                        Fllow_DauTieng, Fllow_BenSuc, Fllow_SonDai,
                        Fllow_BinhNham, Fllow_TL_CDD
                    )
                    VALUES
                    (
                        @CreateAt,
                        @HT_Cylinder1_1, @HT_Cylinder1_2, @HT_Cylinder2_1, @HT_Cylinder2_2,
                        @HT_Cylinder3_1, @HT_Cylinder3_2, @HT_Cylinder4_1, @HT_Cylinder4_2,
                        @HT_Cylinder5_1, @HT_Cylinder5_2, @HT_Cylinder6_1, @HT_Cylinder6_2,
                        @Door1_Aperture, @Door2_Aperture, @Door3_Aperture, @Door4_Aperture, @Door5_Aperture, @Door6_Aperture,
                        @Temp_Oil1, @Temp_Oil2, @Temp_Oil3,
                        @Fllow_Door1, @Fllow_Door2, @Fllow_Door3, @Fllow_Door4, @Fllow_Door5, @Fllow_Door6,
                        @Total_Fllow, @Fllow_Ho,
                        @Fllow_DauTieng, @Fllow_BenSuc, @Fllow_SonDai,
                        @Fllow_BinhNham, @Fllow_TL_CDD
                    )";

                try
                {
                    // Mở kết nối cơ sở dữ liệu.
                    conn.Open();

                    // Lệnh 'using' đảm bảo đối tượng SqlCommand được
                    // giải phóng đúng cách sau khi sử dụng.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số vào lệnh. Việc chỉ định rõ ràng SqlDbType
                        // giúp cải thiện hiệu suất và ngăn ngừa các lỗi liên quan đến kiểu dữ liệu.
                        cmd.Parameters.Add("@CreateAt", SqlDbType.DateTime).Value = data.CreateAt;

                        // Các trường kiểu string (giả định từ GetValue() trả về string)
                        cmd.Parameters.Add("@HT_Cylinder1_1", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder1_1;
                        cmd.Parameters.Add("@HT_Cylinder1_2", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder1_2;
                        cmd.Parameters.Add("@HT_Cylinder2_1", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder2_1;
                        cmd.Parameters.Add("@HT_Cylinder2_2", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder2_2;
                        cmd.Parameters.Add("@HT_Cylinder3_1", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder3_1;
                        cmd.Parameters.Add("@HT_Cylinder3_2", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder3_2;
                        cmd.Parameters.Add("@HT_Cylinder4_1", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder4_1;
                        cmd.Parameters.Add("@HT_Cylinder4_2", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder4_2;
                        cmd.Parameters.Add("@HT_Cylinder5_1", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder5_1;
                        cmd.Parameters.Add("@HT_Cylinder5_2", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder5_2;
                        cmd.Parameters.Add("@HT_Cylinder6_1", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder6_1;
                        cmd.Parameters.Add("@HT_Cylinder6_2", SqlDbType.NVarChar, 50).Value = data.HT_Cylinder6_2;

                        cmd.Parameters.Add("@Door1_Aperture", SqlDbType.NVarChar, 50).Value = data.Door1_Aperture;
                        cmd.Parameters.Add("@Door2_Aperture", SqlDbType.NVarChar, 50).Value = data.Door2_Aperture;
                        cmd.Parameters.Add("@Door3_Aperture", SqlDbType.NVarChar, 50).Value = data.Door3_Aperture;
                        cmd.Parameters.Add("@Door4_Aperture", SqlDbType.NVarChar, 50).Value = data.Door4_Aperture;
                        cmd.Parameters.Add("@Door5_Aperture", SqlDbType.NVarChar, 50).Value = data.Door5_Aperture;
                        cmd.Parameters.Add("@Door6_Aperture", SqlDbType.NVarChar, 50).Value = data.Door6_Aperture;

                        cmd.Parameters.Add("@Temp_Oil1", SqlDbType.NVarChar, 50).Value = data.Temp_Oil1;
                        cmd.Parameters.Add("@Temp_Oil2", SqlDbType.NVarChar, 50).Value = data.Temp_Oil2;
                        cmd.Parameters.Add("@Temp_Oil3", SqlDbType.NVarChar, 50).Value = data.Temp_Oil3;

                        cmd.Parameters.Add("@Fllow_Door1", SqlDbType.NVarChar, 50).Value = data.Fllow_Door1;
                        cmd.Parameters.Add("@Fllow_Door2", SqlDbType.NVarChar, 50).Value = data.Fllow_Door2;
                        cmd.Parameters.Add("@Fllow_Door3", SqlDbType.NVarChar, 50).Value = data.Fllow_Door3;
                        cmd.Parameters.Add("@Fllow_Door4", SqlDbType.NVarChar, 50).Value = data.Fllow_Door4;
                        cmd.Parameters.Add("@Fllow_Door5", SqlDbType.NVarChar, 50).Value = data.Fllow_Door5;
                        cmd.Parameters.Add("@Fllow_Door6", SqlDbType.NVarChar, 50).Value = data.Fllow_Door6;

                        cmd.Parameters.Add("@Total_Fllow", SqlDbType.NVarChar, 50).Value = data.Total_Fllow; // Giả định là string
                        cmd.Parameters.Add("@Fllow_Ho", SqlDbType.NVarChar, 50).Value = data.Fllow_Ho;     // Giả định là string

                        // Các trường đã được xử lý (làm tròn và chia 100) trong tm_login_Tick
                        // và được giả định là kiểu decimal trong DataVanHanhModel
                        cmd.Parameters.Add("@Fllow_DauTieng", SqlDbType.Decimal).Value = data.Fllow_DauTieng;
                        cmd.Parameters.Add("@Fllow_BenSuc", SqlDbType.Decimal).Value = data.Fllow_BenSuc;
                        cmd.Parameters.Add("@Fllow_SonDai", SqlDbType.Decimal).Value = data.Fllow_SonDai;

                        cmd.Parameters.Add("@Fllow_BinhNham", SqlDbType.NVarChar, 50).Value = data.Fllow_BinhNham; // Giả định là string
                        cmd.Parameters.Add("@Fllow_TL_CDD", SqlDbType.NVarChar, 50).Value = data.Fllow_TL_CDD;   // Giả định là string

                        // Thực thi lệnh không truy vấn (INSERT, UPDATE, DELETE).
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Bắt SqlException cụ thể cho các lỗi liên quan đến cơ sở dữ liệu.
                    // Ghi lại toàn bộ thông báo ngoại lệ và dấu vết ngăn xếp để gỡ lỗi chi tiết.
                    Console.Error.WriteLine($"Lỗi chèn DataVanHanh vào SQL: {ex.Message}");
                    Console.Error.WriteLine($"StackTrace: {ex.StackTrace}");
                    // Ném lại ngoại lệ để đảm bảo mã gọi biết về lỗi.
                    throw;
                }
                catch (Exception ex)
                {
                    // Bắt bất kỳ ngoại lệ không mong muốn nào khác.
                    Console.Error.WriteLine($"Đã xảy ra lỗi không mong muốn trong quá trình chèn DataVanHanh: {ex.Message}");
                    Console.Error.WriteLine($"StackTrace: {ex.StackTrace}");
                    // Ném lại ngoại lệ.
                    throw;
                }
            }
        }
        }
}
