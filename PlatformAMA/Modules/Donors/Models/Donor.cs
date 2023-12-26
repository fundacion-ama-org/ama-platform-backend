
using PlatformAMA.Modules.Common.Models;

namespace PlatformAMA.Modules.Donors.Models
{
  public class Donor
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
  }
}