
using System.Data.Entity;

namespace RegistrationForm1
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base("ApplicationDbContextConString")
        {
            
        }

        public DbSet<FT01> FT01s { get; set; }
        public DbSet<FT02> FT02s { get; set; }
        public DbSet<FT03> FT03s { get; set; }
        public DbSet<FT05> FT05s { get; set; }
        public DbSet<FT06> FT06s { get; set; }
    }
}
