using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Beneficiaries.DTOs
{
    public class BeneficiaryTypeCreationDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}