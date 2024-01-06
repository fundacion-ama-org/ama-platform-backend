

using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Common.Models
{
  public class Person
  {
    public int Id { get; set; }
    [Required]
    public int IdentificationTypeId { get; set; }
    public IdentificationType IdentificationType { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string LastName { get; set; }
    public string SecondLastName { get; set; }
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}