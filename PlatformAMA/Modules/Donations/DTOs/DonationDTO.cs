using PlatformAMA.Modules.Common.DTOs;
using PlatformAMA.Modules.Volunteers.DTOs;

namespace PlatformAMA.Modules.Donation.DTOs
{
    public class DonationDto
    {
        public int DonationId { get; set; }
        public string DonationName { get; set; }
        public string DonationType { get; set; }
        public decimal Value { get; set; }
        public decimal Total { get; set; }
        public string Donor { get; set; }
        public DateTime DonationDate { get; set; }
    }
}