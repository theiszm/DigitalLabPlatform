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

                foreach (var instrumentId in instruments)
                {
                    double value;

                    switch (instrumentId)
                    {
                        case 1: // Main Meter (stable)
                            value = 50 + _random.NextDouble() * 5;
                            break;

                        case 2: // Backup Meter (slightly higher variance)
                            value = 60 + _random.NextDouble() * 15;
                            break;

                        case 3: // Test Sensor (noisy / spiky)
                            value = _random.NextDouble() * 100;
                            break;

                        default:
                            value = _random.NextDouble() * 100;
                            break;
                    }

                    var reading = new Reading
                    {
                        InstrumentId = instrumentId,
                        Value = value,
                        Unit = "kWh",
                        Timestamp = DateTime.UtcNow
                    };

                    db.Readings.Add(reading);
                }

                await db.SaveChangesAsync();

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
