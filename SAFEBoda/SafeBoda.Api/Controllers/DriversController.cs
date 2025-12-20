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
    public class DriversController : ControllerBase
    {
        private readonly SafeBodaDbContext _context;

        public DriversController(SafeBodaDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }
        
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            if (driver.Id == Guid.Empty)
            {
                driver.Id = Guid.NewGuid();
            }

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrivers", new { id = driver.Id }, driver);
        }
    }
}