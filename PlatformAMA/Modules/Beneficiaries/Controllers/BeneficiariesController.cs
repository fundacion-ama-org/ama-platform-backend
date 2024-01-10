using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Beneficiaries.DTOs;
using PlatformAMA.Modules.Beneficiaries.Models;
using System.Collections.Generic;

namespace PlatformAMA.Modules.Beneficiaries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BeneficiariesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BeneficiarieDTO>>> Get()
        {
            var beneficiaries = await context.Beneficiaries
              .Include(v => v.Person)
              .Include(v => v.BeneficiaryType)
              .ToListAsync();
            var beneficiariesDTO = mapper.Map<List<BeneficiarieDTO>>(beneficiaries);
            return beneficiariesDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeneficiarieDTO>> Get(int id)
        {
            var beneficiarie = await context.Beneficiaries.FirstOrDefaultAsync(v => v.Id == id);
            if (beneficiarie == null)
            {
                return NotFound();
            }
            var beneficiariesDTO = mapper.Map<BeneficiarieDTO>(beneficiarie);
            return beneficiariesDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BeneficiarieCreationDTO beneficiariesCreationDTO)
        {
            var person = mapper.Map<Person>(beneficiariesCreationDTO);
            context.Add(person);
            await context.SaveChangesAsync();

            var beneficiarie = mapper.Map<Beneficiarie>(beneficiariesCreationDTO);
            beneficiarie.PersonId = person.Id;
            beneficiarie.IsActive = true;
            beneficiarie.CreatedAt = DateTime.Now;
            beneficiarie.UpdatedAt = DateTime.Now;


            context.Add(beneficiarie);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BeneficiarieCreationDTO beneficiariesCreationDTO)
        {
            var beneficiarie = await context.Beneficiaries.FirstOrDefaultAsync(v => v.Id == id);
            if (beneficiarie == null)
            {
                return NotFound();
            }

            var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == beneficiarie.PersonId);
            if (person == null)
            {
                return NotFound();
            }

            mapper.Map(beneficiariesCreationDTO, person);
            await context.SaveChangesAsync();

            mapper.Map(beneficiariesCreationDTO, beneficiarie);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beneficiarie = await context.Beneficiaries.FirstOrDefaultAsync(v => v.Id == id);
            if (beneficiarie == null)
            {
                return NotFound();
            }

            beneficiarie.IsActive = false;
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
