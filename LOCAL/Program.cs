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


            using (var dbContext = new ApplicationDbContext())
            {
                //   var configTable = await dbContext.FT01s.FirstOrDefaultAsync();
                //var configTable = dbContext.FT01s.ToList();
                var configTable = dbContext.FT01s.FirstOrDefault();
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
                    var displayItem = new RealtimeDisplayModel()
                    {
                        LocationId = location.Id,
                        LocationName = location.Name,
                        CalculatorValue = new CalculatorValueModel(),
                    };

                    foreach (var station in location.Stations)
                    {
                        TagsStation tagStation = new TagsStation();
                        tagStation.HT_Cylinder1_1_Offset = station.OffsetConfig.HT_Cylinder1_1;
                        tagStation.HT_Cylinder1_2_Offset = station.OffsetConfig.HT_Cylinder1_2;
                        tagStation.HT_Cylinder2_1_Offset = station.OffsetConfig.HT_Cylinder2_1;
                        tagStation.HT_Cylinder2_2_Offset = station.OffsetConfig.HT_Cylinder2_2;
                        tagStation.Door1_Aperture_Offset = station.OffsetConfig.Door1_Aperture;
                        tagStation.Door2_Aperture_Offset = station.OffsetConfig.Door2_Aperture;
                        tagStation.S1_Temp_Oil_Offset = station.OffsetConfig.S1_Temp_Oil;
                        tagStation.Pressure_Oil_Door1_Offset = station.OffsetConfig.Pressure_Oil_Door1;
                        tagStation.Pressure_Oil_Door2_Offset = station.OffsetConfig.Pressure_Oil_Door2;
                        tagStation.Fllow_Door1_Offset = station.OffsetConfig.Fllow_Door1;
                        tagStation.Fllow_Door2_Offset = station.OffsetConfig.Fllow_Door2;
                        tagStation.Fllow_Ho_Offset = station.OffsetConfig.Fllow_Ho;
                        tagStation.Path = station.Path;
                        tagStation.StationId = station.Id ?? 0;
                        tagStation.StationName = station.Name ?? string.Empty;
                        displayItem.Stations.Add(tagStation);

                    }
                    Globalvariable.RealtimeDisplays.Add(displayItem);
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
            }


            Application.Run(new FrmMain());
         //   Application.Run(new FrmMucnuoc());
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
