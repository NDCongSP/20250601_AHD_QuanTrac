using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RegistrationForm1
{
       public class SQLLoginAlarm
    {
        private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public static void InsertAlarm(DataAlarmModel alarm)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO DataAlarm 
                        (CreateAt, DeviceCode, Area, Severity,
                         Door1_PressureHigh, Door1_PressureLow,
                         Door2_PressureHigh, Door2_PressureLow,
                         Door3_PressureHigh, Door3_PressureLow,
                         Door4_PressureHigh, Door4_PressureLow,
                         Door5_PressureHigh, Door5_PressureLow,
                         Door6_PressureHigh, Door6_PressureLow,
                         S1_DC1_Over, S1_DC2_Over, S1_DC3_Over,
                         S2_DC1_Over, S2_DC2_Over, S2_DC3_Over,
                         S3_DC1_Over, S3_DC2_Over, S3_DC3_Over,
                         TagName)
                        VALUES
                        (@CreateAt, @DeviceCode, @Area, @Severity,
                         @Door1_PressureHigh, @Door1_PressureLow,
                         @Door2_PressureHigh, @Door2_PressureLow,
                         @Door3_PressureHigh, @Door3_PressureLow,
                         @Door4_PressureHigh, @Door4_PressureLow,
                         @Door5_PressureHigh, @Door5_PressureLow,
                         @Door6_PressureHigh, @Door6_PressureLow,
                         @S1_DC1_Over, @S1_DC2_Over, @S1_DC3_Over,
                         @S2_DC1_Over, @S2_DC2_Over, @S2_DC3_Over,
                         @S3_DC1_Over, @S3_DC2_Over, @S3_DC3_Over,
                         @TagName)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", alarm.CreateAt);
                        cmd.Parameters.AddWithValue("@DeviceCode", alarm.DeviceCode ?? "");
                        cmd.Parameters.AddWithValue("@Area", alarm.Area ?? "");
                        cmd.Parameters.AddWithValue("@Severity", alarm.Severity ?? "");
                        cmd.Parameters.AddWithValue("@Door1_PressureHigh", alarm.Door1_PressureHigh ?? "");
                        cmd.Parameters.AddWithValue("@Door1_PressureLow", alarm.Door1_PressureLow ?? "");
                        cmd.Parameters.AddWithValue("@Door2_PressureHigh", alarm.Door2_PressureHigh ?? "");
                        cmd.Parameters.AddWithValue("@Door2_PressureLow", alarm.Door2_PressureLow ?? "");
                        cmd.Parameters.AddWithValue("@Door3_PressureHigh", alarm.Door3_PressureHigh ?? "");
                        cmd.Parameters.AddWithValue("@Door3_PressureLow", alarm.Door3_PressureLow ?? "");
                        cmd.Parameters.AddWithValue("@Door4_PressureHigh", alarm.Door4_PressureHigh ?? "");
                        cmd.Parameters.AddWithValue("@Door4_PressureLow", alarm.Door4_PressureLow ?? "");
                        cmd.Parameters.AddWithValue("@Door5_PressureHigh", alarm.Door5_PressureHigh ?? "");
                        cmd.Parameters.AddWithValue("@Door5_PressureLow", alarm.Door5_PressureLow ?? "");
                        cmd.Parameters.AddWithValue("@Door6_PressureHigh", alarm.Door6_PressureHigh ?? "");
                        cmd.Parameters.AddWithValue("@Door6_PressureLow", alarm.Door6_PressureLow ?? "");
                        cmd.Parameters.AddWithValue("@S1_DC1_Over", alarm.S1_DC1_Over ?? "");
                        cmd.Parameters.AddWithValue("@S1_DC2_Over", alarm.S1_DC2_Over ?? "");
                        cmd.Parameters.AddWithValue("@S1_DC3_Over", alarm.S1_DC3_Over ?? "");
                        cmd.Parameters.AddWithValue("@S2_DC1_Over", alarm.S2_DC1_Over ?? "");
                        cmd.Parameters.AddWithValue("@S2_DC2_Over", alarm.S2_DC2_Over ?? "");
                        cmd.Parameters.AddWithValue("@S2_DC3_Over", alarm.S2_DC3_Over ?? "");
                        cmd.Parameters.AddWithValue("@S3_DC1_Over", alarm.S3_DC1_Over ?? "");
                        cmd.Parameters.AddWithValue("@S3_DC2_Over", alarm.S3_DC2_Over ?? "");
                        cmd.Parameters.AddWithValue("@S3_DC3_Over", alarm.S3_DC3_Over ?? "");
                        cmd.Parameters.AddWithValue("@TagName", alarm.TagName ?? "");

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
