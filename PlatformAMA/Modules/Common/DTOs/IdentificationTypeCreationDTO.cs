using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Common.DTOs
{
  public class IdentificationTypeCreationDTO
  {
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }
  }
}
