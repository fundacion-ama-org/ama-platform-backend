
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donors.Models;
using PlatformAMA.Modules.Volunteers.Models;

namespace PlatformAMA
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<IdentificationType> IdentificationTypes { get; set; }
    public DbSet<Donor> Donors { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<Volunteer> Volunteers { get; set; }
    public object DonationType { get; internal set; }
    }
}