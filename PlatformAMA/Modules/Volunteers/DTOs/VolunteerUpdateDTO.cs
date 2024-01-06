using System;
using System.ComponentModel.DataAnnotations;
using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Volunteers.DTOs
{
  public class VolunteerUpdateDTO : PersonCreationDTO
  {
    public string Gender { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Address { get; set; }
    [Required]
    public int ActivityTypeId { get; set; }
  }
}
