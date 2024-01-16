
using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Common.DTOs
{
  public class PersonCreationDTO
  {
    [Required]
    public int? IdentificationTypeId { get; set; }
    [Required]
    [CedulaEcuatoriana(ErrorMessage = "La cédula no es válida.")]
    public string Identification { get; set; }
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
