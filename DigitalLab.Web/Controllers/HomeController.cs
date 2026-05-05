using DigitalLab.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int instrumentId = 1)
    {
        var readings = await _context.Readings
            .Include(r => r.Instrument)
            .Where(r => r.InstrumentId == instrumentId)
            .OrderBy(r => r.Timestamp)
            .Take(50)
            .ToListAsync();

        ViewBag.Instruments = _context.Instruments.ToList();
        ViewBag.SelectedInstrumentId = instrumentId;

        ViewBag.TotalCount = _context.Readings
            .Count(r => r.InstrumentId == instrumentId);

        return View(readings);
    }
}