using System;
using System.Data; // Required for SqlDbType
using System.Data.SqlClient;

namespace RegistrationForm1
{
    public class SQLLoginMucNuoc
    {
        
        private static readonly string connectionString = ConfigurationHelper.GetConnectionString();
        public static void InsertDataMucNuoc(DataMucNuocModel data)
        {         
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                const string query = @"
                    INSERT INTO DataMucNuoc
                    (
                        CreateAt, Fllow_Ho, Fllow_DauTieng,
                        Fllow_BenSuc, Fllow_SonDai, Fllow_BinhNham, Fllow_TL_CDD
                    )
                    VALUES
                    (
                        @CreateAt, @Fllow_Ho, @Fllow_DauTieng,
                        @Fllow_BenSuc, @Fllow_SonDai, @Fllow_BinhNham, @Fllow_TL_CDD
                    )";

                try
                {
                    // Mở kết nối cơ sở dữ liệu.
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@CreateAt", SqlDbType.DateTime).Value = data.CreateAt;
                        cmd.Parameters.Add("@Fllow_Ho", SqlDbType.Decimal).Value = data.Fllow_Ho;

                        // Làm tròn Fllow_DauTieng đến 2 chữ số thập phân và sau đó chia cho 100.
                        // Sử dụng decimal.TryParse để chuyển đổi an toàn từ string sang decimal.
                        decimal fllowDauTiengValue;
                        if (decimal.TryParse(data.Fllow_DauTieng, out fllowDauTiengValue))
                        {
                         //   cmd.Parameters.Add("@Fllow_DauTieng", SqlDbType.Decimal).Value = Math.Round(fllowDauTiengValue, 2) / 100m;
                            cmd.Parameters.Add("@Fllow_DauTieng", SqlDbType.Decimal).Value = Math.Round(fllowDauTiengValue, 2);
                        }
                        else
                        {
                            // Xử lý trường hợp không thể chuyển đổi (ví dụ: log lỗi và gán giá trị mặc định).
                            Console.Error.WriteLine($"Cảnh báo: Không thể chuyển đổi Fllow_DauTieng '{data.Fllow_DauTieng}' sang Decimal. Sử dụng giá trị 0.");
                            cmd.Parameters.Add("@Fllow_DauTieng", SqlDbType.Decimal).Value = 0m; // Gán giá trị mặc định
                        }

                        // Làm tròn Fllow_BenSuc đến 2 chữ số thập phân và sau đó chia cho 100.
                        decimal fllowBenSucValue;
                        if (decimal.TryParse(data.Fllow_BenSuc, out fllowBenSucValue))
                        {
                            cmd.Parameters.Add("@Fllow_BenSuc", SqlDbType.Decimal).Value = Math.Round(fllowBenSucValue, 2);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Cảnh báo: Không thể chuyển đổi Fllow_BenSuc '{data.Fllow_BenSuc}' sang Decimal. Sử dụng giá trị 0.");
                            cmd.Parameters.Add("@Fllow_BenSuc", SqlDbType.Decimal).Value = 0m;
                        }

                        // Làm tròn Fllow_SonDai đến 2 chữ số thập phân và sau đó chia cho 100.
                        decimal fllowSonDaiValue;
                        if (decimal.TryParse(data.Fllow_SonDai, out fllowSonDaiValue))
                        {
                            cmd.Parameters.Add("@Fllow_SonDai", SqlDbType.Decimal).Value = Math.Round(fllowSonDaiValue, 2);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Cảnh báo: Không thể chuyển đổi Fllow_SonDai '{data.Fllow_SonDai}' sang Decimal. Sử dụng giá trị 0.");
                            cmd.Parameters.Add("@Fllow_SonDai", SqlDbType.Decimal).Value = 0m;
                        }

                        cmd.Parameters.Add("@Fllow_BinhNham", SqlDbType.Decimal).Value = data.Fllow_BinhNham;
                        cmd.Parameters.Add("@Fllow_TL_CDD", SqlDbType.Decimal).Value = data.Fllow_TL_CDD;

                        // Thực thi lệnh không truy vấn (INSERT, UPDATE, DELETE).
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Bắt SqlException cụ thể cho các lỗi liên quan đến cơ sở dữ liệu.
                    // Ghi lại toàn bộ thông báo ngoại lệ và dấu vết ngăn xếp để gỡ lỗi chi tiết.
                    Console.Error.WriteLine($"Lỗi chèn SQL: {ex.Message}");
                    Console.Error.WriteLine($"Dấu vết ngăn xếp: {ex.StackTrace}");
                    // Ném lại ngoại lệ để đảm bảo mã gọi biết về lỗi.
                    throw;
                }
                catch (Exception ex)
                {
                    // Bắt bất kỳ ngoại lệ không mong muốn nào khác.
                    Console.Error.WriteLine($"Đã xảy ra lỗi không mong muốn trong quá trình chèn dữ liệu: {ex.Message}");
                    Console.Error.WriteLine($"Dấu vết ngăn xếp: {ex.StackTrace}");
                    // Ném lại ngoại lệ.
                    throw;
                }
            }
        }
    }


    
}
