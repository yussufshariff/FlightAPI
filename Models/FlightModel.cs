namespace FlightAPI.Models
{
    public class FlightModel
    {
        public string FlightId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Airline { get; set; }
        public string Aircraft { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
