
using System.Data.Entity;

namespace WindowsFormsApp1
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base("ApplicationDbContextConString")
        {
            
        }

        public DbSet<FT01> FT01s { get; set; }


    }
}
