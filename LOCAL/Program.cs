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
                Globalvariable.LocationsInfo = JsonConvert.DeserializeObject<LocationsInfo>(configTable.C001);

                foreach (var location in Globalvariable.LocationsInfo)
                {
                    var diplayItem = new RealtimeDisplayModel()
                    {
                        LocationId = location.Id,
                        LocationName = location.Name,
                        CalculatorValue = new CalculatorValueModel(),

                    };

                    foreach (var station in location.Stations)
                    {
                        TagsStation tagsStation = new TagsStation();
                        tagsStation.HT_Cylinder1_1_Offset = station.OffsetConfig.HT_Cylinder1_1;
                        tagsStation.HT_Cylinder1_2_Offset = station.OffsetConfig.HT_Cylinder1_2;
                        tagsStation.HT_Cylinder2_1_Offset = station.OffsetConfig.HT_Cylinder2_1;
                        tagsStation.HT_Cylinder2_2_Offset = station.OffsetConfig.HT_Cylinder2_2;
                        tagsStation.Door1_Aperture_Offset = station.OffsetConfig.Door1_Aperture;
                        tagsStation.Door2_Aperture_Offset = station.OffsetConfig.Door2_Aperture;
                        tagsStation.S1_Temp_Oil_Offset = station.OffsetConfig.S1_Temp_Oil;
                        tagsStation.Pressure_Oil_Door1_Offset = station.OffsetConfig.Pressure_Oil_Door1;
                        tagsStation.Pressure_Oil_Door2_Offset = station.OffsetConfig.Pressure_Oil_Door2;
                        tagsStation.Fllow_Door1_Offset = station.OffsetConfig.Fllow_Door1;
                        tagsStation.Fllow_Door2_Offset = station.OffsetConfig.Fllow_Door2;
                        tagsStation.Fllow_Ho_Offset = station.OffsetConfig.Fllow_Ho;
                        tagsStation.Path = station.Path;
                        tagsStation.StationId = station.Id ?? 0;
                        tagsStation.StationName = station.Name ?? string.Empty;

                        diplayItem.Stations.Add(tagsStation);
                    }

                    Globalvariable.RealtimeDisplays.Add(diplayItem);

                    var dlRealtime = dbContext.FT05s.FirstOrDefault();

                    if (dlRealtime!=null)
                    {
                        dlRealtime.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                        dlRealtime.UpdateAt=DateTime.Now;
                        dlRealtime.UpdateOperatorId="System";   
                    }
                    else
                    {
                        var newLine = new FT02()
                        {
                            Id=Guid.NewGuid(),
                            C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                            CreateAt=DateTime.Now,
                            CreateOperatorId= "System"
                        };
                    }

                    dbContext.SaveChanges();
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
