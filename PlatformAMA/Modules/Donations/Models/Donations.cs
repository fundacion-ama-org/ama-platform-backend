using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donations.Models;
using System.ComponentModel.DataAnnotations;


namespace PlatformAMA.Modules.Donations.Models
{
    public class Donations
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int BrigadeId { get; set; }
      
        public string DonationName { get; set; }
        public int DonationTypeId { get; set; }
        public DonationType DonationType { get; set; }
        public decimal Total { get; set; }
        public DateTime AsignedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}