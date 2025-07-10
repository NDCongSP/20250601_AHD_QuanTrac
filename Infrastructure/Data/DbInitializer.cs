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
            if (!context.Users.Any())
            {
                await context.Users.AddAsync(new Domain.Entities.ApplicationUser()
                {
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    FullName = "Admin",
                    UserName = "admin",
                    PasswordHash = "admin@gmail.com1",
                    Status = EnumStatus.Activated,
                });
            }

                // save the changes to the database
                await context.SaveChangesAsync();
            #endregion
        }
    }
}
