using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donations.DTOs;
using PlatformAMA.Modules.Donations.Models;

namespace PlatformAMA.Modules.Donations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DonationsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DonationDTO>>> Get()
        {
            var donations = await context.Donations
              .Include(v => v.Person)
              .Include(v => v.ActivityType)
              .ToListAsync();
            var donationDTO = mapper.Map<List<DonationDTO>>(dionations);
            return donationDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonationDTO>> Get(int id)
        {
            var donation = await context.Donations.FirstOrDefaultAsync(v => v.Id == id);
            if (donation == null)
            {
                return NotFound();
            }
            var donationDTO = mapper.Map<DonationsDTO>(donation);
            return donationDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DonationCreationDTO donationCreationDTO)
        {
            var person = mapper.Map<Person>(donationCreationDTO);
            context.Add(person);
            await context.SaveChangesAsync();

            var donation = mapper.Map<Volunteer>(donationCreationDTO);
            
            donation.PersonId = person.Id;
            donation.IsActive = true;
            donation.Available = true;
            donation.CreatedAt = DateTime.Now;
            donation.UpdatedAt = DateTime.Now;


            context.Add(volunteer);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DonationCreationDTO donationCreationDTO)
        {
            var volunteer = await context.Donations.FirstOrDefaultAsync(v => v.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == volunteer.PersonId);
            if (person == null)
            {
                return NotFound();
            }

            mapper.Map(donationCreationDTO, person);
            await context.SaveChangesAsync();

            mapper.Map(donationCreationDTO, donation);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var donation = await context.Donations.FirstOrDefaultAsync(v => v.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            donation.IsActive = false;
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
