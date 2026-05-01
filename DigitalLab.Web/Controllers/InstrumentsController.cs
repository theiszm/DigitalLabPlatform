using Microsoft.AspNetCore.Mvc;
using DigitalLab.Web.Data;

namespace DigitalLab.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstrumentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var instruments = _context.Instruments.ToList();
            return Ok(instruments);
        }
    }
}
