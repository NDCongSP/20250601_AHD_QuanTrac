using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class SQLLoginDataTran
    {
        private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public static void InsertDataTran(DataTranModel data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO DataTran
                        (
                            CreateAt,Position,
                            S1_Remote, S1_Local, S1_Auto, S1_Man, S1_Local_Stop, S1_Stop_Remote,
                            S2_Remote, S2_Local, S2_Auto, S2_Man, S2_Local_Stop, S2_Stop_Remote,
                            S3_Remote, S3_Local, S3_Auto, S3_Man, S3_Local_Stop, S3_Stop_Remote,
                            S1_DC1_Running, S1_DC2_Running, S1_DC3_Running,
                            S2_DC1_Running, S2_DC2_Running, S2_DC3_Running,
                            S3_DC1_Running, S3_DC2_Running, S3_DC3_Running,
                            Door1_Opening, Door1_Closing, Door2_Opening, Door2_Closing,
                            Door3_Opening, Door3_Closing, Door4_Opening, Door4_Closing,
                            Door5_Opening, Door5_Closing, Door6_Opening, Door6_Closing,
                            Doorlock1_Opening, Doorlock1_Closing, Doorlock2_Opening, Doorlock2_Closing,
                            Doorlock3_Opening, Doorlock3_Closing, Doorlock4_Opening, Doorlock4_Closing,
                            Doorlock5_Opening, Doorlock5_Closing, Doorlock6_Opening, Doorlock6_Closing,
                            Door1_Open, Door1_Close, Door2_Open, Door2_Close, Door3_Open, Door3_Close,
                            Door4_Open, Door4_Close, Door5_Open, Door5_Close, Door6_Open, Door6_Close,
                            Doorlock1_1Open, Doorlock1_1Close, Doorlock1_2Open, Doorlock1_2Close,
                            Doorlock2_1Open, Doorlock2_1Close, Doorlock2_2Open, Doorlock2_2Close,
                            Doorlock3_1Open, Doorlock3_1Close, Doorlock3_2Open, Doorlock3_2Close,
                            Doorlock4_1Open, Doorlock4_1Close, Doorlock4_2Open, Doorlock4_2Close,
                            Doorlock5_1Open, Doorlock5_1Close, Doorlock5_2Open, Doorlock5_2Close,
                            Doorlock6_1Open, Doorlock6_1Close, Doorlock6_2Open, Doorlock6_2Close,
                            S1_Station_Run, S2_Station_Run, S3_Station_Run,
                            S1_Station_Stop, S2_Station_Stop, S3_Station_Stop,
                           
                        TagName)
                        VALUES
                        (
                            @CreateAt,@Position,
                            @S1_Remote, @S1_Local, @S1_Auto, @S1_Man, @S1_Local_Stop, @S1_Stop_Remote,
                            @S2_Remote, @S2_Local, @S2_Auto, @S2_Man, @S2_Local_Stop, @S2_Stop_Remote,
                            @S3_Remote, @S3_Local, @S3_Auto, @S3_Man, @S3_Local_Stop, @S3_Stop_Remote,
                            @S1_DC1_Running, @S1_DC2_Running, @S1_DC3_Running,
                            @S2_DC1_Running, @S2_DC2_Running, @S2_DC3_Running,
                            @S3_DC1_Running, @S3_DC2_Running, @S3_DC3_Running,
                            @Door1_Opening, @Door1_Closing, @Door2_Opening, @Door2_Closing,
                            @Door3_Opening, @Door3_Closing, @Door4_Opening, @Door4_Closing,
                            @Door5_Opening, @Door5_Closing, @Door6_Opening, @Door6_Closing,
                            @Doorlock1_Opening, @Doorlock1_Closing, @Doorlock2_Opening, @Doorlock2_Closing,
                            @Doorlock3_Opening, @Doorlock3_Closing, @Doorlock4_Opening, @Doorlock4_Closing,
                            @Doorlock5_Opening, @Doorlock5_Closing, @Doorlock6_Opening, @Doorlock6_Closing,
                            @Door1_Open, @Door1_Close, @Door2_Open, @Door2_Close, @Door3_Open, @Door3_Close,
                            @Door4_Open, @Door4_Close, @Door5_Open, @Door5_Close, @Door6_Open, @Door6_Close,
                            @Doorlock1_1Open, @Doorlock1_1Close, @Doorlock1_2Open, @Doorlock1_2Close,
                            @Doorlock2_1Open, @Doorlock2_1Close, @Doorlock2_2Open, @Doorlock2_2Close,
                            @Doorlock3_1Open, @Doorlock3_1Close, @Doorlock3_2Open, @Doorlock3_2Close,
                            @Doorlock4_1Open, @Doorlock4_1Close, @Doorlock4_2Open, @Doorlock4_2Close,
                            @Doorlock5_1Open, @Doorlock5_1Close, @Doorlock5_2Open, @Doorlock5_2Close,
                            @Doorlock6_1Open, @Doorlock6_1Close, @Doorlock6_2Open, @Doorlock6_2Close,
                            @S1_Station_Run, @S2_Station_Run, @S3_Station_Run,
                            @S1_Station_Stop, @S2_Station_Stop, @S3_Station_Stop,
                           
                        @TagName)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", data.CreateAt);
                        cmd.Parameters.AddWithValue("@Position", string.IsNullOrEmpty(data.Position) ? "0" : data.Position);
                        cmd.Parameters.AddWithValue("@TagName", string.IsNullOrEmpty(data.TagName) ? "" : data.TagName);
                        cmd.Parameters.AddWithValue("@S1_Remote", string.IsNullOrEmpty(data.S1_Remote) ? "0" : data.S1_Remote);
                        cmd.Parameters.AddWithValue("@S1_Local", string.IsNullOrEmpty(data.S1_Local) ? "0" : data.S1_Local);
                        cmd.Parameters.AddWithValue("@S1_Auto", string.IsNullOrEmpty(data.S1_Auto) ? "0" : data.S1_Auto);
                        cmd.Parameters.AddWithValue("@S1_Man", string.IsNullOrEmpty(data.S1_Man) ? "0" : data.S1_Man);
                        cmd.Parameters.AddWithValue("@S1_Local_Stop", string.IsNullOrEmpty(data.S1_Local_Stop) ? "0" : data.S1_Local_Stop);
                        cmd.Parameters.AddWithValue("@S1_Stop_Remote", string.IsNullOrEmpty(data.S1_Stop_Remote) ? "0" : data.S1_Stop_Remote);
                        cmd.Parameters.AddWithValue("@S2_Remote", string.IsNullOrEmpty(data.S2_Remote) ? "0" : data.S2_Remote);
                        cmd.Parameters.AddWithValue("@S2_Local", string.IsNullOrEmpty(data.S2_Local) ? "0" : data.S2_Local);
                        cmd.Parameters.AddWithValue("@S2_Auto", string.IsNullOrEmpty(data.S2_Auto) ? "0" : data.S2_Auto);
                        cmd.Parameters.AddWithValue("@S2_Man", string.IsNullOrEmpty(data.S2_Man) ? "0" : data.S2_Man);
                        cmd.Parameters.AddWithValue("@S2_Local_Stop", string.IsNullOrEmpty(data.S2_Local_Stop) ? "0" : data.S2_Local_Stop);
                        cmd.Parameters.AddWithValue("@S2_Stop_Remote", string.IsNullOrEmpty(data.S2_Stop_Remote) ? "0" : data.S2_Stop_Remote);
                        cmd.Parameters.AddWithValue("@S3_Remote", string.IsNullOrEmpty(data.S3_Remote) ? "0" : data.S3_Remote);
                        cmd.Parameters.AddWithValue("@S3_Local", string.IsNullOrEmpty(data.S3_Local) ? "0" : data.S3_Local);
                        cmd.Parameters.AddWithValue("@S3_Auto", string.IsNullOrEmpty(data.S3_Auto) ? "0" : data.S3_Auto);
                        cmd.Parameters.AddWithValue("@S3_Man", string.IsNullOrEmpty(data.S3_Man) ? "0" : data.S3_Man);
                        cmd.Parameters.AddWithValue("@S3_Local_Stop", string.IsNullOrEmpty(data.S3_Local_Stop) ? "0" : data.S3_Local_Stop);
                        cmd.Parameters.AddWithValue("@S3_Stop_Remote", string.IsNullOrEmpty(data.S3_Stop_Remote) ? "0" : data.S3_Stop_Remote);
                        cmd.Parameters.AddWithValue("@S1_DC1_Running", string.IsNullOrEmpty(data.S1_DC1_Running) ? "0" : data.S1_DC1_Running);
                        cmd.Parameters.AddWithValue("@S1_DC2_Running", string.IsNullOrEmpty(data.S1_DC2_Running) ? "0" : data.S1_DC2_Running);
                        cmd.Parameters.AddWithValue("@S1_DC3_Running", string.IsNullOrEmpty(data.S1_DC3_Running) ? "0" : data.S1_DC3_Running);
                        cmd.Parameters.AddWithValue("@S2_DC1_Running", string.IsNullOrEmpty(data.S2_DC1_Running) ? "0" : data.S2_DC1_Running);
                        cmd.Parameters.AddWithValue("@S2_DC2_Running", string.IsNullOrEmpty(data.S2_DC2_Running) ? "0" : data.S2_DC2_Running);
                        cmd.Parameters.AddWithValue("@S2_DC3_Running", string.IsNullOrEmpty(data.S2_DC3_Running) ? "0" : data.S2_DC3_Running);
                        cmd.Parameters.AddWithValue("@S3_DC1_Running", string.IsNullOrEmpty(data.S3_DC1_Running) ? "0" : data.S3_DC1_Running);
                        cmd.Parameters.AddWithValue("@S3_DC2_Running", string.IsNullOrEmpty(data.S3_DC2_Running) ? "0" : data.S3_DC2_Running);
                        cmd.Parameters.AddWithValue("@S3_DC3_Running", string.IsNullOrEmpty(data.S3_DC3_Running) ? "0" : data.S3_DC3_Running);
                        cmd.Parameters.AddWithValue("@Door1_Opening", string.IsNullOrEmpty(data.Door1_Opening) ? "0" : data.Door1_Opening);
                        cmd.Parameters.AddWithValue("@Door1_Closing", string.IsNullOrEmpty(data.Door1_Closing) ? "0" : data.Door1_Closing);
                        cmd.Parameters.AddWithValue("@Door2_Opening", string.IsNullOrEmpty(data.Door2_Opening) ? "0" : data.Door2_Opening);
                        cmd.Parameters.AddWithValue("@Door2_Closing", string.IsNullOrEmpty(data.Door2_Closing) ? "0" : data.Door2_Closing);
                        cmd.Parameters.AddWithValue("@Door3_Opening", string.IsNullOrEmpty(data.Door3_Opening) ? "0" : data.Door3_Opening);
                        cmd.Parameters.AddWithValue("@Door3_Closing", string.IsNullOrEmpty(data.Door3_Closing) ? "0" : data.Door3_Closing);
                        cmd.Parameters.AddWithValue("@Door4_Opening", string.IsNullOrEmpty(data.Door4_Opening) ? "0" : data.Door4_Opening);
                        cmd.Parameters.AddWithValue("@Door4_Closing", string.IsNullOrEmpty(data.Door4_Closing) ? "0" : data.Door4_Closing);
                        cmd.Parameters.AddWithValue("@Door5_Opening", string.IsNullOrEmpty(data.Door5_Opening) ? "0" : data.Door5_Opening);
                        cmd.Parameters.AddWithValue("@Door5_Closing", string.IsNullOrEmpty(data.Door5_Closing) ? "0" : data.Door5_Closing);
                        cmd.Parameters.AddWithValue("@Door6_Opening", string.IsNullOrEmpty(data.Door6_Opening) ? "0" : data.Door6_Opening);
                        cmd.Parameters.AddWithValue("@Door6_Closing", string.IsNullOrEmpty(data.Door6_Closing) ? "0" : data.Door6_Closing);
                        cmd.Parameters.AddWithValue("@Doorlock1_Opening", string.IsNullOrEmpty(data.Doorlock1_Opening) ? "0" : data.Doorlock1_Opening);
                        cmd.Parameters.AddWithValue("@Doorlock1_Closing", string.IsNullOrEmpty(data.Doorlock1_Closing) ? "0" : data.Doorlock1_Closing);
                        cmd.Parameters.AddWithValue("@Doorlock2_Opening", string.IsNullOrEmpty(data.Doorlock2_Opening) ? "0" : data.Doorlock2_Opening);
                        cmd.Parameters.AddWithValue("@Doorlock2_Closing", string.IsNullOrEmpty(data.Doorlock2_Closing) ? "0" : data.Doorlock2_Closing);
                        cmd.Parameters.AddWithValue("@Doorlock3_Opening", string.IsNullOrEmpty(data.Doorlock3_Opening) ? "0" : data.Doorlock3_Opening);
                        cmd.Parameters.AddWithValue("@Doorlock3_Closing", string.IsNullOrEmpty(data.Doorlock3_Closing) ? "0" : data.Doorlock3_Closing);
                        cmd.Parameters.AddWithValue("@Doorlock4_Opening", string.IsNullOrEmpty(data.Doorlock4_Opening) ? "0" : data.Doorlock4_Opening);
                        cmd.Parameters.AddWithValue("@Doorlock4_Closing", string.IsNullOrEmpty(data.Doorlock4_Closing) ? "0" : data.Doorlock4_Closing);
                        cmd.Parameters.AddWithValue("@Doorlock5_Opening", string.IsNullOrEmpty(data.Doorlock5_Opening) ? "0" : data.Doorlock5_Opening);
                        cmd.Parameters.AddWithValue("@Doorlock5_Closing", string.IsNullOrEmpty(data.Doorlock5_Closing) ? "0" : data.Doorlock5_Closing);
                        cmd.Parameters.AddWithValue("@Doorlock6_Opening", string.IsNullOrEmpty(data.Doorlock6_Opening) ? "0" : data.Doorlock6_Opening);
                        cmd.Parameters.AddWithValue("@Doorlock6_Closing", string.IsNullOrEmpty(data.Doorlock6_Closing) ? "0" : data.Doorlock6_Closing);
                        cmd.Parameters.AddWithValue("@Door1_Open", string.IsNullOrEmpty(data.Door1_Open) ? "0" : data.Door1_Open);
                        cmd.Parameters.AddWithValue("@Door1_Close", string.IsNullOrEmpty(data.Door1_Close) ? "0" : data.Door1_Close);
                        cmd.Parameters.AddWithValue("@Door2_Open", string.IsNullOrEmpty(data.Door2_Open) ? "0" : data.Door2_Open);
                        cmd.Parameters.AddWithValue("@Door2_Close", string.IsNullOrEmpty(data.Door2_Close) ? "0" : data.Door2_Close);
                        cmd.Parameters.AddWithValue("@Door3_Open", string.IsNullOrEmpty(data.Door3_Open) ? "0" : data.Door3_Open);
                        cmd.Parameters.AddWithValue("@Door3_Close", string.IsNullOrEmpty(data.Door3_Close) ? "0" : data.Door3_Close);
                        cmd.Parameters.AddWithValue("@Door4_Open", string.IsNullOrEmpty(data.Door4_Open) ? "0" : data.Door4_Open);
                        cmd.Parameters.AddWithValue("@Door4_Close", string.IsNullOrEmpty(data.Door4_Close) ? "0" : data.Door4_Close);
                        cmd.Parameters.AddWithValue("@Door5_Open", string.IsNullOrEmpty(data.Door5_Open) ? "0" : data.Door5_Open);
                        cmd.Parameters.AddWithValue("@Door5_Close", string.IsNullOrEmpty(data.Door5_Close) ? "0" : data.Door5_Close);
                        cmd.Parameters.AddWithValue("@Door6_Open", string.IsNullOrEmpty(data.Door6_Open) ? "0" : data.Door6_Open);
                        cmd.Parameters.AddWithValue("@Door6_Close", string.IsNullOrEmpty(data.Door6_Close) ? "0" : data.Door6_Close);
                        cmd.Parameters.AddWithValue("@Doorlock1_1Open", string.IsNullOrEmpty(data.Doorlock1_1Open) ? "0" : data.Doorlock1_1Open);
                        cmd.Parameters.AddWithValue("@Doorlock1_1Close", string.IsNullOrEmpty(data.Doorlock1_1Close) ? "0" : data.Doorlock1_1Close);
                        cmd.Parameters.AddWithValue("@Doorlock1_2Open", string.IsNullOrEmpty(data.Doorlock1_2Open) ? "0" : data.Doorlock1_2Open);
                        cmd.Parameters.AddWithValue("@Doorlock1_2Close", string.IsNullOrEmpty(data.Doorlock1_2Close) ? "0" : data.Doorlock1_2Close);
                        cmd.Parameters.AddWithValue("@Doorlock2_1Open", string.IsNullOrEmpty(data.Doorlock2_1Open) ? "0" : data.Doorlock2_1Open);
                        cmd.Parameters.AddWithValue("@Doorlock2_1Close", string.IsNullOrEmpty(data.Doorlock2_1Close) ? "0" : data.Doorlock2_1Close);
                        cmd.Parameters.AddWithValue("@Doorlock2_2Open", string.IsNullOrEmpty(data.Doorlock2_2Open) ? "0" : data.Doorlock2_2Open);
                        cmd.Parameters.AddWithValue("@Doorlock2_2Close", string.IsNullOrEmpty(data.Doorlock2_2Close) ? "0" : data.Doorlock2_2Close);
                        cmd.Parameters.AddWithValue("@Doorlock3_1Open", string.IsNullOrEmpty(data.Doorlock3_1Open) ? "0" : data.Doorlock3_1Open);
                        cmd.Parameters.AddWithValue("@Doorlock3_1Close", string.IsNullOrEmpty(data.Doorlock3_1Close) ? "0" : data.Doorlock3_1Close);
                        cmd.Parameters.AddWithValue("@Doorlock3_2Open", string.IsNullOrEmpty(data.Doorlock3_2Open) ? "0" : data.Doorlock3_2Open);
                        cmd.Parameters.AddWithValue("@Doorlock3_2Close", string.IsNullOrEmpty(data.Doorlock3_2Close) ? "0" : data.Doorlock3_2Close);
                        cmd.Parameters.AddWithValue("@Doorlock4_1Open", string.IsNullOrEmpty(data.Doorlock4_1Open) ? "0" : data.Doorlock4_1Open);
                        cmd.Parameters.AddWithValue("@Doorlock4_1Close", string.IsNullOrEmpty(data.Doorlock4_1Close) ? "0" : data.Doorlock4_1Close);
                        cmd.Parameters.AddWithValue("@Doorlock4_2Open", string.IsNullOrEmpty(data.Doorlock4_2Open) ? "0" : data.Doorlock4_2Open);
                        cmd.Parameters.AddWithValue("@Doorlock4_2Close", string.IsNullOrEmpty(data.Doorlock4_2Close) ? "0" : data.Doorlock4_2Close);
                        cmd.Parameters.AddWithValue("@Doorlock5_1Open", string.IsNullOrEmpty(data.Doorlock5_1Open) ? "0" : data.Doorlock5_1Open);
                        cmd.Parameters.AddWithValue("@Doorlock5_1Close", string.IsNullOrEmpty(data.Doorlock5_1Close) ? "0" : data.Doorlock5_1Close);
                        cmd.Parameters.AddWithValue("@Doorlock5_2Open", string.IsNullOrEmpty(data.Doorlock5_2Open) ? "0" : data.Doorlock5_2Open);
                        cmd.Parameters.AddWithValue("@Doorlock5_2Close", string.IsNullOrEmpty(data.Doorlock5_2Close) ? "0" : data.Doorlock5_2Close);
                        cmd.Parameters.AddWithValue("@Doorlock6_1Open", string.IsNullOrEmpty(data.Doorlock6_1Open) ? "0" : data.Doorlock6_1Open);
                        cmd.Parameters.AddWithValue("@Doorlock6_1Close", string.IsNullOrEmpty(data.Doorlock6_1Close) ? "0" : data.Doorlock6_1Close);
                        cmd.Parameters.AddWithValue("@Doorlock6_2Open", string.IsNullOrEmpty(data.Doorlock6_2Open) ? "0" : data.Doorlock6_2Open);
                        cmd.Parameters.AddWithValue("@Doorlock6_2Close", string.IsNullOrEmpty(data.Doorlock6_2Close) ? "0" : data.Doorlock6_2Close);
                        cmd.Parameters.AddWithValue("@S1_Station_Run", string.IsNullOrEmpty(data.S1_Station_Run) ? "0" : data.S1_Station_Run);
                        cmd.Parameters.AddWithValue("@S2_Station_Run", string.IsNullOrEmpty(data.S2_Station_Run) ? "0" : data.S2_Station_Run);
                        cmd.Parameters.AddWithValue("@S3_Station_Run", string.IsNullOrEmpty(data.S3_Station_Run) ? "0" : data.S3_Station_Run);
                        cmd.Parameters.AddWithValue("@S1_Station_Stop", string.IsNullOrEmpty(data.S1_Station_Stop) ? "0" : data.S1_Station_Stop);
                        cmd.Parameters.AddWithValue("@S2_Station_Stop", string.IsNullOrEmpty(data.S2_Station_Stop) ? "0" : data.S2_Station_Stop);
                        cmd.Parameters.AddWithValue("@S3_Station_Stop", string.IsNullOrEmpty(data.S3_Station_Stop) ? "0" : data.S3_Station_Stop);




                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InsertDataTran Error: {ex.Message}");
            }
        }
         
       
    }
}
