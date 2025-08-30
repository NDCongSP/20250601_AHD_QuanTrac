using Domain;
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
//using System.Globalization;
//using System.Threading;

namespace RegistrationForm1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var dbContext = new ApplicationDbContext())
            {
                //dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var configTable = await dbContext.FT01s.FirstOrDefaultAsync();

                if (configTable == null)
                {
                    MessageBox.Show("Cấu hình cơ sở dữ liệu không hợp lệ hoặc chưa được thiết lập. Vui lòng cấu hình.",
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Globalvariable.ConfigSystem = JsonConvert.DeserializeObject<ConfigModel>(configTable.C000);
                Globalvariable.LocationsInfo = JsonConvert.DeserializeObject<LocationsModel>(configTable.C001);

                foreach (var location in Globalvariable.LocationsInfo)
                {
                    foreach (var station in location.Stations)
                    {
                        var diplayItem = new RealtimeDisplayModel()
                        {
                            Path = station.Path,

                        };

                        Globalvariable.RealtimeDisplays.Add(diplayItem);
                    }
                }
            }

            Application.Run(new FrmMain());
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
