using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<IEnumerable<string>> Get()
    {
      // TODO: Implement logic to retrieve all volunteers
      return new string[] { "Volunteer 1", "Volunteer 2" };
    }

    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      // TODO: Implement logic to retrieve a specific volunteer by ID
      return "Volunteer " + id;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VolunteerCreationDTO volunteerCreationDTO)
    {
      var person = mapper.Map<Person>(volunteerCreationDTO);
      context.Add(person);
      await context.SaveChangesAsync();

      var volunteer = new Volunteer { PersonId = person.Id };
      context.Add(volunteer);
      await context.SaveChangesAsync();

      return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult<string> Put(int id, [FromBody] string value)
    {
      // TODO: Implement logic to update a specific volunteer by ID
      return "Volunteer updated";
    }

    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      // TODO: Implement logic to delete a specific volunteer by ID
      return "Volunteer deleted";
    }
  }
}
