namespace FlightAPI.Models
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public string FlightId { get; set; }
        public int UserId { get; set; }
        public string PassengerName { get; set; }
        public string Email { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumPassengers { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCancelled { get; set; }
    }
}
