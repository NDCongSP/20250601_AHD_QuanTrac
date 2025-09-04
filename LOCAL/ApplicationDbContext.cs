
using Domain.Entities;
using System.Data.Entity;

namespace RegistrationForm1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ApplicationDbContextConString")
        {

        }

        public DbSet<FT01> FT01s { get; set; }
        public DbSet<FT02> FT02s { get; set; }
        public DbSet<FT03> FT03s { get; set; }
        public DbSet<FT04> FT04s { get; set; }
        public DbSet<FT05> FT05s { get; set; }
        public DbSet<ScadaUser> ScadaUsers { get; set; }
    }
}
