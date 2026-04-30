using Microsoft.EntityFrameworkCore;
using DigitalLab.Web.Models;

using System.Security.Cryptography.X509Certificates;

namespace DigitalLab.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }   
        
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Reading> Readings { get; set; }
    }
}
