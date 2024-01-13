

using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Donations.Models
{
	public class DonationType
	{
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Donations> Donations { get; set; }
    }
}
