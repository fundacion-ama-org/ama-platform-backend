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

        public DonationsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DonationDTO>>> Get()
        {
            var donations = await context.Donations
              .Include(d => d.Person)
              .ToListAsync();

            return mapper.Map<List<DonationDTO>>(donations);
        }

        [HttpGet("{id}")]
        public ActionResult<DonationDTO> Get(int id)
        {
            var donor = context.Donations.Include(d => d.Person).FirstOrDefault(d => d.Id == id);


            if (donor == null)
            {
                return NotFound();
            }

            return mapper.Map<DonationDTO>(donor);
        }
