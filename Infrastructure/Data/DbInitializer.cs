using Application.Extentions;
using DocumentFormat.OpenXml.VariantTypes;
using Domain;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace Infrastructure.Data
{
    /// <summary>
    /// Seeding data ban đầu.
    /// </summary>
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            #region Initial DB if not exist DB or any change entity
            // Cách 1: Migration-based (khuyên dùng)
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            // Cách 2: Quick dev setup (chỉ bật khi cần)
            // context.Database.EnsureCreated();

            #endregion

            #region Sedding data if null

            // Check if there are any products already present
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole()
                {
                    Name = ConstantExtention.Roles.Admin,
                    NormalizedName = ConstantExtention.Roles.Admin.ToUpper()
                });
                await context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole()
                {
                    Name = ConstantExtention.Roles.Operator,
                    NormalizedName = ConstantExtention.Roles.Operator.ToUpper()
                });
                await context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole()
                {
                    Name = ConstantExtention.Roles.System,
                    NormalizedName = ConstantExtention.Roles.System.ToUpper()
                });
            }
            //if (!context.Users.Any())
            //{
            //    await context.Users.AddAsync(new Domain.Entities.ApplicationUser()
            //    {
            //        Email = "admin@gmail.com",
            //        EmailConfirmed = true,
            //        FullName = "Admin",
            //        UserName = "admin",
            //        PasswordHash = "admin@gmail.com1",
            //        Status = EnumStatus.Activated,
            //    });
            //}

            if (!context.FT01s.Any())
            {
                var configSystem = new ConfigModel();

                var location = new LocationsInfo();

                var stations = new List<StationInfoModel>()
                {
                  new StationInfoModel(){  Id=1,
                   Name = "Station_1",
                   Path = "Local Station/DauTieng/S71500/Station_1",
                    OffsetConfig = new OffsetConfigModel()
                    {
                        S1_Temp_Oil = 1,
                    }
                  }
                   ,new StationInfoModel(){  Id=2,
                   Name = "Station_2",
                   Path = "Local Station/DauTieng/S71500/Station_2",
                    OffsetConfig = new OffsetConfigModel()
                    {
                        S1_Temp_Oil = 2,
                    }
                   }
                   ,new StationInfoModel(){  Id=3,
                   Name = "Station_3",
                   Path = "Local Station/DauTieng/S71500/Station_3",
                    OffsetConfig = new OffsetConfigModel()
                    {
                        S1_Temp_Oil = -1,
                    }
                   }
                   ,new StationInfoModel(){  Id=4,
                   Name = "Location_Info",
                   Path = "Local Station/DauTieng/S71500/Location_Info",
                    OffsetConfig = new OffsetConfigModel()
                    {
                        Fllow_Ho = 1,
                    }
                   },
                };

                location.Add(new LocationInfoModel()
                {
                    Id = 1,
                    Name = "Hồ Dầu Tiếng",
                    Description = "Local Station DauTieng",
                    Stations = stations
                });

                var ft01 = new FT01()
                {
                    Id = Guid.NewGuid(),
                    C000 = JsonConvert.SerializeObject(configSystem),
                    C001 = JsonConvert.SerializeObject(location),
                    CreateAt = DateTime.Now,
                    CreateOperatorId = "admin", // Assuming admin is the initial user,
                    IsDeleted = false,
                };

                context.FT01s.Add(ft01);

                await context.SaveChangesAsync();
            }

            if (!context.FT02s.Any())
            {
                var sm = new RealtimeDisplays();

                var stations = new List<TagsStation>();
                var lc = await context.FT01s.FirstOrDefaultAsync();
                var d = JsonConvert.DeserializeObject<LocationsInfo>(lc?.C001).FirstOrDefault();
                foreach (var item in d.Stations)
                {
                    stations.Add(new TagsStation()
                    {
                        Path = item.Path,
                        StationId = item.Id ?? 0,
                        StationName = item.Name ?? string.Empty,

                    });
                }

                sm.Add(new RealtimeDisplayModel()
                {
                    LocationId = (int)d.Id,
                    LocationName = d.Name,
                    Stations = stations
                });

                context.FT02s.Add(new FT02()
                {
                    Id = Guid.NewGuid(),
                    C000 = JsonConvert.SerializeObject(sm),
                    CreateAt = DateTime.Now,
                    CreateOperatorId = "admin", // Assuming admin is the initial user,
                    IsDeleted = false,
                });
            }

            if (!context.FT03s.Any())
            {
                var createAt = DateTime.Now;
                var createBy = "admin";

                var newLog = new List<FT03>()
                {
                    new FT03()
                    {
                        Id = Guid.NewGuid(),
                        LogBaseInterval = true,
                        LocationId = 1,
                        LocationName = "Hồ Dầu Tiếng",
                        StationId = 1,
                        StationName = "Station_1",
                        Path = "Local Station/DauTieng/S71500/Station_1",
                        CreateAt = createAt,
                        CreateOperatorId = createBy, // Assuming admin is the initial user,
                        IsDeleted = false,
                    },
                    new FT03()
                    {
                        Id = Guid.NewGuid(),
                        LogBaseInterval = true,
                        LocationId = 1,
                        LocationName = "Hồ Dầu Tiếng",
                        StationId = 2,
                        StationName = "Station_2",
                        Path = "Local Station/DauTieng/S71500/Station_2",
                        CreateAt = createAt,
                        CreateOperatorId = createBy, // Assuming admin is the initial user,
                        IsDeleted = false,
                    },
                    new FT03()
                    {
                        Id = Guid.NewGuid(),
                        LogBaseInterval = false,
                        LocationId = 1,
                        LocationName = "Hồ Dầu Tiếng",
                        StationId = 3,
                        StationName = "Station_3",
                        Path = "Local Station/DauTieng/S71500/Station_3",
                        CreateAt = createAt,
                        CreateOperatorId = createBy, // Assuming admin is the initial user,
                        IsDeleted = false,
                    },
                    new FT03()
                    {
                        Id = Guid.NewGuid(),
                        LogBaseInterval = false,
                        LocationId = 1,
                        LocationName = "Hồ Dầu Tiếng",
                        StationId = 4,
                        StationName = "Location_Info",
                        Path = "Local Station/DauTieng/S71500/Location_Info",
                        CreateAt = createAt,
                        CreateOperatorId = createBy, // Assuming admin is the initial user,
                        IsDeleted = false,
                    },
                };

                await context.FT03s.AddRangeAsync(newLog);
            }

            if (!context.FT04s.Any())
            {
                // With this reflection-based code to get property names:
                List<string> propertyNamesOfTagsStation = typeof(TagsStation).GetProperties().Select(p => p.Name).ToList();
                propertyNamesOfTagsStation.ForEach(name => Console.WriteLine(name));

                var nl = new List<FT04>()
                {
                    new FT04()
                    {
                        Id = Guid.NewGuid(),
                        LocationId = 1,
                        LocationName = "Hồ Dầu Tiếng",
                       StationId = 1,
                       StationName = "Station_1",
                       Path = "Local Station/DauTieng/S71500/Station_1",
                        TagName = propertyNamesOfTagsStation[0],
                        Description = $"Mô tả Tag {propertyNamesOfTagsStation[0]}",
                        CreateAt = DateTime.Now,
                        CreateOperatorId = "admin", // Assuming admin is the initial user,
                        IsDeleted = false,
                    },
                    new FT04()
                    {
                        Id = Guid.NewGuid(),
                        LocationId = 1,
                        LocationName = "Hồ Dầu Tiếng",
                       StationId = 1,
                       StationName = "Station_1",
                       Path = "Local Station/DauTieng/S71500/Station_1",
                        TagName = propertyNamesOfTagsStation[1],
                        Description = $"Mô tả Tag {propertyNamesOfTagsStation[2]}",
                        CreateAt = DateTime.Now,
                        CreateOperatorId = "admin", // Assuming admin is the initial user,
                        IsDeleted = false,
                    },
                };

                await context.FT04s.AddRangeAsync(nl);
            }

            if (!context.ScadaUsers.Any())
            {
                var newuser = new List<ScadaUser>()
                {
                    new ScadaUser()
                    {
                        Id=Guid.NewGuid(),
                        UserName="admin",
                        FullName="Admin",
                        Password =BCryptHelper.HashPassword("admin@123"),
                        PermissionScada=EnumPermissionScada.Admin,
                        CreateAt=DateTime.Now,
                        CreateOperatorId="system",
                        IsDeleted=false,
                    }
                };

                await context.ScadaUsers.AddRangeAsync(newuser);
                await context.SaveChangesAsync();

                var adminUser = await context.ScadaUsers.FirstOrDefaultAsync(u => u.UserName == "admin");
                var result = BCryptHelper.VerifyPassword("admin@123", adminUser.Password);
            }

            #region fixed data for chart
            if (!context.FT05s_ChartHoChua.Any())
            {
                var headers = new[]
     {
                        "7/1", "7/15", "7/16", "7/31",
                        "8/1", "8/15", "8/16", "8/31",
                        "9/1", "9/15", "9/16", "9/30",
                        "10/1", "10/15", "10/16", "10/31",
                        "11/1", "12/1", "12/11", "12/21",
                        "1/1", "1/11", "1/21",
                        "2/1", "2/11", "2/21",
                        "3/1", "3/11", "3/21",
                        "4/1", "4/11", "4/21",
                        "5/1", "5/11", "5/21",
                        "6/1", "6/11", "6/21", "6/30"
                    };


                // === Dữ liệu theo từng dòng ===
                var data_MNC = new double[]
                {
            17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,
            17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,
            17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,17.00,
            17.00,17.00,17.00
                };

                var data_MNDBT = new double[]
                {
            24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,
            24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,
            24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,24.40,
            24.40,24.40,24.40
                };

                var data_MNTK = new double[]
                {
            25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,
            25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,
            25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,25.10,
            25.10,25.10,25.10
                };

                var data_CTDD = new double[]
                {
            28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,
            28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,
            28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,28.00,
            28.00,28.00,28.00
                };

                var data_MNKT = new double[]
                {
            26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,
            26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,
            26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,26.92,
            26.92,26.92,26.92
                };

                var data_DPPH = new double[]
                {
            19.00,19.20,19.24,19.40,19.47,19.90,19.96,20.40,20.45,21.30,21.35,22.15,
            22.24,23.00,23.09,23.80,23.93,24.40,24.40,24.40,24.40,24.19,23.99,23.80,
            23.34,22.92,22.62,22.12,21.66,21.21,20.84,20.51,20.21,20.03,19.86,19.69,
            19.44,19.21,19.00
                };

                var data_HCCN = new double[]
                {
            17.00,17.10,17.17,17.25,17.33,17.60,17.57,17.70,17.80,18.60,18.68,19.50,
            19.56,20.25,20.38,21.10,21.20,21.67,21.60,21.55,21.50,21.21,21.03,20.81,
            20.51,20.21,19.97,19.46,19.03,18.59,18.28,17.98,17.70,17.48,17.45,17.40,
            17.38,17.35,17.00
                };


                var data_DPL = new[]
                {
                        20.30, 20.30, 21.20, 21.20,
                        22.10, 22.10, 22.70, 22.70,
                        23.30, 23.30, 23.65, 23.65,
                        24.00, 24.00, 24.40, 24.40,
                        24.40, 24.40,0,0,
                        0,0,0,0,
                        0,0,0,0,
                        0,0,0,0,
                        0,0,0,0,
                        0,0,0,0
                };

                var list = new List<FT05_ChartHoChua>();
                for (int i = 0; i < headers.Length; i++)
                {
                    list.Add(new FT05_ChartHoChua
                    {
                        Id = Guid.NewGuid(),
                        Index = i + 1,
                        X_Value = headers[i],
                        L_MNC = data_MNC[i],
                        L_MNDBT = data_MNDBT[i],
                        L_MNTK = data_MNTK[i],
                        L_CTDD = data_CTDD[i],
                        L_MNKT = data_MNKT[i],
                        L_DPPH = data_DPPH[i],
                        L_HCCN = data_HCCN[i],
                        L_DPL = data_DPL[i],
                        A_VungA = data_HCCN[i],
                        A_VungB = data_DPPH[i],
                        A_VungC = data_DPL[i],
                    });
                }

                await context.FT05s_ChartHoChua.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }

            if (!context.FT07s_ChartMucNuoc.Any())
            {
                List<FT07_ChartMucNuoc> dInsert = new List<FT07_ChartMucNuoc>();

                Dictionary<string, double> prefix = new Dictionary<string, double>()
                {
                    { "Chân đập", 0 },
                    { "Cầu Mới", 1.5 },
                    { "Km", 3.5 },
                    { "Cầu tàu", 8.5 },
                     { "Trạm TV-DT", 9.5 },
                };


                int index = 1;
                foreach (var d in prefix)
                {
                    var linr = new FT07_ChartMucNuoc()
                    {
                        Id = Guid.NewGuid(),
                        Index = index,
                        X_Prefix = d.Key,
                        X_Value = d.Value,
                        BoPhai = 24.4,
                        BoTrai = 24.4,
                        Q300 = 22.0,
                        Q400 = 20.0,
                        Q600 = 18.0,
                        Q2800 = 15.0,
                        Z_Thuc = null
                    };

                    dInsert.Add(linr);
                    index++;
                }

                await context.FT07s_ChartMucNuoc.AddRangeAsync(dInsert);
                await context.SaveChangesAsync();
            }

            if (!context.FT06s_Interpolation.Any())
            {
                List<FT06_InterpolationTable> dInsert = new List<FT06_InterpolationTable>();

                for (int i = 0; i < 14; i++)
                {
                    var line = new FT06_InterpolationTable()
                    {
                        Id = Guid.NewGuid(),
                        Z = 13 + i,
                    };

                    dInsert.Add(line);
                }

                await context.FT06s_Interpolation.AddRangeAsync(dInsert);
                await context.SaveChangesAsync();
            }
            #endregion

            // save the changes to the database
            await context.SaveChangesAsync();
            #endregion
        }
    }
}
