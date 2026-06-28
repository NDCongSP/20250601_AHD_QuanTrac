using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class DesignTimeApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder =new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(@"Server=14.224.229.6,9168;Initial Catalog=ahd;Persist Security Info=True;User ID=dev1;Password=pR*mBaG)@v(yn*Wc;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30000;", b =>
        //builder.UseSqlServer(@"Server=phucthinhautomation.ddns.net,1433;Initial Catalog=ahd;Persist Security Info=True;User ID=dev1;Password=DaPHA5eY@$AWysDW;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30000;", b =>
        {
            b.MigrationsHistoryTable("__EFMigrationsHistory");
        });
        return new ApplicationDbContext(builder.Options);
    }
}
