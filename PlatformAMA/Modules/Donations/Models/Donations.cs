using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Volunteers.Models;

namespace PlatformAMA.Modules.Donors.Models
{
    public class Donations
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