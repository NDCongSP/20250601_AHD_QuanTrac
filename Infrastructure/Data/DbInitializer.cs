using Application.Extentions;
using Domain;

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
            // Ensure the database is created (or already exists), chỉ chạy 1 lần, sau đó nếu có sự thay đổi Entity thì nó ko cập nhật DB, nên nếu bật thì chỉ bật khi chạy runTime
            //context.Database.EnsureCreated();

            //chạy migration theo các migration đc tạo trong lúc build code.
            //if (context.Database.GetPendingMigrations().Any())
            //    context.Database.Migrate();
            //context.SaveChanges();
            #endregion

            #region Sedding data if null
            //string[] categoryName = { "Categorys 1", "Categorys 2", "Categorys 3", "Categorys 4", "Categorys 5", "Categorys 6" };
            //string[] productName = { "Product 1", "Product 2", "Product 3", "Product 4", "Product 5", "Product 6" };

            //var units = new List<Unit>();
            //units.Add(new Unit()
            //{  

            //    Id = Guid.NewGuid(),
            //    Name = "Box",
            //    CreatedDate = DateTime.Now,
            //    CreatedBy = "SeedingData",
            //    IsActived = true,
            //});
            //units.Add(new Unit()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Pcs",
            //    CreatedDate = DateTime.Now,
            //    CreatedBy = "SeedingData",
            //    IsActived = true,
            //});

            //var categorys = new List<Category>();
            //for (int i = 0; i < 6; i++)
            //{
            //    categorys.Add(new Category()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = categoryName[i],
            //        CreatedDate = DateTime.Now,
            //        CreatedBy = "SeedingData",
            //        IsActived = true,
            //    });
            //}

            //var products = new List<Product>();
            //for (int i = 0; i < 6; i++)
            //{
            //    products.Add(new Product()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = productName[i],
            //        IdUnit = units[0].Id,
            //        UnitName = units[0].Name,
            //        IdCategory = categorys[i].Id,
            //        CategoryName = categorys[i].Name,
            //        CreatedDate = DateTime.Now,
            //        CreatedBy = "SeedingData",
            //        IsActived = true,
            //    });
            //}

            // Check if there are any products already present
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole()
                {
                    Name = ConstantExtention.Roles.WarehouseAdmin,
                    NormalizedName = ConstantExtention.Roles.WarehouseAdmin.ToUpper()
                });
                await context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole()
                {
                    Name = ConstantExtention.Roles.WarehouseStaff,
                    NormalizedName = ConstantExtention.Roles.WarehouseStaff.ToUpper()
                });
                await context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole()
                {
                    Name = ConstantExtention.Roles.WarehouseSystem,
                    NormalizedName = ConstantExtention.Roles.WarehouseSystem.ToUpper()
                });
            }

            if (!context.TenantAuth.Any())
            {
                await context.TenantAuth.AddAsync(new Domain.TenantAuth()
                {
                    //TenantId=1,
                    ParentTenantId = 1,
                    ParentDataKey = "1",
                    TenantFullName = "Tealine",
                    IsHierarchical = true,
                    HasOwnDb = false,
                });
                await context.TenantAuth.AddAsync(new Domain.TenantAuth()
                {
                    //TenantId = 2,
                    ParentTenantId = 1,
                    ParentDataKey = "1",
                    TenantFullName = "Tealine | KOKORO",
                    IsHierarchical = true,
                    HasOwnDb = false,
                });
            }

            // save the changes to the database
            await context.SaveChangesAsync();
            #endregion
        }
    }
}
