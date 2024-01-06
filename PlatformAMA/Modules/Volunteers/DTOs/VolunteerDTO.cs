using System;
using System.ComponentModel.DataAnnotations;
using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Volunteers.DTOs
{
  public class VolunteerDTO
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public bool Available { get; set; }
    public int ActivityTypeId { get; set; }
    public ActivityTypeDTO ActivityType { get; set; }
  }
}
