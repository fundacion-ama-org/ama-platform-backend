using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PlatformAMA.Modules.Volunteers.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VolunteersController : ControllerBase
  {    
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
    public ActionResult<string> Post([FromBody] string value)
    {
      // TODO: Implement logic to create a new volunteer
      return "Volunteer created";
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
