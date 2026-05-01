using DigitalLab.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalLab.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReadingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(int instrumentId)
        {
            var data = _context.Readings
                .Include(r => r.Instrument)
                .Where(r => r.InstrumentId == instrumentId)
                .OrderBy(r => r.Timestamp)
                .ToList();

            return Ok(data);
        }

        /*
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
        */

    }


}
