using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class DesignTimeApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder =new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AHD;Trusted_Connection=True;", b =>
        {
            b.MigrationsHistoryTable("__EFMigrationsHistoryWMS");
        });
        return new ApplicationDbContext(builder.Options);
    }
}
