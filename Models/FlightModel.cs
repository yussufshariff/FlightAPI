using System.Text.Json.Serialization;

namespace FlightAPI.Models
{
    public class Flight
    {
        public string FlightId { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Airline { get; set; } = string.Empty;
        public string Aircraft { get; set; } = string.Empty;
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public TimeSpan Duration { get; set; }
    }
}
