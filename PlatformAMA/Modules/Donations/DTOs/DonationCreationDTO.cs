
using System.ComponentModel.DataAnnotations;
using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Donations.DTOs
{
    public class DonationCreationDTO : PersonCreationDTO
    {
        public int DonationTypeId { get; set; }

    }
}
