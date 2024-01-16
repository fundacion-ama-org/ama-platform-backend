using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Common.DTOs;

namespace PlatformAMA.Modules.Common.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IdentificationTypesController : ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public IdentificationTypesController(ApplicationDbContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    /// <summary>
    /// Obtener todos los tipos de identificación
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
      var identificationTypes = await context.IdentificationTypes.ToListAsync();
      return Ok(mapper.Map<List<IdentificationTypeDTO>>(identificationTypes));
    }

    /// <summary>
    /// Obtener un tipo de identificación por id
    /// </summary>
    /// <param name="id">Id del tipo de identificación</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetById(int id)
    {
      var identificationType = await context.IdentificationTypes.FirstOrDefaultAsync(x => x.Id == id);

      if (identificationType == null)
      {
        return NotFound();
      }

      return Ok(mapper.Map<IdentificationTypeDTO>(identificationType));
    }

    /// <summary>
    /// Crear un tipo de identificación
    /// </summary>
    /// <param name="identificationTypeCreationDTO">Datos para crear el tipo de identificación</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post(IdentificationTypeCreationDTO identificationTypeCreationDTO)
    {
      var identificationType = mapper.Map<IdentificationType>(identificationTypeCreationDTO);
      context.Add(identificationType);
      await context.SaveChangesAsync();
      var identificationTypeDTO = mapper.Map<IdentificationTypeDTO>(identificationType);
      return CreatedAtAction(nameof(GetById), new { id = identificationTypeDTO.Id }, identificationTypeDTO);
    }

    /// <summary>
    /// Actualizar un tipo de identificación
    /// </summary>
    /// <param name="id">Id del tipo de identificación a actualizar</param>
    /// <param name="identificationTypeCreationDTO">Datos para actualizar el tipo de identificación</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, IdentificationTypeCreationDTO identificationTypeCreationDTO)
    {
      var identificationType = await context.IdentificationTypes.FirstOrDefaultAsync(x => x.Id == id);

      if (identificationType == null)
      {
        return NotFound();
      }

      identificationType = mapper.Map(identificationTypeCreationDTO, identificationType);
      await context.SaveChangesAsync();
      return NoContent();
    }

    /// <summary>
    /// Eliminar un tipo de identificación
    /// </summary>
    /// <param name="id">Id del tipo de identificación a eliminar</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
      var identificationType = await context.IdentificationTypes.FirstOrDefaultAsync(x => x.Id == id);

      if (identificationType == null)
      {
        return NotFound();
      }

      context.Remove(identificationType);
      await context.SaveChangesAsync();
      return NoContent();
    }
  }
}
