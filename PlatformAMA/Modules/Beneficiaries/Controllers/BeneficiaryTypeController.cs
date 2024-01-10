using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Beneficiaries.DTOs;
using PlatformAMA.Modules.Beneficiaries.Models;

namespace PlatformAMA.Modules.Beneficiaries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryTypeController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BeneficiaryTypeController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BeneficiaryTypeDTO>>> Get()
        {
            var beneficiaryType = await context.BeneficiaryType.ToListAsync();

            return Ok(mapper.Map<List<BeneficiaryTypeDTO>>(beneficiaryType));
        }

        // GET: api/BeneficiaryTypes/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var beneficiaryType = context.BeneficiaryType.FirstOrDefault(at => at.Id == id);

            if (beneficiaryType == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BeneficiaryTypeDTO>(beneficiaryType));
        }

        // POST: api/BeneficiaryTypes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BeneficiaryTypeCreationDTO beneficiaryTypeCreationDTO)
        {
            var beneficiaryType = mapper.Map<BeneficiaryType>(beneficiaryTypeCreationDTO);

            context.Add(beneficiaryType);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<BeneficiaryTypeDTO>(beneficiaryType));
        }

        // PUT: api/BeneficiaryTypes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BeneficiaryTypeCreationDTO beneficiaryTypeUpdateDTO)
        {
            var existingBeneficiaryType = await context.BeneficiaryType.FirstOrDefaultAsync(at => at.Id == id);

            if (existingBeneficiaryType == null)
            {
                return NotFound();
            }

            mapper.Map(beneficiaryTypeUpdateDTO, existingBeneficiaryType);

            await context.SaveChangesAsync();

            return Ok(mapper.Map<BeneficiaryTypeDTO>(existingBeneficiaryType));
        }

        // DELETE: api/BeneficiaryTypes/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingBeneficiaryType = context.BeneficiaryType.FirstOrDefault(at => at.Id == id);

            if (existingBeneficiaryType == null)
            {
                return NotFound();
            }

            context.Remove(existingBeneficiaryType);
            context.SaveChanges();

            return Ok();
        }
    }
}

