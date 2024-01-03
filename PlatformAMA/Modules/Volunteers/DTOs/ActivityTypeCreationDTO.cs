using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Volunteers.DTOs
{
  public class ActivityTypeCreationDTO
  {
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }
  }
}
