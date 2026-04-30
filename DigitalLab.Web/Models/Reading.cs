namespace DigitalLab.Web.Models
{
    public class Reading
    {
        public int Id { get; set; }

        public int InstrumentId { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
        public Instrument? Instrument { get; set; }
    }
}
