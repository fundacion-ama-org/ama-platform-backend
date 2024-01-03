

using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Volunteers.Models
{
  public class ActivityType
  {
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedBy { get; set; }
    public DateTime UpdatedBy { get; set; }
  }
}
