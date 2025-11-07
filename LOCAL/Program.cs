using Domain;
using Domain.Entities;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //   static async Task Main()
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //using (var dbContext = new ApplicationDbContext())
            //{
            //    //   var configTable = await dbContext.FT01s.FirstOrDefaultAsync();
            //    //var configTable = dbContext.FT01s.ToList();
            //    var configTable = dbContext.FT01s.FirstOrDefault();
            //    if (configTable == null)
            //    {
            //        MessageBox.Show("Cấu hình cơ sở dữ liệu không hợp lệ hoặc chưa được thiết lập. Vui lòng cấu hình.",
            //                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    Globalvariable.ConfigSystem = JsonConvert.DeserializeObject<ConfigModel>(configTable.C000);
            //    Globalvariable.LocationsInfo = JsonConvert.DeserializeObject<LocationsInfo>(configTable.C001);

            Globalvariable.GetConfig();

            

                using (var dbContext = new ApplicationDbContext())
                {
                    var dlRealtime = dbContext.FT02s.FirstOrDefault();
                    if (dlRealtime != null)
                    {
                        dlRealtime.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                        dlRealtime.UpdateAt = DateTime.Now;
                        dlRealtime.UpdateOperatorId = "System";
                    }
                    else
                    {
                        var newItem = new FT02()
                        {
                            Id = Guid.NewGuid(),
                            C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                            CreateAt = DateTime.Now,
                            CreateOperatorId = "System",
                        };

                    }
                    dbContext.SaveChanges();
                }

            Application.Run(new FrmLogin());
            //   Application.Run(new FrmCS1());
        }
        private static bool TestConnectionSilently(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return false;
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }


}
