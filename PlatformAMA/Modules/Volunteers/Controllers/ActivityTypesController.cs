using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Volunteers.DTOs;
using PlatformAMA.Modules.Volunteers.Models;

namespace PlatformAMA.Modules.Volunteers.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ActivityTypesController : ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public ActivityTypesController(ApplicationDbContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivityTypeDTO>>> Get()
    {
      var activityTypes = await context.ActivityTypes.ToListAsync();

      return Ok(mapper.Map<List<ActivityTypeDTO>>(activityTypes));
    }

    // GET: api/ActivityTypes/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      var activityType = context.ActivityTypes.FirstOrDefault(at => at.Id == id);

      if (activityType == null)
      {
        return NotFound();
      }

      return Ok(mapper.Map<ActivityTypeDTO>(activityType));
    }

    // POST: api/ActivityTypes
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ActivityTypeCreationDTO activityTypeCreationDTO)
    {
      var activityType = mapper.Map<ActivityType>(activityTypeCreationDTO);

      context.Add(activityType);
      await context.SaveChangesAsync();

      return Ok(mapper.Map<ActivityTypeDTO>(activityType));
    }

    // PUT: api/ActivityTypes/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ActivityTypeCreationDTO activityTypeUpdateDTO)
    {
      var existingActivityType = await context.ActivityTypes.FirstOrDefaultAsync(at => at.Id == id);

      if (existingActivityType == null)
      {
        return NotFound();
      }

      mapper.Map(activityTypeUpdateDTO, existingActivityType);

      await context.SaveChangesAsync();

      return Ok(mapper.Map<ActivityTypeDTO>(existingActivityType));
    }

    // DELETE: api/ActivityTypes/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var existingActivityType = context.ActivityTypes.FirstOrDefault(at => at.Id == id);

      if (existingActivityType == null)
      {
        return NotFound();
      }

      context.Remove(existingActivityType);
      context.SaveChanges();

      return Ok();
    }
  }
}
