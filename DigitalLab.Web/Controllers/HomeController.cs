using DigitalLab.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DigitalLab.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalLab.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var readings = await _context.Readings
                .OrderBy(r => r.Timestamp)
                .Take(50)
                .ToListAsync();

            return View(readings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
