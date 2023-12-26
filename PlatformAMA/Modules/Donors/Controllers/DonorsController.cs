using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Donors.Models;

namespace PlatformAMA.Modules.Donors.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DonorsController : ControllerBase
  {
    private readonly ApplicationDbContext context;

    public DonorsController(ApplicationDbContext context)
    {
      this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Donor>>> Get()
    {
      return await context.Donors.Include(d => d.Person).ToListAsync();
    }

    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      return "value";
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Donor donor)
    {
      context.Add(donor);
      await context.SaveChangesAsync();
      return Ok();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}