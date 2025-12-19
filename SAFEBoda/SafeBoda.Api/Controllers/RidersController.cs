using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SafeBoda.Core.Models;
using SafeBoda.Infrastructure;

namespace SafeBoda.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RidersController : ControllerBase
    {
        private readonly SafeBodaDbContext _context;

        public RidersController(SafeBodaDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetRiders()
        {
            return await _context.Riders.ToListAsync();
        }
        
        [HttpPost]
        public async Task<ActionResult<Rider>> PostRider(Rider rider)
        {
            if (rider.Id == Guid.Empty)
            {
                rider.Id = Guid.NewGuid();
            }

            _context.Riders.Add(rider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRiders", new { id = rider.Id }, rider);
        }
    }
}