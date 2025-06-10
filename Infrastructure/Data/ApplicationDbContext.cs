using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public string GetConnectionString()
        {
            return this.Database.GetDbConnection().ConnectionString;
        }

        #region authp
        public DbSet<TenantAuth> TenantAuth { get; set; }
        public DbSet<MstUserSetting> MstUserSettings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsTenant> PermissionsTenants { get; set; }
        public DbSet<RoleToPermission> RoleToPermissions { get; set; }
        public DbSet<RoleToPermissionTenant> RoleToPermissionTenants { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region DB Chinh ko migration
            // Ánh xạ bảng "Orders" tới schema "sales"
            //modelBuilder.Entity<PermissionsListModel>().ToTable("PermissionsListModels", "dbo", x => x.ExcludeFromMigrations());//ko cho migration cac bang hien co cua FBT_DEV

            modelBuilder.Entity<TenantAuth>()
               .ToTable("Tenants", "authp");
            #endregion


            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                if (item.Name.Contains("RoleToPermissionTenant"))
                {
                    var a = item.Name;
                }
                
                if (!string.IsNullOrEmpty(item.ClrType.Namespace))
                {
                    if (item.ClrType.Namespace.Contains("WMS"))
                    {
                        item.SetSchema("wms");
                        continue;
                    }
                }
                
            }

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers", "wms");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims", "wms");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles", "wms");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims", "wms");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins", "wms");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles", "wms");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens", "wms");

            ////Thay đổi mô hình code. thay đổi độ dài của prametter UserName của entity framework identity.
            //modelBuilder.Entity<ApplicationUser>(entity =>
            //{
            //    entity.Property(u => u.UserName).HasMaxLength(256); // Hoặc giá trị bạn muốn
            //});

            // Ghi đè bảng __EFMigrationsHistory
            //modelBuilder.HasAnnotation("Relational:Schema", "wms", x => x.ExcludeFromMigrations());  // Đặt schema tùy chỉnh
            // modelBuilder.HasAnnotation("Relational:HistoryTableName", "__EFMigrationsHistoryWMS");  // Đặt tên bảng tùy chỉnh
        }
    }
}
