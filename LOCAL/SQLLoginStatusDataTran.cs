using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace RegistrationForm1
{
    public class SQLLoginStatusDataTran
    {
        private static readonly string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        // Biến static lưu dữ liệu trước đó
        private static DataTranModel _previousData = null;

        public static void InsertStatusDataTran(DataTranModel currentData)
        {
            try
            {
                // Gán thời gian thực nếu cần
                currentData.CreateAt = DateTime.Now;

                // So sánh dữ liệu mới với dữ liệu cũ
                if (_previousData == null || HasChanged(_previousData, currentData))
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        string query = @"INSERT INTO DataTran (
                            CreateAt,
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
                            Doorlock6_1Open, Doorlock6_1Close, Doorlock6_2Open, Doorlock6_2Close
                        ) VALUES (
                            @CreateAt,
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
                            @Doorlock6_1Open, @Doorlock6_1Close, @Doorlock6_2Open, @Doorlock6_2Close
                        )";

                        db.Execute(query, currentData);
                    }

                    Console.WriteLine($"[SQL] Dữ liệu mới đã ghi lúc {currentData.CreateAt:HH:mm:ss}");

                    // Cập nhật dữ liệu cũ (clone)
                    _previousData = Clone(currentData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InsertStatusDataTran Error: {ex.Message}");
            }
        }

        /// <summary>
        /// So sánh hai DataTranModel
        /// </summary>
        private static bool HasChanged(DataTranModel oldData, DataTranModel newData)
        {
            var properties = typeof(DataTranModel).GetProperties();
            foreach (var prop in properties)
            {
                var oldValue = prop.GetValue(oldData)?.ToString();
                var newValue = prop.GetValue(newData)?.ToString();
                if (oldValue != newValue)
                {
                    return true; // Có sự thay đổi
                }
            }
            return false; // Không thay đổi
        }

        /// <summary>
        /// Clone DataTranModel để lưu _previousData tránh reference
        /// </summary>
        //public static DataTranModel Clone(DataTranModel data)
        //{
        //    var clone = new DataTranModel();
        //    foreach (var prop in typeof(DataTranModel).GetProperties())
        //    {
        //        prop.SetValue(clone, prop.GetValue(data));
        //    }
        //    return clone;
        //}
        public static DataTranModel CloneCurrentDataTran()
        {
            var clone = new DataTranModel();
            foreach (var prop in typeof(DataTranModel).GetProperties())
            {
                var value = prop.GetValue(CurrentDataTran);
                prop.SetValue(clone, value);
            }
            clone.CreateAt = DateTime.Now;
            return clone;
        }
    }
}