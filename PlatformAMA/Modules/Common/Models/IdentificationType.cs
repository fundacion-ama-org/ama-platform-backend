using System.ComponentModel.DataAnnotations;

public class IdentificationType
{
  public int Id { get; set; }
  [Required]
  [MinLength(3)]
  [MaxLength(50)]
  public string Name { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public DateTime CreatedBy { get; set; }
  public DateTime UpdatedBy { get; set; }
}
