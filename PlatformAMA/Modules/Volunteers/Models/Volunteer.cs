

using System.ComponentModel.DataAnnotations;
using PlatformAMA.Modules.Common.Models;

namespace PlatformAMA.Modules.Volunteers.Models
{
  public class Volunteer
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public bool IsActive { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Address { get; set; }
    public bool Available { get; set; }
    [Required]
    public int ActivityTypeId { get; set; }
    public ActivityType ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}


