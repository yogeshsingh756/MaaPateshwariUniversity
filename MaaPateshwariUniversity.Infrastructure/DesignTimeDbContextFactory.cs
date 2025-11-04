using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MaaPateshwariUniversity.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../MaaPateshwariUniversity.API");
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var cs = config.GetConnectionString("DefaultConnection");
            var b = new DbContextOptionsBuilder<ApplicationDbContext>();
            b.UseMySql(cs!, ServerVersion.AutoDetect(cs!));
            return new ApplicationDbContext(b.Options);
        }
    }
}
