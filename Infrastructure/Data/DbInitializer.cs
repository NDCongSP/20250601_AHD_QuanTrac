using Application.Extentions;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    /// <summary>
    /// Seeding data ban đầu.
    /// </summary>
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
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
                   Path = "Local Station/DauTieng/S71500/Station_1",}
                   ,new StationInfoModel(){  Id=2,
                   Name = "Station_2",
                   Path = "Local Station/DauTieng/S71500/Station_2",}
                   ,new StationInfoModel(){  Id=3,
                   Name = "Station_3",
                   Path = "Local Station/DauTieng/S71500/Station_3",}
                   ,new StationInfoModel(){  Id=4,
                   Name = "Location_Info",
                   Path = "Local Station/DauTieng/S71500/Location_Info",},
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
                        Path = item.Path
                    });
                }

                sm.Add(new RealtimeDisplayModel()
                {
                    LocationId = 1,
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
                var newuser=new List<ScadaUser>()
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

            // save the changes to the database
            await context.SaveChangesAsync();
            #endregion
        }
    }
}
