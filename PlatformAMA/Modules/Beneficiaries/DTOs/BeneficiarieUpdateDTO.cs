using System;
using System.ComponentModel.DataAnnotations;
using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Beneficiaries.DTOs
{
    public class BeneficiarieUpdateDTO : PersonCreationDTO
    {
        public string Description { get; set; }
        [Required]
        public int BeneficiaryTypeId { get; set; }
    }
}