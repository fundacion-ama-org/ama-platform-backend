using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Donations.DTOs
{
    public class DonationUpdateDTO : PersonCreationDTO
    {
        public int DonationTypeId { get; set; }
    }
}