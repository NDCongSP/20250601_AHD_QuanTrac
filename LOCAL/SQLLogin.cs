using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public class SQLLogin
    {
        private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public static void InsertAllTagsToSQL(DataTranModel data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                    INSERT INTO DataTran (
                        CreateAt, S1_Remote, S1_Local, S1_Auto, S1_Man, S1_Local_Stop, S1_Stop_Remote,
                        S2_Remote, S2_Local, S2_Auto, S2_Man, S2_Local_Stop, S2_Stop_Remote,
                        S3_Remote, S3_Local, S3_Auto, S3_Man, S3_Local_Stop, S3_Stop_Remote,
                        S1_DC1_Running, S1_DC2_Running, S1_DC3_Running,
                        S2_DC1_Running, S2_DC2_Running, S2_DC3_Running,
                        S3_DC1_Running, S3_DC2_Running, S3_DC3_Running,
                        Door1_Opening, Door1_Closing,Door2_Opening,Door2_Closing,
                        Door3_Opening, Door3_Closing, Door4_Opening, Door4_Closing,
                        Door5_Opening, Door5_Closing, Door6_Opening, Door6_Closing,
                        Doorlock1_Opening, Doorlock1_Closing, Doorlock2_Opening, Doorlock2_Closing,
                        Doorlock3_Opening, Doorlock3_Closing,Doorlock4_Opening, Doorlock4_Closing,
                        Doorlock5_Opening, Doorlock5_Closing, Doorlock6_Opening, Doorlock6_Closing,

                        Door1_Open,Door1_Close,Door2_Open, Door2_Close,Door3_Open, Door3_Close,
                        Door4_Open, Door4_Close, Door5_Open, Door5_Close, Door6_Open, Door6_Close,

                        Doorlock1_1Open, Doorlock1_1Close, Doorlock1_2Open, Doorlock1_2Close,
                        Doorlock2_1Open,Doorlock2_1Close, Doorlock2_2Open, Doorlock2_2Close,
                        Doorlock3_1Open, Doorlock3_1Close, Doorlock3_2Open, Doorlock3_2Close,
                        Doorlock4_1Open, Doorlock4_1Close, Doorlock4_2Open, Doorlock4_2Close,
                        Doorlock5_1Open, Doorlock5_1Close, Doorlock5_2Open, Doorlock5_2Close,
                        Doorlock6_1Open, Doorlock6_1Close, Doorlock6_2Open, Doorlock6_2Close

                        -- TODO: thêm tất cả các cột còn lại ở đây
                    )
                    VALUES (
                        @CreateAt, @S1_Remote, @S1_Local, @S1_Auto, @S1_Man, @S1_Local_Stop, @S1_Stop_Remote,
                        @S2_Remote, @S2_Local, @S2_Auto, @S2_Man, @S2_Local_Stop, @S2_Stop_Remote,
                        @S3_Remote, @S3_Local, @S3_Auto, @S3_Man, @S3_Local_Stop, @S3_Stop_Remote,
                        @S1_DC1_Running, @S1_DC2_Running, @S1_DC3_Running,
                        @S2_DC1_Running, @S2_DC2_Running, @S2_DC3_Running,
                        @S3_DC1_Running, @S3_DC2_Running, @S3_DC3_Running,
                        @Door1_Opening, @Door1_Closing,@Door2_Opening,@Door2_Closing,
                        @Door3_Opening, @Door3_Closing, @Door4_Opening, @Door4_Closing,
                        @Door5_Opening, @Door5_Closing, @Door6_Opening, @Door6_Closing,
                        @Doorlock1_Opening, @Doorlock1_Closing, @Doorlock2_Opening, @Doorlock2_Closing,
                        @Doorlock3_Opening, @Doorlock3_Closing,@Doorlock4_Opening, @Doorlock4_Closing,
                        @Doorlock5_Opening, @Doorlock5_Closing, @Doorlock6_Opening, @Doorlock6_Closing,
                        @Door1_Open,@Door1_Close,@Door2_Open, @Door2_Close,@Door3_Open, @Door3_Close,
                        @Door4_Open, @Door4_Close, @Door5_Open, @Door5_Close, @Door6_Open, @Door6_Close,
                        @Doorlock1_1Open, @Doorlock1_1Close, @Doorlock1_2Open, @Doorlock1_2Close,
                        @Doorlock2_1Open,@Doorlock2_1Close, @Doorlock2_2Open, @Doorlock2_2Close,
                        @Doorlock3_1Open, @Doorlock3_1Close, @Doorlock3_2Open, @Doorlock3_2Close,
                        @Doorlock4_1Open, @Doorlock4_1Close, @Doorlock4_2Open, @Doorlock4_2Close,
                        @Doorlock5_1Open, @Doorlock5_1Close, @Doorlock5_2Open, @Doorlock5_2Close,
                        @Doorlock6_1Open, @Doorlock6_1Close, @Doorlock6_2Open, @Doorlock6_2Close
                        -- TODO: thêm tất cả các param còn lại ở đây
                    )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", data.CreateAt);
                        cmd.Parameters.AddWithValue("@S1_Remote", data.S1_Remote ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_Local", data.S1_Local ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_Auto", data.S1_Auto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_Man", data.S1_Man ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_Local_Stop", data.S1_Local_Stop ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_Stop_Remote", data.S1_Stop_Remote ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@S2_Remote", data.S2_Remote ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_Local", data.S2_Local ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_Auto", data.S2_Auto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_Man", data.S2_Man ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_Local_Stop", data.S2_Local_Stop ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_Stop_Remote", data.S2_Stop_Remote ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@S3_Remote", data.S3_Remote ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_Local", data.S3_Local ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_Auto", data.S3_Auto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_Man", data.S3_Man ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_Local_Stop", data.S3_Local_Stop ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_Stop_Remote", data.S3_Stop_Remote ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@S1_DC1_Running", data.S1_DC1_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_DC2_Running", data.S1_DC2_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S1_DC3_Running", data.S1_DC3_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_DC1_Running", data.S2_DC1_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_DC2_Running", data.S2_DC2_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S2_DC3_Running", data.S2_DC3_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_DC1_Running", data.S3_DC1_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_DC2_Running", data.S3_DC2_Running ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@S3_DC3_Running", data.S3_DC3_Running ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@Door1_Opening", data.Door1_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door1_Closing", data.Door1_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door2_Opening", data.Door2_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door2_Closing", data.Door2_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door3_Opening", data.Door3_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door3_Closing", data.Door3_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door4_Opening", data.Door4_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door4_Closing", data.Door4_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door5_Opening", data.Door5_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door5_Closing", data.Door5_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door6_Opening", data.Door6_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door6_Closing", data.Door6_Closing ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@Doorlock1_Opening", data.Doorlock1_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock1_Closing", data.Doorlock1_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock2_Opening", data.Doorlock2_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock2_Closing", data.Doorlock2_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock3_Opening", data.Doorlock3_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock3_Closing", data.Doorlock3_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock4_Opening", data.Doorlock4_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock4_Closing", data.Doorlock4_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock5_Opening", data.Doorlock5_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock5_Closing", data.Doorlock5_Closing ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock6_Opening", data.Doorlock6_Opening ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock6_Closing", data.Doorlock6_Closing ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@Door1_Open", data.Door1_Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door1_Close", data.Door1_Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door2_Open", data.Door2_Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door2_Close", data.Door2_Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door3_Open", data.Door3_Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door3_Close", data.Door3_Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door4_Open", data.Door4_Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door4_Close", data.Door4_Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door5_Open", data.Door5_Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door5_Close", data.Door5_Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door6_Open", data.Door6_Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Door6_Close", data.Door6_Close ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@Doorlock1_1Open", data.Doorlock1_1Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock1_1Close", data.Doorlock1_1Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock1_2Open", data.Doorlock1_2Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock1_2Close", data.Doorlock1_2Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock2_1Open", data.Doorlock2_1Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock2_1Close", data.Doorlock2_1Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock2_2Open", data.Doorlock2_2Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock2_2Close", data.Doorlock2_2Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock3_1Open", data.Doorlock3_1Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock3_1Close", data.Doorlock3_1Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock3_2Open", data.Doorlock3_2Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock3_2Close", data.Doorlock3_2Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock4_1Open", data.Doorlock4_1Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock4_1Close", data.Doorlock4_1Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock4_2Open", data.Doorlock4_2Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock4_2Close", data.Doorlock4_2Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock5_1Open", data.Doorlock5_1Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock5_1Close", data.Doorlock5_1Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock5_2Open", data.Doorlock5_2Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock5_2Close", data.Doorlock5_2Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock6_1Open", data.Doorlock6_1Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock6_1Close", data.Doorlock6_1Close ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock6_2Open", data.Doorlock6_2Open ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Doorlock6_2Close", data.Doorlock6_2Close ?? (object)DBNull.Value);





                        // TODO: tiếp tục AddWithValue cho toàn bộ các property còn lại
                        //Door1_Opening, Door1_Closing,Door2_Opening,Door2_Closing,
                        //Door3_Opening, Door3_Closing, Door4_Opening, Door4_Closing,

                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            Console.WriteLine("⚠️ Không có dòng nào được insert.");
                        }
                        else
                        {
                            Console.WriteLine($"✅ Đã insert {rows} dòng vào DataTran.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi ghi SQL: {ex.Message}", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
