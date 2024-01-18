using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donation.DTOs;
using PlatformAMA.Modules.Donations.DTOs;
using PlatformAMA.Modules.Donations.Models;
using PlatformAMA.Modules.Donors.DTOs;
using PlatformAMA.Modules.Donors.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DonationCreationDTO donationCreationDTO)
        {
            var donation = mapper.Map<Donation>(donationCreationDTO);

            donation.CreatedAt = DateTime.Now;
            donation.UpdatedAt = DateTime.Now;

            context.Donations.Add(donation);
            await context.SaveChangesAsync();
            return Ok("Donación creada exitosamente");
        }

        [HttpGet]
        public async Task<ActionResult<List<DonationDTO>>> Get()
        {
            var donations = await context.Donations
                .Include(v => v.Person)
            .Include(v => v.DonationType)
                .ToListAsync();
            var donationDTO = mapper.Map<List<DonationDTO>>(donations);
            return donationDTO;
        }

        [HttpGet("{nombre_donacion}")]
        public async Task<ActionResult<DonationDTO>> Get(string nombre_donacion)
        {
            var donation = await context.Donations
                .FirstOrDefaultAsync(d => d.Nombre_donacion == nombre_donacion);

            if (donation == null)
            {
                return NotFound();
            }

            var donationDTO = mapper.Map<DonationDTO>(donation);
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
            object value = mapper.Map(DonationCreationDTO, donation);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("donor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostDonor([FromBody] DonorCreationDTO donorCreationDTO)
        {
            try
            {
                var identificationType = await context.IdentificationTypes.AnyAsync(it => it.Id == donorCreationDTO.IdentificationTypeId);

                if (!identificationType)
                {
                    return BadRequest($"Identification Type with Id {donorCreationDTO.IdentificationTypeId} does not exist");
                }

                var person = mapper.Map<Person>(donorCreationDTO);
                context.Add(person);
                await context.SaveChangesAsync();

                var donor = new Donor { PersonId = person.Id };
                context.Add(donor);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = donor.Id }, mapper.Map<DonorDTO>(donor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
