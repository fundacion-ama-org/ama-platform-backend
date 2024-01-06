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

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var identificationTypes = await context.IdentificationTypes.ToListAsync();
      return Ok(mapper.Map<List<IdentificationTypeDTO>>(identificationTypes));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
      var identificationType = await context.IdentificationTypes.FirstOrDefaultAsync(x => x.Id == id);

      if (identificationType == null)
      {
        return NotFound();
      }

      return Ok(mapper.Map<IdentificationTypeDTO>(identificationType));
    }

    [HttpPost]
    public async Task<ActionResult> Post(IdentificationTypeCreationDTO identificationTypeCreationDTO)
    {
      var identificationType = mapper.Map<IdentificationType>(identificationTypeCreationDTO);
      context.Add(identificationType);
      await context.SaveChangesAsync();
      return Ok(mapper.Map<IdentificationTypeDTO>(identificationType));
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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
