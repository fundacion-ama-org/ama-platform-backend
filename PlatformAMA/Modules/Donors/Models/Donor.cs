
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Volunteers.Models;

namespace PlatformAMA.Modules.Donors.Models
{
  public class Donor
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
  }
}