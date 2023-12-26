
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donors.Models;

namespace PlatformAMA
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Donor> Donors { get; set; }
  }
}