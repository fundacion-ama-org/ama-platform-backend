using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Donors.DTOs;
using PlatformAMA.Modules.Donors.Models;

namespace PlatformAMA.Modules.Donors.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DonorsController : ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public DonorsController(ApplicationDbContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<DonorDTO>>> Get()
    {
      var donors = await context.Donors
        .Include(d => d.Person)
        .ToListAsync();

      return mapper.Map<List<DonorDTO>>(donors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<DonorDTO> Get(int id)
    {
      var donor = context.Donors.Include(d => d.Person).FirstOrDefault(d => d.Id == id);

      if (donor == null)
      {
        return NotFound();
      }

      return mapper.Map<DonorDTO>(donor);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] DonorCreationDTO donorCreationDTO)
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] DonorUpdateDTO donorUpdateDTO)
    {

      var donor = await context.Donors.Include(d => d.Person).FirstOrDefaultAsync(d => d.Id == id);

      if (donor == null)
      {
        return NotFound();
      }

      mapper.Map(donorUpdateDTO, donor.Person);
      await context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      var donor = await context.Donors.FindAsync(id);

      if (donor == null)
      {
        return NotFound();
      }

      context.Donors.Remove(donor);
      await context.SaveChangesAsync();

      return NoContent();
    }
  }
}