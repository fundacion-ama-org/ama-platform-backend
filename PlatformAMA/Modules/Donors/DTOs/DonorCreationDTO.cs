

using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Donors.DTOs
{
  public class DonorCreationDTO
  {
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
  }
}
