using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.Models;
using PlatformAMA.Modules.Volunteers.DTOs;
using PlatformAMA.Modules.Volunteers.Models;
using System.Collections.Generic;

namespace PlatformAMA.Modules.Volunteers.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VolunteersController : ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public VolunteersController(ApplicationDbContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<VolunteerDTO>>> Get()
    {
      var volunteers = await context.Volunteers
        .Include(v => v.Person)
        .Include(v => v.ActivityType)
        .ToListAsync();
      var volunteersDTO = mapper.Map<List<VolunteerDTO>>(volunteers);
      return volunteersDTO;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VolunteerDTO>> Get(int id)
    {
      var volunteer = await context.Volunteers.FirstOrDefaultAsync(v => v.Id == id);
      if (volunteer == null)
      {
        return NotFound();
      }
      var volunteerDTO = mapper.Map<VolunteerDTO>(volunteer);
      return volunteerDTO;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VolunteerCreationDTO volunteerCreationDTO)
    {
      var person = mapper.Map<Person>(volunteerCreationDTO);
      context.Add(person);
      await context.SaveChangesAsync();

      var volunteer = mapper.Map<Volunteer>(volunteerCreationDTO);
      volunteer.PersonId = person.Id;
      volunteer.IsActive = true;
      volunteer.Available = true;
      volunteer.CreatedAt = DateTime.Now;
      volunteer.UpdatedAt = DateTime.Now;

      
      context.Add(volunteer);
      await context.SaveChangesAsync();

      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] VolunteerCreationDTO volunteerCreationDTO)
    {
      var volunteer = await context.Volunteers.FirstOrDefaultAsync(v => v.Id == id);
      if (volunteer == null)
      {
        return NotFound();
      }

      var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == volunteer.PersonId);
      if (person == null)
      {
        return NotFound();
      }

      mapper.Map(volunteerCreationDTO, person);
      await context.SaveChangesAsync();

      mapper.Map(volunteerCreationDTO, volunteer);
      await context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var volunteer = await context.Volunteers.FirstOrDefaultAsync(v => v.Id == id);
      if (volunteer == null)
      {
        return NotFound();
      }

      volunteer.IsActive = false;
      await context.SaveChangesAsync();

      return NoContent();
    }
  }
}
