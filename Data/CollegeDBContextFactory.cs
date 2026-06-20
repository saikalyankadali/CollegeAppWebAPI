using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CollegeApp.Data
{
    public class CollegeDBContextFactory : IDesignTimeDbContextFactory<CollegeDBContext>
    {
        public CollegeDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CollegeDBContext>();
            var conn = config.GetConnectionString("CollegeAppConnectionString");
            optionsBuilder.UseSqlServer(conn);

            return new CollegeDBContext(optionsBuilder.Options);
        }
    }
}
