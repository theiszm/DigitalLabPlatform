using DigitalLab.Web.Data;
using DigitalLab.Web.Models;

namespace DigitalLab.Web.Services
{
    public class InstrumentSimulator : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Random _random = new();

        public InstrumentSimulator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var instruments = db.Instruments.Select(i => i.Id).ToList();

                if (!instruments.Any())
                    continue;

                var reading = new Reading
                {
                    InstrumentId = instruments[_random.Next(instruments.Count)],
                    Value = _random.NextDouble() * 100,
                    Unit = "kWh",
                    Timestamp = DateTime.UtcNow
                };

                db.Readings.Add(reading);
                await db.SaveChangesAsync();

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
