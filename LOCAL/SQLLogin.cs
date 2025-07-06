using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace RegistrationForm1
{
    public class SQLLogin
    {
        private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public static DataTranModel CurrentDataTran { get; set; }

        /// <summary>
        /// Khởi tạo CurrentDataTran từ DB khi app khởi động
        /// </summary>
        public static void InitCurrentDataTran()
        {
            CurrentDataTran = GetLatestDataTranModel();
            if (CurrentDataTran == null)
                CurrentDataTran = new DataTranModel { CreateAt = DateTime.Now };

            Console.WriteLine("✅ InitCurrentDataTran thành công.");
        }

        /// <summary>
        /// Lấy bản ghi mới nhất từ DataTran
        /// </summary>
        /// <returns></returns>
        public static DataTranModel GetLatestDataTranModel()
        {
            DataTranModel model = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 * FROM DataTran ORDER BY CreateAt DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new DataTranModel();
                            PropertyInfo[] props = typeof(DataTranModel).GetProperties();

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

        /// <summary>
        /// Insert CurrentDataTran xuống SQL bằng Stored Procedure
        /// </summary>
        /// <param name="data"></param>
        public static void InsertAllTagsToSQL(DataTranModel data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertDataTran", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        PropertyInfo[] properties = typeof(DataTranModel).GetProperties();
                        foreach (PropertyInfo prop in properties)
                        {
                            if (prop.Name == "Id") continue;

                            string paramName = "@" + prop.Name;
                            object value = prop.GetValue(data, null);

                            if (prop.PropertyType == typeof(DateTime))
                            {
                                DateTime dt = (DateTime)value;
                                if (dt == DateTime.MinValue)
                                    value = DBNull.Value;
                            }
                            else
                            {
                                if (value == null)
                                    value = "0"; // default 0 nếu null
                            }

                            cmd.Parameters.AddWithValue(paramName, value);
                        }

                        int rows = cmd.ExecuteNonQuery();
                        Console.WriteLine($"✅ Insert SP thành công. Rows affected: {rows}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi Insert SP: {ex.Message}");
            }
        }

        public static void UpdateTagValue(string tagName, string newValue)
        {
            try
            {
                PropertyInfo prop = typeof(DataTranModel).GetProperty(tagName);
                if (prop == null)
                {
                    Console.WriteLine($"⚠️ Property {tagName} không tồn tại trong DataTranModel");
                    return;
                }

                prop.SetValue(CurrentDataTran, newValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi UpdateTagValue: {ex.Message}");
            }
        }
        /// <summary>
        /// Hàm update value và insert
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="newValue"></param>
        public static void GenericTag_ValueChanged(string tagName, string newValue)
        {
            try
            {
                PropertyInfo prop = typeof(DataTranModel).GetProperty(tagName);
                if (prop == null)
                {
                    Console.WriteLine($"⚠️ Property {tagName} không tồn tại trong DataTranModel");
                    return;
                }

                prop.SetValue(CurrentDataTran, newValue);
                CurrentDataTran.CreateAt = DateTime.Now;

                InsertAllTagsToSQL(CurrentDataTran);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi GenericTag_ValueChanged: {ex.Message}");
            }
        }
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