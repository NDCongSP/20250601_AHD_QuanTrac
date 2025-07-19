using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace RegistrationForm1
{
    public class SQLLogin
    {
        //  private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public static string connectionString => ConfigurationHelper.GetConnectionString();
        public static DataVanHanhModel CurrentDataVanHanh { get; set; }
        /// Khởi tạo CurrentDataTran từ DB khi app khởi động     
        public static void InitCurrentDataTran()
        {
            CurrentDataVanHanh = GetLatestDataVanhHanhModel();
            if (CurrentDataVanHanh == null)
                CurrentDataVanHanh = new DataVanHanhModel { CreateAt = DateTime.Now };

            Console.WriteLine("✅ InitCurrentDataVanhHanh thành công.");
        }
        /// Lấy bản ghi mới nhất từ DataTran     
        public static DataVanHanhModel GetLatestDataVanhHanhModel()
        {
            DataVanHanhModel model = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 * FROM DataVanHanh ORDER BY CreateAt DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new DataVanHanhModel();
                            PropertyInfo[] props = typeof(DataVanHanhModel).GetProperties();
                            foreach (var prop in props)
                            {
                                if (!reader.HasColumn(prop.Name)) continue;

                                object value = reader[prop.Name];
                                if (value == DBNull.Value) continue;

                                if (prop.PropertyType == typeof(DateTime))
                                    prop.SetValue(model, Convert.ToDateTime(value));
                                else
                                    prop.SetValue(model, value.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi GetLatestDataTranModel: {ex.Message}");
            }
            return model;
        }

        /// Insert CurrentDataVanHanh xuống SQL bằng Stored Procedure       
        //public static void InsertAllTagsToSQL(DataVanHanhModel data)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd = new SqlCommand("sp_InsertDataTran", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                PropertyInfo[] properties = typeof(DataVanHanhModel).GetProperties();
        //                foreach (PropertyInfo prop in properties)
        //                {
        //                    if (prop.Name == "Id") continue;

        //                    string paramName = "@" + prop.Name;
        //                    object value = prop.GetValue(data, null);

        //                    if (prop.PropertyType == typeof(DateTime))
        //                    {
        //                        DateTime dt = (DateTime)value;
        //                        if (dt == DateTime.MinValue)
        //                            value = DBNull.Value;
        //                    }
        //                    else
        //                    {
        //                        if (value == null)
        //                            value = "0"; // default 0 nếu null
        //                    }

        //                    cmd.Parameters.AddWithValue(paramName, value);
        //                }

        //                int rows = cmd.ExecuteNonQuery();
        //                Console.WriteLine($"✅ Insert SP thành công. Rows affected: {rows}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"❌ Lỗi Insert SP: {ex.Message}");
        //    }
        //}

        public static void UpdateTagValue(string tagName, string newValue)
        {
            try
            {
                PropertyInfo prop = typeof(DataVanHanhModel).GetProperty(tagName);
                if (prop == null)
                {
                    Console.WriteLine($"⚠️ Property {tagName} không tồn tại trong DataTranModel");
                    return;
                }

                prop.SetValue(CurrentDataVanHanh, newValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi UpdateTagValue: {ex.Message}");
            }
        }
                  
        //public static void GenericTag_ValueChanged(string tagName, string newValue)
        //{
        //    try
        //    {
        //        PropertyInfo prop = typeof(DataVanHanhModel).GetProperty(tagName);
        //        if (prop == null)
        //        {
        //            Console.WriteLine($"⚠️ Property {tagName} không tồn tại trong DataTranModel");
        //            return;
        //        }

        //        prop.SetValue(CurrentDataVanHanh, newValue);
        //        CurrentDataVanHanh.CreateAt = DateTime.Now;

        //        InsertAllTagsToSQL(CurrentDataVanHanh);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"❌ Lỗi GenericTag_ValueChanged: {ex.Message}");
        //    }
        //}
    }

    /// <summary>
    /// Extension check cột tồn tại
    /// </summary>
    public static class SqlDataReaderExtension
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }
    }

}