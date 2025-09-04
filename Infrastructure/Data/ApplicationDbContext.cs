using Domain.Entities;
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

        #region auth
        public DbSet<MstUserSetting> MstUserSettings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<RoleToPermission> RoleToPermissions { get; set; }
        #endregion

        public DbSet<FT01> FT01s { get; set; }
        public DbSet<FT02> FT02s { get; set; }
        public DbSet<FT03> FT03s { get; set; }
        public DbSet<FT04> FT04s { get; set; }
        public DbSet<ScadaUser> ScadaUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
