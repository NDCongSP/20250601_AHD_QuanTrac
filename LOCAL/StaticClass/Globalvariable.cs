using Ahd.Core;
using Domain;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public static class Globalvariable
    {
        public static ConfigModel ConfigSystem { get; set; } = new ConfigModel();

        public static LocationsInfo LocationsInfo { get; set; } = new LocationsInfo();

        public static RealtimeDisplays RealtimeDisplays { get; set; } = new RealtimeDisplays();

        public static FT03 DataLog { get; set; } = new FT03();

        public static FT04 AlarmDataLog { get; set; } = new FT04();

        public static void InvokeIfRequired(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static void GetConfig()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var configTable = dbContext.FT01s.FirstOrDefault();
                if (configTable == null)
                {
                    MessageBox.Show("Cấu hình cơ sở dữ liệu không hợp lệ hoặc chưa được thiết lập. Vui lòng cấu hình.",
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Globalvariable.ConfigSystem = JsonConvert.DeserializeObject<ConfigModel>(configTable.C000);
                Globalvariable.LocationsInfo = JsonConvert.DeserializeObject<LocationsInfo>(configTable.C001);

                var isNotFirst = true;
                foreach (var location in Globalvariable.LocationsInfo)
                {
                    var displayItem = new RealtimeDisplayModel()
                    {
                        LocationId = location.Id,
                        LocationName = location.Name,
                        CalculatorValue = new CalculatorValueModel(),
                    };

                    isNotFirst = Globalvariable.RealtimeDisplays.Count > 0;

                    foreach (var station in location.Stations)
                    {
                        var item = Globalvariable.RealtimeDisplays?.FirstOrDefault()?.Stations?.FirstOrDefault(x => x.StationId == station.Id);
                        if (item != null)
                        {
                            
                            //item.HT_Cylinder1_1_Offset = station.OffsetConfig.HT_Cylinder1_1;
                            //item.HT_Cylinder1_2_Offset = station.OffsetConfig.HT_Cylinder1_2;
                            //item.HT_Cylinder2_1_Offset = station.OffsetConfig.HT_Cylinder2_1;
                            //item.HT_Cylinder2_2_Offset = station.OffsetConfig.HT_Cylinder2_2;
                            //item.Door1_Aperture_Offset = station.OffsetConfig.Door1_Aperture;
                            //item.Door2_Aperture_Offset = station.OffsetConfig.Door2_Aperture;
                            //item.S1_Temp_Oil_Offset = station.OffsetConfig.S1_Temp_Oil;
                            //item.Pressure_Oil_Door1_Offset = station.OffsetConfig.Pressure_Oil_Door1;
                            //item.Pressure_Oil_Door2_Offset = station.OffsetConfig.Pressure_Oil_Door2;
                            //item.Fllow_Door1_Offset = station.OffsetConfig.Fllow_Door1;
                            //item.Fllow_Door2_Offset = station.OffsetConfig.Fllow_Door2;
                            //item.Fllow_Ho_Offset = station.OffsetConfig.Fllow_Ho;
                            item.Path = station.Path;
                            item.StationId = station.Id ?? 0;
                            item.StationName = station.Name ?? string.Empty;
                        }
                        else
                        {
                           

                            TagsStation tagStation = new TagsStation();
                            //tagStation.HT_Cylinder1_1_Offset = station.OffsetConfig.HT_Cylinder1_1;
                            //tagStation.HT_Cylinder1_2_Offset = station.OffsetConfig.HT_Cylinder1_2;
                            //tagStation.HT_Cylinder2_1_Offset = station.OffsetConfig.HT_Cylinder2_1;
                            //tagStation.HT_Cylinder2_2_Offset = station.OffsetConfig.HT_Cylinder2_2;
                            //tagStation.Door1_Aperture_Offset = station.OffsetConfig.Door1_Aperture;
                            //tagStation.Door2_Aperture_Offset = station.OffsetConfig.Door2_Aperture;
                            //tagStation.S1_Temp_Oil_Offset = station.OffsetConfig.S1_Temp_Oil;
                            //tagStation.Pressure_Oil_Door1_Offset = station.OffsetConfig.Pressure_Oil_Door1;
                            //tagStation.Pressure_Oil_Door2_Offset = station.OffsetConfig.Pressure_Oil_Door2;
                            //tagStation.Fllow_Door1_Offset = station.OffsetConfig.Fllow_Door1;
                            //tagStation.Fllow_Door2_Offset = station.OffsetConfig.Fllow_Door2;
                            //tagStation.Fllow_Ho_Offset = station.OffsetConfig.Fllow_Ho;
                            tagStation.Path = station.Path;
                            tagStation.StationId = station.Id ?? 0;
                            tagStation.StationName = station.Name ?? string.Empty;
                            displayItem.Stations.Add(tagStation);
                        }

                    }
                    
                    if(!isNotFirst)
                        Globalvariable.RealtimeDisplays.Add(displayItem);
                }
            }
        }

        public static ScadaUser UserInfo { get; set; }
    }
}
