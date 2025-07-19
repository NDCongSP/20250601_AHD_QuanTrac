using System;
using System.Collections.Generic;
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
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO DataVanHanh
                        (
                            CreateAt,
                            HT_Cylinder1_1, HT_Cylinder1_2,
                            HT_Cylinder2_1, HT_Cylinder2_2,
                            HT_Cylinder3_1, HT_Cylinder3_2,
                            HT_Cylinder4_1, HT_Cylinder4_2,
                            HT_Cylinder5_1, HT_Cylinder5_2,
                            HT_Cylinder6_1, HT_Cylinder6_2,
                            Door1_Aperture, Door2_Aperture, Door3_Aperture,
                            Door4_Aperture, Door5_Aperture, Door6_Aperture,
                            Temp_Oil1, Temp_Oil2, Temp_Oil3,
                            Fllow_Door1, Fllow_Door2, Fllow_Door3,
                            Fllow_Door4, Fllow_Door5, Fllow_Door6,
                            Total_Fllow, Fllow_Ho,
                            Fllow_DauTieng, Fllow_BenSuc, Fllow_SonDai
                           
                        )
                        VALUES
                        (
                            @CreateAt,
                            @HT_Cylinder1_1, @HT_Cylinder1_2,
                            @HT_Cylinder2_1, @HT_Cylinder2_2,
                            @HT_Cylinder3_1, @HT_Cylinder3_2,
                            @HT_Cylinder4_1, @HT_Cylinder4_2,
                            @HT_Cylinder5_1, @HT_Cylinder5_2,
                            @HT_Cylinder6_1, @HT_Cylinder6_2,
                            @Door1_Aperture, @Door2_Aperture, @Door3_Aperture,
                            @Door4_Aperture, @Door5_Aperture, @Door6_Aperture,
                            @Temp_Oil1, @Temp_Oil2, @Temp_Oil3,
                            @Fllow_Door1, @Fllow_Door2, @Fllow_Door3,
                            @Fllow_Door4, @Fllow_Door5, @Fllow_Door6,
                            @Total_Fllow, @Fllow_Ho,
                            @Fllow_DauTieng, @Fllow_BenSuc, @Fllow_SonDai
                           
                        )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", data.CreateAt);

                        cmd.Parameters.AddWithValue("@HT_Cylinder1_1", data.HT_Cylinder1_1);
                        cmd.Parameters.AddWithValue("@HT_Cylinder1_2", data.HT_Cylinder1_2);
                        cmd.Parameters.AddWithValue("@HT_Cylinder2_1", data.HT_Cylinder2_1);
                        cmd.Parameters.AddWithValue("@HT_Cylinder2_2", data.HT_Cylinder2_2);
                        cmd.Parameters.AddWithValue("@HT_Cylinder3_1", data.HT_Cylinder3_1);
                        cmd.Parameters.AddWithValue("@HT_Cylinder3_2", data.HT_Cylinder3_2);
                        cmd.Parameters.AddWithValue("@HT_Cylinder4_1", data.HT_Cylinder4_1);
                        cmd.Parameters.AddWithValue("@HT_Cylinder4_2", data.HT_Cylinder4_2);
                        cmd.Parameters.AddWithValue("@HT_Cylinder5_1", data.HT_Cylinder5_1);
                        cmd.Parameters.AddWithValue("@HT_Cylinder5_2", data.HT_Cylinder5_2);
                        cmd.Parameters.AddWithValue("@HT_Cylinder6_1", data.HT_Cylinder6_1);
                        cmd.Parameters.AddWithValue("@HT_Cylinder6_2", data.HT_Cylinder6_2);
                        cmd.Parameters.AddWithValue("@Door1_Aperture", data.Door1_Aperture);
                        cmd.Parameters.AddWithValue("@Door2_Aperture", data.Door2_Aperture);
                        cmd.Parameters.AddWithValue("@Door3_Aperture", data.Door3_Aperture);
                        cmd.Parameters.AddWithValue("@Door4_Aperture", data.Door4_Aperture);
                        cmd.Parameters.AddWithValue("@Door5_Aperture", data.Door5_Aperture);
                        cmd.Parameters.AddWithValue("@Door6_Aperture", data.Door6_Aperture);
                        cmd.Parameters.AddWithValue("@Temp_Oil1", data.Temp_Oil1);
                        cmd.Parameters.AddWithValue("@Temp_Oil2", data.Temp_Oil2);
                        cmd.Parameters.AddWithValue("@Temp_Oil3", data.Temp_Oil3);
                        cmd.Parameters.AddWithValue("@Fllow_Door1", data.Fllow_Door1);
                        cmd.Parameters.AddWithValue("@Fllow_Door2", data.Fllow_Door2);
                        cmd.Parameters.AddWithValue("@Fllow_Door3", data.Fllow_Door3);
                        cmd.Parameters.AddWithValue("@Fllow_Door4", data.Fllow_Door4);
                        cmd.Parameters.AddWithValue("@Fllow_Door5", data.Fllow_Door5);
                        cmd.Parameters.AddWithValue("@Fllow_Door6", data.Fllow_Door6);
                        cmd.Parameters.AddWithValue("@Total_Fllow", data.Total_Fllow);
                        cmd.Parameters.AddWithValue("@Fllow_Ho", data.Fllow_Ho);
                        cmd.Parameters.AddWithValue("@Fllow_DauTieng", data.Fllow_DauTieng);
                        cmd.Parameters.AddWithValue("@Fllow_BenSuc", data.Fllow_BenSuc);
                        cmd.Parameters.AddWithValue("@Fllow_SonDai", data.Fllow_SonDai);





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
