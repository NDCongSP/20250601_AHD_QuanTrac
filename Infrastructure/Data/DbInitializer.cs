using Application.Extentions;
using Domain;
using Domain.Entities;
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

                var location = new LocationsModel();

                var stations= new List<StationInfoModel>()
                {
                  new StationInfoModel(){  Id=1,
                   Name = "Group 1",
                   Path = "Local Station/DauTieng/S71500/Group1",}
                   ,new StationInfoModel(){  Id=2,
                   Name = "Group 2",
                   Path = "Local Station/DauTieng/S71500/Group2",}
                   ,new StationInfoModel(){  Id=3,
                   Name = "Group 3",
                   Path = "Local Station/DauTieng/S71500/Group3",}
                   ,new StationInfoModel(){  Id=4,
                   Name = "Group 4",
                   Path = "Local Station/DauTieng/S71500/Group4",}
                   ,new StationInfoModel(){  Id=5,
                   Name = "API",
                   Path = "Local Station/DauTieng/S71500/API",}
                };

                location.Add(new LocationInfoModel()
                {
                    Id = 1,
                    Name = "Local Station",
                    Description = "Local Station DauTieng",
                    Stations = stations
                });

                var ft01 = new FT01()
                {
                    Id=Guid.NewGuid(),
                    C000=JsonConvert.SerializeObject(configSystem),
                    C001=JsonConvert.SerializeObject(location),
                    CreateAt= DateTime.Now,
                    CreateOperatorId= "admin", // Assuming admin is the initial user,
                    IsDeleted = false,
                };

                context.FT01s.Add(ft01);
            }

            // save the changes to the database
            await context.SaveChangesAsync();
            #endregion
        }
    }
}
