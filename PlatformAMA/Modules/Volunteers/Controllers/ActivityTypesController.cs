using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Obtener todos los tipos de actividades
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ActivityTypeDTO>>> Get()
    {
      var activityTypes = await context.ActivityTypes.ToListAsync();

      return Ok(mapper.Map<List<ActivityTypeDTO>>(activityTypes));
    }

    /// <summary>
    /// Obtener un tipo de actividad por id
    /// </summary>
    /// <param name="id">Id del tipo de actividad</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> Get(int id)
    {
      var activityType = context.ActivityTypes.FirstOrDefault(at => at.Id == id);

      if (activityType == null)
      {
        return NotFound();
      }

      return Ok(mapper.Map<ActivityTypeDTO>(activityType));
    }

    /// <summary>
    /// Crear un tipo de actividad
    /// </summary>
    /// <param name="activityTypeCreationDTO">Datos para crear el tipo de actividad</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] ActivityTypeCreationDTO activityTypeCreationDTO)
    {
      var activityType = mapper.Map<ActivityType>(activityTypeCreationDTO);

      context.Add(activityType);
      await context.SaveChangesAsync();

      var activityTypeDTO = mapper.Map<ActivityTypeDTO>(activityType);

      return CreatedAtAction(nameof(Get), new { id = activityTypeDTO.Id }, activityTypeDTO);
    }

    /// <summary>
    /// Actualizar un tipo de actividad
    /// </summary>
    /// <param name="id"></param>
    /// <param name="activityTypeUpdateDTO">Datos para actualizar el tipo de actividad</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Put(int id, [FromBody] ActivityTypeCreationDTO activityTypeUpdateDTO)
    {
      var existingActivityType = await context.ActivityTypes.FirstOrDefaultAsync(at => at.Id == id);

      if (existingActivityType == null)
      {
        return NotFound();
      }

      mapper.Map(activityTypeUpdateDTO, existingActivityType);
      await context.SaveChangesAsync();

      return NoContent();
    }

    /// <summary>
    /// Eliminar un tipo de actividad
    /// </summary>
    /// <param name="id">Id del tipo de actividad a eliminar</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
      var existingActivityType = context.ActivityTypes.FirstOrDefault(at => at.Id == id);

      if (existingActivityType == null)
      {
        return NotFound();
      }

      context.Remove(existingActivityType);
      context.SaveChanges();

      return NoContent();
    }
  }
}
