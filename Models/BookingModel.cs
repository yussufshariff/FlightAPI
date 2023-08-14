
using System.ComponentModel.DataAnnotations;

namespace FlightAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string FlightId { get; set; }
        public string PassengerName { get; set; }
        public string Email { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumPassengers { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCancelled { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
