using System.Text.Json.Serialization;

namespace FlightAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? FlightId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumPassengers { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCancelled { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        public Flight? Flight { get; set; }
    }
}