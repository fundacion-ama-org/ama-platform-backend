

namespace PlatformAMA.Modules.Volunteers.Models
{
  public class Volunteer
  {
    public int Id { get; set; }
    public bool Status { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public bool Available { get; set; }
    public int ActivityTypeId { get; set; }
    public ActivityType ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedBy { get; set; }
    public DateTime UpdatedBy { get; set; }
  }
}


