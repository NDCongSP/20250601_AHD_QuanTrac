using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{

    public class SQLLoginMucNuoc
    {
        //   private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public static void InsertDataMucNuoc(DataMucNuocModel data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO DataMucNuoc
                        (
                            CreateAt,
                            Fllow_Ho, Fllow_DauTieng,
                            Fllow_BenSuc, Fllow_SonDai                                                                              
                        )
                        VALUES
                        (
                            @CreateAt,                        
                            @Fllow_Ho, @Fllow_DauTieng,
                            @Fllow_BenSuc, @Fllow_SonDai
                           
                        )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CreateAt", data.CreateAt);
                      
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
                Console.WriteLine($"InsertMuc Nuoc Error: {ex.Message}");
            }
        }
    }
}
