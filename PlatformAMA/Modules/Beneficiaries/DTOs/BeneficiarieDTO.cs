using System;
using System.ComponentModel.DataAnnotations;
using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Beneficiaries.DTOs
{
    public class BeneficiarieDTO
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public BeneficiaryTypeDTO BeneficiciaryType { get; set; }
    }
}