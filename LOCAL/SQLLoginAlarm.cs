using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                         @S3_DC1_Over, @S3_DC2_Over, @S3_DC3_Over
                         )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", alarm.CreateAt);                    
                        cmd.Parameters.AddWithValue("@DeviceCode", string .IsNullOrEmpty(alarm.DeviceCode) ? "0" : alarm.DeviceCode);

                       
                        cmd.Parameters.AddWithValue("@Area", string .IsNullOrEmpty(alarm.Area ) ? "0" : alarm.Area);
                        cmd.Parameters.AddWithValue("@Severity", string .IsNullOrEmpty(alarm.Severity) ? "0" : alarm.Severity);
                        cmd.Parameters.AddWithValue("@Door1_PressureHigh", string.IsNullOrEmpty(alarm.Door1_PressureHigh) ? "0" : alarm.Door1_PressureHigh);
                        cmd.Parameters.AddWithValue("@Door1_PressureLow", string.IsNullOrEmpty(alarm.Door1_PressureLow) ? "0" : alarm.Door1_PressureLow);
                        cmd.Parameters.AddWithValue("@Door2_PressureHigh", string.IsNullOrEmpty(alarm.Door2_PressureHigh) ? "0" : alarm.Door2_PressureHigh);
                        cmd.Parameters.AddWithValue("@Door2_PressureLow", string.IsNullOrEmpty(alarm.Door2_PressureLow) ? "0" : alarm.Door2_PressureLow);
                        cmd.Parameters.AddWithValue("@Door3_PressureHigh", string.IsNullOrEmpty(alarm.Door3_PressureHigh) ? "0" : alarm.Door3_PressureHigh);
                        cmd.Parameters.AddWithValue("@Door3_PressureLow", string.IsNullOrEmpty(alarm.Door3_PressureLow) ? "0" : alarm.Door3_PressureLow);
                        cmd.Parameters.AddWithValue("@Door4_PressureHigh", string.IsNullOrEmpty(alarm.Door4_PressureHigh) ? "0" : alarm.Door4_PressureHigh);
                        cmd.Parameters.AddWithValue("@Door4_PressureLow", string.IsNullOrEmpty(alarm.Door4_PressureLow) ? "0" : alarm.Door4_PressureLow);
                        cmd.Parameters.AddWithValue("@Door5_PressureHigh", string.IsNullOrEmpty(alarm.Door5_PressureHigh) ? "0" : alarm.Door5_PressureHigh);
                        cmd.Parameters.AddWithValue("@Door5_PressureLow", string.IsNullOrEmpty(alarm.Door5_PressureLow) ? "0" : alarm.Door5_PressureLow);
                        cmd.Parameters.AddWithValue("@Door6_PressureHigh", string.IsNullOrEmpty(alarm.Door6_PressureHigh) ? "0" : alarm.Door6_PressureHigh);
                        cmd.Parameters.AddWithValue("@Door6_PressureLow", string.IsNullOrEmpty(alarm.Door6_PressureLow) ? "0" : alarm.Door6_PressureLow);
                        cmd.Parameters.AddWithValue("@S1_DC1_Over", string.IsNullOrEmpty(alarm.S1_DC1_Over) ? "0" : alarm.S1_DC1_Over);
                        cmd.Parameters.AddWithValue("@S1_DC2_Over", string.IsNullOrEmpty(alarm.S1_DC2_Over) ? "0" : alarm.S1_DC2_Over);
                        cmd.Parameters.AddWithValue("@S1_DC3_Over", string.IsNullOrEmpty(alarm.S1_DC3_Over) ? "0" : alarm.S1_DC3_Over);
                        cmd.Parameters.AddWithValue("@S2_DC1_Over", string.IsNullOrEmpty(alarm.S2_DC1_Over) ? "0" : alarm.S2_DC1_Over);
                        cmd.Parameters.AddWithValue("@S2_DC2_Over", string.IsNullOrEmpty(alarm.S2_DC2_Over) ? "0" : alarm.S2_DC2_Over);
                        cmd.Parameters.AddWithValue("@S2_DC3_Over", string.IsNullOrEmpty(alarm.S2_DC3_Over) ? "0" : alarm.S2_DC3_Over);
                        cmd.Parameters.AddWithValue("@S3_DC1_Over", string.IsNullOrEmpty(alarm.S3_DC1_Over) ? "0" : alarm.S3_DC1_Over);
                        cmd.Parameters.AddWithValue("@S3_DC2_Over", string.IsNullOrEmpty(alarm.S3_DC2_Over) ? "0" : alarm.S3_DC2_Over);
                        cmd.Parameters.AddWithValue("@S3_DC3_Over", string.IsNullOrEmpty(alarm.S3_DC3_Over) ? "0" : alarm.S3_DC3_Over);
                    



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
