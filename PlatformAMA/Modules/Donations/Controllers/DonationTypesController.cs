using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformAMA.Modules.Donations.DTOs;
using PlatformAMA.Modules.Donations.Models;

namespace PlatformAMA.Modules.Donations.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DonationTypesController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public DonationTypesController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<List<DonationTypeDTO>>> Get()
		{
			var donationTypes = await context.ActivityTypes.ToListAsync();

			return Ok(mapper.Map<List<DonationTypeDTO>>(donationTypes));
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

		// POST: api/DonationTypes
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] DonationTypeCreationDTO donationTypeCreationDTO)
		{
			var donationType = mapper.Map<ActivityType>(donationTypeCreationDTO);

			context.Add(activityType);
			await context.SaveChangesAsync();

			return Ok(mapper.Map<DonationTypeDTO>(donationType));
		}

		// PUT: api/DonationTypes
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] DonationTypeCreationDTO donationTypeUpdateDTO)
		{
			var existingDonationType = await context.DonationTypes.FirstOrDefaultAsync(at => at.Id == id);

			if (existingDonationType == null)
			{
				return NotFound();
			}

			mapper.Map(donationTypeUpdateDTO, existingDonationType);

			await context.SaveChangesAsync();

			return Ok(mapper.Map<DonationTypeDTO>(existingDonationType));
		}

		// DELETE: api/DonationTypes
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var existingDonationType = context.DonationTypes.FirstOrDefault(at => at.Id == id);

			if (existingDonationType == null)
			{
				return NotFound();
			}

			context.Remove(existingDonationType);
			context.SaveChanges();

			return Ok();
		}
	}
}
