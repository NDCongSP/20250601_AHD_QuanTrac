using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public class DesignTimeApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder =new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(@"Server=phucthinhautomation.ddns.net;Initial Catalog=ahd_quantrac;Persist Security Info=False;User ID=dev1;Password=Dev@12345;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;", b =>
        {
            b.MigrationsHistoryTable("__EFMigrationsHistory");
        });
        return new ApplicationDbContext(builder.Options);
    }
}
