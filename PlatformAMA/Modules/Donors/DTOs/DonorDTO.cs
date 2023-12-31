

using PlatformAMA.Modules.Common.DTOs;
using PlatformAMA.Modules.Volunteers.DTOs;

namespace PlatformAMA.Modules.Donors.DTOs
{
  public class DonorDTO
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
  }
}