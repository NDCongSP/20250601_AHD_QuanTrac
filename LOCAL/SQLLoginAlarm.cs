using System;
using System.Data.SqlClient;

namespace RegistrationForm1
{
    public class SQLLoginAlarm
    {
        //    private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public static void InsertAlarm(DataAlarmModel alarm)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO DataAlarm 
                        (
                            CreateAt, Position, 
                            Door1_PressureHigh, Door1_PressureLow,
                            Door2_PressureHigh, Door2_PressureLow,
                            Door3_PressureHigh, Door3_PressureLow,
                            Door4_PressureHigh, Door4_PressureLow,
                            Door5_PressureHigh, Door5_PressureLow,
                            Door6_PressureHigh, Door6_PressureLow,
                            S1_DC1_Over, S1_DC2_Over, S1_DC3_Over,
                            S2_DC1_Over, S2_DC2_Over, S2_DC3_Over,
                            S3_DC1_Over, S3_DC2_Over, S3_DC3_Over,
                            S1_Station_Alarm, S2_Station_Alarm, S3_Station_Alarm,
                        TagName)
                        VALUES
                        (
                            @CreateAt, @Position, 
                            @Door1_PressureHigh, @Door1_PressureLow,
                            @Door2_PressureHigh, @Door2_PressureLow,
                            @Door3_PressureHigh, @Door3_PressureLow,
                            @Door4_PressureHigh, @Door4_PressureLow,
                            @Door5_PressureHigh, @Door5_PressureLow,
                            @Door6_PressureHigh, @Door6_PressureLow,
                            @S1_DC1_Over, @S1_DC2_Over, @S1_DC3_Over,
                            @S2_DC1_Over, @S2_DC2_Over, @S2_DC3_Over,
                            @S3_DC1_Over, @S3_DC2_Over, @S3_DC3_Over,
                            @S1_Station_Alarm, @S2_Station_Alarm, @S3_Station_Alarm,
                        @TagName)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", alarm.CreateAt);
                        cmd.Parameters.AddWithValue("@Position", string.IsNullOrEmpty(alarm.Position) ? "0" : alarm.Position);
                        cmd.Parameters.AddWithValue("@TagName", string.IsNullOrEmpty(alarm.TagName) ? "" : alarm.TagName);
                        cmd.Parameters.AddWithValue("@Door1_PressureHigh", alarm.Door1_PressureHigh ?? "0");
                        cmd.Parameters.AddWithValue("@Door1_PressureLow", alarm.Door1_PressureLow ?? "0");
                        cmd.Parameters.AddWithValue("@Door2_PressureHigh", alarm.Door2_PressureHigh ?? "0");
                        cmd.Parameters.AddWithValue("@Door2_PressureLow", alarm.Door2_PressureLow ?? "0");
                        cmd.Parameters.AddWithValue("@Door3_PressureHigh", alarm.Door3_PressureHigh ?? "0");
                        cmd.Parameters.AddWithValue("@Door3_PressureLow", alarm.Door3_PressureLow ?? "0");
                        cmd.Parameters.AddWithValue("@Door4_PressureHigh", alarm.Door4_PressureHigh ?? "0");
                        cmd.Parameters.AddWithValue("@Door4_PressureLow", alarm.Door4_PressureLow ?? "0");
                        cmd.Parameters.AddWithValue("@Door5_PressureHigh", alarm.Door5_PressureHigh ?? "0");
                        cmd.Parameters.AddWithValue("@Door5_PressureLow", alarm.Door5_PressureLow ?? "0");
                        cmd.Parameters.AddWithValue("@Door6_PressureHigh", alarm.Door6_PressureHigh ?? "0");
                        cmd.Parameters.AddWithValue("@Door6_PressureLow", alarm.Door6_PressureLow ?? "0");

                        cmd.Parameters.AddWithValue("@S1_DC1_Over", alarm.S1_DC1_Over ?? "0");
                        cmd.Parameters.AddWithValue("@S1_DC2_Over", alarm.S1_DC2_Over ?? "0");
                        cmd.Parameters.AddWithValue("@S1_DC3_Over", alarm.S1_DC3_Over ?? "0");

                        cmd.Parameters.AddWithValue("@S2_DC1_Over", alarm.S2_DC1_Over ?? "0");
                        cmd.Parameters.AddWithValue("@S2_DC2_Over", alarm.S2_DC2_Over ?? "0");
                        cmd.Parameters.AddWithValue("@S2_DC3_Over", alarm.S2_DC3_Over ?? "0");

                        cmd.Parameters.AddWithValue("@S3_DC1_Over", alarm.S3_DC1_Over ?? "0");
                        cmd.Parameters.AddWithValue("@S3_DC2_Over", alarm.S3_DC2_Over ?? "0");
                        cmd.Parameters.AddWithValue("@S3_DC3_Over", alarm.S3_DC3_Over ?? "0");

                        cmd.Parameters.AddWithValue("@S1_Station_Alarm", alarm.S1_Station_Alarm ?? "0");
                        cmd.Parameters.AddWithValue("@S2_Station_Alarm", alarm.S2_Station_Alarm ?? "0");
                        cmd.Parameters.AddWithValue("@S3_Station_Alarm", alarm.S3_Station_Alarm ?? "0");
                        

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InsertAlarm Error: {ex.Message} - {ex.StackTrace}");
            }
        }
    }
}