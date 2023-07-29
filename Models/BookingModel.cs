namespace FlightAPI.Models
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public string FlightId { get; set; }
        public string PassengerName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumPassengers { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCancelled { get; set; }
    }
}
