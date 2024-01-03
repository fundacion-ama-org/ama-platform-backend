

using AutoMapper;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donors.DTOs;
using PlatformAMA.Modules.Donors.Models;
using PlatformAMA.Modules.Volunteers.DTOs;
using PlatformAMA.Modules.Volunteers.Models;

namespace PlatformAMA.Modules.Common
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<DonorCreationDTO, Person>();
      CreateMap<DonorCreationDTO, Donor>();
      CreateMap<DonorUpdateDTO, Person>();
      CreateMap<DonorUpdateDTO, Donor>();
      CreateMap<Donor, DonorDTO>()
        .ForMember(d => d.FirstName, options => options.MapFrom(s => s.Person.FirstName))
        .ForMember(d => d.LastName, options => options.MapFrom(s => s.Person.LastName))
        .ForMember(d => d.Email, options => options.MapFrom(s => s.Person.Email))
        .ForMember(d => d.PhoneNumber, options => options.MapFrom(s => s.Person.PhoneNumber));
      CreateMap<VolunteerCreationDTO, Person>();
      CreateMap<VolunteerCreationDTO, Volunteer>();

      CreateMap<ActivityType, ActivityTypeDTO>();
      CreateMap<ActivityTypeCreationDTO, ActivityType>();
      
    }

  }
}