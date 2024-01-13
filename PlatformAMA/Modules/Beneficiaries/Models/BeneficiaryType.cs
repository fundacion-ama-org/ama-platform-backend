using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Beneficiaries.Models
{
    public class BeneficiaryType
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
        public List<Beneficiarie> Beneficiaries { get; set; }
    }
}
