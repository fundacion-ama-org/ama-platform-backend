using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PlatformAMA;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json")
        .AddJsonFile("appsettings.json")
        .Build();

    var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

    var connectionString = configuration.GetConnectionString("DefaultConnection");

    builder.UseSqlServer(connectionString);

    return new ApplicationDbContext(builder.Options);
  }
}