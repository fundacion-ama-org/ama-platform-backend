using PlatformAMA.Modules.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Beneficiaries.Models

{
    public class Beneficiarie
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string Description { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public BeneficiaryType BeneficiaryType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedBy { get; set; }
    }
}
